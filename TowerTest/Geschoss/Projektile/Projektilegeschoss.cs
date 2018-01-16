using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerTest
{
    public abstract class Projektilegeschoss:Geschoss
    {

        #region Members
        //Diese Variablen werden auf true gesetzt wenn die Strecke nicht mehr abgezogen werden kann
        private bool    M_NoX = false;
        private bool    M_NoY = false;
        protected int   M_PosMitteX;
        protected int   M_PosMitteY;
        protected Image M_Image;
             

        #endregion

        public Projektilegeschoss():base() 
        {

        }

        public Projektilegeschoss(Point Startposition, Einheit Ziel):base(Startposition,Ziel)
        {
            
        }


        
        

        public override void OnPaint(Graphics graphics) 
        {
            
            
            if (M_Getroffen==false&&M_Ziel.Lebensenergie>0 )
            {
                Rectangle rect = new Rectangle(M_Position, M_Grösse);
                graphics.DrawImage(M_Image, M_Position.X, M_Position.Y, 5, 5);//(M_Pen, new Rectangle(MiddleTower_Pos, new Size(6, 6)));
                int DifferenzX = M_Ziel.MiddlePosition.X - M_Position.X;
                int DifferenzY = M_Ziel.MiddlePosition.Y - M_Position.Y;

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
                int StreckeY=M_Strecke;
                int StreckeX = M_Strecke;
                if(tmpDiffX!=0&&tmpDiffY!=0)
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
                }
                
                if(M_NoX==true && M_NoY==true)
                {
                    M_Getroffen = true;
                    SetSchaden();
                    
                }
                if (M_Ziel.Lebensenergie < 0)
                {
                    M_Getroffen = true;
                }
            }
            
            
        }
    }

