using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TowerTest
{
    public class Projektile7 : Projektilegeschoss
    {
        public Projektile7(): base()
        {
            IntializeGeschoss();
        }

        public Projektile7(Point Startposition, Einheit Ziel): base(Startposition, Ziel)
        {
            IntializeGeschoss();
        }
        public void IntializeGeschoss()
        {
            base.M_Grösse.Height = 5;
            base.M_Grösse.Width = 5;
            base.M_Brush = Brushes.LimeGreen;
            base.M_Schadenswert = 2304000;
            base.M_Strecke = 22;
            base.M_Image = Image.FromFile(Application.StartupPath + "\\Grafiken\\geschoss4.png");
        }


        protected override void SetSchaden()
        {
            M_Ziel.Lebensenergie -= M_Schadenswert;
        }
    }
}
