using System.Linq;
using System.Collections.Generic;
using Turbo.Plugins.Default;
using Turbo.Plugins.Jack.Decorators.TopTables;

namespace Turbo.Plugins.Gigi
{
 
    public class RiftTrackerPlugin : BasePlugin, IInGameTopPainter, IMonsterKilledHandler, INewAreaHandler
    {
        private Dictionary<string, HashSet<uint>> MonsterTracked = new Dictionary<string, HashSet<uint>>();
		private Dictionary<string, Dictionary<string, float>> MonsterProgression = new Dictionary<string, Dictionary<string, float>>();
		private Dictionary<string, Dictionary<string, int>> MonsterSeenCount = new Dictionary<string, Dictionary<string, int>>();
        private Dictionary<string, Dictionary<string, int>> MonsterSummonedCount = new Dictionary<string, Dictionary<string, int>>();
		private Dictionary<string, Dictionary<string, int>> MonsterKilledCount = new Dictionary<string, Dictionary<string, int>>();
        public TopTable Table { get; set; }
        private string currentFloor = "";
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
            Enabled = false;
        }
 
        public override void Load(IController hud)
        {
            base.Load(hud);
             // create the table with options
            Table = new TopTable(Hud)
            {
                RatioPositionX = 0.2f,
                RatioPositionY = 0.3f,
                HorizontalCenter = true,
                VerticalCenter = false,
                PositionFromRight = false,
                PositionFromBottom = false,
                ShowHeaderLeft = true,
                ShowHeaderTop = true,
                ShowHeaderRight = true,
                ShowHeaderBottom = true,
                DefaultCellDecorator = new TopTableCellDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(255, 0, 0, 0, 0),
                    BorderBrush = Hud.Render.CreateBrush(255, 255, 255, 255, -1),
                    TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 255, 255, false, false, true),
                },
                DefaultHighlightDecorator = new TopTableCellDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(255, 0, 0, 242, 0),
                    BorderBrush = Hud.Render.CreateBrush(255, 255, 255, 255, -1),
                    TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 255, 255, false, false, true),
                },
                DefaultHeaderDecorator = new TopTableCellDecorator(Hud)
                {
                    TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 255, 255, false, false, true),
                }
            };

            Table.DefineColumns(
                new TopTableHeader(Hud, (pos, curPos) => "Type")        //Monstertype
                {
                    RatioHeight = 22 / 1080f, // define only once on first column, value on others will be ignored
                    RatioWidth = 108 / 1080f,
                    HighlightFunc = (pos, curPos) => false,
                    TextAlign = align,
                },
                new TopTableHeader(Hud, (pos, curPos) => "% Single")    //Percent Progression of Monstertype
                {
                    RatioWidth = 0.1f,
                    HighlightFunc = (pos, curPos) => false,
                    TextAlign = align,
                },
                new TopTableHeader(Hud, (pos, curPos) => "% Tracked")   //Percent Progression of all Monstertype instances
                {
                    RatioWidth = 0.1f,
                    HighlightFunc = (pos, curPos) => false,
                    TextAlign = align,
                },
                new TopTableHeader(Hud, (pos, curPos) => "# Tracked")   //Count of all Monstertype instances
                {
                    RatioWidth = 0.1f,
                    HighlightFunc = (pos, curPos) => false,
                    TextAlign = align,
                },
                new TopTableHeader(Hud, (pos, curPos) => "% Killed")    //Percent Progression off killed Monstertype instances
                {
                    RatioWidth = 0.1f,
                    HighlightFunc = (pos, curPos) => false,
                    TextAlign = align,
                },
                new TopTableHeader(Hud, (pos, curPos) => "# Killed")    //Count of killed Monstertype instances
                {
                    RatioWidth = 0.1f,
                    HighlightFunc = (pos, curPos) => false,
                    TextAlign = align,
                }
            );            
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
                    //add floor in collection structures
                    MonsterTracked.Add(currentFloor, new HashSet<uint>());
                    MonsterProgression.Add(currentFloor, new Dictionary<string, float>());
                    MonsterSeenCount.Add(currentFloor, new Dictionary<string, int>());
                    MonsterKilledCount.Add(currentFloor, new Dictionary<string, int>());
                    MonsterSummonedCount.Add(currentFloor, new Dictionary<string, int>());
                    //Add an empty line in the table for a new area
                    var cfloor = currentFloor.ToString(); //not sure if this is a "true copy" or still same reference
                    Table.AddLine(
                        new TopTableHeader(Hud, (pos, curPos) => cfloor)
                        {
                            RatioWidth = 62 / 1080f, // define only once on first line, value on other will be ignored
                            RatioHeight = 22 / 1080f,
                            HighlightFunc = (pos, curPos) => true, //highlight empty line for new area
                            TextAlign = align,
                            HighlightDecorator = new TopTableCellDecorator(Hud)
                            {
                                BackgroundBrush = Hud.Render.CreateBrush(255, 120, 120, 120, 0),
                                BorderBrush = Hud.Render.CreateBrush(255, 255, 255, 255, -1),
                                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 255, 255, true, false, true),
                            },
                            CellHighlightDecorator = new TopTableCellDecorator(Hud)
                            {
                                BackgroundBrush = Hud.Render.CreateBrush(255, 120, 120, 120, 0),
                                BorderBrush = Hud.Render.CreateBrush(255, 255, 255, 255, -1),
                                TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 255, 255, true, false, true),
                            },
                        },
                        new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => string.Empty) { TextAlign = align },
                        new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => string.Empty) { TextAlign = align },
                        new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => string.Empty) { TextAlign = align },
                        new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => string.Empty) { TextAlign = align },
                        new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => string.Empty) { TextAlign = align },
                        new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => string.Empty) { TextAlign = align }
                    );         
                }
            }
        }

		public void OnMonsterKilled(IMonster monster){
			if (monster.IsElite || monster.Rarity == ActorRarity.Boss){
				//track killing data on elites/bosses here
				return;
			}
            if (!MonsterKilledCount.ContainsKey(currentFloor))
                return;
            if (MonsterKilledCount[currentFloor].ContainsKey(monster.SnoMonster.NameLocalized))
                MonsterKilledCount[currentFloor][monster.SnoMonster.NameLocalized] += 1;
            else
                MonsterKilledCount[currentFloor].Add(monster.SnoMonster.NameLocalized, 1);
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
		}

        private void ProcessCollectedData(){
        }

		private void ProcessMonster(IMonster m){
            //make sure data accessing is safe (we probably don't need this - better be safe than sorry)
            if (!MonsterProgression.ContainsKey(currentFloor) || 
                !MonsterSeenCount.ContainsKey(currentFloor) ||
                !MonsterSummonedCount.ContainsKey(currentFloor) ||
                !MonsterTracked.ContainsKey(currentFloor))
                return;

            //don't track elites
            if (m.IsElite || m.Rarity == ActorRarity.Boss){
				//track data on elites/bosses here
                return;
            }

			//do we already know that monster?
            if (MonsterTracked[currentFloor].Contains(m.AnnId))
                return;
            MonsterTracked[currentFloor].Add(m.AnnId);

            //is summoned?
            if (m.SummonerAcdDynamicId == 0){ //not summoned
                if (MonsterSeenCount[currentFloor].ContainsKey(m.SnoMonster.NameLocalized))
                    MonsterSeenCount[currentFloor][m.SnoMonster.NameLocalized] += 1;
                else
                    MonsterSeenCount[currentFloor].Add(m.SnoMonster.NameLocalized, 1);
            }else{ //summoned
                if (MonsterSummonedCount[currentFloor].ContainsKey(m.SnoMonster.NameLocalized))
                    MonsterSummonedCount[currentFloor][m.SnoMonster.NameLocalized] += 1;
                else
                    MonsterSummonedCount[currentFloor].Add(m.SnoMonster.NameLocalized, 1);
            }
            //add progression entry for monster
            if (!MonsterProgression[currentFloor].ContainsKey(m.SnoActor.NameLocalized)){
                MonsterProgression[currentFloor].Add(m.SnoMonster.NameLocalized, m.SnoMonster.RiftProgression);
                //hmmm ... how do I map into the collected data... guess I need to ProcessData first and then Table.Paint() on the Results?
                Table.AddLine(
                    new TopTableHeader(Hud, (pos, curPos) => string.Empty)
                    {
                        RatioWidth = 62 / 1080f, // define only once on first line, value on other will be ignored
                        RatioHeight = 22 / 1080f,
                        HighlightFunc = (pos, curPos) => false,
                        TextAlign = align,
                        HighlightDecorator = new TopTableCellDecorator(Hud)
                        {
                            BackgroundBrush = Hud.Render.CreateBrush(255, 200, 200, 200, 0),
                            BorderBrush = Hud.Render.CreateBrush(255, 255, 255, 255, -1),
                            TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 255, 255, true, false, true),
                        },
                        CellHighlightDecorator = new TopTableCellDecorator(Hud)
                        {
                            BackgroundBrush = Hud.Render.CreateBrush(255, 200, 200, 200, 0),
                            BorderBrush = Hud.Render.CreateBrush(255, 255, 255, 255, -1),
                            TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 255, 255, true, false, true),
                        },
                    },
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => string.Empty) { TextAlign = align },
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => string.Empty) { TextAlign = align },
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => string.Empty) { TextAlign = align },
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => string.Empty) { TextAlign = align },
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => string.Empty) { TextAlign = align },
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => string.Empty) { TextAlign = align }
                );  
            }
		}

        private string GetCellText(int line, int column){
            return "";
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.BeforeClip) return;
            if (Hud.Game.Me.IsInTown && (riftQuest.State == QuestState.completed || riftQuest.State == QuestState.none)){
                //process data?
                //Table.Paint()?
                return;
            }
            if (!IsNephalemRift && !IsGreaterRift) return;

			//iterate trash monster data
            var monsters = Hud.Game.AliveMonsters.Where(m => !m.IsElite);
          	foreach (var monster in monsters)
				ProcessMonster(monster);
        }
 
    }


 
}