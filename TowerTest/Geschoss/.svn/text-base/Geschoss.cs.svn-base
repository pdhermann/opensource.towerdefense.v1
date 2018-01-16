using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace TowerTest
{
    public abstract class Geschoss
    {

        protected Point     M_Position;
        protected int       M_Schadenswert;
        protected int       M_Slowdown;
        protected Pen       M_Pen;
        protected Size      M_Grösse;
        protected Einheit   M_Ziel;
        protected int       M_Strecke;
        protected bool      M_Getroffen = false;
        protected Brush     M_Brush;

        public Geschoss() 
        {
            M_Grösse = new Size();
            M_Pen = new Pen(Color.White);
            M_Schadenswert = 0;
            M_Slowdown = 0;
            M_Strecke = 0;
        }

        public Geschoss(Point Startposition, Einheit Ziel) 
        {
            M_Ziel = Ziel;
            M_Position = Startposition;
            M_Grösse = new Size();
            M_Pen = new Pen(Color.White);
            M_Schadenswert = 0;
            M_Slowdown = 0;
            M_Strecke = 0;
        }
        public int Schaden
        {
            get 
            {
                return M_Schadenswert;
            }
            set
            {
                M_Schadenswert = value;
            }
        }

        public int Slowdown 
        {
            get 
            {
                return M_Slowdown;
            }
        }

        public bool Getroffen 
        {
            get
            {
                return M_Getroffen;
            }
        }
           

        public abstract void OnPaint(Graphics graphics);

        protected abstract void SetSchaden() ;


        

        public Point Position
        {
            get
            {
                return M_Position;
            }
            
        }
    }
}
