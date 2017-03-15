using System;
using System.Linq;
using System.Collections.Generic;
using Turbo.Plugins.Default;
using Turbo.Plugins.Gigi.Engine;
using Turbo.Plugins.Jack.Decorators.TopTables;

namespace Turbo.Plugins.Gigi
{
    public class BreakpointPlugin : BasePlugin, IInGameTopPainter
    {
        private BreakpointFactory bpf { get; set; }
        public TopTable Table { get; set; }
        private List<Tuple<double, int, double>> cbp;
        private float _baseweapon = 1.61f; 

        public TopTableCellDecorator DefaultCellDecorator { get; set; }
        public TopTableCellDecorator HighlightCellDecorator { get; set; }

        public int ShowLower = 5;
        public int ShowHigher = 5;

        public BreakpointPlugin()
        {
            Enabled = true;
        }
 
        public override void Load(IController hud)
        {
            base.Load(hud);
            bpf = new BreakpointFactory(hud);
            cbp = bpf.CreateBreakpointTable(25, 1.0f);

            DefaultCellDecorator = new TopTableCellDecorator(Hud)
            {
                BackgroundBrush = Hud.Render.CreateBrush(120, 75, 75, 75, 0),
                BorderBrush = Hud.Render.CreateBrush(255, 175, 175, 175, -1),
                TextFont = Hud.Render.CreateFont("tahoma", 6, 255, 255, 255, 255, false, false, true),
            };
            HighlightCellDecorator = new TopTableCellDecorator(Hud)
            {
                BackgroundBrush = Hud.Render.CreateBrush(120, 0, 175, 0, 0),
                BorderBrush = Hud.Render.CreateBrush(255, 175, 175, 175, -1),
                TextFont = Hud.Render.CreateFont("tahoma", 6, 255, 255, 255, 255, false, false, true),
            };

            Table = new TopTable(Hud)
            {
                RatioPositionX = 0.5f,
                RatioPositionY = 0.01f,
                HorizontalCenter = true,
                VerticalCenter = false,
                PositionFromRight = false,
                PositionFromBottom = false,
                ShowHeaderLeft = true,
                ShowHeaderTop = true,
                ShowHeaderRight = false,
                ShowHeaderBottom = false,
                DefaultCellDecorator = DefaultCellDecorator,
                DefaultHeaderDecorator = new TopTableCellDecorator(Hud)
                {
                    //BackgroundBrush = Hud.Render.CreateBrush(0, 0, 0, 0, 0),
                    //BorderBrush = Hud.Render.CreateBrush(255, 255, 255, 255, 1),
                    TextFont = Hud.Render.CreateFont("tahoma", 7, 255, 255, 255, 255, false, false, true),
                }
            };

            Table.DefineColumns(
                new TopTableHeader(Hud)
                {
                    RatioHeight = 22 / 1080f, // define only once on first column, value on others will be ignored
                    RatioWidth = 75 / 1080f,
                    TextFunc = () => "FPA",
                },
                new TopTableHeader(Hud)
                {
                    RatioWidth = 75 / 1080f,
                    TextFunc = () => "Min APS",
                },
                new TopTableHeader(Hud)
                {
                    RatioWidth = 75 / 1080f,
                    TextFunc = () => "Max APS",
                },
                new TopTableHeader(Hud)
                {
                    RatioWidth = 75 / 1080f,
                    TextFunc = () => "\u0394 IAS",
                },
                new TopTableHeader(Hud)
                {
                    RatioWidth = 75 / 1080f,
                    TextFunc = () => "\u0394 FPA-DPS",
                }
            );                   

            makeTable();
        }	
		
        private Tuple<double, int, double> getBreakpointTuple(int idx){
            if (cbp == null || idx < 0 || idx >= cbp.Count())
                return new Tuple<double, int, double>(0,0,0);     
            return cbp[idx];   
        }

        private int getCurrentBreakpointIndex(){
            double aps = getRoundedAPS();
            int i;
            for(i = 0; i < cbp.Count(); ){
                if (aps >= cbp[i].Item1 && aps <= cbp[i].Item3)
                    return i;
                i += 1;
            }
            return i;
        }

        private double getRoundedAPS(){
            double val = Hud.Game.Me.Offense.AttackSpeed;
            val = Math.Round(val * 10000)/10000;
            return val;
        }
        private void makeTable(){
            for(int i = 0; i < cbp.Count(); i++)
                createLine(i);
        }

        private void createLine(int i){       
            string desc = "0.0000";
            Table.AddLine(                    
                new TopTableHeader(Hud){
                    RatioHeight = 20 / 1080f,
                    TextFunc = () => ((i==getCurrentBreakpointIndex())?Hud.Game.Me.Offense.AttackSpeed.ToString("0.0000"):string.Empty),
                    CellDecorator = ((i==getCurrentBreakpointIndex())?HighlightCellDecorator:DefaultCellDecorator),
                },
                new TopTableCell(Hud)
                {
                    TextFunc = () => getBreakpointTuple(i).Item2.ToString(),
                },
                new TopTableCell(Hud)
                {
                    TextFunc = () => getBreakpointTuple(i).Item1.ToString(desc),
                },
                new TopTableCell(Hud)
                {
                    TextFunc = () => getBreakpointTuple(i).Item3.ToString(desc),
                },
                new TopTableCell(Hud)
                {
                    TextFunc = () => (100*(getBreakpointTuple(i).Item1-Hud.Game.Me.Offense.AttackSpeed)/_baseweapon).ToString("0.00")+" %",
                },
                new TopTableCell(Hud)
                {
                    TextFunc = () => ((100.0f*getBreakpointTuple(getCurrentBreakpointIndex()).Item2)/(getBreakpointTuple(i).Item2)-100).ToString("0.00")+" %",
                }
            );    
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.BeforeClip) return;
            Table.Paint();

            /*
            var size = 55f / 1200.0f * Hud.Window.Size.Height * Ratio;

            var portraitRect = Hud.Game.Me.PortraitUiElement.Rectangle;
            foreach (var skill in Hud.Game.Me.Powers.UsedSkills)
            {
                var index = skill.Key <= ActionKey.RightSkill ? (int)skill.Key + 4 : (int)skill.Key - 2;
                var x = portraitRect.Right + size * index;
                var y = portraitRect.Top + portraitRect.Height * 0.21f;
                var rect = new RectangleF(x, y, size, size);
                if (Hud.Window.CursorInsideRect(rect.Left, rect.Top, rect.Width, rect.Height))
                    DrawBreakpointsTable(skill);            
            }
            */
		}
    }
 
}