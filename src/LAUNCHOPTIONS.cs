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

namespace PswgLauncher
{
    public partial class LAUNCHOPTIONS : Form
    {
    	
    	private GuiController Controller;
    	
        public LAUNCHOPTIONS(GuiController gc)
        {
        	this.Controller = gc;
            InitializeComponent();
            
             soundControl.Checked = Controller.soundOption;
             checksumControl.Checked = Controller.checksumOption;
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
            if (Controller.soundOption) { player.Play(); }
            Donate donate = new Donate();
            donate.Show();
        }

        private void Support_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
            if (Controller.soundOption) { player.Play(); }
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "/TREFix.exe"))
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
                if (Controller.soundOption) { player.Play(); }
                System.Diagnostics.Process.Start(Application.StartupPath + "/TREFix.exe");
            }

            else
            {

                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Error.wav");
                if (Controller.soundOption) { player.Play(); }
            }

        }
   

        void ChecksumControlCheckedChanged(object sender, EventArgs e)
        {
        	
        	Controller.checksumOption = checksumControl.Checked;
        	
        }
        
        
        void SoundControlCheckedChanged(object sender, EventArgs e)
        {
        	
        	Controller.soundOption = soundControl.Checked;
        	
        }
        
        void LinkDebugWindowLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        	Controller.LaunchDebug();
        }
    }
}
