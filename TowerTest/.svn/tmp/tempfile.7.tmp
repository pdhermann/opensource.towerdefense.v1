using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;


namespace TowerTest
{
    public class Spielfeld
    {
        ArrayList M_Einheiten;
        ArrayList M_Geschosse;
        ArrayList M_Türme;
        int       M_Money;
        ArrayList M_Route;
        Welle     M_Welle;
        int       M_Highscore;
        int       M_Lives;
        string    M_Status=string.Empty;
        DateTime  M_Startzeit;
        Einstellungen M_Einstellungen;
        

        public Spielfeld(Einstellungen Einst) 
        {
            M_Einheiten = new ArrayList();
            M_Geschosse = new ArrayList();
            M_Türme = new ArrayList();
            M_Route = new ArrayList();
            M_Einstellungen = Einst;
            M_Money = M_Einstellungen.StartMoney;
            M_Lives = M_Einstellungen.Lives;
            M_Welle = new Welle(this,3, M_Einstellungen);
            CreateRoute();
        }
        #region Properies

        public string Status 
        {
            get 
            {
                return M_Status;
            }
        }

        public int Level 
        {
            get 
            {
                return M_Welle.Level;
            }
        }
        public int Highscore 
        {
            get 
            {
                return M_Highscore;
            }
        }

        public int Lives 
        {
            get 
            {
                return M_Lives;
            }
            set
            {
                M_Lives = value;
            }
        }

        public int Money 
        {
            get 
            {
                return M_Money;
            }
            set 
            {
                M_Money = value;
            }
        }

        public ArrayList Route 
        {
            get 
            {
                return M_Route;
            }
            set
            {
                M_Route = value;
            }
        }

        public DateTime Startzeit
        {
            get
            {
                return M_Startzeit;
            }
            set
            {
                M_Startzeit = value;
            }
        }


        public ArrayList Geschosse 
        {
            get 
            {
                return M_Geschosse;
            }
        }

        public ArrayList Türme 
        {
            get 
            {
                return M_Türme;
            }
        }


        public ArrayList Einheiten 
        {
            get 
            {
                return M_Einheiten;
            }
        }

        #endregion


        public void CreateRoute() 
        {
            M_Route.Add(new Point( 30,  20));
            M_Route.Add(new Point( 30, 570));
            M_Route.Add(new Point(550, 570));
            M_Route.Add(new Point(650, 300));
            M_Route.Add(new Point(300, 300));
            M_Route.Add(new Point(300, 500));
            M_Route.Add(new Point(150, 500));
            M_Route.Add(new Point(150,  20));
        }

        public void StartWave() 
        {
            M_Welle.StartNextWave();
        }

       

        public void LostLive() 
        {
            if (M_Lives > 0)
            {
                M_Lives--;
            }
        }
        

        public void DestroyEnemy(int Points,int Money) 
        {
            M_Money += Money;
            M_Highscore += Points;
        }

        public void OnPaint(Graphics graphics) 
        {
            if (M_Lives > 0)
            {
                if (M_Welle.Level == 200)
                {

                    if (M_Einheiten.Count == 0)
                    {
                        if (M_Welle.Einheiten.Count == 0)
                        {
                            M_Status = "Gewonnen";
                        }
                    }
                }
                if (M_Welle.Level < 201)
                {
                    //Raster test
                    //for (int i = 0; i < 700; i = i + 20)
                    //{
                    //    Point Oben = new Point(i, 0);
                    //    Point Unten = new Point(i, 500);
                    //    graphics.DrawLine(new Pen(Color.Blue), Oben, Unten);
                    //}
                    //for (int i = 0; i < 500; i = i + 20)
                    //{
                    //    Point Oben = new Point(0, i);
                    //    Point Unten = new Point(700, i);
                    //    graphics.DrawLine(new Pen(Color.Blue), Oben, Unten);
                    //}


                    Point[] points = new Point[M_Route.Count];

                    for (int i = 0; i < M_Route.Count; i++)
                    {
                        points[i] = ((Point)M_Route[i]);
                    }

                    graphics.DrawLines(new Pen(Color.Black, 40), points);//Route zeichenen


                    //Erst die Einheiten zeichnen
                    for (int i = 0; i < M_Einheiten.Count; i++)
                    {
                        ((Einheit)M_Einheiten[i]).OnPaint(graphics);
                    }

                    if (M_Einheiten.Count > 0)
                    {
                        //Sortierung der Einheiten
                        Einheit[] Temp = new Einheit[M_Einheiten.Count];

                        for (int i = 0; i < M_Einheiten.Count; i++)
                        {
                            Temp[((Einheit)M_Einheiten[i]).Platz] = ((Einheit)M_Einheiten[i]);
                        }
                        M_Einheiten.Clear();
                        for (int i = 0; i < Temp.Length; i++)
                        {
                            M_Einheiten.Add(Temp[i]);
                        }
                    }

                    //Dann die Türme zeichen
                    for (int i = 0; i < M_Türme.Count; i++)
                    {
                        ((Turm)Türme[i]).OnPaint(graphics);
                    }

                    //Dann die Geschosse zeichnen
                    for (int i = 0; i < M_Geschosse.Count; i++)
                    {
                        ((Geschoss)Geschosse[i]).OnPaint(graphics);
                    }


                    //Einheiten löschen die Tot sind oder nicht mehr auf den Spielfeld
                    for (int i = 0; i < M_Einheiten.Count; i++)
                    {
                        if (((Einheit)M_Einheiten[i]).Zielerreicht == true)
                        {
                            LostLive();

                            M_Einheiten.RemoveAt(i);
                        }

                        else if (((Einheit)M_Einheiten[i]).Lebensenergie <= 0)
                        {

                            DestroyEnemy(((Einheit)M_Einheiten[i]).PointsforKill, ((Einheit)M_Einheiten[i]).MoneyforKill);

                            M_Einheiten.RemoveAt(i);

                        }
                    }

                    //Sortierung der Einheiten
                    for (int i = 0; i < M_Einheiten.Count; i++)
                    {
                        if (i == 0)
                        {
                            ((Einheit)M_Einheiten[i]).Vorgänger = null;
                            ((Einheit)M_Einheiten[i]).Platz = 0;
                        }
                        else
                        {
                            ((Einheit)M_Einheiten[i]).Vorgänger = ((Einheit)M_Einheiten[i - 1]);
                            ((Einheit)M_Einheiten[i]).Platz = i;
                        }
                    }


                    //Geschosse löschen die das Ziel getroffen haben
                    for (int i = 0; i < M_Geschosse.Count; i++)
                    {
                        if (((Geschoss)Geschosse[i]).Getroffen)
                        {
                            M_Geschosse.RemoveAt(i);
                        }
                    }

                    if (M_Welle != null)
                    {
                        M_Welle.PaintNextEinheit();//nächste Einheit auf das spielfeld lassen
                    }
                }

            }
            else 
            {
                M_Status="Verloren";
            }
            GC.Collect();
        }
    }
}
