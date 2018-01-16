using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
namespace TowerTest
{
   public class Einheit1:Einheit
    {
       public Einheit1(ArrayList Route ):base(Route)
       {

           M_brush =Brushes.Blue;
           M_pens = Pens.GreenYellow ;
           M_EinheitSize.Height=10;
           M_EinheitSize.Width =10;
           M_Lebensenergie = 1000;
           M_MoneyForKill = 500;
           M_PointsForKill= 500;
           M_Strecke = 12;
           M_Geschwindigkeit = 12;
           M_Slowdowntime = 30;
           M_SlowDownTemp = 30;
       }
    }
}
