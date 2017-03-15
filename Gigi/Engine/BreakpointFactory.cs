using System;
using System.Collections.Generic;
namespace Turbo.Plugins.Gigi.Engine
{
    public class BreakpointFactory
    {
        public IController Hud { get; set; }

        private int _minframe;
        private int _maxframe;
        private double _deltaAPS;

        public BreakpointFactory(IController hud)
        {
            Hud = hud;
            _minframe = 15;
            _maxframe = 40;
            _deltaAPS = 0.0001;
        }

        public List<Tuple<double, int, double>> CreateBreakpointTable(int b_anim, float s_coeff=1.0f){
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
    }
}