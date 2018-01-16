using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
namespace TowerTest
{
   public class Einheit4:Einheit
    {
       public Einheit4(ArrayList Route ):base(Route)
       {

           M_brush = Brushes.Orange;
           M_pens = Pens.Black ;
           M_EinheitSize.Height=10;
           M_EinheitSize.Width =10;
           M_Lebensenergie = 1000;
           M_MoneyForKill = 500;
           M_PointsForKill= 500;
           M_Strecke = 12;
           M_Geschwindigkeit = 12;
           M_Slowdowntime = 10;
           M_SlowDownTemp = 10;
       }
    }
}
