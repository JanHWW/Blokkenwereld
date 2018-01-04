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


    partial class Rondlopen : Form
    { 
        public Rondlopen()
        {
            InitializeComponent(); 
        }

        public static byte[,,] blk = new byte[1, 1, 1];
        public static int lb;
        public static int lh;
        public static double[] posa = new double[4];
        public static string WW;
        public static long[] l = new long[4];
        public int MyGlobalValue = 42;

       static int[,,] px = new int[1388, 1001, 8];
       static int[] blok = new int[7];
       static int[] tlr = new int[7];
       static int[] pos = new int[4];
       static long[,] at = new long[101, 6];
       static byte[,] klr = new byte[101, 6];
        static bool[] toets = new bool[256];
        static int[,] d = new int[101, 5];
        static int[] schaal = new int[3];
        static string[] mz = new string[101];
        static double[] r = new double[3];
        static double[] hoek = new double[3];
        static double[] rad = new double[3];
        static double[] vrad = new double[3];
        static double[] factor = new double[4];
        static double[] posb = new double[4];
        static double[] posvlak = new double[4];
        static double[,] csin = new double[3, 3];
        static double[,] dhe = new double[7, 4];
        static PictureBox plt = new PictureBox();
        static double[,,] factorxy ;
        static double afstand, awr, mist, vaagheid, roodtemp, groentemp, blauwtemp, tijd, zfactor, pi180;
       static byte voorraad, kant, kleurcode;
       static int fa, wtr, xp, yp, fotonummer, adn,tlr1, tlr2, tlr3, tlr4, tlr5, nut, kijkwijdte, kw3;
       static bool kijken, zw, film, herhalen, sluiten, opgang, lijn,geladen,sc;
       static int rood, groen, blauw, minuut;
       static string pad, filmpad, beginpad;
        const byte vol = 255;
        const double sa = 0.5;

        public double  Getalin(string vraag, double minim, double maxim, double standrd) {
            double antw=0;
            string aw;
            aw = (Microsoft.VisualBasic.Interaction.InputBox(vraag, WW));
            if (aw.Length > 0)
            {
                try
                {
                    antw = Convert.ToDouble(aw);
                }
                catch
                {return standrd;}
            }
            else
            {
                return standrd;
            }
            if ((int)antw < minim || antw > maxim) { antw = standrd; }
                        return antw;

        }

        private void NieuwToolStripMenuItem1_Click(object sender, EventArgs e) {

        }
        private void ToolStripButton1_Click(object sender, EventArgs e) {
            Mistig();
        }
        private void VaagheidToolStripMenuItem_Click(object sender, EventArgs e) {
            //Opslaan();
        }
        private void KijkenToolStripMenuItem_Click(object sender, EventArgs e) {
            Film_kijken();
        }
        private void OpenenToolStripMenuItem_Click(object sender, EventArgs e) {
           // Openen();
        }
        private void NieuwToolStripMenuItem_Click(object sender, EventArgs e) {
            Nieuwewereld zien = new Nieuwewereld();
                zien.Show();
        }
        private void ToolStripButton2_Click(object sender, EventArgs e) {
            Vgi();
        }
        private void ToolStripButton3_Click(object sender, EventArgs e) {
            Informatie();
        }
        private void ToolStripButton4_Click(object sender, EventArgs e) {
            if (opgang)
            { opgang = false; MessageBox.Show("dag-nachtwisseling is aan",WW); }
            else {
                opgang = true;
                MessageBox.Show ("dag-nachtwisseling is uit", WW);
            }
        }
        private void KaartToolStripMenuItem_Click(object sender, EventArgs e) {
            Kaart();
        }
        private void ToolStripButton6_Click(object sender, EventArgs e) {
            if (lijn) {
                lijn = false;
                MessageBox.Show("Lijn is uit", WW);
            } else {
                lijn = true;
                MessageBox.Show("Lijn is aan", WW);
            } }

        private void Rondlopen_Closing(object sender, FormClosingEventArgs e)
        {

                if (sluiten) { return; }
                sluiten = true;
                switch (MessageBox.Show("Wilt u de wereld opslaan?", WW, MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.Yes:
                        // Opslaan();
                        break;
                }
                herhalen = false;
            }

        private void ZwaartekrachtToolStripMenuItem_Click(object sender, EventArgs e) {
            if (zw) { zw = false; MessageBox.Show("zwaartekracht is uit", WW); } else { zw = true; MessageBox.Show("zwaartekracht is aan", WW); }
        }
        private void AsToolStripMenuItem_Click(object sender, EventArgs e) {
            Verplaatsing();
        }
        private void ToolStripButton5_Click(object sender, EventArgs e) {
            kijkwijdte = (int)Getalin("Hoe ver kan je maximaal kijken?\nNu: " + kijkwijdte, 1, 500, kijkwijdte);
        }



        private void PrintScreenToolStripMenuItem_Click(object sender, EventArgs e) {
            Plaatje.Image.Save(Nieuw_bestand("bmp"));
        }
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e) {
            byte plts=0, dims=0;
            Double b5;
            xp = Schaalx(e.X);
            yp =Schaaly(e.Y);
            if (xp > 0 && yp > 0 && geladen) {
                    Welk_blokje();
                    if (blok[4] == 0)
                    {
                        switch (e.Button) {
                            case MouseButtons.Left:
                                plts = 0;
                                break;
                            case MouseButtons.Right:
                                plts = voorraad;
                                if (voorraad == 10) {
                                    if (adn > 99) { MessageBox.Show("Teveel dynamiet", "WereldWijd"); return; }
                                    adn++;
                                    d[adn, 1] = (int)blok[1];
                                    d[adn, 2] = (int)blok[2];
                                    d[adn, 3] = (int)blok[3];
                                    d[adn, 4] = 1;
                                }
                                b5 = blok[5] - 3.5;
                                dims = Convert.ToByte(Math.Abs(b5) + 0.5);
                                blok[dims] = (int)(blok[dims] + (Math.Abs(b5) / b5));

                            break;
                        }
                    if (blok[dims] > l[dims] || blok[dims]<0){ if (dims < 3) { goto buitenber; } }
                    if (nut < 100) { nut += 1; } else { nut = 0; }
                        at[nut, 1] = blok[1];
                        at[nut, 2] = blok[2];
                        at[nut, 3] = blok[3];
                        at[nut, 4] = blk[blok[1], blok[2], blok[3]];
                        at[nut, 5] = 1;
                        if (blok[3] > l[3]) { if (blok[3] <= lh) { l[3] = blok[3]; } }

                        blk[blok[1], blok[2], blok[3]] = plts;
                    buitenber:;
                    } Beeld();
                } 
        }
        string Welke_map() {
            string wm = Padn("");
            FolderBrowserDialog1.ShowDialog();
            wm = FolderBrowserDialog1.SelectedPath;
            return wm;
        }

        string Welk_bestand(string extensie) {
            if (extensie == "map") { OpenFileDialog1.RestoreDirectory = true; } else { OpenFileDialog1.RestoreDirectory = false; OpenFileDialog1.Filter = "WereldWijd bestanden(*." + extensie + "|*." + extensie; }
            OpenFileDialog1.InitialDirectory = Padn("");
            string wb;
            OpenFileDialog1.ShowDialog();
            wb = OpenFileDialog1.FileName;
            return wb;
        }
        string Nieuw_bestand(string extensie)
        {
            extensie = "WereldWijd bestanden(*." + extensie + "|*." + extensie;
            SaveFileDialog1.InitialDirectory = Padn("");
            SaveFileDialog1.Filter = extensie;
            string wb;
            SaveFileDialog1.ShowDialog();
            wb = SaveFileDialog1.FileName;
            return wb;
        }
        void Kleur() {
            Bitmap bmp;
            bmp = new Bitmap(schaal[1], schaal[2]);
            for (xp = 0; xp < schaal[1]; xp++) {
                for (yp = 0; yp < schaal[2]; yp++) {

                    Pixel();
                    bmp.SetPixel((int)xp, (int)yp, Color.FromArgb(vol, rood, groen, blauw));
                }
            }
            Plaatje.Image = bmp;
        }
        void Pixel() {
            if (px[xp, yp, 6] == 1)
            {
                if (px[xp, yp, 4] > 0) { rood = 0; groen = 176; blauw = 235; } else { rood = klr[100, 1]; groen = klr[100, 2]; blauw = klr[100, 3]; }
                return;
            }
            else
            {
                kleurcode = (blk[px[xp, yp, 1], px[xp, yp, 2], px[xp, yp, 3]]); kant = 1;
            }
                afstand = (px[xp, yp, 5]) / mist;
                kant = 1;
                fa = (int)(afstand * 150);
                wtr = px[xp, yp, 4];
                awr = wtr + 1 + afstand;
                rood = (int)(((klr[kleurcode, 1] + fa) / awr) * kant);
                groen = (int)(((klr[kleurcode, 2] + fa + (wtr * 176)) / awr) * kant);
                blauw = (int)(((klr[kleurcode, 3] + fa + (wtr * 235)) / awr) * kant);
            } 

        void Bewegen(double richting) {
            double[] npa = new double[3];
            npa[1] = posa[1] + Gsin(r[1] + 90 * (richting - 1));
            npa[2] = posa[2] + Gcos(r[1] + 90 * (richting - 1));
            if (zw) {
                if (Binnenber(npa[1], npa[2], posa[3])) { if (blk[(int)npa[1], (int)npa[2], (int)posa[3]] == 0) { return; } }
                posa[1] = npa[1];
                posa[2] = npa[2];
            }
            else {
                posa[1] = npa[1];
                posa[2] = npa[2];
            }

        }
        double Gsin(double getal) {
            return Math.Sin(getal * pi180);
        }
        double Gcos(double getal) {
            return Math.Cos(getal * pi180);
        }
        public void Beeld() {
            if (!sc) { Factorbep(); }
            geladen = true;
            Bitmap bmp = new Bitmap(schaal[1], schaal[2]);
            double[] fx = new double[4];
            int[] sx = new int[4];
            double[] vx = new double[4];
            double[] spx = new double[4];
            long i, s1, s2, wiestap;
            byte j,a;
            bool ber;
            double dh, wtr;
            kw3 = kijkwijdte * 2;
            tlr[1] = 0;
            ber = Binnenber(posa[1], posa[2], posa[3]);
            //naar pixels
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpdata = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpdata.Scan0;
            int bytes = Math.Abs(bmpdata.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            //System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)
            //einde pixels
            s1 = schaal[1] - 1;
            s2 = schaal[2] - 1;
            //------------------------------------------------------------------------
            for (yp = 0; yp < schaal[2]; yp++) {
                for (xp = 0; xp < schaal[1]; xp++) {
                    for (i = 1; i < 4; i++) {
                        fx[i] = Math.Abs(factorxy[xp,yp,i]);
                        if (fx[i] == 0) { sx[i] = 0; } else { sx[i] = (int)(factorxy[xp,yp,i] / fx[i]); }
                        pos[i] = (int)posa[i];
                        vx[i] = posa[i] - Convert.ToDouble(pos[i]);
                       if (sx[i] > 0) { vx[i] = 1 - vx[i]; }
                        if (fx[i] == 0) { spx[i] = 0; } else { spx[i] = vx[i] / fx[i]; }
                    }
                    wtr = 0;
                    dh = 6;
                    j = 0;
                    a = 0;
                    for (wiestap = 0; wiestap <= kw3; wiestap++) {
                        if (wiestap > 0) {
                            if (spx[1] <= spx[2] && spx[1] <= spx[3])
                            { j = 1; }
                            else if (spx[2] <= spx[3]) {
                                j = 2; }
                            else
                            { j = 3; }

                            pos[j] += sx[j];
                            vx[j] += 1;
                            spx[j] = vx[j] / fx[j];
                        }
                        if (!ber) { if (pos[1] >= 0 && pos[1] <= lb && pos[2] >= 0 && pos[2] <= lb && pos[3] >= 0 && pos[3] <= l[3]){ } else { goto volgende; } }

                        if (pos[j] >= 0 && pos[j] <= l[j]) {
                            a = blk[pos[1], pos[2], pos[3]];
                            if (a == 1) { wtr += 1; }
                            else if (a > 1)
                            { goto uit; }
                            a = 0;
                        }
                        else {
                            if (ber) { goto uit; }
                        }
                        volgende:
                        ; }
                    uit:
                    dh = 3.5 + (Convert.ToDouble(sx[j]) * (j - 0.5) * -1); //vlak

                    if (a < 2) {
                        if (wtr > 0) { rood = 0; groen = 176; blauw = 235; } else { rood = klr[100, 1]; groen = klr[100, 2]; blauw = klr[100, 3]; }
                        px[xp, yp, 4] = 1;
                        dh = 6;
                    }
                    else {
                        if (lijn) {
                            for (i = 1; i <= 3; i++) {
                                if (!(i == j)) {
                                    posvlak[i] = (((pos[j] - posa[j]) / fx[j]) * fx[i]) * sx[j] * sx[i];
                                    posvlak[i] += (posa[i] - (int)posa[i]);
                                    if (Math.Abs(posvlak[i] - Math.Round(posvlak[i], 0)) < 0.1) { rood = 0; groen = 0; blauw = 0; goto geenkleur; }
                                } } }

                        afstand = (int)Math.Pow((Math.Pow(vx[1] , 2) +( Math.Pow(vx[2] ,2)) + Math.Pow(vx[3] , 2)) , 0.5) / mist;
                        wtr /= (mist * 0.1);
                        fa = (int)(afstand * vol);
                        awr = (wtr + 1 + afstand);
                        kleurcode = a;
                        rood = (int)(klr[kleurcode, 1] / awr);
                        groen = (int)((klr[kleurcode, 2] + fa + (wtr * 176)) / awr);
                        blauw = (int)((klr[kleurcode, 3] + fa + (wtr * 235)) / awr);
                        geenkleur:
                        px[xp, yp, 5] = (int)dh;
                        px[xp, yp, 1] = (int)pos[1];
                        px[xp, yp, 2] = (int)pos[2];
                        px[xp, yp, 3] = (int)pos[3];
                        px[xp, yp, 4] = 0;
                    }
                    //vgd:
                rgbValues[tlr[1]] = Convert.ToByte(blauw *  dhe[(int)dh, 3]);
                    rgbValues[tlr[1] + 1] = Convert.ToByte(groen * dhe[(int)dh, 2]);
                    rgbValues[tlr[1] + 2] = Convert.ToByte( (rood) * dhe[(int)dh, 1]);
                    rgbValues[tlr[1] + 3] = vol;
                    tlr[1] += 4;
                } }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpdata);
            //Controls.Add(plt);
            Plaatje.Image = bmp;
            
            if (film) { bmp.Save(filmpad + fotonummer + ".BMP"); fotonummer += 1; }
        }
        int Sgn(double getal) {
            if (getal > 0) {
                return 1;
            } else if (getal == 0) {
                return 0;
            }
            else {
                return -1;
            } }
        void Inventaris() {
            voorraad = Convert.ToByte(Getalin("Welk soort blokje(1 tot 20) wil je gebruiken?", 1, 20, Convert.ToDouble(voorraad)));
        }
        void Welk_blokje() {
            for (int i = 1; i <= 5; i++) {
                blok[i] = px[xp, yp, i];
            }
        }
        /*                void Openen() {
                    filesystem.FileClose(1);
                    string b, bpad;
                    byte c;
                    long f;
                    char wat;

                    pos[1] = 0;
                    pos[2] = 0;
                    pos[3] = 0;
                    bpad = Welk_bestand("3Dwereld");
                    if(bpad == null) { return; }
                    tlr[1] = 1;
                    try
                    {
                        using (StreamReader bd = new StreamReader(bpad))
                        {

                            do
                            {
                                b = bd.ReadLine();
                                c = (int)(b.Substring(1, 1));

                                c=(int)wat;
                                f = Strings.Right(b, Len(b) - 1) + 0;
                                tlr[6] = 1;
                                do
                                {
                                    blk[pos[1], pos[2], pos[3]] = c;


                                    pos[1] += 1;
                                    if (pos[1] > lb)
                                    {
                                        pos[1] = 0;
                                        pos[2] += 1;
                                        if (pos[2] > lb)
                                        {
                                            pos[2] = 0;
                                            pos[3] += 1;
                                            if (pos[3] > lh)
                                            {
                                                pos[3] = 0;
                                            }
                                        }
                                    }
                                    tlr[6] += 1;
                                } while (!EOF[1]);
                            } while (tlr[6] <= f);

                        }                //Nieuwewereld.Hoogste(blk, false);
                    } catch {
                        MessageBox.Show("Het bestand bestaat niet.", WW);
                    }
                    FileSystem.FileClose(1);
                }
        */
        void Opnemen() {
            fotonummer = 0;
            if (film) {
                film = false;
                MessageBox.Show("film stopt",  WW);
            }
            else {
                if (MessageBox.Show("Wilt u iets opnemen?", WW,MessageBoxButtons.OKCancel) == DialogResult.OK) {
                    film = true;
                    filmpad = Padn(Microsoft.VisualBasic.Interaction.InputBox("Hoe moet de film heten?", WW));
                    Directory.CreateDirectory(filmpad);
                    filmpad = filmpad + '\'';
                } }
        }
        void Film_kijken() {
            kijken = true;
            double td=0;
            long pos;
            string nmp, omp;
            fotonummer = 0;
            filmpad = Welke_map();
            if (filmpad.Length == 0) { return; }
            do {
                do {
                } while (Convert.ToDouble(DateTime.UtcNow) < td);
                td = Convert.ToDouble(DateTime.UtcNow) + 0.04;
                if (Directory.GetDirectories(filmpad + "\"" + fotonummer + ".BMP")[0] == null) { goto klaar; }
                Plaatje.Image = Image.FromFile(filmpad + '\'' + fotonummer + ".BMP");
                    fotonummer += 1;
                Application.DoEvents();
            } while (kijken);
            klaar:
            kijken = false;
            fotonummer = 0;

            omp = Directory.GetDirectories(filmpad)[0];
                pos = omp.IndexOf("(") +1;
            if (pos > 0) {
                tlr1 =Convert.ToInt32( omp.Substring( (int)pos + 1, omp.Length - (int)pos - 2));
                nmp = omp.Substring(1, (int)pos - 1);
                tlr1 += 1;
            }
            else {
                nmp = omp + "1";
            }
        }
/*        void Opslaan() {
            byte a, b;
            if (pad == null) { pad = Nieuw_bestand("3Dwereld"); }
            if (pad == null) { MessageBox.Show("U hebt geen bestandsnaam opgegeven", WW); return; }
            FileClose();
            FileOpen(1, pad, OpenMode.Output);
            for (tlr3 = 0; tlr3 <= lh; tlr3++) {
                for (tlr2 = 0; tlr2 <= lb; tlr2++) {
                    for (tlr1 = 0; tlr1 <= lb; tlr1++) {


                        a = blk[tlr1, tlr2, tlr3];
                    if (a == b) {
                            tlr[1] += 1;
                        }
                        else {
                            FileSystem.PrintLine(1, Chr(b + 65) + tlr[1]);
                            b = a;
                            tlr[1] = 1;
                        }
                    }
                }
            }
            FileClose();
        }
*/        string Padn(string pad) {
            return beginpad + pad;
        }
        int Schaalx(double ex) {
            return (int)((ex / Plaatje.Width) * schaal[1]);
        }



        int Schaaly(double ey) {
            return (int)((ey / Plaatje.Height) * schaal[2]);
        }
        void Explosie() {
            int[,] minmax = new int[3, 4];
            for (tlr4 = 1; tlr4 <= adn; tlr4++) {
                if (d[tlr4, 4] == 1) {
                    for (tlr5 = 1; tlr5 <= 3; tlr5++) {
                        if (d[tlr4, tlr5] - 10 < 0) { minmax[1, tlr5] = 0; } else { minmax[1, tlr5] = d[tlr4, tlr5] - 10; }
                        if (d[tlr4, tlr5] + 10 > l[tlr5]) {
                            minmax[2, tlr5] = (int)l[tlr5];
                        }
                        else {
                            minmax[2, tlr5] = d[tlr4, tlr5] + 10;
                        }
                    
                    for (tlr1 = minmax[1, 1]; tlr1 <= minmax[2, 1]; tlr1++)

                        for (tlr2 = minmax[1, 2]; tlr2 <= minmax[2, 2]; tlr2++) {

                            for (tlr3 = minmax[1, 3]; tlr3 <= minmax[2, 3]; tlr3++) { 
                                //omtrek=10
                                if( Pyt(tlr1 - d[tlr4, 1], tlr2 - d[tlr4, 2], tlr3 - d[tlr4, 3]) < 10) {
                                    blk[tlr1, tlr2, tlr3] = 0;
}
                        }
                }
            }
                d[tlr4, 1] = 0;
                d[tlr4, 2] = 0;
                d[tlr4, 3] = 0;
                d[tlr4, 4] = 0;
            }
        }
        adn = 0;
            }
        double Pyt(double g1, double g2,double g3) {
    return (Math.Pow(Math.Pow(g1, 2) + Math.Pow(g2, 2)  + Math.Pow(g3, 2),0.5) );
}
void Kaart() {
    long wtr;
            for (xp = 1; xp <= schaal[1]; xp++) {
                pos[1] = (int)(Convert.ToDouble(xp) / schaal[1] * lb);
                    for (yp = 1; yp <= schaal[2]; yp++) {
                    pos[2] = (int)(Convert.ToDouble(yp) / schaal[2] * lb);
                    wtr = 0;
                    for (pos[3] = lh; pos[3] >= 0; pos[3]--) {
                        if (blk[pos[1], pos[2], pos[3]] > 1) { goto vulling; }
                        if (blk[pos[1], pos[2], pos[3]] == 1) { wtr++; }
                        pos[3]--;
                    }
                    px[xp, yp, 6] = 1;
                    goto volgende;
                    vulling:
                    px[xp, yp, 6] = 1;
                    //pixelkleur
                    px[xp, yp, 1] = (int)pos[1];
                    px[xp, yp, 2] = (int)pos[2];
                    px[xp, yp, 3] = (int)pos[3];
                    //water
                    px[xp, yp, 4] = (int)wtr;
                    //afstand
                    px[xp, yp, 5] = 0;
                    px[xp, yp, 7] = 6;
                    px[xp, yp, 6] = 0;
                    volgende:;
                } }
               Kleur();
        xp = (int)((((posa[1] / lb) * 0.5) + 0.5) * schaal[1]);
        yp = (int)((((posa[2] / lb) * 0.5) + 0.5) * schaal[2]);
        rood = 0;
        groen = 0;
        blauw = 0;
            Kleur();
        Application.DoEvents();
            tijd = (DateTime.UtcNow.Second + 2) % 60;
            do
            {


        } while (!(DateTime.UtcNow.Second == tijd));
    }
    void Verplaatsing() {
            Beeld();
        posa[1] = Getalin("X", -lb, lb* 2, lb* 0.5);
        posa[2] = Getalin("Y", -lb, lb* 2, lb* 0.5);
        posa[3] = Getalin("Z", -lh, lh* 2, lh* 0.5);
            r[2] = -90;
    }
    void Informatie() {
            string hlp = "positie:/nx: " + posa[1] + "\ny: " + posa[2] + "\nz: " + posa[3] +  "\nAantal dynamiet: " + adn + " staven." + "\ninventaris: " + voorraad;
            MessageBox.Show(hlp, "WereldWijd");
    }
    void Mistig() {
            mist = Getalin("Hoe helder moet het zijn?/nnu: " + mist, 0, 1000, mist);
    }
    void Vgi() {
            double  inp;
        inp = Getalin("Hoe vaag moet het beeldscherm zijn? Nu: " + vaagheid, 1, 100, 5);
        schaal[1] = (int)(Plaatje.Width / inp);
            schaal[2] = (int)(Plaatje.Height / inp);
            vaagheid = inp;
            sc = false;
        }
   public static bool Binnenber(double p1,double  p2,double  p3) {
            if (p1 >= 0 && p1 <= lb && p2 >= 0 && p2 <= lb && p3 >= 0 && p3 <= l[3]) { return true; } else { return false; }
    }
    void Klin() {
            csin[1, 1] = Gsin(r[1]);
            csin[1, 2] = Gcos(r[1]);
            csin[2, 1] = Gsin(r[2]);
            csin[2, 2] = Gcos(r[2]);
        if (opgang) {
            roodtemp = 1;
            groentemp = 1;
            blauwtemp = 1;
}        else{
                
                double sec = DateTime.UtcNow.Second;
                minuut = (int)(DateTime.UtcNow.Minute) % 30;
            if(minuut > 22 || minuut< 7){ //nacht
    roodtemp = 0;
    groentemp = 0;
    blauwtemp = 0;
}            else if( minuut > 7 && minuut < 22) { //dag
                roodtemp = 1;
                groentemp = 1;
                blauwtemp = 1;
 }           else{ //ondergang
                    if (minuut == 7) { sec = 60 - sec; }
                    if (sec < 20) {
                        roodtemp = 1;
                        blauwtemp = 1;
                        groentemp = 1 * (20 - sec) / 20;
                    } else if (sec < 40) {
                        roodtemp = 1;
                        groentemp = 0;
                        blauwtemp = 1 * (40 - sec) / 20;
                    }
                    else { 
                        groentemp = 0;
                        blauwtemp = 0;
                        roodtemp = 1 * (60 - sec) / 20;
    }
            }
}        //diameter van de zon is 0,53 graden
        roodtemp = roodtemp * 0.8 + 0.2;
        groentemp = groentemp * 0.8 + 0.2;
        blauwtemp = blauwtemp * 0.8 + 0.2;
        for (int i = 1;i<=6;i++) {
            dhe[i, 1] = roodtemp * (i * 0.1 + 0.4);
            dhe[i, 2] = groentemp * (i * 0.1 + 0.4);
            dhe[i, 3] = blauwtemp * (i * 0.1 + 0.4);
        }
    }
    void Terug() {
            if (at[nut, 5] == 1) {
                blk[at[nut, 1], at[nut, 2], at[nut, 3]] = Convert.ToByte(at[nut, 4]);
                at[nut, 1] = 0;
                at[nut, 2] = 0;
                at[nut, 3] = 0;
                at[nut, 4] = 0;
                nut--;
    }
        }
    void Indrukken() {
            herhalen = true;
        do
            {
                Klin();
                if (zw) { if (Binnenber(posa[1], posa[2], posa[3])) { if (blk[(int)posa[1], (int)posa[2], (int)posa[3] - 2] < 2) { posa[3]--; } } }
            TextBox1.Text = null;
                if (toets[37])
                {
                    Bewegen(4);
                }
                else if (toets[39])
                {
                    Bewegen(2);
                }
                if (toets[38])
                {
                    Bewegen(1);
                }
                else if (toets[40])
                {
                    Bewegen(3);
                }
                if (toets[34])
                { //page-down
                    posa[3]--;
                }
                else if (toets[33]){ //page-up
                    posa[3]++;
}            if (toets[65])
                {  //a
                    r[1] = r[1] - 10;
                    sc =false;
                }
                else if (toets[68])
                { //d
                    r[1] = r[1] + 10;
                    sc = false;
                }
                if (toets[87]) { //w
                    if (r[2] > 80) { r[2] = 90; } else { r[2] += 10; }
                    sc = false;
                }
                else if (toets[83]) {  //s
                    if (r[2] < -80) { r[2] = -90; } else { r[2] -= 10; }
                    sc = false;
                }
                if (toets[32]) { //spatie
                    if (zw) { posa[3] += 2; }
            }
            if (toets[36]) { //home
                        zfactor *= 0.8;
                    } else if (toets[35]) { //end
                        zfactor /= 0.8;
                    }
                if (toets[90]) { //z
                    Terug();                    toets[90] = false;
     }        
            if (toets[13]) { //enter
                        Explosie();
                        toets[13] = false;
            }
                if (toets[73]){ //i van inventaris
                    Inventaris();
                    toets[73] = false;
                }
            if (r[1] > 360) {
                        r[1] = r[1] - 360;
            } else if (r[1] < 0) {
                        r[1] = r[1] + 360;
}
                TextBox1.Text = null;
                Beeld();
                Application.DoEvents();
            } while (herhalen);
    }
        private void Rondlopen_Resize(object sender, EventArgs e) {
            sc = false;
            long  dikte = 50;
            if (Height > 60 + dikte) { 
                schaal[1] = Width;
            schaal[2] = (int)(Height - 60 - dikte);
            Plaatje.Width = schaal[1];
            Plaatje.Height = schaal[2];
            Plaatje.Top = (int)dikte;
            Plaatje.Left = 0;
                TextBox1.Width = Width;
                    TextBox1.Height = 20;
                    TextBox1.Top = Height - 60;
                TextBox1.Left = 0;
                    if (vaagheid == 0) { vaagheid = 5; }
                schaal[1] /=(int) vaagheid;
            schaal[2] /= (int)vaagheid;
        }    }
    private void TextBox1_KeyDown(object sender, KeyEventArgs e) {
            toets[(int)e.KeyCode] = true;
        if (!herhalen) { Indrukken(); }
    }

    void Map_laden() {
            string[] c = new string[0];
                byte b;
            char scf,a;
            scf = '\'';
            for (b = 0; b < 26; b++)
            {
                a = (char)(90 - b);
                try
                {
                    c = (Directory.GetDirectories(a + ":" + scf));
                }
                catch { }
                if (!(c == null))
                {
                    try
                    {
                        c = Directory.GetDirectories(a + ":" + scf + "Blokkenwereld");
                    }
                    catch
                    {                    }
                }
                else
                {
                    if (c[0] == "Blokkenwereld")
                    {
                        beginpad = a + ":" + scf + "Blokkenwereld" + scf;
                    }
                    else
                    {
                        try
                        {
                            Directory.CreateDirectory(a + ":" + scf + "Blokkenwereld");
                        }
                        catch
                        {
                            goto bestaatal;
                        }
                        beginpad = a + ":" + scf + "Blokkenwereld" + scf;
                    }
                }
                    goto uit;
                    bestaatal:;
                    }
    uit:
                ;
    }
        void Factorbep() {
            sc = true;
            factorxy = new double[schaal[1]+1,schaal[2]+1,4];
            if (schaal[1] > schaal[2])
            {hoek[1] = zfactor;hoek[2] = zfactor / (schaal[1] / schaal[2]);}
            else
            {
                hoek[2] = zfactor;
                hoek[1] = zfactor / Convert.ToDouble(schaal[2] / schaal[1]);
            }
            csin[1, 1] = Gsin(r[1]);
            csin[1, 2] = Gcos(r[1]);
            csin[2, 1] = Gsin(r[2]);
            csin[2, 2] = Gcos(r[2]);
            int s2 = schaal[2] - 1;
            int s1 = schaal[1] - 1;
            for (yp = 0; yp <= schaal[2]; yp++)
            {
                vrad[2] = (((Convert.ToDouble(yp) / schaal[2]) - 0.5) * hoek[2]); //yz
                factor[3] = vrad[2] * -csin[2, 2] + csin[2, 1] * sa;
                for (xp = 0; xp <= schaal[1]; xp++)

                {
                            vrad[1] = (((Convert.ToDouble(xp) / schaal[1]) - 0.5) * hoek[1]); //xy
                            factor[2] = csin[2, 2] * sa + csin[2, 1] * vrad[2];
                            factor[1] = csin[1, 2] * vrad[1] + csin[1, 1] * factor[2];
                            factor[2] = csin[1, 2] * factor[2] - csin[1, 1] * vrad[1];
                    for (int i = 1; i < 4; i++)
                    {

                        factorxy[xp,yp,i]=factor[i];
                        }
                    }
            }
        }
        private void Rondlopen_Load(object sender, EventArgs e) { 

        Map_laden();
        klr[2, 1] = vol;
        klr[2, 2] = vol;
        klr[2, 3] = vol;
        klr[3, 1] = vol;
        klr[3, 2] = vol;
        klr[4, 2] = vol;
        klr[5, 1] = 128;
        klr[5, 2] = 64;
        klr[5, 3] = 64;
        klr[6, 1] = vol;
        klr[6, 2] = vol;
        klr[6, 3] = vol;
        klr[7, 1] = 204;
        klr[7, 2] = 51;
        klr[8, 2] = 200;
        klr[9, 1] = vol;
        klr[9, 2] = 153;
        klr[10, 1] = 200;
        
            klr[11, 1] = vol;
            klr[11, 2] = vol;
            klr[12, 2] = vol;
            klr[12, 3] = vol;
            klr[13, 1] = vol;
            klr[13, 3] = vol;
            klr[14, 1] = vol;
            klr[15, 3] = vol;

            klr[100, 1] = 100;
            klr[100, 2] = 200;
            klr[100, 3] = vol;
            vaagheid = 12;
            WindowState = FormWindowState.Maximized;
            posa[3] = 100;
            posa[2] = 50;
            posa[1] = 50;
            mist = 100;
            WW = "WereldWijd";
            kijkwijdte = 100;

            pi180 = Math.PI / 180;
            zfactor = 1;
    }

    private void TextBox1_KeyUp(object sender, KeyEventArgs e) {
    toets[(int)e.KeyCode] = false;
    }

}
}
