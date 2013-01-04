using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Net;

namespace WindowsFormsApplication1
{
    public partial class LAUNCHOPTIONS : Form
    {
        public LAUNCHOPTIONS()
        {
            InitializeComponent();
        }

        private void LAUNCHOPTIONS_Load(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        Point mouseDownPoint = Point.Empty;
        private void LAUNCHOPTIONS_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPoint = new Point(e.X, e.Y);
        }

        private void LAUNCHOPTIONS_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownPoint = Point.Empty;
        }

        private void LAUNCHOPTIONS_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void LAUNCHOPTIONS_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDownPoint.IsEmpty)
                return;
            Form f = sender as Form;
            f.Location = new Point(f.Location.X + (e.X - mouseDownPoint.X), f.Location.Y + (e.Y - mouseDownPoint.Y));
        }

        private void Donate_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
            player.Play();
            Donate donate = new Donate();
            donate.Show();
        }

        private void Support_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
            player.Play();
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "/TREFix.exe"))
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
                player.Play();
                System.Diagnostics.Process.Start(Application.StartupPath + "/TREFix.exe");
            }

            else
            {

                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Error.wav");
                player.Play();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
            label1.Text = "Checking Launcher Version...";
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
            player.Play();
            
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
               System.Threading.Thread.Sleep(15000);
                string FTP = "ftp://173.242.114.16/files/";
                string delsrvpatch = Application.StartupPath + "/lpatchsrv.cfg";
            try
            {
             
                File.Delete(delsrvpatch);
            }
             catch (FileNotFoundException) {
            }
                string lpatchusr = System.IO.File.ReadAllText(Application.StartupPath + "/lpatchusr.cfg");
                WebClient wc = new WebClient();
                wc.Credentials = new NetworkCredential("anonymous", "anonymous");
                wc.DownloadFile("ftp://173.242.114.16/files/lpatch.cfg", Application.StartupPath + "/lpatchsrv.cfg");
            
          
        
         
                
            
        }
         
        

         private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
         {
             {
                 string lpatchusr = System.IO.File.ReadAllText(Application.StartupPath + "/lpatchusr.cfg");
                 string lpatchsrv = System.IO.File.ReadAllText(Application.StartupPath + "/lpatchsrv.cfg");


                 if (lpatchsrv == lpatchusr)
                 {

                     label2.ForeColor = Color.Aqua;
                     label2.Text = "GOOD!";


                 }
                 else
                 {

                     this.Hide();
                     Form3 form3 = new Form3();
                     form3.Show();
                 }


             }
         }
    }
}
