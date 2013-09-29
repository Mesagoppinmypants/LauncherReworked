using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

using PswgLauncher.Model.Tasks;
using PswgLauncher.Util;

namespace PswgLauncher.GUI.Forms
{

    public partial class LauncherWindow : Form
    {

    	int errorcounter = 0;
        
    	private bool InstallerAvailable=false;
  
        private OptionsWindow Options;
        private GuiController Controller;

        private LauncherProgressBar launcherProgressBarFile;
        private LauncherProgressBar launcherProgressBarTask;
        private LauncherProgressBar launcherProgressBarTotal;

        private LauncherButton MinimizeButton;
        private LauncherButton CloseButton;        
        private LauncherButton AcctButton;
        private LauncherButton OptButton;
        private LauncherButton ScanButton;
        private LauncherButton LOptButton;
        private LauncherButton DonateButton;
        private LauncherButton PlayButton;
        
        private System.Windows.Forms.Timer timer;
        
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private LauncherTask CurrentTask;
        
        public LauncherWindow(GuiController gc)
        {
        	
        	this.Controller = gc;

        	this.AutoScaleMode = AutoScaleMode.None;
        	
        	InitializeComponent();
        	InitializeComponent2();
        	
        	this.Show();
        	
            Point mouseDownPoint = Point.Empty;
            this.Process();
            
        }
        
        public void InitializeComponent2() {
        	
        	this.webBrowser1.Url = new System.Uri(ProgramConstants.UPDATENOTES, System.UriKind.Absolute);
        	
       		this.backgroundWorker = new BackgroundWorker();
       		this.backgroundWorker.WorkerReportsProgress = true;
        	//this.backgroundWorker.WorkerSupportsCancellation = true;
        	this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWDoWork);
        	this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BWProgressChanged);
        	this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BWCompleted);
        	
        	// modify existing components from designer

        	this.Region = System.Drawing.Region.FromHrgn(GuiController.CreateRoundRectRgn( 0, 0, Width, Height, 24, 24));      	
        	this.Icon= Controller.GetAppIcon();
        	this.BackgroundImage = Controller.GetResourceImage("Background_Launcher");

        	MinimizeButton = Controller.SpawnMinimizeButton(new Point(535, 20));
        	CloseButton = Controller.SpawnCloseButton(new Point(562, 8));
        	
        	MinimizeButton.Click += MinimizeClick;
        	CloseButton.Click += CloseClick;
        	
        	this.Controls.Add(MinimizeButton);
        	this.Controls.Add(CloseButton);
        	
        	//add new components
        	launcherProgressBarFile = Controller.SpawnProgressBar(new System.Drawing.Point(23, 421), new System.Drawing.Size(441, 17));
        	launcherProgressBarTask = Controller.SpawnProgressBar(new System.Drawing.Point(23, 439), new System.Drawing.Size(441, 17));        	
        	launcherProgressBarTotal = Controller.SpawnProgressBar(new System.Drawing.Point(23, 457), new System.Drawing.Size(441, 17));
        	this.Controls.Add(launcherProgressBarFile);
        	this.Controls.Add(launcherProgressBarTask);
        	this.Controls.Add(launcherProgressBarTotal);
        	
        	AcctButton = Controller.SpawnStandardButton("My Account", new Point(10, 365	));
        	OptButton = Controller.SpawnStandardButton("Game options", new Point(126, 365 ));
        	ScanButton = Controller.SpawnStandardButton("Scan", new Point(242, 365 ));
        	LOptButton = Controller.SpawnStandardButton("Launcher options", new Point(358, 365));
        	DonateButton = Controller.SpawnStandardButton("Donate", new Point(474, 365));
        	
        	PlayButton = Controller.SpawnPlayButton("Play", new Point(484,434));

        	AcctButton.Click += acct_Click_1;
        	OptButton.Click += options_Click_1;
        	ScanButton.Click += Scan_Click;
        	LOptButton.Click += button2_Click;
        	DonateButton.Click += Donate_Click;
        	PlayButton.Click += PLAY_Click_1;
        	
        	this.Controls.Add(AcctButton);
        	this.Controls.Add(OptButton);
        	this.Controls.Add(ScanButton);
        	this.Controls.Add(LOptButton);
        	this.Controls.Add(DonateButton);
        	this.Controls.Add(PlayButton);
        	        	        	
        	CurrentTask = new RunTask();
        	
        }        

        public void Process() {

        	if (CurrentTask == null) {
        		Application.Exit();
        		return;
        	}
        	
        	if (CurrentTask.GetBusy()) { return; }
        	
        	CurrentTask.Init(backgroundWorker, Controller);
        	backgroundWorker.RunWorkerAsync();

        }
        
        private void BWCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
        	
        	if (CurrentTask.Complete(backgroundWorker, Controller, sender, e)) {
        		
        		CurrentTask = CurrentTask.GetNextTask(Controller);
        		Process();
        		
        	}
        	
        }
        	
        private void BWProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e) {
        	
        	if (CurrentTask != null) {
        		if (CurrentTask.ReportProgress(backgroundWorker, Controller, sender, e)) {
        			return;
        		}
        	}
        	
	       	if (e.UserState != null) {
	        	
        		String s = e.UserState as String;
	        	//FIXME: this is a hack. got a better idea?
	        	String[] msg = s.Split(null,2);
	        	
	        	if (msg[0] == "Debug") {
	        		Controller.AddDebugMessage(msg[1]);
	        		return;
	        	}
	        	
	        	this.launcherProgressBarTotal.Text = s;
	        	this.launcherProgressBarTotal.Refresh();
	        	
	        	if (msg[0] == "Patched" || msg[0] == "Read" || msg[0] == "OK") {
	        		
	        		if (Controller.SWGFiles.SwgFileTable.ContainsKey(msg[1])) {

	        			//Controller.SWGFiles.AddGoodFile(msg[1]);
	        			//Controller.SWGFiles.SwgFileTable.Remove(msg[1]);
	        		}
	        	}
        	
	        	if (msg[0] == "installer") {
	        		Controller.AddDebugMessage(msg[1]);
	        		if (msg[1] == "available") {
	        			Controller.AddDebugMessage("xyz");
	        			InstallerAvailable = true;
	        		}
	        	}
        	}
        	
        	launcherProgressBarTask.Value = ((e.ProgressPercentage > 100) ? 100 : e.ProgressPercentage );
        	RefreshStatus(false);
        
        }

        
        private void BWDoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
        	if (CurrentTask != null) {
        		CurrentTask.Work(backgroundWorker, Controller, sender, e);        		
        	}
        }
                
        public bool RefreshStatus(bool updating) {
	
       		/*if (updating) {
        		launcherProgressBarTask.Text = "";
        	}*/
        	
        	OptButton.Disable = !File.Exists(Controller.SwgSavePath + @"\swgclientsetup_r.exe");

        	if (Options != null) {
        		Options.RefreshButtonState();
        	}
        	
        	PlayButton.Disable = CurrentTask.GetPlayDisabled();
        	ScanButton.Disable = CurrentTask.GetScanDisabled();
        	pictureBox2.Image = ((CurrentTask.GetBusy()) ? Controller.GetResourceImage("small-loading") : null);
        	PlayButton.Text = CurrentTask.GetPlayText();
        	launcherProgressBarFile.Text = CurrentTask.GetFileText();
        	launcherProgressBarFile.Value = CurrentTask.GetFileProgress();
        	launcherProgressBarFile.ForeColor = CurrentTask.GetFileColor();
        	launcherProgressBarTask.Text = CurrentTask.GetTaskText();
        	launcherProgressBarTask.Value = CurrentTask.GetTaskProgress();
        	launcherProgressBarTask.ForeColor = CurrentTask.GetTaskColor();
        	launcherProgressBarTotal.Text = CurrentTask.GetTotalText();
        	launcherProgressBarTotal.Value = CurrentTask.GetTotalProgress();
        	launcherProgressBarTotal.ForeColor = CurrentTask.GetTotalColor();        	
        	this.Refresh();
        	
        	return true;
        	
        }

        private void PLAY_Click_1(object sender, EventArgs e) {

			int rv = CurrentTask.PlayClick(Controller);
			if (rv < 0) {
				return;
			}
			
			if (rv > 0) {
				CurrentTask = CurrentTask.GetNextTask(Controller);
				
			}
        	
        	Process();
			
        }


        private void Scan_Click(object sender, EventArgs e)
        {
        	
        	CurrentTask.ScanClick(Controller);

        }


        private void button2_Click(object sender, EventArgs e)
        {
        	
        	Controller.PlaySound("Sound_Click");
			
        	if (Options != null) {
        		Options.RefreshButtonState();
        		Options.Show();
        		Options.BringToFront();
        		return;
        	}
        	
            OptionsWindow launchoptions = new OptionsWindow(Controller);
            launchoptions.Show();
            Options = launchoptions;
        }


        Point mouseDownPoint = Point.Empty;
        private void launcher2_MouseDown(object sender, MouseEventArgs e)
        
        {
            mouseDownPoint = new Point(e.X, e.Y);
        
        }

        private void launcher2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void launcher2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownPoint = Point.Empty;
        }

        private void launcher2_MouseMove(object sender, MouseEventArgs e)
        {
        	if (mouseDownPoint.IsEmpty) {
                return;
        	}
            Form f = sender as Form;
            f.Location = new Point(f.Location.X + (e.X - mouseDownPoint.X), f.Location.Y + (e.Y - mouseDownPoint.Y));
        }

        
        private void Donate_Click(object sender, EventArgs e)
        {
        	Controller.PlaySound("Sound_Click");
            DonateWindow donate = new DonateWindow(Controller);
            donate.Show();
        }
        

        private void MinimizeClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void CloseClick(object sender, EventArgs e)
        {
        	/*
        	//FIXME
        	if (StatusProcessor.Status == (int) StatusProcessor.StatusCodes.UpdatingChecksum || StatusProcessor.Status == (int) StatusProcessor.StatusCodes.Patching)
            {
               
        		DialogResult res = MessageBox.Show("PSWG Launcher is still patching. Are you sure you want to close PSWG Launcher?", "Close ProjectSWG Launcher?",MessageBoxButtons.YesNo);
        		
        		if (res == DialogResult.No) {
        			return;
        		}
        		
            }
        	*/
            Application.Exit();
        }

        private void acct_Click_1(object sender, EventArgs e)
        {
        	
        	Controller.PlaySound("Sound_Click");
            AccountWindow acct = new AccountWindow(Controller);
            acct.Show();
        }
        
        private void options_Click_1(object sender, EventArgs e)
        {
            if (File.Exists(Controller.SwgSavePath + @"\SwgClientSetup_r.exe"))
            {
            	Controller.PlaySound("Sound_Click");
                System.Diagnostics.Process.Start(Controller.SwgSavePath + @"\SwgClientSetup_r.exe");
            }
            else
            {
            	Controller.AddDebugMessage("Cannot launch swgclientsetup_r.exe because it is missing.");
            	Controller.PlaySound("Sound_Error");
            }
        }        
        
        
    }
}
       
       

        


       
    

        
    


