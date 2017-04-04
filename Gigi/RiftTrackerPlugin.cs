using System.Linq;
using SharpDX.DirectInput;
using System.Collections.Generic;
using Turbo.Plugins.Default;
using Turbo.Plugins.Jack.Decorators.TopTables;

namespace Turbo.Plugins.Gigi
{
 
    public class RiftTrackerPlugin : BasePlugin, IInGameTopPainter, IMonsterKilledHandler, INewAreaHandler, IAfterCollectHandler, IKeyEventHandler
    {
        public TopTableCellDecorator DefaultCellDecorator { get; set; }
        public TopTableCellDecorator HighlightCellDecorator { get; set; }
        public uint SortByColumn { get; set; }
        public bool SortDescending { get; set; }
        public float Table2TableXDistance { get; set; }
        public float Table2TableYDistance { get; set; }
        public float XPosRatio { get; set; }
        public float YPosRatio { get; set; }
        public string HeaderSingleProgression { get; set; }
        public string HeaderTrackedProgression { get; set; }
        public string HeaderKilledProgression { get; set; }
        public string HeaderTrackedCount { get; set; }
        public string HeaderKilledCount { get; set; }
        public bool ShowSingleProgression { get; set; }
        public bool ShowTrackedProgression { get; set; }
        public bool ShowKilledProgression { get; set; }
        public bool ShowTrackedCount { get; set; }
        public bool ShowKilledCount { get; set; }
        public bool IncludeProgressionOrbs { get; set; }
        public float CellRatioHeight { get; set; }
        public IKeyEvent tKey { get; set; }        
        private Dictionary<string, HashSet<uint>> MonsterTracked = new Dictionary<string, HashSet<uint>>();
		private Dictionary<string, Dictionary<string, float>> MonsterProgression = new Dictionary<string, Dictionary<string, float>>();
		private Dictionary<string, Dictionary<string, int>> MonsterSeenCount = new Dictionary<string, Dictionary<string, int>>();
        private Dictionary<string, Dictionary<string, int>> MonsterSummonedCount = new Dictionary<string, Dictionary<string, int>>();
		private Dictionary<string, Dictionary<string, int>> MonsterKilledCount = new Dictionary<string, Dictionary<string, int>>();
        private List<TopTable> Tables = new List<TopTable>();
        private string currentFloor = "";
        private bool Show = false;
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
            //Table Definition
            HeaderSingleProgression = "%S";
            HeaderTrackedProgression = "%T";
            HeaderTrackedCount = "#T";
            HeaderKilledProgression = "%K";
            HeaderKilledCount = "#K";
            ShowSingleProgression = true;
            ShowTrackedProgression = true;
            ShowTrackedCount = true;
            ShowKilledProgression = true;
            ShowKilledCount = true;
            IncludeProgressionOrbs = true;
            SortByColumn = 4;
            SortDescending = true;
            //Spacing
            Table2TableXDistance = 0.18f;
            Table2TableYDistance = 0.05f;
            XPosRatio = 0.1f;
            YPosRatio = 0.075f;
            CellRatioHeight  = 22 / (float)Hud.Window.Size.Height;
            //Display Definition
            tKey = Hud.Input.CreateKeyEvent(true, Key.F7, false, false, false);
            //Colorization
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
            if (isNewGame){
                ClearData();
                Show = false;
                tablesProcessed = false;
            }
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
                MonsterTracked.Clear();
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
                MonsterProgression[floor].Add(m.SnoMonster.NameLocalized, m.SnoMonster.RiftProgression);
            }
		}

        private void ProcessGlobe(IActor globe, string floor){ 
            //handle a globe as if it would be a monster (same datastructure)
            //make sure data accessing is safe (we probably don't need this - better be safe than sorry)
            if (!MonsterProgression.ContainsKey(floor) || 
                !MonsterSeenCount.ContainsKey(floor) ||
                !MonsterSummonedCount.ContainsKey(floor) ||
                !MonsterTracked.ContainsKey(floor))
                return;
            
			//do we already know that globe?
            if (MonsterTracked[floor].Contains(globe.AnnId))
                return;
            MonsterTracked[floor].Add(globe.AnnId);            
            
            //track it as seen
            if (MonsterSeenCount[floor].ContainsKey(globe.SnoActor.Kind.ToString("G")))
                MonsterSeenCount[floor][globe.SnoActor.Kind.ToString("G")] += 1;
            else
                MonsterSeenCount[floor].Add(globe.SnoActor.Kind.ToString("G"), 1);

            //track as killed also (there is no "OnPickUp" event for globes)
            if (MonsterKilledCount[currentFloor].ContainsKey(globe.SnoActor.Kind.ToString("G")))
                MonsterKilledCount[currentFloor][globe.SnoActor.Kind.ToString("G")] += 1;
            else
                MonsterKilledCount[currentFloor].Add(globe.SnoActor.Kind.ToString("G"), 1);

            //add progression entry for monster
            if (!MonsterProgression[floor].ContainsKey(globe.SnoActor.Kind.ToString("G"))){
                MonsterProgression[floor].Add(globe.SnoActor.Kind.ToString("G"), 7.5f);
            }

        }

        private IEnumerable<TopTableHeader> getHeader(string f){
            yield return new TopTableHeader(Hud, (pos, curPos) => "Type @ " + f)        //Monstertype
                {
                    RatioHeight = CellRatioHeight, // define only once on first column, value on others will be ignored
                    RatioWidth = 108 / (float)Hud.Window.Size.Height,
                    HighlightFunc = (pos, curPos) => false,
                    TextAlign = align,
                };
            if (ShowSingleProgression){
                yield return new TopTableHeader(Hud, (pos, curPos) => HeaderSingleProgression)    //Percent Progression of Monstertype
                    {
                        RatioWidth = 0.04f,
                        HighlightFunc = (pos, curPos) => false,
                        TextAlign = align,
                    };
            }
            if (ShowTrackedProgression){
                yield return new TopTableHeader(Hud, (pos, curPos) => HeaderTrackedProgression)   //Percent Progression of all Monstertype instances
                    {
                        RatioWidth = 0.05f,
                        HighlightFunc = (pos, curPos) => false,
                        TextAlign = align,
                    };
            }
            if (ShowTrackedCount){
                yield return new TopTableHeader(Hud, (pos, curPos) => HeaderTrackedCount)   //Count of all Monstertype instances
                    {
                        RatioWidth = 0.035f,
                        HighlightFunc = (pos, curPos) => false,
                        TextAlign = align,
                    };
            }
            if (ShowKilledProgression){
                yield return new TopTableHeader(Hud, (pos, curPos) => HeaderKilledProgression)    //Percent Progression off killed Monstertype instances
                    {
                        RatioWidth = 0.05f,
                        HighlightFunc = (pos, curPos) => false,
                        TextAlign = align,
                    };
            }
            if (ShowKilledCount){
                yield return new TopTableHeader(Hud, (pos, curPos) => HeaderKilledCount)    //Count of killed Monstertype instances
                    {
                        RatioWidth = 0.035f,
                        HighlightFunc = (pos, curPos) => false,
                        TextAlign = align,
                    };
            }
        }

        private IEnumerable<TopTableCell> getLine(string floor, string mtype){
            yield return new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => mtype);
            if (ShowSingleProgression){
                yield return new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getSingleProgress(floor, mtype));
            }
            if (ShowTrackedProgression){
                yield return new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getTrackedProgress(floor, mtype));
            }
            if (ShowTrackedCount){
                yield return new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getTrackedCount(floor, mtype));
            }
            if (ShowKilledProgression){
                yield return new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getKilledProgression(floor, mtype));
            }
            if (ShowKilledCount){
                yield return new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getKilledCount(floor, mtype));
            }
        }

        private TopTable getNewTable(float XRatio, float YRatio, string f){
            TopTable Table = new TopTable(Hud)
            {
                RatioPositionX = XRatio,
                RatioPositionY = YRatio,
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
            Table.DefineColumns(getHeader(f).ToArray());
            return Table;      
        }

        private void ProcessTables(){
            //scaling and positining
            var w = Hud.Window.Size.Width;
            var h = Hud.Window.Size.Height;
            var XRatio = XPosRatio;
            var YRatio = YPosRatio;
            int tablesPerLine = (int)(1.0/Table2TableXDistance);
            int count = 1;
            //iterate floors where monsters are tracked for
            int maxLineCount = 0;
            foreach(string f in MonsterTracked.Keys.ToList()){
                if (!MonsterProgression.ContainsKey(f) || 
                    !MonsterSeenCount.ContainsKey(f) ||
                    !MonsterSummonedCount.ContainsKey(f) ||
                    !MonsterKilledCount.ContainsKey(f))
                    continue;
                //create table for floor
                TopTable t = getNewTable(XRatio, YRatio, f);
                //fill table with each tracked monster on floor
                foreach(string m in MonsterProgression[f].Keys.ToList()){
                    string mtype = m.ToString();
                    string floor = f.ToString();
                    TopTableCell[] l = getLine(floor, mtype).ToArray();
                    t.AddLine(
                        new TopTableHeader(Hud, (pos, curPos) => string.Empty)
                        {
                            RatioWidth = 62 / (float)Hud.Window.Size.Height,
                            RatioHeight = CellRatioHeight,
                            HighlightFunc = (pos, curPos) => false, //highlight empty line for new area
                            TextAlign = align,
                        }, l
                    );
                }//foreach-end monster 
                //sort and add table
                t.SortLines((int)SortByColumn, SortDescending);
                Tables.Add(t);
                //counters and offsets
                maxLineCount = (t.Lines.Count > maxLineCount) ? t.Lines.Count : maxLineCount;
                XRatio += Table2TableXDistance;       
                if (count % tablesPerLine == 0){    //move tables to next line
                    XRatio = XPosRatio;
                    YRatio = YPosRatio + CellRatioHeight * maxLineCount + Table2TableYDistance;
                    maxLineCount = 0;
                }
                count++;        
            }//foreach-end floor 
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

        private void DrawTables(){
            if (!tablesProcessed)
                ProcessTables();
            foreach(TopTable t in Tables)
                t.Paint();
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
            
            //collect data
            var monsters = Hud.Game.AliveMonsters.Where(m => !m.IsElite);
            var floor = currentFloor;
          	foreach (IMonster monster in monsters)
				ProcessMonster(monster, floor);
            if (IncludeProgressionOrbs){
                foreach(IActor globe in Hud.Game.Actors.Where(a => a.SnoActor.Kind == ActorKind.RiftOrb))
                    ProcessGlobe(globe, floor);
            }

            tablesProcessed = false;
            Show = true;
        }

    }


 
}