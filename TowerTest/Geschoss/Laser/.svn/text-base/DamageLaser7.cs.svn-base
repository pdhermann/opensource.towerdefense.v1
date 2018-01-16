using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerTest.Laser
{
    class DamageLaser7:Laserstrahl
    {
          public DamageLaser7():base() 
        {
            IntializeGeschoss();
        }

        public DamageLaser7(Point Startposition, Einheit Ziel): base(Startposition, Ziel)
        {
            IntializeGeschoss();
        }

        public void IntializeGeschoss() 
        {
            base.M_Grösse.Height = 5;
            base.M_Grösse.Width = 5;
            base.M_Pen.Color = Color.Red;
                                //1000000000000000;
            base.M_Schadenswert = 1152000;
        }

        protected override void SetSchaden() 
        {
            M_Ziel.Lebensenergie -= M_Schadenswert;
        }
    }
}
