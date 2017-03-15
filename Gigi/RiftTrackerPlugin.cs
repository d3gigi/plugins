
using System.Linq;
using System.Collections.Generic;
using Turbo.Plugins.Default;
using Turbo.Plugins.Jack.Decorators.TopTables;
using SharpDX.DirectInput;

namespace Turbo.Plugins.Gigi
{
 
    public class RiftTrackerPlugin : BasePlugin, IInGameTopPainter, IMonsterKilledHandler
    {
		public IFont TextFont { get; set; }
        private List<uint> MonsterTracked = new List<uint>();
		private Dictionary<string, float> MonsterProgression = new Dictionary<string, float>();
		private Dictionary<string, int> MonsterSeenCount = new Dictionary<string, int>();
		private Dictionary<string, int> MonsterKilledCount = new Dictionary<string ,int>();
        public TopTable Table { get; set; }

        public RiftTrackerPlugin()
        {
            Enabled = false;
        }
 
        public override void Load(IController hud)
        {
            base.Load(hud);
			
            Table = new TopTable(Hud)
            {
                RatioPositionX = 0.85f,
                RatioPositionY = 0.6f,
                HorizontalCenter = true,
                VerticalCenter = false,
                PositionFromRight = false,
                PositionFromBottom = false,
                ShowHeaderLeft = true,
                ShowHeaderTop = true,
                ShowHeaderRight = false,
                ShowHeaderBottom = false,
                DefaultCellDecorator = new TopTableCellDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(120, 75, 75, 75, 0),
                    BorderBrush = Hud.Render.CreateBrush(255, 175, 175, 175, -1),
                    TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 255, 255, 255, false, false, true),
                },
                DefaultHeaderDecorator = new TopTableCellDecorator(Hud)
                {
                    //BackgroundBrush = Hud.Render.CreateBrush(0, 0, 0, 0, 0),
                    //BorderBrush = Hud.Render.CreateBrush(255, 255, 255, 255, 1),
                    TextFont = Hud.Render.CreateFont("tahoma", 8, 255, 255, 255, 255, false, false, true),
                }
            };

            Table.DefineColumns(
                new TopTableHeader(Hud)
                {
                    RatioHeight = 22 / 1080f, // define only once on first column, value on others will be ignored
                    RatioWidth = 180 / 1080f,
                    TextFunc = () => "Monstertype",
                },
                new TopTableHeader(Hud)
                {
                    RatioWidth = 0.1f,
                    TextFunc = () => "Prog Seen",
                },
                new TopTableHeader(Hud)
                {
                    RatioWidth = 0.1f,
                    TextFunc = () => "Prog Killed",
                }
            );

			TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 255, 255, 255, false, false, true);
        }		

		public void OnMonsterKilled(IMonster monster){
			if (monster.IsElite || monster.Rarity == ActorRarity.Boss){
				//track killing data on elites/bosses here
				return;
			}
			//track data on trash here
			if (!MonsterKilledCount.ContainsKey(monster.SnoMonster.NameLocalized))
				MonsterKilledCount.Add(monster.SnoMonster.NameLocalized, 1);
			else
				MonsterKilledCount[monster.SnoMonster.NameLocalized] += 1;
		}

		private void ClearData(){
			MonsterProgression.Clear();
			MonsterSeenCount.Clear();
			MonsterKilledCount.Clear();
            MonsterTracked.Clear();
		}

		private void ProcessMonster(IMonster monster){
			//do we already know that monster?
            if (MonsterTracked.Contains(monster.AnnId))
                return;
            MonsterTracked.Add(monster.AnnId);

            //track elite data here
			if (monster.IsElite || monster.Rarity == ActorRarity.Boss){
				return;
			}

			//track trash monster
			if (!MonsterProgression.ContainsKey(monster.SnoMonster.NameLocalized)){
				MonsterProgression.Add(monster.SnoMonster.NameLocalized, monster.SnoMonster.RiftProgression);
                Table.AddLine(
                    new TopTableHeader(Hud)
                    {
                        RatioWidth = 108 / 1080f, // define only once on first line, value on other will be ignored
                        RatioHeight = 22 / 1080f,
                        TextFunc = () => string.Empty,
                    },
                    new TopTableCell(Hud)
                    {
                        TextFunc = () => monster.SnoMonster.NameLocalized,
                    },
                    new TopTableCell(Hud)
                    {
                        TextFunc = () => getProgressionSeenForMonster(monster.SnoMonster.NameLocalized).ToString("0.00"),
                    },
                    new TopTableCell(Hud)
                    {
                        TextFunc = () => getProgressionKilledForMonster(monster.SnoMonster.NameLocalized).ToString("0.00"),
                    }
                );
            }
			if (!MonsterSeenCount.ContainsKey(monster.SnoMonster.NameLocalized))
				MonsterSeenCount.Add(monster.SnoMonster.NameLocalized, 1);
			else
				MonsterSeenCount[monster.SnoMonster.NameLocalized] += 1;
		}

        private double getProgressionSeenForMonster(string n){
            if (MonsterProgression.ContainsKey(n) && MonsterSeenCount.ContainsKey(n))
                return MonsterProgression[n] * MonsterSeenCount[n] * 100.0d / this.Hud.Game.MaxQuestProgress;
            return 0f;
        }

        private double getProgressionKilledForMonster(string n){
            if (MonsterProgression.ContainsKey(n) && MonsterKilledCount.ContainsKey(n))
                return MonsterProgression[n] * MonsterKilledCount[n] * 100.0d / this.Hud.Game.MaxQuestProgress;
            return 0f;
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (Hud.Game.Me.IsInTown) {
                ClearData();
                Table.Lines.Clear();
                return;
            }
			//iterate trash monster data
            var monsters = Hud.Game.AliveMonsters.Where(m => !m.IsElite);
          	foreach (var monster in monsters)
				ProcessMonster(monster);

            if (clipState != ClipState.BeforeClip) return;
            if (Table.Lines.Count() > 0 && Table.Columns.Count() > 0){
                Table.Columns.Sort((x,y) => x.TextFunc().CompareTo(y.TextFunc()));
                Table.Paint();
            }
        }
 
    }


 
}