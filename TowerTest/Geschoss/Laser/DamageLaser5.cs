using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerTest.Laser
{
    class DamageLaser5:Laserstrahl
    {
          public DamageLaser5():base() 
        {
            IntializeGeschoss();
        }

        public DamageLaser5(Point Startposition, Einheit Ziel): base(Startposition, Ziel)
        {
            IntializeGeschoss();
        }

        public void IntializeGeschoss() 
        {
            base.M_Gr�sse.Height = 5;
            base.M_Gr�sse.Width = 5;
            base.M_Pen.Color = Color.HotPink;
            base.M_Schadenswert = 9600;
        }

        protected override void SetSchaden() 
        {
            M_Ziel.Lebensenergie -= M_Schadenswert;
        }
    }
}
