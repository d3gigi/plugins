using System;
using SharpDX;
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
        public uint ShowLower = 5;
        public uint ShowHigher = 5;
        public string AttackSpeedDescriptor = "0.0000";
        public string PercentDescriptor = "0.00";

        public BreakpointPlugin()
        {
            Enabled = true;
        }
 
        public override void Load(IController hud)
        {
            base.Load(hud);
            bpf = new BreakpointFactory(hud);
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
                RatioPositionY = 0.35f,
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
                new TopTableHeader(Hud, (pos, curPos) => "FPA")
                {
                    RatioHeight = 22 / 1080f, // define only once on first column, value on others will be ignored
                    RatioWidth = 75 / 1080f,
                },
                new TopTableHeader(Hud, (pos, curPos) => "Min APS")
                {
                    RatioWidth = 75 / 1080f,
                },
                new TopTableHeader(Hud, (pos, curPos) => "Max APS")
                {
                    RatioWidth = 75 / 1080f,
                },
                new TopTableHeader(Hud, (pos, curPos) => "\u0394 IAS")
                {
                    RatioWidth = 75 / 1080f,
                },
                new TopTableHeader(Hud, (pos, curPos) => "\u0394 FPA-DPS")
                {
                    RatioWidth = 75 / 1080f,
                }
            );

            uint lineCount = ShowLower + ShowHigher + 1;
            for(int i = 0; i < lineCount; i++)
            {
                Table.AddLine(
                    new TopTableHeader(Hud, (pos, curPos) =>  (pos == ShowHigher)?Hud.Game.Me.Offense.AttackSpeed.ToString(AttackSpeedDescriptor):"")
                    {
                        RatioWidth = 62 / 1080f, // define only once on first line, value on other will be ignored
                        RatioHeight = 22 / 1080f,
                        HighlightFunc = (pos, curPos) => pos == ShowHigher,
                        HighlightDecorator = HighlightCellDecorator,
                    },
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => GetCellText(line, column)),
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => GetCellText(line, column)),
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => GetCellText(line, column)),
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => GetCellText(line, column)),
                    new TopTableCell(Hud, (line, column, lineSorted, columnSorted) => GetCellText(line, column))
                );
            }            
        }	

        private Tuple<double, int, double> getBreakpointTuple(int idx){
            if (cbp == null || idx < 0 || idx >= cbp.Count())
                return new Tuple<double, int, double>(0,0,0);     
            return cbp[idx];   
        }

        private string GetCellText(int line, int column){
            int sidx = getStartingIndex();
            switch(column){
                case 0:
                    return getBreakpointTuple(sidx+line).Item2.ToString();
                case 1:
                    return getBreakpointTuple(sidx+line).Item1.ToString(AttackSpeedDescriptor);
                case 2:
                    return getBreakpointTuple(sidx+line).Item3.ToString(AttackSpeedDescriptor);
                case 3:
                    return (100*(getBreakpointTuple(sidx+line).Item1-Hud.Game.Me.Offense.AttackSpeed)/_baseweapon).ToString(PercentDescriptor)+" %";
                case 4:
                    return ((100.0f*getBreakpointTuple(getCurrentBreakpointIndex()).Item2)/(getBreakpointTuple(sidx+line).Item2)-100).ToString(PercentDescriptor)+" %";
                default:
                    return "GetCellText() failed";
            }
        }

        private int getStartingIndex(){
            if (getCurrentBreakpointIndex()-(int)ShowHigher >= 0)
                return getCurrentBreakpointIndex()-(int)ShowHigher;
            return 0;
        }

        private int getCurrentBreakpointIndex(){
            double aps = getRoundedAPS();
            int i;
            for(i = 0; i < cbp.Count(); i++)
                if (aps >= cbp[i].Item1 && aps <= cbp[i].Item3)
                    return i;
            return i;
        }

        private double getRoundedAPS(){
            double val = Hud.Game.Me.Offense.AttackSpeed;
            val = Math.Round(val * 10000)/10000;
            return val;
        }
        
        public void DrawBreakpointsTable(IPlayerSkill skill){
            var ui = Hud.Render.GetPlayerSkillUiElement(skill.Key);
            var rect = new RectangleF((float)Math.Round(ui.Rectangle.X) + 0.5f, (float)Math.Round(ui.Rectangle.Y) + 0.5f, (float)Math.Round(ui.Rectangle.Width), (float)Math.Round(ui.Rectangle.Height));
            if (Hud.Window.CursorInsideRect(rect.Left, rect.Top, rect.Width, rect.Height))
                cbp = bpf.CreateBreakpointTable(skill.SnoPower);
            else
                return;

            if (cbp != null)
                Table.Paint();
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.BeforeClip) return;
            foreach (var skill in Hud.Game.Me.Powers.UsedSkills)
                DrawBreakpointsTable(skill); 
	    }
    }
 
}