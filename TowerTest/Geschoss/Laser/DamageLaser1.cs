using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerTest.Laser
{
    public class DamageLaser1:Laserstrahl
    {
        public DamageLaser1():base() 
        {
            IntializeGeschoss();
        }

        public DamageLaser1(Point Startposition, Einheit Ziel): base(Startposition, Ziel)
        {
            IntializeGeschoss();
        }

        public void IntializeGeschoss() 
        {
            base.M_Grösse.Height = 5;
            base.M_Grösse.Width = 5;
            base.M_Pen.Color = Color.LimeGreen;
            base.M_Schadenswert = 25;
            base.M_Slowdown = 0;
            base.M_Strecke = 20;
        }

        protected override void SetSchaden() 
        {
            M_Ziel.Lebensenergie -= M_Schadenswert;
            //M_Ziel.SetSlowdown(M_Slowdown);//Einheit verlangsammen
        }
    }
}
