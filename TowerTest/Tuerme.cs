using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace TowerTest
{
    class Tuerme
    {
        private Panel spielfeld;
        private int ID;
        private int Level;
        private int GeschossID;
        private int Feuerrate;
        private int Reichweite;
        private double Kosten;
        private int MaxLevel;
        private double UpgradeKosten;
        private Point Position;
        private Point MiddleTower_Fire;
        private Point MiddleTower_Pos;
        private Pen pen;
        Rectangle rect;
        Rectangle range;
        private bool m_Showrange;
        bool rangechanged;
        ArrayList Einheiten;

        public bool ShowRange
        {
            get
            {
                return m_Showrange;
            }
            set
            {
                m_Showrange = value;
            }
        }


        public Tuerme(Panel sp, int ID,Point Position, ArrayList Einheiten)
        {
            this.Einheiten = Einheiten;
            this.spielfeld = sp;
            this.ID = ID;
            this.Position = Position;
            this.MiddleTower_Fire = new Point(Position.X + 10, Position.Y + 10);
            this.MiddleTower_Pos = new Point(Position.X + 7, Position.Y + 7);
            this.Level = 1;
            switch (ID)
            {
                case 1:             //Raketen
                    GeschossID = 1;
                    Feuerrate = 50;
                    Reichweite = 300;
                    Kosten = 50;
                    MaxLevel = 5;
                    UpgradeKosten = 50;
                    break;
                case 2:             //Laser
                    GeschossID = 2;
                    Feuerrate = 50;
                    Reichweite = 50;
                    Kosten = 50;
                    MaxLevel = 5;
                    UpgradeKosten = 50;
                    break;
                case 3:             //Slowdown
                    GeschossID = 3;
                    Feuerrate = 5;
                    Reichweite = 50;
                    Kosten = 50;
                    MaxLevel = 5;
                    UpgradeKosten = 50;
                    break;
            }
        }

        public void changeRangeState(Graphics g)
        {
            if (m_Showrange)
            {
                pen = new Pen(Color.DarkMagenta);
                g.DrawEllipse(pen, range);
            }
            //else 
            //{
            //    pen = new Pen(Color.Transparent);
            //    g.DrawEllipse(pen, range);
            //}
                
        }

        public void OnPaint(Graphics g)
        {
            switch (ID)
            {
                case 1:
                    pen = new Pen(Color.Blue);
                    rect = new Rectangle(Position, new Size(20, 20));                    
                    g.FillRectangle(pen.Brush, rect);

                    pen = new Pen(Color.AliceBlue);
                    rect = new Rectangle(MiddleTower_Pos, new Size(6, 6));
                    g.DrawRectangle(pen, rect);
                    range = new Rectangle(MiddleTower_Fire.X - Reichweite, MiddleTower_Fire.Y - Reichweite, Reichweite * 2, Reichweite * 2);
                    
                    break;
                case 2:
                    pen = new Pen(Color.Red);
                    
                    rect = new Rectangle(Position, new Size(20, 20));
                    g.FillRectangle(pen.Brush, rect);

                    pen = new Pen(Color.DarkRed);
                    rect = new Rectangle(MiddleTower_Pos, new Size(6, 6));
                    g.DrawRectangle(pen, rect);

                    range = new Rectangle(MiddleTower_Fire.X - Reichweite, MiddleTower_Fire.Y - Reichweite, Reichweite * 2, Reichweite * 2);
                    
                    break;
                case 3:
                    pen = new Pen(Color.Green);
                    rect = new Rectangle(Position, new Size(20, 20));
                    g.FillRectangle(pen.Brush, rect);

                    pen = new Pen(Color.DarkSeaGreen);
                    rect = new Rectangle(MiddleTower_Pos, new Size(6, 6));
                    g.DrawRectangle(pen, rect);

                    range = new Rectangle(MiddleTower_Fire.X - Reichweite, MiddleTower_Fire.Y - Reichweite, Reichweite * 2, Reichweite * 2);
                    
                    break;
            }
        }
        public void Upgrade()
        {
            if (Level + 1 != MaxLevel)
            {
                Level++;
                switch (ID)
                {
                    case 1:
                        break;
                }
            }
        }
        public int GetCurrentLevel()
        {
            return Level;
        }
        public int GetMaxLevel()
        {
            return MaxLevel;
        }
        public Geschoss Shoot(Einheit e)
        {
          
                Geschoss g = new Laser.SlowLaser1(MiddleTower_Pos, e);
                Geschoss f = new Laser.SlowLaser2(MiddleTower_Pos, e);
            return g;
        }

        public bool IsInRange(Point p)
        {
            bool InRange = false;
            if (range.Contains(p))
            {
                InRange = true;
            }
            
            return InRange;
        }
    }
}
