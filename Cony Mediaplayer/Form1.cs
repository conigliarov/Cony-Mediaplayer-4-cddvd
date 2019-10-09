using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static String Traccia = "";

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();



        #region Variabili Globali
        // Dichiaro la WinApi winmm 
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern long mciSendString(string lpstrCommand, string lpstrReturnString, long uReturnLength, long hwndCallback);


        //messaggio stringa
        string Strmssg;
        //diver
        string Strdriveletter;
        //valore di ritorno
        long ReturnValue;
        Boolean status = false;

        private long setTraystatus;
        short NumeroTraccie; //Qui vanno tutte le traccie audio
        int TracciaCorrente = 1; // Qui va immagazzinata la traccia corrente
        int traccia = 0;

        CdState CST = new CdState();
 
        #endregion



        private void Form1_MouseDown(object sender,
        System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        Point MousePos;
        bool IsMouseDown;
        Bitmap Form;

        int Ac = 255;
        int Rc;
        int Gc;
        int Bc;

        private void MDwn(object sender, EventArgs e) 
{
    MousePos = new Point(MousePosition.X - this.Location.X, MousePosition.Y - this.Location.Y);
    IsMouseDown = true;
}
        private void MUp(object sender, EventArgs e)
{
    IsMouseDown = false;
}
        private void MMove(object sender, EventArgs e)
{
    if (IsMouseDown) {
        this.Location = new Point(MousePosition.X - MousePos.X, MousePosition.Y - MousePos.Y);
    }
}

      

        private void Form1_Load(object sender, EventArgs e)
        {
            song.SetText("Nessuna Traccia Audio Rilevata       ");
            song.DoScroll(450, MatrixLib.ScrollDirection.Left);
            song.SetAutoPadding(true, ' ');
            
            matrixControl1.SetText(" Cony Media Player");
          //matrixControl1.DoScroll(500, MatrixLib.ScrollDirection.Left);
          this.matrixControl1.SetAutoPadding(true, ' ');
          //Moduli.CFGReader.lmain();
          //Moduli.CFGReader.ReadCFG(Application.StartupPath);
          this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\data\\Default\\default.png");
          init();
          configura();
          CST = CdState.CdChiuso;


          try
          {
              mciSendString("close all", "0", 0, 0); //Chiude tutte le traccie audio se eventualmente sono aperte
              mciSendString("open cdaudio alias cd wait shareable", "0", 0, 0); //Apre l'mcd per la sessione audio
              mciSendString("set cd time format tmsf wait", "0", 0, 0); //Imposto il formato del tempo al cd audio
              numerotraccie();

          }
          catch { };
            
        }



        private void init()
        {
            PictureBox pic = new PictureBox();

            try
            {
                pic.Image = Image.FromFile(Application.StartupPath + "\\data\\Default\\key.bmp");
            }
            catch
            {
                MessageBox.Show("Attenzione!\n\nLa skin Corrente non contine TrasparencyKey,\nVerrà ripristinata la skin Originale.");
            }

            
            Point pt = new Point(0, 0);

            String px =  ((pic.Image as Bitmap).GetPixel(pt.X,
            pt.Y) ).ToString();

            //Split dei valori Detectati
            String R = "0";
            String G = "0";
            String B = "0";


            R = px.Substring(16, 3);
            if (R == "0, ") { R = "0"; }
            G = px.Substring(23, 3);
            if (G == "0, ") { G = "0"; }
            try
            {
                B = px.Substring(30, 3);
                if (B == "0, ") { B = "0"; }
            }
            catch 
            {
                B = "0";
            }

            /*MessageBox.Show(R);
            this.BackColor = Color.FromArgb(255,Convert.ToInt16(R), Convert.ToInt16(G),Convert.ToInt16(B));*/
            
            this.TransparencyKey = Color.FromArgb(255, Convert.ToInt16(R), Convert.ToInt16(G), Convert.ToInt16(B));
            Rc = Convert.ToInt16(R);
            Gc = Convert.ToInt16(G);
            Bc = Convert.ToInt16(B);
            this.TransparencyKey = Color.Red;

        }


        public void configura()
        {

            matrixControl1.Width = 394;
            song.Width = 394;

            System.IO.StreamReader R = new System.IO.StreamReader(Application.StartupPath + "\\data\\Default\\config.cfg");
            String riga;

            while ((riga = R.ReadLine()) != null)
            {
                if (riga.StartsWith("//"))
                {
                    continue;
                }
                else
                {
                            if(riga.StartsWith("width")) {
                            String prm = riga.Remove(0, riga.IndexOf(":") + 1);
                            this.Width = Convert.ToInt16(prm);
                            }
                            if (riga.StartsWith("height"))
                            {
                                String prm = riga.Remove(0, riga.IndexOf(":") + 1);
                                this.Height = Convert.ToInt16(prm);
                            }

                    }
                 }
             }

        private void panel3_Paint(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel4_Paint(object sender, EventArgs e)
        {
            MessageBox.Show("Questa Skin non permette di Ingrandire il Form.","Cony Mediaplayer");
        }

        private void panel5_Paint(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel6_Paint(object sender, EventArgs e)
        {
            if (Traccia == "") { MessageBox.Show("Nessuna Traccia Audio Selezionata", "Cony MediaPlayer"); }
  

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_MouseClick(object sender, MouseEventArgs e)
        {
            if (CST == CdState.CdChiuso)
            {
                ReturnValue = mciSendString("open " + Strdriveletter + " Type cdaudio Alias cd", Strmssg, 255, 0);
                ReturnValue = mciSendString("set cd door open", "", 0, 0);
                song.StopScroll();
                song.SetText("In Attesa di un CD Room");
                CST = CdState.CdAperto;
            }
            else if
            (CST == CdState.CdAperto)
            {
                song.SetText("Attendere Prego...");
                this.Cursor = Cursors.Hand;
                ReturnValue = mciSendString("open " + Strdriveletter + " Type cdaudio Alias cd", Strmssg, 255, 0);
                ReturnValue = mciSendString("set cd door closed", "", 0, 0);
                CST = CdState.CdChiuso;
                numerotraccie();
            }
        }



        void DoScrolls()
        {
            song.SetText("");
            song.DoScroll(450, MatrixLib.ScrollDirection.Left);
            song.SetAutoPadding(true, ' ');
        }

        private void numerotraccie()
        {
            String NumeroTraccia;
            string Tracks = "30";
            ReturnValue = mciSendString("status cd number of tracks wait", Tracks, Tracks.Length, 0);
            NumeroTraccia = Convert.ToString(traccia);
            MessageBox.Show(Tracks.Length.ToString());
            this.Cursor = Cursors.Arrow;

        }


        enum CdState : uint
        {
            CdAperto = 0x01,
            CdChiuso = 0x12,
            CdPlay = 0x23
        }


        [StructLayout(LayoutKind.Sequential)]
        public class CDROM_TOC
        {
            public ushort Lunghezza;
            public byte PrimaTraccia = 0;
            public byte UltimaTraccia = 0;
             public CDROM_TOC()
            {
                Lunghezza = (ushort)Marshal.SizeOf(this);
            }
        }
             
        }      
    
    }
