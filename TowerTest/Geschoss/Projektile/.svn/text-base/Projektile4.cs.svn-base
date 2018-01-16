using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TowerTest
{
    public class Projektile4 : Projektilegeschoss
    {
        public Projektile4(): base()
        {
            IntializeGeschoss();
        }

        public Projektile4(Point Startposition, Einheit Ziel)
            : base(Startposition, Ziel)
        {
            IntializeGeschoss();
        }
        public void IntializeGeschoss()
        {
            base.M_Grösse.Height = 5;
            base.M_Grösse.Width = 5;
            base.M_Brush = Brushes.LimeGreen;
            base.M_Schadenswert = 2400;
            base.M_Slowdown = 1;
            base.M_Strecke = 18;
            base.M_Image = Image.FromFile(Application.StartupPath + "\\Grafiken\\geschoss4.png");
        }


        protected override void SetSchaden()
        {
            M_Ziel.Lebensenergie -= M_Schadenswert;
        }
    }
}
