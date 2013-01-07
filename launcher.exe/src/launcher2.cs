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

namespace PswgLauncher
{

    public partial class launcher2 : Form
    {


        string FTP = "ftp://173.242.114.16/files";
        string swgdirsave;
        string[] userdir;
        string checks;
        
        int status;
        bool ForceChecksums = false;
        
        enum StatusCodes :int {
        	
        	NoChecksum = 0,
        	UpdatingChecksum = 1,
        	ChecksumFailed = 2,
        	ChecksumOk = 3,
        	Patching = 4,
        	PatchingFailed = 5,
        	PatchingComplete = 6
        	
        }
        
        
		private LAUNCHOPTIONS OptionWindow;
        
        private GuiController Controller;
        
        public launcher2(GuiController gc)
        {
        	
        	this.Controller = gc;
        	InitializeComponent();
        	InitializeComponent2();
        	this.Show();
        	
        	swgdirsave = Application.StartupPath;  //loads the swg install directory from the patch file
        	Controller.AddDebugMessage(Controller.SwgDir);
        	userdir = Directory.GetFiles(Controller.SwgDir);  //gets a list of all files in the SWG directory 
        	
            
            
            Point mouseDownPoint = Point.Empty;
            
            
            this.Process();

            
            
           
        }
        
        public void InitializeComponent2() {

        	this.Region = System.Drawing.Region.FromHrgn(GuiController.CreateRoundRectRgn( 0, 0, Width, Height, 24, 24));      	
        	this.Icon= Controller.GetAppIcon();
        	this.BackgroundImage = Controller.GetResourceImage("Background_Launcher");
        	
        	this.button1.Image = Controller.GetResourceImage("WButton_minimize");
        	this.close.Image = Controller.GetResourceImage("WButton_close");
        	this.acct.Image = Controller.GetResourceImage("Button_MyAccount");
        	this.options.Image = Controller.GetResourceImage("Button_GameOptions");
        	this.scan.Image = Controller.GetResourceImage("Button_ScanFiles");
        	this.button2.Image = Controller.GetResourceImage("Button_LauncherOptions");
        	this.PLAY.Image = Controller.GetResourceImage("Button_playbad");
        	
			this.button1.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255); //Transparent
        	this.close.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        	this.acct.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        	this.options.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        	this.scan.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        	this.button2.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        	this.PLAY.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        

        private void Process() {
        	
        	
			
            if (status == null) { UpdateStatus((int)StatusCodes.NoChecksum); }
            
            if (status == (int) StatusCodes.NoChecksum || status == (int) StatusCodes.ChecksumFailed) {
            	
            	WebClient wc = new WebClient();
	            wc.Credentials = new NetworkCredential("anonymous","anonymous");

            	Controller.AddDebugMessage("Processing Checksums.");
            	this.ProcessChecksums(wc);
            }
            Controller.AddDebugMessage("Status is right now:" + status);
            if (status == (int) StatusCodes.ChecksumOk || status == (int) StatusCodes.PatchingFailed) {
            	
            	Controller.AddDebugMessage("Dispatching Worker.");
            	this.DispatchWorker();
            }
            

        }


        
        private void DispatchWorker() {
        	
        	if (status != (int)StatusCodes.ChecksumOk && status != (int) StatusCodes.PatchingFailed ) {
            	return;
            }
        	
        	progressBar1.Step = 100 / Controller.SWGFiles.SwgFileTable.Count;
        	progressBar1.Value = 0;
        	UpdateStatus((int)StatusCodes.Patching);
        	
        	backgroundWorker2.RunWorkerAsync();

        }
        

        private void UpdateStatus(int newstatus) {
        	
        	Controller.AddDebugMessage("New Status: " + newstatus);
        	
        	switch (newstatus) {
        		//checksums need downloading	
        		case (int) StatusCodes.NoChecksum:
        		
        			// only null to 0 is possible.
        		
        			if (status != null) { return; }
        		
        			status = 0;
        			this.progressBar1.ForeColor = System.Drawing.Color.Red;
                	PLAY.Image = Controller.GetResourceImage("Button_playbad");
                	label1.ForeColor = Color.Blue;
                	pictureBox2.Image = null;
                	label1.Text = "Checksums need Checking (" + newstatus + ")";
                	labelFilename.Text = "";
                	linkRetry.Visible = false;
                	linkRetryChecksums.Visible = false;
                	linkListMissing.Visible = false;
                	linkLabelContinueChecksum.Visible = false;
        			break;
        		case (int) StatusCodes.UpdatingChecksum:
        			// only 0 to 1 is possible.
        		
        			if (status != (int) StatusCodes.NoChecksum && status != (int) StatusCodes.ChecksumFailed) { return; }
        			
        			status = newstatus;
        			this.progressBar1.ForeColor = System.Drawing.Color.Red;
        			PLAY.Image = Controller.GetResourceImage("Button_playbad");
                	label1.ForeColor = Color.Blue;
                	pictureBox2.Image = Controller.GetResourceImage("small-loading");
                	label1.Text = "DL'ing Checksums (" + newstatus + ")";
                	labelFilename.Text = "";
                	linkRetry.Visible = false;
                	linkRetryChecksums.Visible = false;
                	linkListMissing.Visible = false;
					linkLabelContinueChecksum.Visible = false;
        			break;


        		case (int) StatusCodes.ChecksumFailed:
        			
        			//FIXME: add retry button
        			
        			// only from downloading to failed or OK
        			if (status != (int) StatusCodes.UpdatingChecksum) { return; }
        			
        			status = newstatus;
        			this.progressBar1.ForeColor = System.Drawing.Color.Red;
        			PLAY.Image = Controller.GetResourceImage("Button_playbad");
                	label1.ForeColor = Color.Red;
                	pictureBox2.Image = null;
                	label1.Text = "Checksum DL failed. (" + newstatus + ")";
                	labelFilename.Text = "";
                	linkRetry.Visible = false;
                	linkRetryChecksums.Visible = true;
                	linkListMissing.Visible = false;
                	if (Controller.SWGFiles.HasFileList) {
						linkLabelContinueChecksum.Visible = true;
                	} else {
                		linkLabelContinueChecksum.Visible = false;
                	}
        			break;
        			
        		//checksums present and working
        		case (int) StatusCodes.ChecksumOk:
        		
        			// only from downloading to failed or OK
        			if (status != (int) StatusCodes.UpdatingChecksum && status != (int) StatusCodes.PatchingComplete && status != (int) StatusCodes.ChecksumFailed )
        			{ return; }
        		
        			status = newstatus;
        			this.progressBar1.ForeColor = System.Drawing.Color.Red;
        			PLAY.Image = Controller.GetResourceImage("Button_playbad");
                	label1.ForeColor = Color.Green;
                	pictureBox2.Image = null;
                	label1.Text = "Checksums loaded. (" + newstatus + ")";
                	labelFilename.Text = "";
                	linkRetry.Visible = false;
                	linkRetryChecksums.Visible = false;
                	linkListMissing.Visible = false;
                	linkLabelContinueChecksum.Visible = false;
        			break;
        			
        		case (int) StatusCodes.Patching:
        		
        			if (status != (int) StatusCodes.ChecksumOk && status != (int) StatusCodes.PatchingFailed) { return; }
        		
        			status = newstatus;
        			
        			this.progressBar1.ForeColor = System.Drawing.Color.Red;
        			PLAY.Image = Controller.GetResourceImage("Button_playbad");
                	label1.ForeColor = Color.Blue;
                	pictureBox2.Image = Controller.GetResourceImage("small-loading");
                	label1.Text = "Patching (" + newstatus + ")";
                	linkRetry.Visible = false;
                	linkRetryChecksums.Visible = false;
                	linkListMissing.Visible = false;
					linkLabelContinueChecksum.Visible = false;                	
        			break;
        		
        		case (int) StatusCodes.PatchingFailed:
					//FIXME: add retry button
        			if (status != (int) StatusCodes.Patching) { return; }
        		
        			status = newstatus;
        			
        			this.progressBar1.ForeColor = System.Drawing.Color.Red;
        			PLAY.Image = Controller.GetResourceImage("Button_playbad");
                	label1.ForeColor = Color.Red;
                	pictureBox2.Image = null;
                	label1.Text = "Patching Failed (" + newstatus + ")";
                	linkRetry.Visible = true;
                	linkRetryChecksums.Visible = false;
                	linkListMissing.Visible = true;
                	linkLabelContinueChecksum.Visible = false;
        			break;
        		
        		case (int) StatusCodes.PatchingComplete:
        			
        			if (status != (int) StatusCodes.Patching) { return; }
        			
        			status = newstatus;
					this.progressBar1.ForeColor = System.Drawing.Color.Green;
            		label1.ForeColor = Color.Aqua;
            		label1.Text = "Ready to play! (" + newstatus + ")";
            		pictureBox2.Image = null;
            		PLAY.Image = Controller.GetResourceImage("Button_playgood");
            		linkRetry.Visible = false;
            		linkRetryChecksums.Visible = false;
                	linkListMissing.Visible = false;
					linkLabelContinueChecksum.Visible = false;
        			break;
        	}
        	
        }
        
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) //displays current project status
        {
            

        }

 

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void close_Click_1(object sender, EventArgs e)
        {
        	if (status == (int) StatusCodes.UpdatingChecksum || status == (int) StatusCodes.Patching)
            {
               
        		DialogResult res = MessageBox.Show("PSWG Launcher is still patching. Are you sure you want to close PSWG Launcher?", "Close ProjectSWG Launcher?",MessageBoxButtons.YesNo);
        		
        		if (res == DialogResult.No) {
        			return;
        		}
        		
            }
        	
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
            if (File.Exists(Application.StartupPath + "/SwgClientSetup_r.exe"))
            {
            	Controller.PlaySound("Sound_Click");
                System.Diagnostics.Process.Start(swgdirsave + "/SwgClientSetup_r.exe");
            }
            else
            {
            	Controller.AddDebugMessage("Cannot launch swgclientsetup_r.exe because it is missing.");
            	
            	Controller.PlaySound("Sound_Error");
            }
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
        	
        	// only run if checksums are present
		    if (status != (int)StatusCodes.Patching) {
		            	
		    	e.Cancel = true;
		        return;
		    }

        	BackgroundWorker backgroundWorker = sender as BackgroundWorker;
  			if(backgroundWorker != null)
  			{
	    		backgroundWorker.ReportProgress(0);
	    		
	    		
		
		            
		        WebClient wc = new WebClient();
		        wc.Credentials = new NetworkCredential("anonymous", "anonymous");
		
		            
		        Dictionary<String,String> files = Controller.SWGFiles.SwgFileTable;
		            
		        float step = (float) 100 / (float) files.Count;
		        int i = 0;
		            
		            
		            
		        foreach (KeyValuePair<String,String> file in files) {
		        	i++;
		            	
		        	// don't download good files again.
		            if (Controller.SWGFiles.isGood(file.Key)) {
		            	continue;
		            }
		            	
		        	int progress = Convert.ToInt32(i * step);
		        	//backgroundWorker.ReportProgress( progress, "Debug " + files.Count + " " +progress + " " + i + " "+ step);
		        	bool dlstatus;
		        	String theKey = file.Key;
		        	backgroundWorker.ReportProgress( progress, "Debug " + file.Key);
		            backgroundWorker.ReportProgress( progress, file.Key);
		            	

		            dlstatus = DownloadFile(file.Key, file.Value, wc,backgroundWorker, progress);
		            	
		            
		            if(backgroundWorker.CancellationPending) {
			      		e.Cancel = true;
			      		break;
			    	}
		            
		            
	    		}
            
	    		if(backgroundWorker.CancellationPending) {
			      e.Cancel = true;
			    }
			    else {
            		backgroundWorker.ReportProgress(100,"Patching finished.");
	    		}
  			}
        
        }
        
        private void backgroundWorker2_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
        	//Controller.AsyncSync();
        	
        	if (e.UserState != null) {
	        	this.labelFilename.Text = e.UserState as String;
	        	
	        	//FIXME: this is a hack. got a better idea?
	        	String[] msg = (e.UserState as String).Split(null,2);
	        	
	        	if (msg[0] == "Debug") {
	        		Controller.AddDebugMessage(msg[1]);
	        	}
	        	
	        	if (msg[0] == "Patched" || msg[0] == "Read" || msg[0] == "OK") {
	        		
	        		if (Controller.SWGFiles.SwgFileTable.ContainsKey(msg[1])) {

	        			Controller.SWGFiles.AddGoodFile(msg[1]);
	        			//Controller.SWGFiles.SwgFileTable.Remove(msg[1]);
	        		}
	        		
	        	}
        	}
        	
        	
        	
        	progressBar1.Value = ((e.ProgressPercentage > 100) ? 100 : e.ProgressPercentage );

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
        	
        	
        	if (ForceChecksums) {
        		ForceChecksums = false;
        	}
        	
	       	if (e.Cancelled || e.Error != null) {
        		
        		Debug.WriteLine("DL cancel/fail: " + (e.Cancelled ? "cancelled" : "") + (e.Error != null ? e.ToString() : ""));
        		this.labelFilename.Text ="";
        		UpdateStatus((int) StatusCodes.PatchingFailed);
        	} else {
        		
        		if (checkForCompleteness()) {
        			UpdateStatus((int) StatusCodes.PatchingComplete);
        		} else {
        			UpdateStatus((int) StatusCodes.PatchingFailed);
        		}
        		
        		
        	}
           
            
        }
        
        private bool checkForCompleteness() {
        	
        	foreach (KeyValuePair<String,String> file in Controller.SWGFiles.SwgFileTable) {
        		
        		if (Controller.SWGFiles.isGood(file.Key)) {
		           	continue;
		        }

        		return false;
        		
        	}
        	
        	return true;

        }
        
        private bool MakeDirIfRequired(String filename) {
        	
        	
        	if (!filename.Contains("/")) {
        		return true;
        	}
        	
        	// split on last /
        	Regex r = new Regex(@"^(.+)\/([^/]+)$");
        	
        	Match m1 = r.Match(filename);
        	
        	if (!m1.Success) {
        		return true;
        	}
	
        	try {
        		Directory.CreateDirectory(swgdirsave + "\\" + m1.Groups[1].Value);
        	} catch (Exception e) {
        		
        		return false;
        	}
        	
        	return true;
        	
        }
        
        
        private bool DownloadFile(String file, String checksum, WebClient webclient, BackgroundWorker backgroundWorker, int progress) {
        	
        	String path = swgdirsave + "\\" + file;
        	String localsrc = Controller.SwgDir + "\\" + file;
        	String remotesrc;
        	
        	if (file.Contains("/")) {
	        	if (!this.MakeDirIfRequired(file)) {
	        		backgroundWorker.ReportProgress( progress, "Debug " + "Couldnt create dir for file " + file);
	        		return false;
	        	}
        	}

        	remotesrc = FTP + "/" + file;
        	
        	try {
        	
	        	backgroundWorker.ReportProgress(progress, "Checking " + file);
	        	
	        	if (Controller.SWGFiles.isGood(file)) {
	        		backgroundWorker.ReportProgress( progress, "Debug " + "was already good: " + file);
	        		return true;
	        	}
	        	
	        	if (File.Exists(file) && !Controller.checksumOption && !this.ForceChecksums) {
	        		backgroundWorker.ReportProgress(progress, "OK " + file);
	        		return true;
	        	}
	        	
	        	if (compareCheckSum(path, checksum)) {
	        		backgroundWorker.ReportProgress( progress, "Debug " + "is good: " + file);
	        		backgroundWorker.ReportProgress(progress, "OK " + file);
	        		return true;
	        	}
	        	
	        	
	        	// only get tre files from local storage
	        	if (Regex.IsMatch(file, @"\.tre$", RegexOptions.IgnoreCase)) {
	        		
	        		backgroundWorker.ReportProgress(progress, "Checking SWGDir for " + file);
	        		
	        		
	        		if (compareCheckSum(localsrc, checksum)) {
	        		
						//File.Copy(localsrc, swgdirsave);
						File.Copy(@localsrc, @path, true);
	        			
	        			backgroundWorker.ReportProgress( progress, "Debug " + "Reading " + file);
	        			
	        			backgroundWorker.ReportProgress(progress, "Reading " + file);
	        			
	        			if (compareCheckSum(path,checksum) ) {
	        				backgroundWorker.ReportProgress( progress, "Debug " + "Read " + file);
	        				backgroundWorker.ReportProgress(progress, "Read " + file);
	        				return true;
	        			}
	        			
	        			return false;
	        		}
	        	}
	        	
	        	backgroundWorker.ReportProgress( progress, "Debug " + "Patching " + remotesrc);
				backgroundWorker.ReportProgress(progress, "Patching " + file);
	        	
				
	        	webclient.DownloadFile(remotesrc,path);
	        		
	        	if (compareCheckSum(path,checksum) ) {
	        		backgroundWorker.ReportProgress(progress, "Patched " + file);
	        		return true;
	        	}
        			
        	} catch(Exception e) {
        		backgroundWorker.ReportProgress(progress, "Error patching " + file);
        	}
        	
        	
        	return false;
        }
        
        
        private bool compareCheckSum(string Filepath,string Checksum) {
        	
        	if (!File.Exists(Filepath)) {
        		return false;
        	}

			System.IO.FileStream FileCheck = System.IO.File.OpenRead(Filepath);                
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] md5Hash = md5.ComputeHash(FileCheck);                
			FileCheck.Close();
			                
			string Calc =   BitConverter.ToString(md5Hash).Replace("-", "").ToLower();
			if (Calc == Checksum.ToLower()) {
				return true;
			}
			return false;                  
		}
        
        
        //FIXME: this might as well go in the Controller.
        private int ProcessChecksums(WebClient wc) {

        	UpdateStatus((int) StatusCodes.UpdatingChecksum);
        	
        	try {
        		
        		StreamReader sr = new StreamReader(wc.OpenRead(FTP + "/launcher.dl.dat"));
				
        		Controller.SWGFiles.CreateFileList(sr,true);

        		
        	} catch (Exception ex) {
        		
        		Controller.AddDebugMessage("chksum exception/download" + ex.ToString());
        		
        		UpdateStatus((int) StatusCodes.ChecksumFailed);
        		return -1;

        	}
        	
        	
        	if (!Controller.SWGFiles.HasFileList) {
        		Controller.AddDebugMessage("chksum exception/incomplete");
        		
        		UpdateStatus((int) StatusCodes.ChecksumFailed);
        		return -1;
        	}
        	
        	Controller.SWGFiles.WriteConfig(swgdirsave + @"\launcher.dl.dat");
        	
        	UpdateStatus((int) StatusCodes.ChecksumOk);
        	return 0;
        	
        }
        
        //FIXME: this should be in a different class.
        private void Download(WebClient wc, String Filename) {
        	
        	this.labelFilename.Text = Filename;
        	wc.DownloadFile(FTP + "/" + Filename, swgdirsave + "/" + Filename);
        	
        }



        private void PLAY_Click_1(object sender, EventArgs e)
        {
        	
        	
        	if (status != (int) StatusCodes.PatchingComplete) {
        		
        		Controller.PlaySound("Sound_Error");

        		return;
        	}
        	
              
        	Controller.PlaySound("Sound_Play");
            this.Hide();
            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential("anonymous", "anonymous");
            wc.DownloadFile(FTP + "/login.cfg", swgdirsave + "/login.cfg");
            Directory.SetCurrentDirectory(swgdirsave);
            System.Threading.Thread.Sleep(200);
            System.Diagnostics.Process.Start(swgdirsave + "/SwgClient_r.exe");
            System.Threading.Thread.Sleep(50000);
            File.Delete(swgdirsave + "/login.cfg");
            Application.Exit();
                   

        }

        private void options_SizeChanged(object sender, EventArgs e)
        {
              

			// !?! 
            //  backgroundWorker2.CancelAsync();
                
         
        }

        private void scan_Click(object sender, EventArgs e)
        {

        	
            //only scan if complete
            if (status != (int) StatusCodes.PatchingComplete && status != (int) StatusCodes.PatchingFailed)
            {
            
            	return;
            }
            
            Controller.PlaySound("Sound_Click");

			// reset what good files we thought we had
			Controller.SWGFiles.ResetGoodFiles();
			
			ForceChecksums = true;
			
			
			UpdateStatus((int) StatusCodes.ChecksumOk);
			Process();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        	
        	Controller.PlaySound("Sound_Click");
			
        	if (OptionWindow != null) {
        		OptionWindow.Show();
        		OptionWindow.BringToFront();
        		return;
        	}
        	
            LAUNCHOPTIONS launchoptions = new LAUNCHOPTIONS(Controller);
            launchoptions.Show();
            OptionWindow = launchoptions;
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

        
        
        void LinkRetryLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        	
        	
        	if (status != (int) StatusCodes.PatchingFailed) {
        		Controller.AddDebugMessage("Wrong Status");
        		return;
        	}
        	
        	Process();
        	
        }
        
        void LinkRetryChecksumsLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        	if (status != (int) StatusCodes.ChecksumFailed) {
        		return;
        	}
        	
        	Process();
        	
        }
        
        void LinkListMissingLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        	String missing = "Missing Files " + Environment.NewLine;
        	
        	
        	foreach (KeyValuePair<String,String> file in Controller.SWGFiles.SwgFileTable) {
        		
        		if (Controller.SWGFiles.isGood(file.Key)) {
		           	continue;
		        }

        		missing += file.Key + Environment.NewLine;
        		
        	}
        	
        	
        	MessageBox.Show(missing, "Missing Files", MessageBoxButtons.OK);

        	
        	
        }
        
        void LinkLabelContinueChecksumLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        	
        	UpdateStatus((int) StatusCodes.ChecksumOk);
        	Process();
        	
        	
        }
    }
}
       
       

        


       
    

        
    

