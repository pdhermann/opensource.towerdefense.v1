using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerTest
{
    public abstract class Laserstrahl:Geschoss
    {
        public Laserstrahl():base() 
        {

        }

        public Laserstrahl(Point Startposition, Einheit Ziel):base(Startposition,Ziel)
        {
            
        }

        public override void OnPaint(Graphics graphics)
        {
            if (M_Getroffen == false)
            {
                Rectangle rect = new Rectangle(M_Position, M_Grösse);
                graphics.DrawLine(M_Pen, M_Position, M_Ziel.MiddlePosition);
                
                M_Getroffen = true;
                SetSchaden();

            }

        }
    }
}
