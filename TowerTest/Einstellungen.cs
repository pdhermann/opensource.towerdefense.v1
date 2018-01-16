using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml.Serialization;
using System.IO;


namespace TowerTest
{
    public class Einstellungen
    {
        int M_Schritte;
        int M_MoneyMulti;
        int M_Money;
        int M_Lives;
        double M_Lebensenergie;
        Highscore.Schwierigkeitsgrad M_Typ;


        public Einstellungen()
        {
            EinstellungenLaden("Mittel");
        }
        /// <summary>
        /// Einstellungen laden
        /// </summary>
        /// <param name="Typ">Einfach, Mittel oder Schwer</param>
        public void EinstellungenLaden(string Typ)
        {
            switch (Typ)
            {
                case "Einfach":
                    MultiplikatSchritte = 12;
                    MultiplikatMoney = 5;
                    MultiplikatLebensenergie = 1.4;
                    StartMoney = 800;
                    Lives = 90;
                    M_Typ = Highscore.Schwierigkeitsgrad.Einfach;
                    break;
                default:
                case "Mittel":
                    MultiplikatSchritte = 14;
                    MultiplikatMoney = 4;
                    MultiplikatLebensenergie = 1.6;
                    StartMoney = 500;
                    Lives = 60;
                    M_Typ = Highscore.Schwierigkeitsgrad.Mittel;
                    break;
                case "Schwer":
                    MultiplikatSchritte = 17;
                    MultiplikatMoney = 4;
                    MultiplikatLebensenergie = 1.8;
                    StartMoney = 300;
                    Lives = 30;
                    M_Typ = Highscore.Schwierigkeitsgrad.Schwer;
                    break;
            }
        }

        public void EinstellungenAusDatei(string Pfad)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(int),typeof(double) });
            FileStream LoadSettings = new FileStream(Pfad, FileMode.Open, FileAccess.Read);
            ArrayList Einstellungen = ser.Deserialize(LoadSettings) as ArrayList;

            MultiplikatSchritte = Convert.ToInt16(Einstellungen[0]);
            MultiplikatMoney = Convert.ToInt16(Einstellungen[1]);
            MultiplikatLebensenergie = Convert.ToDouble(Einstellungen[2]);
            StartMoney = Convert.ToInt16(Einstellungen[3]);
            Lives = Convert.ToInt16(Einstellungen[4]);
        }

        public void EinstellungenSpeichern(string Pfad)
        {
            ArrayList tmpSettings = new ArrayList();
            tmpSettings.Add(MultiplikatSchritte);
            tmpSettings.Add(MultiplikatMoney);
            tmpSettings.Add(MultiplikatLebensenergie);
            tmpSettings.Add(StartMoney);
            tmpSettings.Add(Lives);

            XmlSerializer ser = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(int),typeof(double) });

            FileStream writer = new FileStream(Pfad, FileMode.Create, FileAccess.Write);
            try
            {
                ser.Serialize(writer, tmpSettings);
            }
            catch (Exception ex)
            {

            }
        }

        public int MultiplikatSchritte
        {
            get
            {
                return M_Schritte;
            }
            set
            {
                M_Schritte = value;
            }
        }
        public int MultiplikatMoney
        {
            get
            {
                return M_MoneyMulti;
            }
            set
            {
                M_MoneyMulti= value;
            }
        }
        public double MultiplikatLebensenergie
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
        public int StartMoney
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
        public Highscore.Schwierigkeitsgrad Typ()
        {
                return M_Typ;
        }
    }
}
