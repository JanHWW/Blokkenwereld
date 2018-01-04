using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using System.Net;

namespace WindowsFormsApp1
{
    public partial class Nieuwewereld : Form
    {
        public Nieuwewereld()
        {
            InitializeComponent();
        }


        byte[,,] blk = new byte[0, 0, 0];
        long[] l = new long[4];
        int lb, lh, opv, max, tlr1, tlr2, tlr3, hoogte, vlkgt, wh, gemiddeld;
        double boom, stl, t,stli;
        Random doblstn = new Random();
        private void Nieuwewereld_Load(Object sender, EventArgs e) {
        }
        private void Nieuwewereld_Resize(Object sender, EventArgs e) { }
        private void Nieuw_Click(Object sender, EventArgs e) {
            boom = BM.Value;
            stli = TrackBar1.Value;
            wh = TrackBar2.Value;
            max = TrackBar4.Value;
            opv = TrackBar3.Value;
            t = TrackBar5.Value;
            vlkgt = TrackBar6.Value;
            lb = 500;
            lh = 300;
            Nieuwe_wereld();
        }
        void Hoogste(byte[,,] blokjes, bool sneeuw) {
            l[3] = 0;
            for (tlr1 = 0; tlr1 <= lb; tlr1++) {
                for (tlr2 = 0; tlr2 <= lb; tlr2++)
                {
                    for (tlr3 = lh - 1; tlr3 >= 0; tlr3--)
                    {
                        if (blk[tlr1, tlr2, tlr3] > 0) {
                            if (sneeuw) { blokjes[tlr1, tlr2, tlr3 + 1] = 2; }
                            goto uit;
                        }
                    }
                    uit:
                    if (tlr3 > l[3]) { l[3] = tlr3; }
                }
            } }
        void Nieuwe_wereld() {
            byte onder, boven, gras, water;
            int wzh;
            double temp;
            blk = new byte[lb+1, lb+1, lh+1];
            gemiddeld = (opv + max) / 2;

                        l[1] = lb;
            l[2] = lb;
            l[3] = lh;
            stl = (stli / 1000) * (vlkgt * vlkgt);
            //perlin noise
            Nieuwe_noise();
            wzh = wh + 3;
            onder = 3;
            water = 1;
            for (tlr2 = 0; tlr2 <= lb; tlr2++) {
                for (tlr1 = 0; tlr1 <= lb; tlr1++) {
                    temp = noisekaart[2, tlr1, tlr2];
                    if (temp < 30) { boven = 2; } else if(temp < 40) { boven = 5; }else { boven = 3; }
                    if (temp > 10 && noisekaart[2, tlr1, tlr2] < 40) { gras = 4; } else { gras = boven; }
                    hoogte = noisekaart[1,tlr1, tlr2]-gemiddeld;
                    hoogte = (int)(Convert.ToDouble(gemiddeld) + (Convert.ToDouble(hoogte) *(1- Math.Abs(temp) * 0.02)) );
                    noisekaart[1, tlr1, tlr2]=hoogte ;

                    if (hoogte < 0) { hoogte = 0; } else if (hoogte > lh) { hoogte = lh; }
                    

                    for (tlr3 = 0; tlr3 <= hoogte - 1; tlr3++) {
                        if (tlr3 < wzh) { blk[tlr1, tlr2, tlr3] = onder; } else { blk[tlr1, tlr2, tlr3] = boven; }
                    }
                    tlr3 = hoogte;
                    if (tlr3 < wzh) { blk[tlr1, tlr2, tlr3] = onder; } else { blk[tlr1, tlr2, tlr3] = gras; }
                    for (tlr3 = hoogte + 1; tlr3 <= lh; tlr3++) {
                        if (tlr3 > wh) { blk[tlr1, tlr2, tlr3] = 0; } else {
                            blk[tlr1, tlr2, tlr3] = water; }
                    }
                } }
            //planten


            for (tlr2 = 3; tlr2 <= (lb - 3); tlr2++)
                for (tlr1 = 3; tlr1 <= (lb - 3); tlr1++)
                    if (doblstn.NextDouble() > 1 - (boom * 0.01))
                    {
                        tlr3 = noisekaart[1,tlr1, tlr2 + 1] + 1;
                        if (tlr3 < lh-10)
                        {

                            if (tlr3 > wh)
                            {
                                    if (noisekaart[1,tlr1 - 1, tlr2] + 1 == tlr3)
                                    {
                                        if (noisekaart[1,tlr1 + 1, tlr2] + 1 == tlr3)
                                        {
                                            if (noisekaart[1,tlr1, tlr2 - 1] + 1 == tlr3)
                                            {
                                                if (noisekaart[1,tlr1, tlr2 + 1] + 1 == tlr3)
                                                {
                                                if (noisekaart[2,tlr1,tlr2] < 40)
                                                {
                                                    if (blk[tlr1, tlr2 - 1, tlr3] == 5) { Bloem(); goto eblm; }
                                                    if (blk[tlr1, tlr2 + 1, tlr3] == 5) { Bloem(); goto eblm; }
                                                    Boom_plaatsen();
                                                    tlr1++;
                                                }
                                                else { Cactus(); }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        eblm:;
                    }


            //Instellen()
            Huis();
            Hoogste(blk, t < 0);
            Rondlopen.posa[1] = lb * 0.5;
            Rondlopen.posa[2] = lb * 0.5;
            Rondlopen.posa[3] = noisekaart[1,lb / 2, lb / 2] + 20;
            Rondlopen.lb = lb;
            Rondlopen.lh = lh;
            Rondlopen.l = l;
            Rondlopen.blk = blk;

            Application.DoEvents();
            if (MessageBox.Show("Wilt u uw keuze opslaan?", Rondlopen.WW, MessageBoxButtons.YesNo) == DialogResult.Yes) { //Keuze_opslaan()
                ; }
            blk = new byte[0, 0, 0];
            noisekaart = new int[0,0, 0];
        }
        void Bloem() {
            byte bklr;
            bklr = Convert.ToByte(doblstn.Next(11, 15));
            blk[tlr1, tlr2, tlr3] = 4;
            blk[tlr1, tlr2, tlr3 + 1] = bklr;
        }
        void Cactus()
        {
            blk[tlr1, tlr2, tlr3] = 8;
            blk[tlr1, tlr2, tlr3 + 1] = 8;
            blk[tlr1, tlr2, tlr3 + 2] = 8;
            blk[tlr1, tlr2, tlr3 + 3] = 8;
            blk[tlr1, tlr2, tlr3 + 4] = 8;
            blk[tlr1, tlr2, tlr3 + 5] = 8;
            blk[tlr1 + 1, tlr2, tlr3 + 3] = 8;
            blk[tlr1 + 2, tlr2, tlr3 + 3] = 8;
            blk[tlr1 + 2, tlr2, tlr3 + 4] = 8;
            blk[tlr1 - 1, tlr2, tlr3 + 3] = 8;
            blk[tlr1 - 2, tlr2, tlr3 + 3] = 8;
            blk[tlr1 - 2, tlr2, tlr3 + 4] = 8;
        }
        void Huis() {
            int a;
            int l2;
            l2 = (int)(lb * 0.5);
            a = noisekaart[1,l2, l2] + 6;
            if (a < lh - 10 && a>6)
            {
                Vul(l2 - 5, l2 - 5, a - 5, l2 + 5, l2 + 5, a + 5, 2);
                Vul(l2 - 4, l2 - 4, a - 4, l2 + 4, l2 + 4, a + 4, 0);
                Vul(l2 - 5, l2 - 1, a - 5, l2 - 5, l2 + 1, a - 2, 0);
                Vul(l2 - 5, l2 - 5, a + 6, l2 + 5, l2 + 5, a + 6, 5);
                Vul(l2 - 4, l2 - 4, a + 7, l2 + 4, l2 + 4, a + 7, 5);
                Vul(l2 - 3, l2 - 3, a + 8, l2 + 3, l2 + 3, a + 8, 5);
                Vul(l2 - 2, l2 - 2, a + 9, l2 + 2, l2 + 2, a + 9, 5);
                Vul(l2 - 1, l2 - 1, a + 10, l2 + 1, l2 + 1, a + 10, 5);
                Vul(l2, l2, a + 11, l2, l2, a + 11, 5);
            }
        }
        void Vul(int b1, int b2, int b3, int e1, int e2, int e3, byte nmr) {
            int ba1, ba2, ba3;
            for (ba3 = b3; ba3 <= e3; ba3++) {
                for (ba2 = b2; ba2 <= e2; ba2++) {
                    for (ba1 = b1; ba1 <= e1; ba1++) {
                        blk[ba1, ba2, ba3] = nmr; } } }

        }
        void Boom_plaatsen() {
            int i, j, k;
            byte bm;
            blk[tlr1 - 1, tlr2, tlr3] = 5;
            blk[tlr1 + 1, tlr2, tlr3] = 5;
            blk[tlr1, tlr2 - 1, tlr3] = 5;
            blk[tlr1, tlr2 + 1, tlr3] = 5;

            blk[tlr1, tlr2, tlr3] = 5;
            blk[tlr1, tlr2, tlr3 + 1] = 5;
            blk[tlr1, tlr2, tlr3 + 2] = 5;
            blk[tlr1, tlr2, tlr3 + 3] = 5;


            if (doblstn.NextDouble() * 50 > t) { bm = 9; } else { bm = 8; }

            blk[tlr1, tlr2, tlr3 + 7] = bm;
            blk[tlr1, tlr2 - 3, tlr3 + 5] = bm;
            blk[tlr1, tlr2 + 3, tlr3 + 5] = bm;
            blk[tlr1 - 3, tlr2, tlr3 + 5] = bm;
            blk[tlr1 + 3, tlr2, tlr3 + 5] = bm;
            for (i = -2; i <= 2; i++) {
                for (j = -2; j <= 2; j++) {
                    for (k = 4; k <= 6; k++) {
                        if (i == -2 || i == 2) { if (j == -2 || j == 2) { if (k == 4 || k == 6) { goto niet; } } }
                        blk[tlr1 + i, tlr2 + j, tlr3 + k] = bm;
                        niet:;
                    } } }
        }
        /*
                void Keuze_opslaan() {
                    string naam;
                    //       naam = Padn(InputBox("Hoe moet uw keuze heten?", WW) & ".3dKZ")
                    naam = Rondlopen.Nieuw_bestand("3dKZ");
                    if (naam == null) { return; }
                    FileClose();
                    FileOpen(1, naam, OpenMode.Output)
                    PrintLine(1, boom)
                    PrintLine(1, stl)
                    PrintLine(1, t)
                    PrintLine(1, opv)
                    PrintLine(1, wh)
                    PrintLine(1, max)
                    PrintLine(1, vlkgt)
                    FileClose(1)
            }
                void Open_keuze() {
                    FileClose()
                Dim naam As String
                naam = Rondlopen.Welk_bestand(extensie:= "3dKZ")
                If naam = vbNullString Then Exit Sub
                FileOpen(FileNumber:= 1, FileName:= naam, Mode:= OpenMode.Input)
                boom = LineInput(1)
                stl = LineInput(1)
                t = LineInput(1)
                opv = LineInput(1)
                wh = LineInput(1)
                max = LineInput(1)
                vlkgt = LineInput(1)
                FileClose(1)
        }
        */
//1=ht,2=temp,3=vc
        int[,,] noisekaart = new int[0,0, 0];
        public void Nieuwe_noise()
        {
            noisekaart = new int[4, lb + 1, lb + 1];
            int bg, x, y, nx, ny, a, b, ex, ey, vlkgt2;
            bool ig;
            double tot = 0, vkk, vs, gem;
            double[] mn = new double[4], mx = new double[4];
            mn[1] = opv;
            mx[1] = max;
            mn[2] = -10;
            mx[2] = 50;
            mn[3] = 0;
            mx[3] = 100;
            for (int i = 0; i < 4; i++)
            {
                vs = max - opv;
                gem = (mn[i] + mx[i]) / 2;
                vlkgt2 = vlkgt * 2;
                vkk = (int)Math.Pow((vlkgt * 2 + 1), 2);
                bg = lb + vlkgt * 2;
                double[,] basis = new double[bg + 1, bg + 1];
                for (x = 0; x <= bg; x++)
                {
                    for (y = 0; y <= bg; y++)
                    {
                        basis[x, y] = (doblstn.NextDouble() - 0.5); ;
                    }
                }
                for (ny = 0; ny <= lb; ny++)
                {
                    ig = false;
                    for (nx = 0; nx <= lb; nx++)
                    {
                        ex = nx + vlkgt2;
                        ey = ny + vlkgt2;
                        if (ig)
                        {
                            for (b = ny; b <= ey; b++)
                            {
                                tot += (basis[ex, b] - basis[nx, b]);
                            }
                        }
                        else
                        {
                            tot = 0;
                            for (a = nx; a <= ex; a++)
                            {
                                for (b = ny; b <= ey; b++)
                                {
                                    tot += basis[a, b];
                                    tlr1++;
                                }
                            }
                            ig = true;
                        }
                        noisekaart[i, nx, ny] = (int)(((tot / vkk) * vs * stl) + gem);
                    }
                }
            }
        }    }
}
