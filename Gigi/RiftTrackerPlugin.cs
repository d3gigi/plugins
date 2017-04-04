using System.Linq;
using SharpDX.DirectInput;
using System.Collections.Generic;
using Turbo.Plugins.Default;
using Turbo.Plugins.Jack.Decorators.TopTables;

namespace Turbo.Plugins.Gigi
{
 
    public class RiftTrackerPlugin : BasePlugin, IInGameTopPainter, IMonsterKilledHandler, INewAreaHandler, IAfterCollectHandler, IKeyEventHandler
    {
        private Dictionary<string, HashSet<uint>> MonsterTracked = new Dictionary<string, HashSet<uint>>();
		private Dictionary<string, Dictionary<string, float>> MonsterProgression = new Dictionary<string, Dictionary<string, float>>();
		private Dictionary<string, Dictionary<string, int>> MonsterSeenCount = new Dictionary<string, Dictionary<string, int>>();
        private Dictionary<string, Dictionary<string, int>> MonsterSummonedCount = new Dictionary<string, Dictionary<string, int>>();
		private Dictionary<string, Dictionary<string, int>> MonsterKilledCount = new Dictionary<string, Dictionary<string, int>>();
        public TopTableCellDecorator DefaultCellDecorator { get; set; }
        public TopTableCellDecorator HighlightCellDecorator { get; set; }
        public IKeyEvent tKey;
        private List<TopTable> Tables = new List<TopTable>();
        private string currentFloor = "";
        private bool Show = true;
        private bool tablesProcessed = false;
        private HorizontalAlign align = Default.HorizontalAlign.Center;
        //shameless copy from https://github.com/JackCeparou/JackCeparouCompass/blob/master/RiftInfoPlugin.cs
        private IQuest riftQuest
        {
            get
            {
                return Hud.Game.Quests.FirstOrDefault(q => q.SnoQuest.Sno == 337492) ?? // rift
                       Hud.Game.Quests.FirstOrDefault(q => q.SnoQuest.Sno == 382695);   // gr
            }
        }
        public bool IsNephalemRift
        {
            get
            {
                return riftQuest != null && (riftQuest.QuestStepId == 1 || riftQuest.QuestStepId == 3 || riftQuest.QuestStepId == 10);
            }
        }
        public bool IsGreaterRift
        {
            get
            {
                return riftQuest != null &&
                       (riftQuest.QuestStepId == 13 || riftQuest.QuestStepId == 16 || riftQuest.QuestStepId == 34 ||
                        riftQuest.QuestStepId == 46);
            }
        }        
        private IUiElement uiProgressBar
        {
            get
            {
                return IsNephalemRift ? Hud.Render.NephalemRiftBarUiElement : Hud.Render.GreaterRiftBarUiElement;
            }
        }
        //shameless copy end
        public RiftTrackerPlugin()
        {
            Enabled = true;
        }
 
        public override void Load(IController hud)
        {
            base.Load(hud);
            tKey = Hud.Input.CreateKeyEvent(true, Key.F7, false, false, false);
            DefaultCellDecorator = new TopTableCellDecorator(Hud)
            {
                BackgroundBrush = Hud.Render.CreateBrush(185, 75, 75, 75, 0),
                BorderBrush = Hud.Render.CreateBrush(175, 175, 175, 175, -1),
                TextFont = Hud.Render.CreateFont("tahoma", 6, 255, 255, 255, 255, false, false, true),
            };
            HighlightCellDecorator = new TopTableCellDecorator(Hud)
            {
                BackgroundBrush = Hud.Render.CreateBrush(185, 0, 175, 0, 0),
                BorderBrush = Hud.Render.CreateBrush(175, 175, 175, 175, -1),
                TextFont = Hud.Render.CreateFont("tahoma", 6, 255, 255, 255, 255, false, false, true),
            };
        }		

        public void OnNewArea(bool isNewGame, ISnoArea area){
            if (!Hud.Game.IsInTown){
                currentFloor = area.NameLocalized;
                if (!MonsterProgression.ContainsKey(currentFloor) && 
                    !MonsterSeenCount.ContainsKey(currentFloor) &&
                    !MonsterSummonedCount.ContainsKey(currentFloor) &&
                    !MonsterTracked.ContainsKey(currentFloor) &&
                    !MonsterKilledCount.ContainsKey(currentFloor))
                {
                    //add floor in data collection structures
                    MonsterTracked.Add(currentFloor, new HashSet<uint>());
                    MonsterProgression.Add(currentFloor, new Dictionary<string, float>());
                    MonsterSeenCount.Add(currentFloor, new Dictionary<string, int>());
                    MonsterKilledCount.Add(currentFloor, new Dictionary<string, int>());
                    MonsterSummonedCount.Add(currentFloor, new Dictionary<string, int>());
                }
            }
        }

		public void OnMonsterKilled(IMonster monster){
			//track killing data on elites/bosses here
			if (monster.IsElite || monster.Rarity == ActorRarity.Boss)
				return;
			//data structure for kiilled-tracking existsd, monster has been seen
            if (!MonsterKilledCount.ContainsKey(currentFloor) || !MonsterTracked.ContainsKey(currentFloor) || !MonsterTracked[currentFloor].Contains(monster.AnnId))
                return;
            //track kill
            if (MonsterKilledCount[currentFloor].ContainsKey(monster.SnoMonster.NameLocalized))
                MonsterKilledCount[currentFloor][monster.SnoMonster.NameLocalized] += 1;
            else
                MonsterKilledCount[currentFloor].Add(monster.SnoMonster.NameLocalized, 1);
		}

        public void OnKeyEvent(IKeyEvent keyEvent)
        {
            if (Show && keyEvent.IsPressed && tKey.Matches(keyEvent)){
                Show = false;
                tablesProcessed = false;
                ClearData();
            }
        }

		private void ClearData(){
            if (MonsterProgression != null){
                foreach(var m in MonsterProgression)
                    if (m.Value != null)
                        m.Value.Clear();
                MonsterProgression.Clear();
            }
            if (MonsterSeenCount != null){
                foreach(var m in MonsterSeenCount)
                    if (m.Value != null)
                        m.Value.Clear();
                MonsterSeenCount.Clear();
            }
            if (MonsterSummonedCount != null){
                foreach(var m in MonsterSummonedCount)
                    if (m.Value != null)
                        m.Value.Clear();
                MonsterSummonedCount.Clear();
            }
            if (MonsterKilledCount != null){
                foreach(var m in MonsterKilledCount)
                    if (m.Value != null)
                        m.Value.Clear();
                MonsterKilledCount.Clear();
            }
            if (MonsterTracked != null){
                foreach(var m in MonsterTracked)
                    if (m.Value != null)
                        m.Value.Clear();
            }
            if (Tables != null){
                foreach(var t in Tables){
                    if (t != null){
                        t.Columns.Clear();
                        t.Lines.Clear();
                    }
                }
                Tables.Clear();
            }
		}

		private void ProcessMonster(IMonster m, string floor){
            //make sure data accessing is safe (we probably don't need this - better be safe than sorry)
            if (!MonsterProgression.ContainsKey(floor) || 
                !MonsterSeenCount.ContainsKey(floor) ||
                !MonsterSummonedCount.ContainsKey(floor) ||
                !MonsterTracked.ContainsKey(floor))
                return;

            //don't track elites
            if (m.IsElite || m.Rarity == ActorRarity.Boss){
				//track data on elites/bosses here
                return;
            }

			//do we already know that monster?
            if (MonsterTracked[floor].Contains(m.AnnId))
                return;
            MonsterTracked[floor].Add(m.AnnId);

            //is summoned?
            if (m.SummonerAcdDynamicId == 0){ //not summoned
                if (MonsterSeenCount[floor].ContainsKey(m.SnoMonster.NameLocalized))
                    MonsterSeenCount[floor][m.SnoMonster.NameLocalized] += 1;
                else
                    MonsterSeenCount[floor].Add(m.SnoMonster.NameLocalized, 1);
            }else{ //summoned
                if (MonsterSummonedCount[floor].ContainsKey(m.SnoMonster.NameLocalized))
                    MonsterSummonedCount[floor][m.SnoMonster.NameLocalized] += 1;
                else
                    MonsterSummonedCount[floor].Add(m.SnoMonster.NameLocalized, 1);
            }

            //add progression entry for monster
            if (!MonsterProgression[floor].ContainsKey(m.SnoMonster.NameLocalized)){
                MonsterProgression[floor].Add(m.SnoMonster.NameLocalized, 
                m.SnoMonster.RiftProgression);
            }
		}

        private TopTable getNewTable(float XPosRatio, float YPosRatio, string f){
            TopTable Table = new TopTable(Hud)
            {
                RatioPositionX = XPosRatio,
                RatioPositionY = YPosRatio,
                HorizontalCenter = true,
                VerticalCenter = false,
                PositionFromRight = false,
                PositionFromBottom = false,
                ShowHeaderLeft = false,
                ShowHeaderTop = true,
                ShowHeaderRight = false,
                ShowHeaderBottom = false,
                DefaultCellDecorator = DefaultCellDecorator,
                DefaultHighlightDecorator = HighlightCellDecorator,
                DefaultHeaderDecorator = new TopTableCellDecorator(Hud)
                {
                    TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 255, 255, 255, false, false, true),
                }
            };

            Table.DefineColumns(
                new TopTableHeader(Hud, (pos, curPos) => "Type on " + f)        //Monstertype
                {
                    RatioHeight = 22 / 1080f, // define only once on first column, value on others will be ignored
                    RatioWidth = 108 / 1080f,
                    HighlightFunc = (pos, curPos) => false,
                    TextAlign = align,
                },
                new TopTableHeader(Hud, (pos, curPos) => "%S")    //Percent Progression of Monstertype
                {
                    RatioWidth = 0.04f,
                    HighlightFunc = (pos, curPos) => false,
                    TextAlign = align,
                },
                new TopTableHeader(Hud, (pos, curPos) => "%T")   //Percent Progression of all Monstertype instances
                {
                    RatioWidth = 0.05f,
                    HighlightFunc = (pos, curPos) => false,
                    TextAlign = align,
                },
                new TopTableHeader(Hud, (pos, curPos) => "#T")   //Count of all Monstertype instances
                {
                    RatioWidth = 0.035f,
                    HighlightFunc = (pos, curPos) => false,
                    TextAlign = align,
                },
                new TopTableHeader(Hud, (pos, curPos) => "%K")    //Percent Progression off killed Monstertype instances
                {
                    RatioWidth = 0.05f,
                    HighlightFunc = (pos, curPos) => false,
                    TextAlign = align,
                },
                new TopTableHeader(Hud, (pos, curPos) => "#K")    //Count of killed Monstertype instances
                {
                    RatioWidth = 0.035f,
                    HighlightFunc = (pos, curPos) => false,
                    TextAlign = align,
                }
            );
            return Table;      
        }

        private void DrawTables(){
            if (!tablesProcessed)
                processTables();
            foreach(TopTable t in Tables)
                t.Paint();
        }

        private void processTables(){
            //scaling and positining
            var w = Hud.Window.Size.Width;
            var h = Hud.Window.Size.Height;
            var xoff = 0.05f;
            var yoff = 0.05f;
            var XPosRatio = 0.1f;
            var YPosRatio = 0.05f;
            var count = 0;
            //iterate floors where monsters are tracked for
            foreach(string f in MonsterTracked.Keys.ToList()){
                if (!MonsterProgression.ContainsKey(f) || 
                    !MonsterSeenCount.ContainsKey(f) ||
                    !MonsterSummonedCount.ContainsKey(f) ||
                    !MonsterKilledCount.ContainsKey(f))
                    continue;
                //create table for floor
                if (count == 5){
                    XPosRatio = 0.1f;
                    YPosRatio = 0.55f;
                }
                TopTable t = getNewTable(XPosRatio, YPosRatio, f);
                //fill table with each tracked monster on floor
                foreach(string m in MonsterProgression[f].Keys.ToList()){
                    string mtype = m.ToString();
                    string floor = f.ToString();
                    t.AddLine(
                        new TopTableHeader(Hud, (pos, curPos) => string.Empty)
                        {
                            RatioWidth = 62 / 1080f, // define only once on first line, value on other will be ignored
                            RatioHeight = 22 / 1080f,
                            HighlightFunc = (pos, curPos) => false, //highlight empty line for new area
                            TextAlign = align,
                        },
                        new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => mtype),
                        new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getSingleProgress(floor, mtype)),
                        new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getTrackedProgress(floor, mtype)),
                        new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getTrackedCount(floor, mtype)),
                        new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getKilledProgression(floor, mtype)),
                        new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getKilledCount(floor, mtype))
                    );
                }//foreach-end monster iteration per floor
                XPosRatio += 0.18f;
                Tables.Add(t);
                count++;               
            }
            tablesProcessed = true;
        }

        private string getSingleProgress(string f, string m){
            if (!MonsterProgression.ContainsKey(f) || !MonsterProgression[f].ContainsKey(m)) return "-";
            double val = MonsterProgression[f][m] * 100.0 / this.Hud.Game.MaxQuestProgress;
            return val.ToString("0.00");
        }

        private string getTrackedProgress(string f, string m){
            if (!MonsterProgression.ContainsKey(f) || 
                !MonsterProgression[f].ContainsKey(m) ||
                !MonsterSeenCount.ContainsKey(f) ||
                !MonsterSummonedCount.ContainsKey(f)) 
                return "-";
            double val = MonsterProgression[f][m] * 100.0 / this.Hud.Game.MaxQuestProgress;
            int summoned = MonsterSeenCount[f].ContainsKey(m) ? MonsterSeenCount[f][m] : 0 ;
            int seen = MonsterSummonedCount[f].ContainsKey(m) ? MonsterSummonedCount[f][m] : 0 ;
            return (val*(seen+summoned)).ToString("0.00");
        }

        private string getTrackedCount(string f, string m){
            if (!MonsterSeenCount.ContainsKey(f) ||
                !MonsterSummonedCount.ContainsKey(f)) 
                return "-";
            int summoned = MonsterSeenCount[f].ContainsKey(m) ? MonsterSeenCount[f][m] : 0 ;
            int seen = MonsterSummonedCount[f].ContainsKey(m) ? MonsterSummonedCount[f][m] : 0 ;
            return (seen+summoned).ToString();
        }

        private string getKilledProgression(string f, string m){
            if (!MonsterProgression.ContainsKey(f) ||
                !MonsterProgression[f].ContainsKey(m) ||
                !MonsterKilledCount.ContainsKey(f)) 
                return "-";
            double val = MonsterProgression[f][m] * 100.0 / this.Hud.Game.MaxQuestProgress;
            int killed = MonsterKilledCount[f].ContainsKey(m) ? MonsterKilledCount[f][m] : 0 ;
            return (val*killed).ToString("0.00");
        }

        private string getKilledCount(string f, string m){
            if (!MonsterKilledCount.ContainsKey(f)) return "-";
            int killed = MonsterKilledCount[f].ContainsKey(m) ? MonsterKilledCount[f][m] : 0 ;
            return killed.ToString();
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.BeforeClip) return;
            if (Show && Hud.Game.Me.IsInTown && (riftQuest.State == QuestState.completed || riftQuest.State == QuestState.none)){
                DrawTables();
                return;
            }
        }

        public void AfterCollect(){
            if (Hud.Game.SpecialArea != SpecialArea.Rift && Hud.Game.SpecialArea != SpecialArea.GreaterRift){
                return; //not in a rift
            }
            var monsters = Hud.Game.AliveMonsters.Where(m => !m.IsElite);
            var floor = currentFloor;
          	foreach (var monster in monsters)
				ProcessMonster(monster, floor);
            tablesProcessed = false;
            Show = true;
        }
 
    }


 
}