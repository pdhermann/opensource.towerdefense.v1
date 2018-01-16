using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerTest.Laser
{
    public class DamageLaser3:Laserstrahl
    {
        public DamageLaser3():base() 
        {
            IntializeGeschoss();
        }

        public DamageLaser3(Point Startposition, Einheit Ziel): base(Startposition, Ziel)
        {
            IntializeGeschoss();
        }

        public void IntializeGeschoss() 
        {
            base.M_Grösse.Height = 5;
            base.M_Grösse.Width = 5;
            base.M_Pen.Color = Color.AliceBlue;
            base.M_Schadenswert = 200;
        }

        protected override void SetSchaden() 
        {
            M_Ziel.Lebensenergie -= M_Schadenswert;
        }
    }
}
