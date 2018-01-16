using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;


namespace TowerTest
{
    public  class Einheit
    {

        #region Members

        protected Point     M_Position;             //Positon der Einheit auf dem Spielfeld
        protected Point     M_MiddlePosition;
        protected int       M_NextPointToRun;       //Gibt an zu welchem Punkt die Einheit läuft
        protected int       M_MoneyForKill;         //Gibt an wieviel Geld es gibt für das töten dieser Einheit
        protected int       M_Lebensenergie = 100; //Gibt an wieviel Lebenspunkte die Einheit hat
        protected int       M_StartLebensenergie;
        protected Einheit   M_Vorgänger;            //Gibt die Einheit an die Vorgänger von dieser ist
        protected int       M_Platz;                //Gibt den Platz in der Welle an
        protected ArrayList M_Route;                //Gibt die Route von Punkten an die die Einheiten durchlaufen
        protected bool      M_Paint;
        protected int       M_Strecke;              //Gibt an welche Strecke pro Tick von dieser Einheit bewältigt wird
        private bool        M_NoX = false;          //Gibt an ob in x Richtung die volle Strecke gegangen werden konnte oder nicht
        private bool        M_NoY = false;          //Gibt an ob in y Richtung die volle Strecke gegangen werden konnte oder nicht
        protected int       M_PointsForKill;        //Gibt an wieviel Punkte es gibt für das töten dieser Einheit
        protected Pen       M_pens;
        protected Brush     M_brush;
        protected Size      M_EinheitSize;
        protected Rectangle M_rect;
        protected int       M_Slowdowntime;         //Gibt an wie lange die Einheit gelähmt sein kann
        protected int       M_Geschwindigkeit;
        protected bool      M_SlowdownSet = false;
        protected int       M_SlowDownTemp;
        protected bool      M_Zielerreicht = false;
        protected int       M_LastRounds = 10;
        protected bool      M_LastRound = false;
        protected Einstellungen M_Einstellungen;
        private int M_EinheitLevel;
             

        #endregion



        public Einheit(ArrayList Route,int Level,int Lebensenergie, Einstellungen Einst)
        {
            M_Einstellungen = Einst;
            M_brush = Brushes.Blue;
            M_pens = Pens.GreenYellow;
            M_EinheitSize.Height = 10;
            M_EinheitSize.Width = 10;
            M_Lebensenergie = Lebensenergie;//Lebensenergie;
            M_StartLebensenergie = M_Lebensenergie;
            M_MoneyForKill = 1;
            M_PointsForKill = 50;
            M_Strecke = 10;
            M_Geschwindigkeit = 10;
            M_Slowdowntime = 30;
            M_SlowDownTemp = 30;
            M_NextPointToRun = 0;
            M_Paint = false;
            M_Platz = Platz;
            M_Vorgänger = Vorgänger;
            M_Route = Route;
            M_Position = ((Point)M_Route[0]);
            M_MiddlePosition = new Point(M_Position.X + 5, M_Position.Y + 5);
            M_NextPointToRun++;//den ersten punkt der Route ansteuern
            Multiplikat(Level);
            M_EinheitLevel = Level;
        }

        public void Multiplikat(int Mul)
        {
            try
            {
               
                    if (Mul != 0)
                    {
                        M_Lebensenergie += ((10 * M_Lebensenergie) / 100);
                    }
                    if (Mul != 0)
                    {
                        M_MoneyForKill = Mul;
                        int Ganz = Mul % M_Einstellungen.MultiplikatSchritte;

                        if (Ganz == 0)
                        {
                            M_MoneyForKill *= M_Einstellungen.MultiplikatMoney;

                            int Prozent = Convert.ToInt16((double)M_Lebensenergie * M_Einstellungen.MultiplikatLebensenergie);//((100 * M_Lebensenergie) / 100);
                            M_Lebensenergie = Prozent;
                        }
                    }
                
                if (M_Lebensenergie < 0) 
                {
                    
                    
                        M_Lebensenergie = M_StartLebensenergie;
                        
                    
                }
            }
            catch 
            {
                M_Lebensenergie = M_StartLebensenergie;
                
            }
            
            
            
        }

        public void ResetSlowDown()
        {
            //Lähmung aufheben
            M_Strecke = M_Geschwindigkeit;
        }

        public void SetSlowdown(int SlowDown)
        {
            //Einheit verlangsamen
            if (M_SlowdownSet == false)
            {
                if (M_Slowdowntime == M_SlowDownTemp)
                {

                    M_Strecke = M_Strecke - SlowDown;
                    if (M_Strecke < 1)
                    {
                        M_Strecke = 1;
                    }
                }
                M_SlowdownSet = true;
            }

        }


        public int StreckeProTick
        {
            get
            {
                return M_Strecke;
            }
            set
            {

            }
        }

        public int Lebensenergie
        {
            get
            {
                return M_Lebensenergie;
            }
            set
            {
                M_Lebensenergie = value;
            }
        }
        public int StartLebensenergie
        {
            get
            {
                return M_StartLebensenergie;
            }
        }
        public int EinheitLevel
        {
            get
            {
                return M_EinheitLevel;
            }
        }

        /// <summary>
        /// Diese Methode prüft ob die Einheit den richtigen platzt in der welle hat
        /// </summary>
        public void CheckPosition()
        {
            if (M_Vorgänger != null)
            {
                if (M_Vorgänger.M_NextPointToRun < M_NextPointToRun)
                {
                    PlatzTausch();
                }
                if (M_Vorgänger.M_NextPointToRun == M_NextPointToRun)
                {
                    int DifferenzVorX = ((Point)M_Route[M_NextPointToRun]).X - M_Vorgänger.Position.X;
                    int DifferenzVorY = ((Point)M_Route[M_NextPointToRun]).Y - M_Vorgänger.Position.Y;
                    int DifferenzX = ((Point)M_Route[M_NextPointToRun]).X - M_Position.X;
                    int DifferenzY = ((Point)M_Route[M_NextPointToRun]).Y - M_Position.Y;

                    if (DifferenzVorX < 0)
                    {
                        DifferenzVorX *= -1;
                    }
                    if (DifferenzVorY < 0)
                    {
                        DifferenzVorY *= -1;
                    }
                    if (DifferenzX < 0)
                    {
                        DifferenzX *= -1;
                    }
                    if (DifferenzX < 0)
                    {
                        DifferenzY *= -1;
                    }

                    if (DifferenzVorX > DifferenzX)
                    {
                        PlatzTausch();
                    }

                    if (DifferenzVorY > DifferenzY)
                    {
                        PlatzTausch();
                    }
                }
            }
        }

        /// <summary>
        /// Diese Methode tauscht den Platz der aktuellen Einheit mit seinen Vorgänger
        /// </summary>
        public void PlatzTausch()
        {
            int Temp = M_Vorgänger.Platz;
            M_Vorgänger.Platz = M_Platz;            //Platztausch
            M_Platz = Temp;
            M_Vorgänger = M_Vorgänger.M_Vorgänger;  //Sein Vorgänger ist mein Vorgänger
            M_Vorgänger = this;                     //Vorgänger von ihm bin ich
        }

        public Einheit Vorgänger
        {
            get
            {
                return M_Vorgänger;
            }
            set
            {
                M_Vorgänger = value;
            }

        }

        public int Platz
        {
            get
            {
                return M_Platz;
            }
            set
            {
                M_Platz = value;
            }
        }

        public void OnPaint(Graphics graph)
        {
            CheckPosition();
            if (M_Lebensenergie > 0)
            {

                if (M_NextPointToRun < M_Route.Count)
                {
                    int DifferenzX = ((Point)M_Route[M_NextPointToRun]).X - M_Position.X;
                    int DifferenzY = ((Point)M_Route[M_NextPointToRun]).Y - M_Position.Y;

                    int tmpDiffX = DifferenzX;
                    int tmpDiffY = DifferenzY;
                    if (tmpDiffX < 0)
                    {
                        tmpDiffX = tmpDiffX * (-1);
                    }
                    if (tmpDiffY < 0)
                    {
                        tmpDiffY = tmpDiffY * (-1);
                    }
                    int StreckeY = M_Strecke;
                    int StreckeX = M_Strecke;
                    if (tmpDiffX != 0 && tmpDiffY != 0)
                    {
                        //11.04:Sebastian Hier Problem wenn durch 0 geteilt wird
                        StreckeY = Convert.ToInt32(((double)M_Strecke / (double)(tmpDiffX + tmpDiffY)) * tmpDiffY);
                        StreckeX = Convert.ToInt32(((double)M_Strecke / (double)(tmpDiffX + tmpDiffY)) * tmpDiffX);
                    }

                    if (DifferenzX < 0)//ist die Differnz im negativem Bereich?
                    {
                        //Wenn ja muss die Strecke abgezogen werden

                        if (!(DifferenzX * (-1) <= StreckeX))//Kann die volle Strecke Abgezogen werden?
                        {
                            M_Position.X -= StreckeX;
                        }
                        else
                        {
                            //Wenn nein dann der Rest der noch übrig bleibt
                            M_Position.X -= (DifferenzX * -(1));
                            M_NoX = true;//wenn true dann konnte nicht die volle Strecke bewältigt werden
                        }
                    }
                    else
                    {
                        //Wenn nein dann mus die strecke draufgerechnet werden
                        if (!(DifferenzX <= StreckeX))
                        {
                            M_Position.X += StreckeX;
                        }
                        else
                        {
                            M_Position.X += DifferenzX;
                            M_NoX = true;
                        }
                    }

                    //Siehe oben die Vorgehensweise ist die Selbe nur auf den Y Bereich
                    if (DifferenzY < 0)
                    {
                        if (!(DifferenzY * (-1) <= StreckeY))
                        {

                            M_Position.Y -= StreckeY;
                        }
                        else
                        {
                            M_Position.Y -= (DifferenzY * (-1));
                            M_NoY = true;
                        }
                    }
                    else
                    {
                        if (!(DifferenzY <= StreckeY))
                        {
                            M_Position.Y += StreckeY;
                        }
                        else
                        {
                            M_Position.Y += DifferenzY;
                            M_NoY = true;
                        }

                    }

                    //Einheit zeichnen
                    int LWidth = 10;
                    if (M_Lebensenergie<=M_StartLebensenergie)
                        LWidth = Convert.ToInt16(((double)M_EinheitSize.Width/(double)M_StartLebensenergie) * (double)M_Lebensenergie);
                    Size LSize = new Size (LWidth, 2);
                    Point LPosition = new Point(M_Position.X, M_Position.Y-2);
                    Rectangle Lebensenergie = new Rectangle(LPosition, LSize);
                    graph.FillRectangle(M_pens.Brush, Lebensenergie);
                    M_rect = new Rectangle(M_Position, M_EinheitSize);
                    graph.FillEllipse(M_brush, M_rect);
                    M_rect = new Rectangle(M_Position.X + ((M_EinheitSize.Width - 2) / 2), M_Position.Y + ((M_EinheitSize.Height - 2) / 2), 2, 2);
                    graph.DrawEllipse(M_pens, M_rect);

                    if (M_SlowdownSet)
                    {
                        M_Slowdowntime--;
                    }
                    if (M_Slowdowntime <= 0)
                    {
                        M_Slowdowntime = M_SlowDownTemp;
                        M_SlowdownSet = false;
                        M_Strecke = M_Geschwindigkeit;
                    }
                    //Wenn in beiden Bereichen nicht die volle Strecke abgezogen bzw.draufgerechnet
                    //werden konnte dann wurde der angegebene Routenpunkt erreicht
                    if (M_NoY == true && M_NoX == true)
                    {
                        if (M_NextPointToRun<M_Route.Count-1)
                        {
                            M_NextPointToRun++;//nächsten Routenpunkt ansteuern
                        }
                        else
                        {
                            M_Zielerreicht = true;
                        }
                        M_NoX = false;
                        M_NoY = false;
                    }

                }
                M_MiddlePosition = new Point(M_Position.X + 5, M_Position.Y + 5);
            }
        }

        public int PointsforKill 
        {
            get 
            {
                return M_PointsForKill;
            }
        }

        public int MoneyforKill 
        {
            get 
            {
                return M_MoneyForKill;
            }
        }


        public bool Zielerreicht 
        {
            get 
            {
                return M_Zielerreicht;
            }
        }

        public Point Position
        {
            get
            {
                return M_Position;
            }
            set
            {
                M_Position = value;
            }

        }
        public Point MiddlePosition
        {
            get
            {
                return M_MiddlePosition;
            }
            set
            {
                M_MiddlePosition = value;
            }

        }
    }
}
