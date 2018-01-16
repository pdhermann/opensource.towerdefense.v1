using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace TowerTest
{
    public abstract class Turm
    {
        #region Members

        protected Spielfeld M_Spielfeld;
        protected int       M_MaxEinheitToShoot;    //Gibt an wieviel Einheit pro Turm beschossen werden können
        protected int       M_SchadenProGeschoss;       
        protected int       M_Level;
        protected int       M_Feuerrate;
        protected int       M_IReichweite;
        protected Rectangle M_Reichweite;
        protected int       M_Kosten;
        protected int       M_UpgradeKosten;
        protected int       M_MaxLevel;
        protected Point     M_Position;
        protected Pen       M_Pen;
        protected Rectangle M_rect;
        protected bool      M_Showrange;
        protected bool      M_rangechanged;
        protected ArrayList M_Einheiten;
        protected int       M_PaintCount;
        protected Point MiddleTower_Fire;
        protected Point MiddleTower_Pos;
        protected Type      M_Geschosstyp;
        protected Geschoss  M_Geschoss;
        

        #endregion

        protected void Shoot(ArrayList EinheitenToShoot) 
        {
            
            for (int i = 0; i < EinheitenToShoot.Count; i++)
            {
                M_Spielfeld.Geschosse.Add(Activator.CreateInstance(M_Geschosstyp, MiddleTower_Fire, ((Einheit)EinheitenToShoot[i])));
            }
        }
        public void RaiseReichweite(int Value)
        {
            M_IReichweite = Value;
            M_Reichweite = new Rectangle(MiddleTower_Fire.X - M_IReichweite, MiddleTower_Fire.Y - M_IReichweite, M_IReichweite * 2, M_IReichweite * 2);
        }

        public Geschoss Geschoss 
        {
            get
            {
                return ((Geschoss)Activator.CreateInstance(M_Geschosstyp));
            }
        }

        /// <summary>
        /// Diese Methode überprüft die Reichweite des Turms und gibt eine Arraylist zurück welche 
        /// die Einheiten enthält die beschossen werden können
        /// </summary>
        /// <returns></returns>
        public ArrayList CheckRange()
        {
            ArrayList EinheitenInReichweite = new ArrayList();
            int j = 1;
            for (int i = 0; i < M_Spielfeld.Einheiten.Count; i++)
            {
                if (M_Reichweite.Contains(((Einheit)M_Spielfeld.Einheiten[i]).Position))
                {
                    /*
                    double EinX = (double)((Einheit)M_Spielfeld.Einheiten[i]).Position.X;
                    double EinY = (double)((Einheit)M_Spielfeld.Einheiten[i]).Position.Y;
                    double TurmX = (double)this.M_Position.X;
                    double TurmY = (double)this.M_Position.Y;

                    if (EinX < TurmX)
                    {
                        EinX = TurmX + (TurmX - EinX);
                    }
                    EinX = EinX - (TurmX - M_IReichweite);
                    EinX = EinX / (M_IReichweite*3);
                    if (EinY < TurmY)
                    {
                        EinY = TurmY + (TurmY - EinY);
                    }
                    EinY = EinY - (TurmY - M_IReichweite);
                    EinY = EinY / (M_IReichweite*3);

                    if ((EinX * EinX) + (EinY * EinY) <= 1)
                    {*/
                        if (M_MaxEinheitToShoot >= j)
                        {
                            EinheitenInReichweite.Add(M_Spielfeld.Einheiten[i]);
                            j++;
                        }
                    //}
                }
            }
            return EinheitenInReichweite;


        } 

        public abstract void OnPaint(Graphics graphics);

        public abstract void Upgrade();


        #region Eigenschaften

        public Rectangle Rechteck 
        {
            get 
            {
                return M_rect;
            }
        }

        public int MaxEinheitToShoot 
        {
            
            get 
            {
                
                return M_MaxEinheitToShoot;
            }
            set
            {
                M_MaxEinheitToShoot = value;
            }
        }


        public int SchadenProGeschoss 
        {
            get 
            {
                return M_SchadenProGeschoss;
            }
            set
            {
                Geschoss.Schaden = value;
                M_SchadenProGeschoss = value;
            }
        }

        public int Feuerrate 
        {
            get 
            {
                return M_Feuerrate;
            }
            set
            {
                M_Feuerrate = value;
            }
        }


        public Rectangle Reichweite 
        {
            get 
            {
                return M_Reichweite;
            }
            set
            {
                M_Reichweite = value;
            }
        }
        public Rectangle TurmRect
        {
            get
            {
                return M_rect;
            }
        }

        public int Level
        {
            get 
            {
                return M_Level;
            }
        }


        public int MaxLevel 
        {
            get 
            {
                return M_MaxLevel;
            }
        }


        public int Kosten 
        {
            get 
            {
                return M_Kosten;
            }
        }
        

        public double UpgradeKosten 
        {
            get 
            {
                return M_UpgradeKosten;
            }
        }
        #endregion
    }
}
