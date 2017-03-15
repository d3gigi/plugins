using System;
using System.Collections.Generic;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.Gigi
{
 
    public class PartyBuffPlugin : BasePlugin, IInGameWorldPainter, ICustomizer
    {
        public BuffPainter BuffPainter { get; set; }
        public BuffRuleCalculator RuleCalculatorMe { get; private set;}
        public Dictionary<HeroClass, BuffRuleCalculator> RuleCalculators { get; private set; }
        public WorldDecoratorCollection DebugDecorator { get; set; }
        public bool Debug { get; set; }
        private float SizeMultiplier { get; set; }
        public float Opacity { get; set; }
        public float PositionOffset { get; set; }
        private BuffRuleFactory buffRuleFactory;
		
        public PartyBuffPlugin()
        {
            Enabled = true;
        }
		
        public void Customize()
        { 
            Hud.RunOnPlugin<PlayerBottomBuffListPlugin>(plugin => {
                plugin.RuleCalculator.Rules.Clear();
                plugin.Enabled = false;
            });
        }

        public override void Load(IController hud)
        {   
            base.Load(hud);
            buffRuleFactory = new BuffRuleFactory(hud);
            SizeMultiplier = 0.85f;
            PositionOffset = 0.085f;
            Debug = false;
            Opacity = 0.85f;
            RuleCalculatorMe = new BuffRuleCalculator(Hud);
            RuleCalculatorMe.SizeMultiplier = SizeMultiplier;
            RuleCalculators = new Dictionary<HeroClass, BuffRuleCalculator>();
            foreach(HeroClass h in Enum.GetValues(typeof(HeroClass))){
                RuleCalculators.Add(h, new BuffRuleCalculator(Hud));
                RuleCalculators[h].SizeMultiplier = SizeMultiplier;
            }

            BuffPainter = new BuffPainter(Hud, true)
            {
                Opacity = Opacity,
                ShowTimeLeftNumbers = true,
                ShowTooltips = false,
                TimeLeftFont = Hud.Render.CreateFont("tahoma", 7, 255, 255, 255, 255, false, false, 255, 0, 0, 0, true),
                StackFont = Hud.Render.CreateFont("tahoma", 6, 255, 255, 255, 255, false, false, 255, 0, 0, 0, true),
            };

            DebugDecorator = new WorldDecoratorCollection(
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(100, 20, 20, 20, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 255, 255, 255, false, false, false),
                }
            );
		}

        public void PaintWorld(WorldLayer layer)
        {
            var players = Hud.Game.Players;
            foreach(var p in players){
                if (p.IsMe){
                    RuleCalculatorMe.CalculatePaintInfo(p);
                    if (RuleCalculatorMe.PaintInfoList.Count != 0)
                        BuffPainter.PaintHorizontalCenter(
                            RuleCalculatorMe.PaintInfoList,
                            p.ScreenCoordinate.X, 
                            p.ScreenCoordinate.Y + Hud.Window.Size.Height * PositionOffset,
                            0,
                            RuleCalculatorMe.StandardIconSize,
                            RuleCalculatorMe.StandardIconSpacing
                        );
                }else{
                    if (p.IsOnScreen && p.CoordinateKnown){
                        RuleCalculators[p.HeroClassDefinition.HeroClass].CalculatePaintInfo(p);
                        if (RuleCalculators[p.HeroClassDefinition.HeroClass].PaintInfoList.Count != 0){
                            BuffPainter.PaintHorizontalCenter(
                                RuleCalculators[p.HeroClassDefinition.HeroClass].PaintInfoList, 
                                p.ScreenCoordinate.X, 
                                p.ScreenCoordinate.Y + Hud.Window.Size.Height * PositionOffset, 
                                0, 
                                RuleCalculators[p.HeroClassDefinition.HeroClass].StandardIconSize, 
                                RuleCalculators[p.HeroClassDefinition.HeroClass].StandardIconSpacing
                            );
                        }
                    }
                }

                if (Debug)
                    DebugPrint(layer, p);
            }
		}

        private void DebugPrint(WorldLayer layer, IPlayer p){
            string data = "";
            foreach(IBuff b in p.Powers.AllBuffs)
                if (BuffRuleExsitsForPlayer(p, b))
                    data += DataOnBuff(b) + "\n";
            DebugDecorator.Paint(layer, p, p.FloorCoordinate, data);
        }

        private bool BuffRuleExsitsForPlayer(IPlayer p, IBuff b){
            BuffRuleCalculator r = (p.IsMe) ? RuleCalculatorMe : RuleCalculators[p.HeroClassDefinition.HeroClass];
            foreach(BuffRule t in r.Rules)
                if (t.PowerSno == b.SnoPower.Sno)
                    return true;
            return false;
        }

        private string DataOnBuff(IBuff b){
            string res = "";
            res += b.SnoPower.Sno.ToString() + " \t";
            res += "["+string.Join(",", b.IconCounts)+"]" + "\t";
            res += b.Active.ToString() + " \t";
            res += b.SnoPower.NameLocalized + " \t";
            res += b.SnoPower.Code + " \t";
            //res += t.SnoPower.DescriptionEnglish;   
            return res;
        }

        public void DisplayOnAll(params ISnoPower[] pwrs){
            foreach(HeroClass h in Enum.GetValues(typeof(HeroClass)))
                AddPower(RuleCalculators[h], pwrs);
            AddPower(RuleCalculatorMe, pwrs);       
        }

        public void DisplayOnMe(params ISnoPower[] pwrs){
            AddPower(RuleCalculatorMe, pwrs);
        }

        public void DisplayOnAllClassesExceptMe(params ISnoPower[] pwrs){
            foreach(HeroClass h in Enum.GetValues(typeof(HeroClass)))
                AddPower(RuleCalculators[h], pwrs);       
        }

        public void DisplayOnClassExceptMe(HeroClass h, params ISnoPower[] pwrs){
            AddPower(RuleCalculators[h], pwrs);
        }

        private void AddPower(BuffRuleCalculator bf, params ISnoPower[] pwrs){
            if (pwrs == null) return;
            foreach(ISnoPower p in pwrs)
                AddPower(bf, p.Sno);
        }

        private void AddPower(BuffRuleCalculator bf, uint pwr){
            var buffRules = buffRuleFactory.CreateBuffRules(pwr);       //ty jack!
            if (buffRules != null){
                bf.Rules.AddRange(buffRules);
            } 
        }
    }
}