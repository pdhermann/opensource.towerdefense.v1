using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerTest.Laser
{
    public class SlowLaser5 : Laserstrahl
    {
        public SlowLaser5():base() 
        {
            IntializeGeschoss();
        }
   
        public SlowLaser5(Point Startposition, Einheit Ziel):base(Startposition,Ziel)
        {
            IntializeGeschoss();
        }

        public void IntializeGeschoss() 
        {
            base.M_Grösse.Height = 5;
            base.M_Grösse.Width = 5;
            base.M_Pen.Color = Color.LimeGreen;
            base.M_Schadenswert = 5760;
            base.M_Slowdown = 5;
            
        }

        protected override void SetSchaden() 
        {
            M_Ziel.Lebensenergie -= M_Schadenswert;
            M_Ziel.SetSlowdown(M_Slowdown);//Einheit verlangsammen
        }
    }
}
