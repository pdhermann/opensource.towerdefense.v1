using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using a = System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace TowerTest
{
    public class Highscore
    {
        int M_Highscore;
        string M_Path;
        Einfach M_E;
        Mittel M_M;
        Schwer M_S;
        XmlSerializer M_Serializer;
        StreamReader M_Reader;
        StreamWriter M_Writer;

        public enum Schwierigkeitsgrad
        {
            Einfach=1,
            Mittel,
            Schwer
        }

        public Highscore()
        {
            M_E = new Einfach();
            M_M = new Mittel();
            M_S = new Schwer();
            M_Path = Application.StartupPath;
            DeSerializeHighscore();
        }

        public Highscore(int Punkte, int Time,Schwierigkeitsgrad grad, bool geschafft,int Money)
        {
            M_E = new Einfach();
            M_M = new Mittel();
            M_S = new Schwer();
            M_Path = Application.StartupPath;
            int Bonus = 0;
            switch (grad)
            {
                case Schwierigkeitsgrad.Einfach:
                    if (geschafft)
                        Bonus = 2000;
                    M_Highscore = Punkte * (Money / 3) + Bonus - (Time * 100);
                    
                    break;
                case Schwierigkeitsgrad.Mittel:
                    if (geschafft)
                        Bonus = 5000;
                    M_Highscore = Punkte * (Money / 4)+ Bonus - (Time * 150);

                    break;
                case Schwierigkeitsgrad.Schwer:
                    if (geschafft)
                        Bonus = 10000;
                    M_Highscore = Punkte * (Money / 4)+ Bonus - (Time * 200);

                    break;
            }
        }

        public int Punkte
        {
            get
            {
                return M_Highscore;
            }
        }
        public Einfach EHighscore
        {
            get
            {
                return M_E;
            }
            set
            {
                M_E = value;
            }
        }
        public Mittel MHighscore
        {
            get
            {
                return M_M;
            }
            set
            {
                M_M = value;
            }
        }
        public Schwer SHighscore
        {
            get
            {
                return M_S;
            }
            set
            {
                M_S = value;
            }
        }

        private void CheckPath()
        {
            if (!File.Exists(M_Path + M_E.DateiName))
            {
                File.Create(M_Path + M_E.DateiName);
                SerializeHighscore();
            }

            if (!File.Exists(M_Path + M_M.DateiName))
            {
                File.Create(M_Path + M_M.DateiName);
                SerializeHighscore();
            }

            if (!File.Exists(M_Path + M_S.DateiName))
            {
                File.Create(M_Path + M_S.DateiName);
                SerializeHighscore();
            }
        }
        //Speichert die Highscore
        public void SerializeHighscore()
        {
            M_Serializer = new XmlSerializer(typeof(Einfach));
            M_Writer = new StreamWriter(M_Path + M_E.DateiName);
            M_Serializer.Serialize(M_Writer, M_E);
            M_Writer.Close();

            M_Serializer = new XmlSerializer(typeof(Mittel));
            M_Writer = new StreamWriter(M_Path + M_M.DateiName);
            M_Serializer.Serialize(M_Writer, M_M);
            M_Writer.Close();

            M_Serializer = new XmlSerializer(typeof(Schwer));
            M_Writer = new StreamWriter(M_Path + M_S.DateiName);
            M_Serializer.Serialize(M_Writer, M_S);
            M_Writer.Close();
        }
        //Lädt die Highscore
        public void DeSerializeHighscore()
        {
            CheckPath();

            M_Serializer = new XmlSerializer(typeof(Einfach));
            M_Reader = new StreamReader(M_Path + M_E.DateiName);
            object e = M_Serializer.Deserialize(M_Reader);
            M_Reader.Close();
            
            M_Serializer = new XmlSerializer(typeof(Mittel));
            M_Reader = new StreamReader(M_Path + M_M.DateiName);
            object m = M_Serializer.Deserialize(M_Reader);
            M_Reader.Close();

            M_Serializer = new XmlSerializer(typeof(Schwer));
            M_Reader = new StreamReader(M_Path + M_S.DateiName);
            object s = M_Serializer.Deserialize(M_Reader);
            M_Reader.Close();

            EHighscore = (Einfach)e;
            MHighscore = (Mittel)m;
            SHighscore = (Schwer)s;
        }

        [Serializable()]
        public class ListofHighscore
        {
            protected int[] M_Punkte = new int[20];
            protected string[] M_Namen = new string[20];
            protected string M_DateiName;
            protected ArrayList M_List = new ArrayList();
            
            
            public string DateiName
            {
                get
                {
                    return M_DateiName;
                }
            }
            
            public int[] Punkte
            {
                get
                {
                    return M_Punkte;
                }
                set
                {
                    M_Punkte = value;
                }
            }
            public string[] Namen
            {
                get
                {
                    return M_Namen;
                }
                set
                {
                    M_Namen = value;
                }
            }
            public ArrayList List
            {
                get
                {
                    M_List = new ArrayList();
                    if (M_Namen[0] != null)
                        for (int i = 0; i < M_Namen.Length - 1; i++)
                        {
                            if (M_Punkte[i] == 0)
                            {
                                M_List.Add(Convert.ToString(i + 1) + ". ");
                            }
                            else
                            {
                                M_List.Add(Convert.ToString(i + 1) + ". " + M_Namen[i] + ": " + M_Punkte[i].ToString());
                            }
                        }
                    else
                        M_List.Add("Keine Einträge vorhanden!");
                    return M_List;
                }
                set
                {
                    M_List = value;
                }
            }
            public void AddValue(int Punkte, string Name)
            {
                if (CheckIfHighscore(Punkte))
                {
                    string[] newNames = new string[20];
                    int[] newPunkte = new int[20];
                    bool eingtragen = false;
                    for (int i = 0; i < M_Namen.Length-1; i++)
                    {
                        if (M_Punkte[i] < Punkte && eingtragen == false)
                        {
                            newPunkte[i] = Punkte;
                            newNames[i] = Name;
                            eingtragen = true;
                        }
                        else
                        {
                            newPunkte[i] = M_Punkte[i];
                            newNames[i] = M_Namen[i];
                        }
                    }
                    M_Namen = newNames;
                    M_Punkte = newPunkte;
                }
            }
            public bool CheckIfHighscore(int Punkte)
            {
                bool isHighscore = false;

                for (int i = 0; i < M_Punkte.Length; i++)
                {
                    if (Punkte > M_Punkte[i])
                    {
                        isHighscore = true;
                    }
                }

                return isHighscore;
            }
        }

        public class Einfach:ListofHighscore
        {
            public Einfach()
            {
                M_DateiName = "\\einfach.hscr";
            }
        }

        public class Mittel:ListofHighscore
        {
            public Mittel()
            {
                M_DateiName = "\\mittel.hscr";
            }
        }

        public class Schwer:ListofHighscore
        {
            public Schwer()
            {
                M_DateiName = "\\schwer.hscr";
            }
        }
    }
}
