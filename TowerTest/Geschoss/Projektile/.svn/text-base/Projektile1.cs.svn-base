using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TowerTest
{
    public class Projektile1:Projektilegeschoss
    {
        public Projektile1():base() 
        {
            IntializeGeschoss();
        }

        public Projektile1(Point Startposition, Einheit Ziel):base(Startposition,Ziel)
        {
            IntializeGeschoss();
        }
        public void IntializeGeschoss()
        {
            base.M_Grösse.Height = 5;
            base.M_Grösse.Width = 5;
            base.M_Brush = Brushes.LimeGreen;
            base.M_Schadenswert = 50;
            base.M_Slowdown = 1;
            base.M_Strecke = 15;
            base.M_Image = Image.FromFile(Application.StartupPath + "\\Grafiken\\geschoss1.png");
        }


        protected override void SetSchaden() 
        {
            M_Ziel.Lebensenergie -= M_Schadenswert;
        }
    }
}
