using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;

namespace TowerTest
{
    public class Welle
    {
        protected Spielfeld M_Spielfeld;
        protected ArrayList M_Einheiten;
        protected int       M_LastPaint;
        protected int       M_PaintIntervall;
        protected int       M_Paintcounter;
        protected int       M_Level;
        protected int       M_Lebensenergie=100;
        Einstellungen M_Einstellungen;


        public int Level 
        {
            get 
            {
                return M_Level;
            }
        }

        public ArrayList Einheiten
        {
            get
            {
                return M_Einheiten;
            }
        }
        /// <summary>
        /// Erzeugt eine neue Welle von Einheiten
        /// </summary>
        public void StartNextWave() 
        {
            
            int Temp;
            Temp = M_Lebensenergie;
         

            
            for (int i = 0; i <30; i++)
            {
                Einheit e = new Einheit(M_Spielfeld.Route, M_Level, Temp, M_Einstellungen);    
                
                M_Lebensenergie = e.Lebensenergie;
                M_Einheiten.Add(e);
            }
            
            M_Level++;
        }


        public Welle(Spielfeld spielfeld,int Paintintervall, Einstellungen Einst) 
        {
            M_Einstellungen = Einst;
            M_PaintIntervall = Paintintervall;
            M_Paintcounter = Paintintervall;
            M_LastPaint = 0;
            M_Einheiten = new ArrayList();
            M_Spielfeld = spielfeld;
            M_Level = 0;
            
        }

        /// <summary>
        /// Diese Methode l?st die Einheiten nacheinander auf das Spiefeld los
        /// </summary>
        public void PaintNextEinheit() 
        {

            if (M_LastPaint < M_Einheiten.Count)
            {
                
                if (M_Paintcounter == 0)
                {
                    
                    if (M_Spielfeld.Einheiten.Count == 0)//Noch keine Einheit auf dem Spielfeld 
                    {
                        ((Einheit)M_Einheiten[M_LastPaint]).Platz = 0;//also 0 platz in der Welle
                        ((Einheit)M_Einheiten[M_LastPaint]).Vorg?nger = null;//und kein Vorg?nger vorhanden
                        M_Spielfeld.Einheiten.Add(M_Einheiten[M_LastPaint]);//Einheit dem Spiefeld hinzuf?gen 
                    }
                    else
                    {
                        ((Einheit)M_Einheiten[M_LastPaint]).Platz =  M_Spielfeld.Einheiten.Count ;
                        ((Einheit)M_Einheiten[M_LastPaint]).Vorg?nger = ((Einheit)M_Spielfeld.Einheiten[M_Spielfeld.Einheiten.Count - 1]);
                        M_Spielfeld.Einheiten.Add(M_Einheiten[M_LastPaint]);
                        
                    }
                    M_Paintcounter = M_PaintIntervall;
                    M_LastPaint++;
                    
                }
               
                M_Paintcounter--;
            }
            if (M_LastPaint == M_Einheiten.Count) 
            {
                M_Einheiten.Clear();
                M_LastPaint = 0;
            }
        }
    }
}
