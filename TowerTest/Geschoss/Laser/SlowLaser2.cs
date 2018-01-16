using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerTest.Laser
{
    public class SlowLaser2:Laserstrahl
    {
        public SlowLaser2():base() 
        {
            IntializeGeschoss();
        }


        public SlowLaser2(Point Startposition, Einheit Ziel):base(Startposition,Ziel)
        {
            IntializeGeschoss();
        }

        public void IntializeGeschoss()
        {
            base.M_Grösse.Height = 5;
            base.M_Grösse.Width = 5;
            base.M_Pen.Color = Color.YellowGreen;
            base.M_Schadenswert = 30;
            base.M_Slowdown = 2;
            //base.M_Strecke = 20;
        }

        protected override void SetSchaden() 
        {
            M_Ziel.Lebensenergie -= M_Schadenswert;
            M_Ziel.SetSlowdown(M_Slowdown);
        }
    }
}

