using System.Linq;
using SharpDX;
using Turbo.Plugins.Default;
 
namespace Turbo.Plugins.Gigi
{
 
    public class SelfBuffPlugin : BasePlugin, IInGameWorldPainter
    {   
        public WorldDecoratorCollection PlayerDecorator { get; set; }
        
        public SelfBuffPlugin()
        {
            Enabled = true;
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

        private void ShowPowerData(WorldLayer layer, uint sno, string name, string desc, IPlayer p){
            string data = "BuffData:\n";
            foreach(var s in p.Powers.AllBuffs){
                if (s.SnoPower.Sno == sno || s.SnoPower.Code.Contains(name) || s.SnoPower.DescriptionEnglish != null && s.SnoPower.DescriptionEnglish.Contains(desc)){
                    data += s.SnoPower.Sno.ToString() + "\t";
                    data +=  "["+string.Join(",", s.IconCounts)+"]" + "\t";
                    data += s.Active.ToString() + "\t";
                    data += s.SnoPower.NameEnglish + " : ";
                    data += s.SnoPower.Code + " : ";
                    data += s.SnoPower.DescriptionEnglish + "\n";
                }
            }
            
            ISnoPower skill = Hud.Sno.GetSnoPower(sno);
            if(p.IsMe && skill != null){
                data += "\nSNOData:\n";
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
                data += "]" + "\t";
                data += skill.NameEnglish + " : ";
                data += skill.Code + " : ";
                data += skill.DescriptionEnglish + "\n";
            }
            
            PlayerDecorator.Paint(layer, p, p.FloorCoordinate, data);
        }

        private void DrawTexture(uint tid, RectangleF r){
            ITexture t = Hud.Texture.GetTexture(tid);
            if (t == null) return;
            t.Draw(r, 1.0f);

        }

        public void PaintWorld(WorldLayer layer)
        {   /*
            var pets = Hud.Game.Actors.Where(x => x.SummonerAcdDynamicId == Hud.Game.Me.SummonerId);
            foreach(var p in pets)
                PlayerDecorator.Paint(layer, p, p.FloorCoordinate, "Pet");
            */
            var s = Hud.Game.Me.Powers.UsedSkills.First();
            if (s == null) return;
            var player = Hud.Game.Players;
            foreach(var p in player)
                ShowPowerData(layer, Hud.Sno.SnoPowers.DemonHunter_Impale.Sno, "Inner", "XXXXXXXXXX", p);
        }
    }
}