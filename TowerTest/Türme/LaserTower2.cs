using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace TowerTest
{
    public class LaserTower2:Turm
    {
     public  LaserTower2(Spielfeld spiefeld ,Point Position) 
        {
            M_Spielfeld = spiefeld;
            M_Position=Position;
            InitTower();
            MiddleTower_Fire = new Point(M_Position.X + 10, M_Position.Y + 10);
            MiddleTower_Pos = new Point(M_Position.X + 7, M_Position.Y + 7);
           
            M_Pen = new Pen(Color.RosyBrown);
            
            M_rect = new Rectangle(M_Position, new Size(20, 20));
            
            M_Reichweite = new Rectangle(MiddleTower_Fire.X - M_IReichweite, MiddleTower_Fire.Y - M_IReichweite, M_IReichweite * 2, M_IReichweite * 2);
            
            
        }
        public void InitTower() 
        {
            M_Feuerrate = 1;
            M_Kosten = 800;
            M_Level = 1;
            M_MaxEinheitToShoot = 1;
            M_MaxLevel = 7;
            M_UpgradeKosten = 400;
            M_PaintCount = 0;
            M_IReichweite = 150;
            M_Geschosstyp = typeof(Laser.DamageLaser3);
            
        }

        public LaserTower2() 
        {
            InitTower();
        }

        public override void OnPaint(Graphics graphics) 
        {
            
            //graphics.DrawEllipse(M_Pen, M_Reichweite);
            //graphics.DrawRectangle(M_Pen, M_rect);
            //graphics.DrawRectangle(M_Pen, new Rectangle(MiddleTower_Pos, new Size(6, 6)));
            graphics.DrawImage(Image.FromFile(Application.StartupPath + "\\Grafiken\\Laser-Geschoss-Turm-blau.png"), M_Position.X, M_Position.Y, 20, 20);//(M_Pen, new Rectangle(MiddleTower_Pos, new Size(6, 6)));
            if (M_PaintCount == 0)
            {
                M_PaintCount = M_Feuerrate;
                ArrayList EinheitenToShoot = CheckRange();
                Shoot(EinheitenToShoot);
            }
            M_PaintCount--;
        }

        

        public override void Upgrade() 
        {
            if (Level <= MaxLevel)
            {
                if(M_Spielfeld.Money>M_UpgradeKosten)
                {
                    switch (Level)
                    {
                        case 1: M_Spielfeld.Money -= M_UpgradeKosten; M_UpgradeKosten = 400; M_MaxEinheitToShoot = 2; M_Feuerrate = 2; M_IReichweite += 5; break;
                        case 2: M_Spielfeld.Money -= M_UpgradeKosten; M_UpgradeKosten = 400; M_MaxEinheitToShoot = 3; M_Feuerrate = 3; M_IReichweite += 10; break;
                        case 3: M_Spielfeld.Money -= M_UpgradeKosten; M_UpgradeKosten = 400; M_MaxEinheitToShoot = 4; M_Feuerrate = 4; M_IReichweite += 15; break;
                        case 4: M_Spielfeld.Money -= M_UpgradeKosten; M_UpgradeKosten = 400; M_MaxEinheitToShoot = 5; M_Feuerrate = 5; M_IReichweite += 20;  break;
                        case 5: M_Spielfeld.Money -= M_UpgradeKosten; M_UpgradeKosten = 400; M_MaxEinheitToShoot = 6; M_Feuerrate = 6; M_IReichweite += 25; break;
                        case 6: M_Spielfeld.Money -= M_UpgradeKosten; M_UpgradeKosten = 400; M_MaxEinheitToShoot = 7; M_Feuerrate = 7; M_IReichweite += 30; break;
                        case 7: M_Spielfeld.Money -= M_UpgradeKosten; M_UpgradeKosten = 0; M_MaxEinheitToShoot = 8; M_Feuerrate = 8; M_Geschosstyp = typeof(Laser.DamageLaser4); M_IReichweite += 35; break;
                    }
                    RaiseReichweite(M_IReichweite);
                    M_Level++;
                }
            }
            
        }

    }
}
