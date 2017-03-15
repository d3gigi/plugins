using System.Linq;
using SharpDX;
using Turbo.Plugins.Default;
 
namespace Turbo.Plugins.Gigi
{
 
    public class SetCheckPlugin : BasePlugin, IInGameWorldPainter
    {   
        public WorldDecoratorCollection PlayerDecorator { get; set; }
        
        public SetCheckPlugin()
        {
            Enabled = false;
        }

 
        public override void Load(IController hud)
        {
            base.Load(hud);
            PlayerDecorator = new WorldDecoratorCollection(
                new GroundLabelDecorator(Hud)
                {
                    BackgroundBrush = Hud.Render.CreateBrush(100, 20, 20, 20, 0),
                    TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 255, 255, 255, false, false, false),
                }
                );
        }

        private string SnoPowerIconsToString(SnoPowerIcon[] ar){
            string res = "[";
            int length = ar.Length;
            for(int i = 0; i < length; i++)
                res += ar[i].TextureId.ToString() + ",";
            res += "]";
            return res;      
        }

        private void ShowPowerData(WorldLayer layer, uint sno){
            string data = "BuffData:\n";
            foreach(var s in Hud.Game.Me.Powers.AllBuffs){
                if (s.SnoPower.Sno == sno || s.SnoPower.Code.Contains("SetItemBonusBuff")){
                    data += s.SnoPower.Sno.ToString() + "\t";
                    data +=  "["+string.Join(",", s.IconCounts)+"]" + "\t";
                    data += s.Active.ToString() + "\t";
                    data += s.SnoPower.NameEnglish + " : ";
                    data += s.SnoPower.Code + " : ";
                    data += s.SnoPower.DescriptionEnglish + "\n";
                }
            }
            data += "\nSNOData:\n";
            ISnoPower skill = Hud.Sno.GetSnoPower(sno);
            data += skill.Sno.ToString() + "\t";
            data += skill.NormalIconTextureId + "\t";
            int i = 0;
            float x = Hud.Window.Size.Width / 2;
            float y  = Hud.Window.Size.Height / 2 + Hud.Window.Size.Height * 0.1f;
            data += "[";
            DrawTexture(skill.NormalIconTextureId, new RectangleF(x-55, y, 50, 50));
            foreach(SnoPowerIcon si in skill.Icons){
                if (si.Exists){
                    data += "(" + (i++) + ":"+si.TextureId.ToString() + "),";
                    if (si.TextureId > 0)
                        DrawTexture(si.TextureId, new RectangleF(x, y, 50, 50));
                    x+=55;
                }
            }
            data += "]";
            PlayerDecorator.Paint(layer, Hud.Game.Me, Hud.Game.Me.FloorCoordinate, data);
        }

        private void DrawTexture(uint tid, RectangleF r){
            ITexture t = Hud.Texture.GetTexture(tid);
            if (t == null) return;
            t.Draw(r, 1.0f);

        }

        public void PaintWorld(WorldLayer layer)
        {  
            ShowPowerData(layer, 423234);
        }
    }
 
}