using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerTest.Laser
{
    class DamageLaser2:Laserstrahl
    {
        public DamageLaser2():base() 
        {
            IntializeGeschoss();
        }

        public DamageLaser2(Point Startposition, Einheit Ziel): base(Startposition, Ziel)
        {
            IntializeGeschoss();
        }

        public void IntializeGeschoss() 
        {
            base.M_Gr�sse.Height = 5;
            base.M_Gr�sse.Width = 5;
            base.M_Pen.Color = Color.Purple;
            base.M_Schadenswert = 50;
            base.M_Slowdown = 0;
            
        }

        protected override void SetSchaden() 
        {
            M_Ziel.Lebensenergie -= M_Schadenswert;
            
        }
    }
}
