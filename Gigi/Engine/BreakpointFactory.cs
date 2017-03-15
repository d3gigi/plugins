using System;
using System.Collections.Generic;
namespace Turbo.Plugins.Gigi.Engine
{
    public class BreakpointFactory
    {
        public IController Hud { get; set; }

        private Dictionary<uint, Tuple<int, float>> _parameters;

        private int _minframe;
        private int _maxframe;
        private double _deltaAPS;
        private int _b_anim;
        private float _s_coeff; 

        public BreakpointFactory(IController hud)
        {
            Hud = hud;
            _minframe = 3;
            _maxframe = 120;
            _deltaAPS = 0.0001;
            _b_anim = 0;
            _s_coeff = 0f;
            _parameters = new Dictionary<uint, Tuple<int, float>>();
            fillParameters();
        }

        public List<Tuple<double, int, double>> CreateBreakpointTable(ISnoPower pwr){
            if (_parameters.ContainsKey(pwr.Sno)){
                _b_anim = _parameters[pwr.Sno].Item1;
                _s_coeff = _parameters[pwr.Sno].Item2;
                return CreateBreakpointTable(_b_anim, _s_coeff);
            }
            return null;
        }

        public int getBaseAnimationLength(){
            return _b_anim;
        }

        public float getSpeedCoefficient(){
            return _s_coeff;
        }

        private List<Tuple<double, int, double>> CreateBreakpointTable(int b_anim, float s_coeff=1.0f){
            List<Tuple<double, int, double>> res = new List<Tuple<double, int, double>>();
            for (int i = _minframe; i <= _maxframe; i++){
                double minaps = calculateMinAPS(b_anim, s_coeff, i);
                double maxaps = calculateMinAPS(b_anim, s_coeff, i-1) - _deltaAPS;
                res.Add(new Tuple<double, int, double>(minaps, i, maxaps));
            }
            return res;
        }

        private double calculateMinAPS(int b_anim, float s_coeff, int fpa, int engine_tickrate=60){
            double val = 0f;
            if (b_anim != 0)
                val = (b_anim-1)*engine_tickrate / (fpa * b_anim * s_coeff);
            else
                val = engine_tickrate / (fpa * s_coeff);
            val = Math.Round(val * 10000)/10000;
            return val;
        }

        private void fillParameters(){
            _parameters.Clear();
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_ArcaneOrb.Sno, new Tuple<int, float>(28, 1.0f));                   
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_ArcaneTorrent.Sno, new Tuple<int, float>(0, 3.0f));                 //Rune: Arcane Mines s_coeff = 1.5 !
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_ArchonArcaneStrike.Sno, new Tuple<int, float>(25, 1.0f));          
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_ArchonDisintegrationWave.Sno, new Tuple<int, float>(0, 3.0f));     
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_BlackHole.Sno, new Tuple<int, float>(19, 1.0f));                   
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_Blizzard.Sno, new Tuple<int, float>(26, 1.0f));                    
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_Disintegrate.Sno, new Tuple<int, float>(0, 3.0f));                 
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_Electrocute.Sno, new Tuple<int, float>(0, 2.0f));                  
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_EnergyArmor.Sno, new Tuple<int, float>(16, 1.0f));                 
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_EnergyTwister.Sno, new Tuple<int, float>(25, 1.0f));               
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_ExplosiveBlast.Sno, new Tuple<int, float>(16, 1.0f));              
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_IceArmor.Sno, new Tuple<int, float>(16, 1.0f));                    
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_MagicMissile.Sno, new Tuple<int, float>(25, 1.0f));                
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_MagicWeapon.Sno, new Tuple<int, float>(16, 1.0f));                 
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_Meteor.Sno, new Tuple<int, float>(21, 1.0f));                      
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_MirrorImage.Sno, new Tuple<int, float>(26, 1.0f));                 
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_RayOfFrost.Sno, new Tuple<int, float>(0, 2.0f));                   
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_ShockPulse.Sno, new Tuple<int, float>(21, 1.0f));                  
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_SlowTime.Sno, new Tuple<int, float>(16, 1.0f));                    
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_SpectralBlade.Sno, new Tuple<int, float>(16, 1.0f));               
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_StormArmor.Sno, new Tuple<int, float>(16, 1.0f));                  
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_Teleport.Sno, new Tuple<int, float>(7, 1.0f));                     
            _parameters.Add(Hud.Sno.SnoPowers.Wizard_WaveOfForce.Sno, new Tuple<int, float>(33, 1.0f));                 
        }
    }
}