using System.Linq;
using System;
using System.Collections.Generic;
using Turbo.Plugins.Jack.Decorators.TopTables;
using Turbo.Plugins.Default;
 
namespace Turbo.Plugins.Gigi
{
    public class StrickenTestPlugin : BasePlugin, IInGameWorldPainter, IAfterCollectHandler
    {   
        private int _index = 2;
        private double _timeleft = 0;
        private int _stacks = 0;
        private AcdAnimationState lastState = AcdAnimationState.Invalid;
        private IWatch icdTimer;
        private Dictionary<AcdAnimationState, IWatch> acdTimer = new Dictionary<AcdAnimationState, IWatch>();
        private Dictionary<AcdAnimationState, int> acdCounter = new Dictionary<AcdAnimationState, int>();
        private TopTable Table;
        public TopTableCellDecorator DefaultCellDecorator { get; set; }
        public TopTableCellDecorator HighlightCellDecorator { get; set; }
        public WorldDecoratorCollection PlayerDecorator { get; set; }
        
        public StrickenTestPlugin()
        {
            Enabled = true;
            
        }

 
        public override void Load(IController hud)
        {
            base.Load(hud);
            icdTimer = Hud.CreateWatch();
            foreach(AcdAnimationState acd in Enum.GetValues(typeof(AcdAnimationState))){
                acdTimer[acd] = Hud.CreateWatch();
                acdCounter[acd] = 0;
            }
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
            // create the table with options
            Table = new TopTable(Hud)
            {
                RatioPositionX = 0.5f,
                RatioPositionY = 0.1f,
                HorizontalCenter = true,
                VerticalCenter = false,
                PositionFromRight = false,
                PositionFromBottom = false,
                ShowHeaderLeft = true,
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
                new TopTableHeader(Hud, (pos, curPos) => "# instance")
                {
                    RatioHeight = 22 / 1080f, // define only once on first column, value on others will be ignored
                    RatioWidth = 75 / 1080f,
                },
                new TopTableHeader(Hud, (pos, curPos) => "# frames")
                {
                    RatioWidth = 125 / 1080f,
                },
                new TopTableHeader(Hud, (pos, curPos) => "# seconds")
                {
                    RatioWidth = 125 / 1080f,
                },
                new TopTableHeader(Hud, (pos, curPos) => "# frames / # instance")
                {
                    RatioWidth = 125 / 1080f,
                }
            );

            Table.AddLine(
                new TopTableHeader(Hud, (pos, curPos) => "Stricken ICD")
                {
                    RatioWidth = 100 / 1080f, // define only once on first line, value on other will be ignored
                    RatioHeight = 22 / 1080f,
                    HighlightFunc = (pos, curPos) => false,
                    HighlightDecorator = HighlightCellDecorator,
                    TextAlign = Default.HorizontalAlign.Left,
                },
                new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => _stacks.ToString()),
                new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getTotalICD(icdTimer).ToString()),
                new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => (getTotalICD(icdTimer)/60.0).ToString("0.00")),
                new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getAvgICD(icdTimer, _stacks).ToString("0.00"))
            );

            foreach(AcdAnimationState acd in Enum.GetValues(typeof(AcdAnimationState))){
                Table.AddLine(
                    new TopTableHeader(Hud, (pos, curPos) => acd.ToString("G"))
                    {
                        RatioWidth = 100 / 1080f, // define only once on first line, value on other will be ignored
                        RatioHeight = 22 / 1080f,
                        HighlightFunc = (pos, curPos) => false,
                        HighlightDecorator = HighlightCellDecorator,
                        TextAlign = Default.HorizontalAlign.Left,
                    },
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => (acdCounter[acd]==0)?"":acdCounter[acd].ToString()),
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getTotalICD(acdTimer[acd]).ToString()),
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => (getTotalICD(acdTimer[acd])/60.0).ToString("0.00")),
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => getAvgICD(acdTimer[acd] ,acdCounter[acd]).ToString("0.00"))
                );
            }
        }

        private bool HasBuffPower(IPlayer p, ISnoPower s){
            return p.Powers.AllBuffs.Any(x => x.SnoPower.Sno == s.Sno);
        }

        private void measureStrickenICD(){
            IBuff s = Hud.Game.Me.Powers.AllBuffs.FirstOrDefault(x => x.SnoPower.Sno == Hud.Sno.SnoPowers.BaneOfTheStrickenPrimary.Sno);
            double c = getRemainingTime(s);
            if (IsOnCooldown(s)){
                if (!icdTimer.IsRunning)
                    icdTimer.Start();
                if (c > _timeleft){
                    //stricken got on new cooldown -> stack
                    _stacks++;
                }
                _timeleft = c;
            }else{
                icdTimer.Stop();
            }
        }

        private void measureCharacterState(){
            var acd  = Hud.Game.Me.AnimationState;
            if (!acdTimer[acd].IsRunning){
                acdTimer[acd].Start();
                if (acd != lastState){
                    acdTimer[lastState].Stop();
                    lastState = acd;
                    switch(acd){
                        case AcdAnimationState.Attacking:
                        case AcdAnimationState.Casting:
                        case AcdAnimationState.Channeling:
                            acdCounter[acd] += 1;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private int getTotalICD(IWatch t){
            return (int)((t.ElapsedMilliseconds/1000.0)*60);
        }

        private double getAvgICD(IWatch t, int c){
            if (c == 0)
                return 0;
            return getTotalICD(t)/(double)c;
        }

        private double getRemainingTime(IBuff s){
            return s.TimeLeftSeconds[_index];
        }

        private bool IsOnCooldown(IBuff s){
            return s.IconCounts[_index] != 0;
        }

        public void PaintWorld(WorldLayer layer)
        {  
            Table.Paint();
        }

        public void AfterCollect(){
            if (!HasBuffPower(Hud.Game.Me, Hud.Sno.SnoPowers.BaneOfTheStrickenPrimary)) return;
            measureStrickenICD();
            measureCharacterState();
        }
    }
}