using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerTest.Laser
{
    public class SlowLaser7 : Laserstrahl
    {
        public SlowLaser7():base() 
        {
            IntializeGeschoss();
        }
   
        public SlowLaser7(Point Startposition, Einheit Ziel):base(Startposition,Ziel)
        {
            IntializeGeschoss();
        }

        public void IntializeGeschoss() 
        {
            base.M_Grösse.Height = 5;
            base.M_Grösse.Width = 5;
            base.M_Pen.Color = Color.LimeGreen;
            base.M_Schadenswert = 691200;
            base.M_Slowdown = 7;
            
        }

        protected override void SetSchaden() 
        {
            M_Ziel.Lebensenergie -= M_Schadenswert;
            M_Ziel.SetSlowdown(M_Slowdown);//Einheit verlangsammen
        }
    }
}
