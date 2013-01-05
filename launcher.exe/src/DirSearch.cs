using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Media;



namespace PswgLauncher
{

    public partial class DirSearch : Form
    {
    	private GuiController Controller;
    	private String SwgDir;
    	Point mouseDownPoint = Point.Empty;
    	
        public DirSearch(GuiController gc)
        {
        	this.Controller = gc;
            InitializeComponent();
            InitializeComponent2();
        }
      
        
        private void InitializeComponent2() {
        	
        	this.Region = System.Drawing.Region.FromHrgn(GuiController.CreateRoundRectRgn( 0, 0, Width, Height, 24, 24));      	
        	this.Icon= Controller.GetAppIcon();
        	this.BackgroundImage = Controller.GetResourceImage("Background_DirSearch");
        	this.BrowseButton.Image = Controller.GetResourceImage("Button_Browse");
        	this.NextButton1.Image = Controller.GetResourceImage("Button_Next");
        	this.buttonClose.Image = Controller.GetResourceImage("WButton_close");
        	this.buttonMinimize.Image = Controller.GetResourceImage("WButton_minimize");
			this.BrowseButton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255); //Transparent
			this.NextButton1.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        	this.buttonClose.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        	this.buttonMinimize.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        
        


     

        private void Form1_Load(object sender, EventArgs e)
        {
        	
        }
        
       

        private void textBox1_TextChanged(object sender, EventArgs e)  //used to display dir path
        {

        }


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
        

        private void BrowseButton_Click(object sender, EventArgs e)  //to browse for SWG install dir
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
            if (Controller.soundOption) { player.Play(); }
            
            folderBrowserDialog1.ShowDialog();  //opens the file browse window
            SwgDir = folderBrowserDialog1.SelectedPath; // gathers data and writes it to swggetdir
            textBox2.Text = SwgDir;  // displays data to textbox1
            
            //FIXME this is a hack, clearly...
            string[] validfilenames = new String[] {
            	
            	"BugTool.exe",
				"SwgClient_r.exe",
				"SwgClientSetup_r.exe",
				"TreFix.exe",
				"*.tre",
				"*.toc",
				"dbghelp*.dll",
				"LP_Diagnostics.exe",
				"lp_manifest.cache",
				"favicon.ico",
				"Canada_SWG_Manual_French.pdf",
				"SWG_JP_*.pdf",
				"SWGExpPack_MG_*.pdf",
				"SWG-CoA Troubleshooting Guide v1b.rtf",
				"SWG troubleshooting guide.rtf",
				"SOE TOU*.doc",
				"Activision SLA*.doc",
				"characterlist_*.txt",
				"preload.cfg",
				"live.cfg",
				"client.cfg",
				"login.cfg",
				"SWGVoiceService.exe",
				"*vivox*",
				"lp_dldat.ctl",
				"local_machine_options.default",
				"local_machine_options.low",
				"options.default",
				"options.low",
				"SwgClient_r.exe-stage.*"
            };
            
            
            try
            {
                    	
            	bool isvaliddirectory = false;
                    	
                foreach (String f in validfilenames) {
                	string[] files = Directory.GetFiles(SwgDir, f, SearchOption.AllDirectories);
                    		
                    if (files.Length > 0) {
                    			
                    	isvaliddirectory = true;
                    	break;
                    }
                    		
                }
                    	
                           
                if (isvaliddirectory)
                //this if is for valid install dir
                {
                                
                    	richTextBox1.ForeColor = Color.Green;
                        richTextBox1.Text = "SWG INSTALLATION FOUND";


                }
                else  //all else statements result in invalid directory error (also intended to prevent out of range, etc)
                {
                    	richTextBox1.ForeColor = Color.Red;
                        richTextBox1.Text = "   INVALID STAR WARS GALAXIES DIRECTORY";

                }
                            
                           
                }
            
            catch (Exception ex) {
            	
            	if (ex is UnauthorizedAccessException || ex is IndexOutOfRangeException) {
	            	richTextBox1.ForeColor = Color.Red;
	                richTextBox1.Text = "   INVALID STAR WARS GALAXIES DIRECTORY";
            	}
                
            } 
          
        }
        


        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)//used when searching directory
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)//used to display directory data to the user
        {

        }
        private void NextButton1_Click(object sender, EventArgs e)  //used to get to launcher if directory is valid
        {
            if (richTextBox1.Text ==  "SWG INSTALLATION FOUND")
            {
            	
            	
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
                if (Controller.soundOption) { player.Play(); }
                
                
                Controller.SwgDir = SwgDir;
                
                this.DialogResult = DialogResult.OK;
                           

            }
            else  //this prevents non valid directories from working
            {
                errordir errordir = new errordir(Controller); //brings up invalid directory error window
                errordir.Show();
                
            }

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
        	this.WindowState = FormWindowState.Minimized;
             
        }

       
       
    }
}

// int filecheck1; filecheck1 = swgclient_r.exe; if filecheck1 = swggetdir/SwgClient_r.exe