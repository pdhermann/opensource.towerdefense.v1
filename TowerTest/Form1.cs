using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml.Serialization;
using M=System.Media;
using System.IO;

namespace TowerTest
{
    public partial class Form1 : Form
    {
        int turm;
        int AktuellesLevel=0;
        private ArrayList a;
        public ArrayList geschoss;
        public ArrayList Einheiten;
        public ArrayList M_PressedButtons;
        bool painttower = false;
        bool EditorFirstStart = true;
        bool Mouserup = false;
        Spielfeld M_Spielfeld;
        Point Mousepostion;
        Turm M_SelectedTurm;
        int M_IndexofSelectedTurm;
        bool Pause = false;
        Einstellungen M_Einstellungen;
        Random r;
        M.SoundPlayer sound;
        string[] Sounds;
        public ArrayList M_EditorRoute;
        public Point OnEditRoute;
        bool Edit = false;
        Highscore tmpHighscore;
        Highscore.ListofHighscore tmpHighscoreList;
        


        public Form1()
        {
            InitializeComponent();
            M_Einstellungen = new Einstellungen();
            rb_Middle.Checked = true;
            a = new ArrayList();
            geschoss = new ArrayList();
            Einheiten = new ArrayList();
            M_PressedButtons = new ArrayList();
            sound = new System.Media.SoundPlayer();
            LoadSound();
            tmpHighscore = new Highscore();
        }
        public void LoadSound() 
        {
            Sounds=Directory.GetFiles(Application.StartupPath+"\\Sound");

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            ts_Time.Text = DateTime.Now.ToShortTimeString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
            label7.Text = M_Spielfeld.Lives.ToString();
            label4.Text = M_Spielfeld.Money.ToString();
            label5.Text = M_Spielfeld.Highscore.ToString();
            label9.Text = M_Spielfeld.Level.ToString();
            if (M_Spielfeld.Einheiten.Count > 0)
            {
                if (((Einheit)M_Spielfeld.Einheiten[M_Spielfeld.Einheiten.Count - 1]).EinheitLevel + 1 > AktuellesLevel)
                {
                    AktuellesLevel = ((Einheit)M_Spielfeld.Einheiten[M_Spielfeld.Einheiten.Count - 1]).EinheitLevel + 1;
                    lab_Aktuell.Text = ((Einheit)M_Spielfeld.Einheiten[M_Spielfeld.Einheiten.Count - 1]).StartLebensenergie.ToString() + " (" + Convert.ToString(AktuellesLevel) + ")";
                }
            }
        }

        private void p_test_Paint(object sender, PaintEventArgs e)
        {
            if (M_Spielfeld.Status == "Gewonnen" || M_Spielfeld.Status == "Verloren")
            {
                Highscore highs = null;
                Highscore.ListofHighscore listh= null;
                if (M_Spielfeld.Status == "Gewonnen")
                {
                    switch (M_Einstellungen.Typ())
                    {
                        case Highscore.Schwierigkeitsgrad.Einfach:
                            highs = new Highscore(M_Spielfeld.Highscore, M_Spielfeld.Startzeit.CompareTo(DateTime.Now), Highscore.Schwierigkeitsgrad.Einfach, true, M_Spielfeld.Money);
                            listh = new Highscore.Einfach();
                            break;
                        case Highscore.Schwierigkeitsgrad.Mittel:
                            highs = new Highscore(M_Spielfeld.Highscore, M_Spielfeld.Startzeit.CompareTo(DateTime.Now), Highscore.Schwierigkeitsgrad.Mittel, true, M_Spielfeld.Money);
                            listh = new Highscore.Mittel();
                            break;
                        case Highscore.Schwierigkeitsgrad.Schwer:
                            highs = new Highscore(M_Spielfeld.Highscore, M_Spielfeld.Startzeit.CompareTo(DateTime.Now), Highscore.Schwierigkeitsgrad.Schwer, true, M_Spielfeld.Money);
                            listh = new Highscore.Schwer();
                            break;
                    }
                }
                else
                {
                    switch (M_Einstellungen.Typ())
                    {
                        case Highscore.Schwierigkeitsgrad.Einfach:
                            highs = new Highscore(M_Spielfeld.Highscore, M_Spielfeld.Startzeit.CompareTo(DateTime.Now), Highscore.Schwierigkeitsgrad.Einfach, false, M_Spielfeld.Money);
                            listh = new Highscore.Einfach();
                            break;
                        case Highscore.Schwierigkeitsgrad.Mittel:
                            highs = new Highscore(M_Spielfeld.Highscore, M_Spielfeld.Startzeit.CompareTo(DateTime.Now), Highscore.Schwierigkeitsgrad.Mittel, false, M_Spielfeld.Money);
                            listh = new Highscore.Mittel();
                            break;
                        case Highscore.Schwierigkeitsgrad.Schwer:
                            highs = new Highscore(M_Spielfeld.Highscore, M_Spielfeld.Startzeit.CompareTo(DateTime.Now), Highscore.Schwierigkeitsgrad.Schwer, false, M_Spielfeld.Money);
                            listh = new Highscore.Schwer();
                            break;
                    }
                }
                if (listh.CheckIfHighscore(highs.Punkte))
                {

                    gb_Menu.Visible = false;
                    gb_Spielfeld.Visible = false;
                    gb_Editor.Visible = false;
                    gb_Highscore.Visible = true;
                    EditorFirstStart = false;

                    lab_Highscore_Status.Text = "Ihre Punktezahl ist " + highs.Punkte.ToString() + "! Damit k?nnen Sie sich in die Highscoreliste eintragen!";
                    tb_Highscore_Name.Text = "";
                    gb_Eintragen.Visible = true;
                    tmpHighscore = highs;
                    tmpHighscoreList = listh;
                    HighscoreListeaktuallisieren();
                }
                else
                {

                    gb_Menu.Visible = false;
                    gb_Spielfeld.Visible = false;
                    gb_Editor.Visible = false;
                    gb_Highscore.Visible = true;
                    EditorFirstStart = false;
                    gb_Eintragen.Visible = false;
                    lab_Highscore_Status.Text = "Ihre Punktezahl ist " + highs.Punkte.ToString() + "! Leider haben Sie die Highscore nicht erreicht!";
                    tb_Highscore_Name.Text = "";
                    HighscoreListeaktuallisieren();
                }
                timer1.Stop();
                timer1.Enabled = false;
                p_test.Enabled = false;
            }

            if (M_Spielfeld.Level == 200)
            {
                Startwave.Enabled = false;
            }

            M_Spielfeld.OnPaint(e.Graphics);
            if(M_Spielfeld.Status!=string.Empty)
            {
                LAB_Level.Text = M_Spielfeld.Status;
            }
            if (M_SelectedTurm != null)
            {
                LAB_Reichweite.Text = M_SelectedTurm.Reichweite.Height.ToString();
                LAB_Feuerrate.Text = M_SelectedTurm.Feuerrate.ToString();
                LAB_Einheiten.Text = M_SelectedTurm.MaxEinheitToShoot.ToString();
                LAB_UpgradeKosten.Text = M_SelectedTurm.UpgradeKosten.ToString();
                LAB_Slowdown.Text = M_SelectedTurm.Geschoss.Slowdown.ToString();
                LAB_Schaden.Text = M_SelectedTurm.Geschoss.Schaden.ToString();
                LAB_MaxLevel.Text = M_SelectedTurm.MaxLevel.ToString();
                LAB_Level.Text = M_SelectedTurm.Level.ToString();
                LAB_Turmname.Text = M_SelectedTurm.Kosten.ToString();
            }
            else
            {
                LAB_Reichweite.Text = string.Empty;
                LAB_Feuerrate.Text = string.Empty;
                LAB_Einheiten.Text = string.Empty;
                LAB_UpgradeKosten.Text = string.Empty;
                LAB_Turmname.Text = string.Empty;
                LAB_Schaden.Text = string.Empty;
                LAB_Slowdown.Text = string.Empty;
                LAB_Schaden.Text = string.Empty;
                LAB_MaxLevel.Text = string.Empty;
                LAB_Level.Text = string.Empty;
                LAB_Turmname.Text = string.Empty; 
            }
            if (Mouserup == true ) 
            {
                if (painttower == true)
                {
                    Rectangle rect = new Rectangle(Mousepostion, new Size(20, 20));
                    Turm tmpt = null;
                    switch (turm)
                    {
                        case 1: tmpt = new LaserTower1(); break;
                        case 2: tmpt = new LaserTower2(); break;
                        case 3: tmpt = new LaserTower3(); break;
                        case 4: tmpt = new UltimateLaserTower(); break;
                        case 5: tmpt = new SlowTower1(); break;
                        case 6: tmpt = new SlowTower2(); break;
                        case 7: tmpt = new SlowTower3(); break;
                        case 8: tmpt = new UltimateSlowTower(); break;
                        case 9: tmpt = new ProjetileTower1(); break;
                        case 10: tmpt = new ProjetileTower2(); break;
                        case 11: tmpt = new ProjetileTower3(); break;
                        case 12: tmpt = new UltimateProjektileTower1(); break;
                    }
                    if (TurmClick() == null && MouseoverPfad(rect) == false && tmpt.Kosten <= M_Spielfeld.Money)
                    {

                        e.Graphics.DrawRectangle(new Pen(Color.FromArgb(90, 0, 255, 0)), rect);
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 0, 255, 0)), rect);
                    }
                    else
                    {

                        e.Graphics.DrawRectangle(new Pen(Color.FromArgb(90, 255, 0, 0)), rect);
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 255, 0, 0)), rect);
                    }
                }
            }
            if (M_SelectedTurm != null) 
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(90, 255, 0, 0)),M_SelectedTurm.Reichweite);
            }

           
        }
        public bool MouseoverPfad(Rectangle rect) 
        {
            bool Over = false;
            for(int i=0;i<M_Spielfeld.Route.Count;i++)
            {
                if(rect.Contains(((Point)M_Spielfeld.Route[i])))
                {
                    Over=true;
                }
            }
            return Over;

        }
        public void PlaySound() 
        {
            if (cb_Sound.Checked)
            {
                r = new Random();
                int i = r.Next(Sounds.Length - 1);
                sound.SoundLocation = Sounds[i];
                sound.Play();
            }
        }


        public Turm TurmClick() 
        {
            Turm SelectedTurm=null;
            for (int i = 0;i< M_Spielfeld.T?rme.Count; i++) 
            {
                if (((Turm)M_Spielfeld.T?rme[i]).Rechteck.Contains(Mousepostion)) 
                {
                    SelectedTurm = ((Turm)M_Spielfeld.T?rme[i]);
                    M_IndexofSelectedTurm = i;
                    PlaySound();
                }
            }
            return SelectedTurm;
        }


        private void p_test_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                painttower = false;
                M_SelectedTurm = null;
                LAB_Reichweite.Text = string.Empty;
                LAB_Feuerrate.Text = string.Empty;
                LAB_Einheiten.Text = string.Empty;
                LAB_UpgradeKosten.Text = string.Empty;
                LAB_Turmname.Text = string.Empty;
                LAB_Schaden.Text = string.Empty;
                LAB_Slowdown.Text = string.Empty;
                LAB_MaxLevel.Text=string.Empty;
                LAB_Level.Text = string.Empty;
                LAB_Turmname.Text = string.Empty;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (painttower == true)
                {
                    BuildTower();

               
               
                }
                else
                {
                    M_SelectedTurm = TurmClick();
                    
                }

            }
            
        }
        public void SellTower() 
        {
            if (M_SelectedTurm!=null) 
            {
                M_Spielfeld.Money+= M_SelectedTurm.Kosten / 2;
                M_Spielfeld.T?rme.RemoveAt(M_IndexofSelectedTurm);
                M_IndexofSelectedTurm = -1;
                M_SelectedTurm = null;
            }
        }

        public void BuildTower() 
        {
            Turm a=null;
            switch(turm)
            {
                case 1:  a = new LaserTower1(M_Spielfeld, Mousepostion) ; break;
                case 2: a = new LaserTower2(M_Spielfeld, Mousepostion); break;
                case 3: a = new LaserTower3(M_Spielfeld, Mousepostion); break;
                case 4: a = new UltimateLaserTower(M_Spielfeld, Mousepostion); break;
                case 5: a = new SlowTower1(M_Spielfeld, Mousepostion); break;
                case 6: a = new SlowTower2(M_Spielfeld, Mousepostion); break;
                case 7: a = new SlowTower3(M_Spielfeld, Mousepostion); break;
                case 8: a = new UltimateSlowTower(M_Spielfeld, Mousepostion); break;
                case 9: a = new ProjetileTower1(M_Spielfeld, Mousepostion); break;
                case 10: a = new ProjetileTower2(M_Spielfeld, Mousepostion); break;
                case 11: a = new ProjetileTower3(M_Spielfeld, Mousepostion); break;
                case 12: a = new UltimateProjektileTower1(M_Spielfeld, Mousepostion); break;

            }
            if (M_Spielfeld.Money >= a.Kosten)
            {
                M_Spielfeld.T?rme.Add(a);
                M_Spielfeld.Money -= a.Kosten;
                painttower = false;
            }

            
        }

        private void btn_turm1_Click(object sender, EventArgs e)
        {
            
            turm = 1;
            painttower = true;
        }

        private void btn_turm2_Click(object sender, EventArgs e)
        {
            turm = 2;
            painttower = true;
        }

        private void btn_turm3_Click(object sender, EventArgs e)
        {
            turm = 3;
            painttower = true;
        }


        private void Startwave_Click(object sender, EventArgs e)
        {
            M_Spielfeld.StartWave();
            if (M_Spielfeld.Level == 200)
            {
                Startwave.Enabled = false;
            }
        }

        private void p_test_MouseUp(object sender, MouseEventArgs e)
        {
            Mouserup = true;
        }

        private void p_test_MouseLeave(object sender, EventArgs e)
        {
           
        }
        
        private void p_test_MouseMove(object sender, MouseEventArgs e)
        {
            int X = e.Location.X / 20;
            int Y = e.Location.Y / 20;
            Mousepostion.X = X * 20;    //Rasterschritte 20
            Mousepostion.Y =Y*20; 
            Mouserup = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(M_SelectedTurm!=null)
            {
                M_SelectedTurm.Upgrade();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            turm = 4;
            painttower = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SellTower();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            turm = 5;
            painttower = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            turm = 6;
            painttower = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            turm = 7;
            painttower = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            turm = 8;
            painttower = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            turm = 9;
            painttower = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            turm = 10;
            painttower = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            turm = 11;
            painttower = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            turm = 12;
            painttower = true;
        }

        private void btn_turm2_MouseHover(object sender, EventArgs e)
        {
            M_SelectedTurm =new LaserTower2();
            
        }

        private void btn_turm1_MouseHover(object sender, EventArgs e)
        {
            M_SelectedTurm = new LaserTower1();
        }

        private void btn_turm3_MouseHover(object sender, EventArgs e)
        {
            M_SelectedTurm = new LaserTower3();
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            M_SelectedTurm = new ProjetileTower1();
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            M_SelectedTurm = new ProjetileTower2();
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            M_SelectedTurm = new ProjetileTower3();
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            M_SelectedTurm = new SlowTower1();
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            M_SelectedTurm = new SlowTower2();
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            M_SelectedTurm = new SlowTower3();
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            M_SelectedTurm = new UltimateLaserTower();
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            M_SelectedTurm = new UltimateProjektileTower1();
        }

        private void button10_MouseHover(object sender, EventArgs e)
        {
            M_SelectedTurm = new UltimateSlowTower();
        }

        private void btn_turm1_MouseLeave(object sender, EventArgs e)
        {
            M_SelectedTurm = null;
        }

        private void btn_turm2_MouseLeave(object sender, EventArgs e)
        {
            M_SelectedTurm = null;
        }

        private void btn_turm3_MouseLeave(object sender, EventArgs e)
        {
            M_SelectedTurm = null;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            M_SelectedTurm = null;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            M_SelectedTurm = null;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            M_SelectedTurm = null;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            M_SelectedTurm = null;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            M_SelectedTurm = null;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            M_SelectedTurm = null;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            M_SelectedTurm = null;
        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {
            M_SelectedTurm = null;
        }

        private void button10_MouseLeave(object sender, EventArgs e)
        {
            M_SelectedTurm = null;
        }

        private void btn_Pause_Click(object sender, EventArgs e)
        {
            if (Pause)
            {
                Pause = false;
                btn_Pause.Text = "Pause";
                p_test.Enabled = true;
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                Pause = true;
                btn_Pause.Text = "Fortsetzen";
                p_test.Enabled = false;
                timer1.Stop();
                timer1.Enabled = false;
            }
        }

        private void btn_Starten_Click(object sender, EventArgs e)
        {
            M_Spielfeld = new Spielfeld(M_Einstellungen);
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(Point) });
                FileStream LoadRoute = new FileStream(cb_Routenwahl.SelectedItem.ToString(), FileMode.Open, FileAccess.Read);
                ArrayList tmpRoute = ser.Deserialize(LoadRoute) as ArrayList;
                M_Spielfeld.Route = tmpRoute;
            }
            catch
            {
            }
            AktuellesLevel = 0;
            lab_Aktuell.Text= "";
            M_Spielfeld.Startzeit = DateTime.Now;
            timer1.Enabled = true;
            timer1.Start();
            gb_Menu.Visible = false;
            gb_Spielfeld.Visible = true;
            gb_Editor.Visible = false;
            gb_Highscore.Visible = false;
            EditorFirstStart = false;
        }

        private void hauptmen?ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
            HauptFensterAktualisieren();
            gb_Menu.Visible = true;
            gb_Spielfeld.Visible = false;
            gb_Editor.Visible = false;
            gb_Highscore.Visible = false;
            EditorFirstStart = false;
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void gb_Menu_Enter(object sender, EventArgs e)
        {
            HauptFensterAktualisieren();
        }

        private void HauptFensterAktualisieren()
        {
            lv_Background.BeginUpdate();
            if (lv_Background.Items.Count < 1)
            {
                for (int i = 0; i < il_Hintergruende.Images.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem("Hintergrund " + Convert.ToString(i + 1), i);
                    lv_Background.Items.Add(lvi);
                }
            }
            lv_Background.EndUpdate();
            try
            {
                cb_Routenwahl.BeginUpdate();
                cb_Routenwahl.Items.Clear();
                string[] Routen = Directory.GetFiles(Application.StartupPath + "\\Route\\", "*.rte");
                for (int i = 0; i < Routen.Length; i++)
                {
                    cb_Routenwahl.Items.Add(Routen[i]);
                }
                cb_Routenwahl.SelectedIndex = 0;
                cb_Routenwahl.EndUpdate();
            }
            catch
            {
                cb_Routenwahl.EndUpdate();
                //MessageBox.Show("Keine Routen gefunden!", "TowerDefense");
            }
        }

        private void lv_Background_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_Background.SelectedIndices.Count > 0)
            {
                p_test.BackgroundImage = il_Hintergruende.Images[lv_Background.SelectedIndices[0]];
                p_test.BackgroundImageLayout = ImageLayout.Tile;
            }
        }

        #region Editor

        private void editorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gb_Menu.Visible = false;
            gb_Spielfeld.Visible = false;
            gb_Editor.Visible = true;
            gb_Highscore.Visible = false;
            EditorFirstStart = true;
        }

        private void p_Editor_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Edit = false;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (Edit)
                    M_EditorRoute.Add(e.Location);
            }
        }

        private void p_Editor_Paint(object sender, PaintEventArgs e)
        {
            if (Edit)
            {
                if (M_EditorRoute.Count > 0)
                {
                    Point[] points = new Point[M_EditorRoute.Count+1];

                    for (int i = 0; i < M_EditorRoute.Count; i++)
                    {
                        points[i] = ((Point)M_EditorRoute[i]);
                    }
                    points[points.Length-1] = OnEditRoute;
                    e.Graphics.DrawLines(new Pen(Color.Black, 40), points);//Route zeichenen
                }
            }
            else
            {
                if (M_EditorRoute.Count > 1)
                {
                    Point[] points = new Point[M_EditorRoute.Count];

                    for (int i = 0; i < M_EditorRoute.Count; i++)
                    {
                        points[i] = ((Point)M_EditorRoute[i]);
                    }

                    e.Graphics.DrawLines(new Pen(Color.Black, 40), points);//Route zeichenen
                }
            }
            
        }

        private void gb_Editor_Paint(object sender, PaintEventArgs e)
        {
            if (EditorFirstStart)
            {
                EditorFirstStart = false;
                M_EditorRoute = new ArrayList();
            }
        }

        #endregion

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 's')
            {
                if (M_Spielfeld.Level<200)
                    M_Spielfeld.StartWave();
            }
            if (e.KeyChar == 'x' || e.KeyChar == 'u')
            {
                if (M_SelectedTurm != null)
                {
                    M_SelectedTurm.Upgrade();
                }
            }
            if (e.KeyChar == 'v')
            {
                if (M_SelectedTurm != null)
                {
                    SellTower();
                }
            }
            M_PressedButtons.Add(e.KeyChar);

            if (M_SelectedTurm is UltimateLaserTower)
            {
                if (M_PressedButtons.Count > 7)
                {
                    int c = M_PressedButtons.Count;
                    if ((char)M_PressedButtons[c - 1] == 'd' && (char)M_PressedButtons[c - 2] == 'o' && (char)M_PressedButtons[c - 3] == 'g' && (char)M_PressedButtons[c - 4] == 'r' && (char)M_PressedButtons[c - 5] == 'e' && (char)M_PressedButtons[c - 6] == 'w' && (char)M_PressedButtons[c - 7] == 'o' && (char)M_PressedButtons[c - 8] == 't')
                    {
                        M_SelectedTurm.MaxEinheitToShoot = 60;
                        M_SelectedTurm.SchadenProGeschoss = 99999999;//geht nicht?
                        M_SelectedTurm.Reichweite = p_test.ClientRectangle;
                    }
                }
            }
            if (M_PressedButtons.Count > 4)
            {
                int c = M_PressedButtons.Count;
                if ((char)M_PressedButtons[c - 1] == 'm' && (char)M_PressedButtons[c - 2] == 'o' && (char)M_PressedButtons[c - 3] == 'n' && (char)M_PressedButtons[c - 4] == 'e' && (char)M_PressedButtons[c - 5] == 'y')
                {
                    M_Spielfeld.Money += 20000;
                }
            }
        }

        private void rb_Middle_CheckedChanged(object sender, EventArgs e)
        {
            gb_Individuell.Visible = false;
            if (rb_Easy.Checked)
            {
                M_Einstellungen.EinstellungenLaden("Einfach");
                tb_Settings_Lebensenergie.Text = M_Einstellungen.MultiplikatLebensenergie.ToString();
                tb_Settings_Money.Text = M_Einstellungen.StartMoney.ToString();
                tb_Settings_MoneyMulti.Text = M_Einstellungen.MultiplikatMoney.ToString();
                tb_Settings_Schritte.Text = M_Einstellungen.MultiplikatSchritte.ToString();
            }
            if (rb_Middle.Checked)
            {
                M_Einstellungen.EinstellungenLaden("Mittel");
                tb_Settings_Lebensenergie.Text = M_Einstellungen.MultiplikatLebensenergie.ToString();
                tb_Settings_Money.Text = M_Einstellungen.StartMoney.ToString();
                tb_Settings_MoneyMulti.Text = M_Einstellungen.MultiplikatMoney.ToString();
                tb_Settings_Schritte.Text = M_Einstellungen.MultiplikatSchritte.ToString();
            }
            if (rb_Hard.Checked)
            {
                M_Einstellungen.EinstellungenLaden("Schwer");
                tb_Settings_Lebensenergie.Text = M_Einstellungen.MultiplikatLebensenergie.ToString();
                tb_Settings_Money.Text = M_Einstellungen.StartMoney.ToString();
                tb_Settings_MoneyMulti.Text = M_Einstellungen.MultiplikatMoney.ToString();
                tb_Settings_Schritte.Text = M_Einstellungen.MultiplikatSchritte.ToString();
            }
            if (rb_CustomizeSettings.Checked)
            {
                btn_SaveToFile.Enabled = false;
                btn_Uebernehmen.Enabled = false;
                gb_Individuell.Visible = true;
            }
            if (rb_FromFile.Checked)
            {
                ofd_openSettings.InitialDirectory = Application.StartupPath;
                if (ofd_openSettings.ShowDialog() == DialogResult.OK)
                {
                    M_Einstellungen.EinstellungenAusDatei(ofd_openSettings.FileName);
                    tb_Settings_Lebensenergie.Text = M_Einstellungen.MultiplikatLebensenergie.ToString();
                    tb_Settings_Money.Text = M_Einstellungen.StartMoney.ToString();
                    tb_Settings_MoneyMulti.Text = M_Einstellungen.MultiplikatMoney.ToString();
                    tb_Settings_Schritte.Text = M_Einstellungen.MultiplikatSchritte.ToString();                    
                }

            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Edit = true;
        }

        private void p_Editor_MouseMove(object sender, MouseEventArgs e)
        {
            if (Edit)
            {
                OnEditRoute = e.Location;
                p_Editor.Refresh();
            }
        }

        private void btn_RouteSpeichern_Click(object sender, EventArgs e)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ArrayList), new Type[] {typeof(Point)});
            
            if (!Directory.Exists(Application.StartupPath+"\\Route\\"))
            {
                Directory.CreateDirectory(Application.StartupPath+"\\Route\\");
            }

            sfd_RouteSpeichern.InitialDirectory = Application.StartupPath + "\\Route\\";

            if (sfd_RouteSpeichern.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream writer = new FileStream(sfd_RouteSpeichern.FileName, FileMode.Create, FileAccess.Write);
                try
                {
                    ser.Serialize(writer, M_EditorRoute);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void btn_Uebernehmen_Click(object sender, EventArgs e)
        {
            M_Einstellungen.MultiplikatLebensenergie = Convert.ToDouble(tb_Settings_Lebensenergie.Text);
            M_Einstellungen.MultiplikatMoney = Convert.ToInt16(tb_Settings_MoneyMulti.Text);
            M_Einstellungen.MultiplikatSchritte = Convert.ToInt16(tb_Settings_Schritte.Text);
            M_Einstellungen.StartMoney = Convert.ToInt16(tb_Settings_Money.Text);

            btn_Uebernehmen.Enabled = false;
        }

        private void btn_SaveToFile_Click(object sender, EventArgs e)
        {
            M_Einstellungen.MultiplikatLebensenergie = Convert.ToDouble(tb_Settings_Lebensenergie.Text);
            M_Einstellungen.MultiplikatMoney = Convert.ToInt16(tb_Settings_MoneyMulti.Text);
            M_Einstellungen.MultiplikatSchritte = Convert.ToInt16(tb_Settings_Schritte.Text);
            M_Einstellungen.StartMoney = Convert.ToInt16(tb_Settings_Money.Text);

            sfd_saveSettings.InitialDirectory = Application.StartupPath;
            if (sfd_saveSettings.ShowDialog() == DialogResult.OK)
            {
                M_Einstellungen.EinstellungenSpeichern(sfd_saveSettings.FileName);
            }

            btn_Uebernehmen.Enabled = false;
            btn_SaveToFile.Enabled = false;
        }

        private void tb_Settings_TextChanged(object sender, EventArgs e)
        {
            btn_Uebernehmen.Enabled = true;
            btn_SaveToFile.Enabled = true;
        }

        private void highscoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gb_Menu.Visible = false;
            gb_Spielfeld.Visible = false;
            gb_Editor.Visible = false;
            gb_Highscore.Visible = true;
            gb_Eintragen.Visible = false;
            tb_Highscore_Name.Text = "";
            lab_Highscore_Status.Text = "";
            HighscoreListeaktuallisieren();
            EditorFirstStart = false;
        }

        private void btn_Eintragen_Click(object sender, EventArgs e)
        {
            if (tmpHighscoreList != null)
            {
                if (tb_Highscore_Name.Text != "")
                {
                    tmpHighscoreList.AddValue(tmpHighscore.Punkte, tb_Highscore_Name.Text);

                    switch (M_Einstellungen.Typ())
                    {
                        case Highscore.Schwierigkeitsgrad.Einfach:
                            tmpHighscore.EHighscore = (Highscore.Einfach)tmpHighscoreList;
                            break;
                        case Highscore.Schwierigkeitsgrad.Mittel:
                            tmpHighscore.MHighscore = (Highscore.Mittel)tmpHighscoreList;
                            break;
                        case Highscore.Schwierigkeitsgrad.Schwer:
                            tmpHighscore.SHighscore = (Highscore.Schwer)tmpHighscoreList;
                            break;
                    }

                    gb_Eintragen.Visible = false;
                    HighscoreListeaktuallisieren();
                    tb_Highscore_Name.Text = "";
                    lab_Highscore_Status.Text = "";
                }
                else
                {
                    MessageBox.Show("Tragen Sie bitte einen Namen ein!");
                }
            }
        }
        private void HighscoreListeaktuallisieren()
        {
            tmpHighscore.SerializeHighscore();



            lb_h_Einfach.Items.Clear();
            lb_h_Einfach.Items.AddRange(tmpHighscore.EHighscore.List.ToArray());

            lb_h_mittel.Items.Clear();
            lb_h_mittel.Items.AddRange(tmpHighscore.MHighscore.List.ToArray());

            lb_h_schwer.Items.Clear();
            lb_h_schwer.Items.AddRange(tmpHighscore.SHighscore.List.ToArray());

    
            tmpHighscore.DeSerializeHighscore();
        }
 
    }
}