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


    	int errorcounter = 0;
    	
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
        
        private LauncherProgressBar launcherProgressBar1;

        private LauncherButton MinimizeButton;
        private LauncherButton CloseButton;        
        private LauncherButton AcctButton;
        private LauncherButton OptButton;
        private LauncherButton ScanButton;
        private LauncherButton LOptButton;
        private LauncherButton DonateButton;
        private LauncherButton PlayButton;
        
        private LauncherLabel label1;
        private LauncherLabel labelError;
        
        private System.Windows.Forms.Timer timer;
        
        
        public launcher2(GuiController gc)
        {
        	
        	this.Controller = gc;

        	this.AutoScaleMode = AutoScaleMode.None;
        	
        	InitializeComponent();
        	InitializeComponent2();
        	
        	
        	
        	this.Show();
        	
        	Controller.AddDebugMessage(Controller.SwgDir);
        	userdir = Directory.GetFiles(Controller.SwgDir);  //gets a list of all files in the SWG directory 
            
            Point mouseDownPoint = Point.Empty;
            
            
            this.Process();
            
           
        }
        
        public void InitializeComponent2() {
        	
        	
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
        	
        	launcherProgressBar1 = Controller.SpawnProgressBar(new System.Drawing.Point(23, 452), new System.Drawing.Size(441, 17));
        	this.Controls.Add(launcherProgressBar1);
        	
        	AcctButton = Controller.SpawnStandardButton("My Account", new Point(10, 365	));
        	OptButton = Controller.SpawnStandardButton("Game options", new Point(126, 365 ));
        	ScanButton = Controller.SpawnStandardButton("Scan", new Point(242, 365 ));
        	LOptButton = Controller.SpawnStandardButton("Launcher options", new Point(358, 365));
        	DonateButton = Controller.SpawnStandardButton("Donate", new Point(474, 365));
        	
        	PlayButton = Controller.SpawnPlayButton("Play", new Point(484,434));

        	AcctButton.Click += acct_Click_1;
        	OptButton.Click += options_Click_1;
        	ScanButton.Click += scan_Click;
        	LOptButton.Click += button2_Click;
        	DonateButton.Click += Donate_Click;
        	PlayButton.Click += PLAY_Click_1;
        	
        	this.Controls.Add(AcctButton);
        	this.Controls.Add(OptButton);
        	this.Controls.Add(ScanButton);
        	this.Controls.Add(LOptButton);
        	this.Controls.Add(DonateButton);
        	this.Controls.Add(PlayButton);
        	
        	label1 = Controller.SpawnLabel("", new Point(23, 415), new Size(160, 15));
        	this.Controls.Add(label1);
        	
        	labelError = Controller.SpawnLabel("", new Point(23, 430), new Size(160, 15));
        	this.Controls.Add(labelError);
        	
        	
        }
        

        private void Process() {
        	
        	
			
            if (status == null) {
        		
        		UpdateStatus((int)StatusCodes.NoChecksum);
        		
        	}
            
            if (status == (int) StatusCodes.NoChecksum || status == (int) StatusCodes.ChecksumFailed) {
            	
        		
            	WebClient wc = new WebClient();
            	wc.Encoding = System.Text.Encoding.UTF8;
            	wc.Credentials = Controller.GetNetworkCredential();

            	Controller.AddDebugMessage("Processing Checksums.");
            	
            	bool rv = false;
				int c = 0;
            	
				while (rv == false) {
					
            		if (c>0) {
						UpdateStatus((int) StatusCodes.ChecksumFailed);
            			errorcounter++;
            			UpdateErrors();

		            	DialogResult res = MessageBox.Show("File checksums could not be read from server. Unfortunately we don't have a local copy that we could use instead. You can retry with OK button now or try again later.", "Retry downloading?",MessageBoxButtons.OKCancel);
        		
			        	if (res == DialogResult.Cancel) {
			        		return;
			        	}
            			
            		}
					
					UpdateStatus((int) StatusCodes.UpdatingChecksum);
					
					c++;
					rv = this.ProcessChecksums(wc);
				}
            	
            	
            	UpdateStatus((int) StatusCodes.ChecksumOk);

            	
            }
        	
            Controller.AddDebugMessage("Status is right now:" + status);
            if (status == (int) StatusCodes.ChecksumOk || status == (int) StatusCodes.PatchingFailed) {
            	
            	Controller.AddDebugMessage("Dispatching Worker.");
            	this.DispatchWorker();
            }
            

        }
        
        
        private void UpdateErrors() {
        	if (errorcounter > 0) {
        		labelError.Text = "Download Errors: " + errorcounter;
        	} else {
        		labelError.Text = "";
        	}
        }


        
        private void DispatchWorker() {
        	
        	UpdateErrors();
        	
        	if (status != (int)StatusCodes.ChecksumOk && status != (int) StatusCodes.PatchingFailed ) {
            	return;
            }
        	
        	launcherProgressBar1.Step = 100 / Controller.SWGFiles.SwgFileTable.Count;
        	launcherProgressBar1.Value = 0;
        	UpdateStatus((int)StatusCodes.Patching);
        	
        	backgroundWorker2.RunWorkerAsync();

        }
        

        private void RefreshButtonState() {
        	
        	OptButton.Disable = !File.Exists(Controller.SwgSavePath + @"\swgclientsetup_r.exe");
        	
        	if (OptionWindow != null) {
        		OptionWindow.RefreshButtonState();
        	}
        	
        }
        
        private void UpdateStatus(int newstatus) {
        	
        	Controller.AddDebugMessage("New Status: " + newstatus);
        	
        	RefreshButtonState();
        	
        	switch (newstatus) {
        		//checksums need downloading	
        		case (int) StatusCodes.NoChecksum:
        		
        			// only null to 0 is possible.
        		
        			if (status != null ) { return; }
        		
        			status = 0;
        			this.launcherProgressBar1.ForeColor = System.Drawing.Color.Red;
        			PlayButton.Disable = true;
        			PlayButton.Text = "Play";
                	label1.ForeColor = Color.Blue;
                	pictureBox2.Image = null;
                	label1.Text = "Checksums need Checking (" + newstatus + ")";
                	launcherProgressBar1.Text = "";
                	ScanButton.Disable = true;
        			break;
        		case (int) StatusCodes.UpdatingChecksum:
        			// only 0or2 to 1 is possible.
        		
        			if (status != (int) StatusCodes.NoChecksum && status != (int) StatusCodes.ChecksumFailed) { return; }
        			status = newstatus;
        			this.launcherProgressBar1.ForeColor = System.Drawing.Color.Red;
        			PlayButton.Disable = true;
        			PlayButton.Text = "Play";
                	label1.ForeColor = Color.Blue;
                	pictureBox2.Image = Controller.GetResourceImage("small-loading");
                	label1.Text = "DL'ing Checksums (" + newstatus + ")";
                	launcherProgressBar1.Text = "";
					ScanButton.Disable = true;
        			break;


        		case (int) StatusCodes.ChecksumFailed:
        			
        			// only from downloading to failed or OK
        			if (status != (int) StatusCodes.UpdatingChecksum) { return; }
        			
        			status = newstatus;
        			this.launcherProgressBar1.ForeColor = System.Drawing.Color.Red;
        			PlayButton.Disable = false;
        			PlayButton.Text = "Retry";
                	label1.ForeColor = Color.Red;
                	pictureBox2.Image = null;
                	label1.Text = "Checksum DL failed. (" + newstatus + ")";
                	launcherProgressBar1.Text = "";

                	ScanButton.Disable = true;
        			break;
        			
        		//checksums present and working
        		case (int) StatusCodes.ChecksumOk:
        		
        			// only from downloading to failed or OK
        			if (status != (int) StatusCodes.UpdatingChecksum && status != (int) StatusCodes.PatchingComplete && status != (int) StatusCodes.ChecksumFailed )
        			{ return; }
        		
        			status = newstatus;
        			this.launcherProgressBar1.ForeColor = System.Drawing.Color.Red;
        			PlayButton.Disable = true;
        			PlayButton.Text = "Play";
                	label1.ForeColor = Color.Green;
                	pictureBox2.Image = null;
                	label1.Text = "Checksums loaded. (" + newstatus + ")";
                	launcherProgressBar1.Text = "";
                	ScanButton.Disable = false;
        			break;
        			
        		case (int) StatusCodes.Patching:
        		
        			if (status != (int) StatusCodes.ChecksumOk && status != (int) StatusCodes.PatchingFailed) { return; }
        		
        			status = newstatus;
        			
        			this.launcherProgressBar1.ForeColor = System.Drawing.Color.Red;
        			PlayButton.Disable = true;
        			PlayButton.Text = "Play";
                	label1.ForeColor = Color.Blue;
                	pictureBox2.Image = Controller.GetResourceImage("small-loading");
                	label1.Text = "Patching (" + newstatus + ")";
					ScanButton.Disable = true;
        			break;
        		
        		case (int) StatusCodes.PatchingFailed:
					//FIXME: add retry button
        			if (status != (int) StatusCodes.Patching) { return; }
        		
        			status = newstatus;
        			
        			this.launcherProgressBar1.ForeColor = System.Drawing.Color.Red;
        			PlayButton.Disable = false;
        			PlayButton.Text = "Retry";
                	label1.ForeColor = Color.Red;
                	pictureBox2.Image = null;
                	label1.Text = "Patching Failed (" + newstatus + ")";
                	ScanButton.Disable = false;
        			break;
        		
        		case (int) StatusCodes.PatchingComplete:
        			
        			if (status != (int) StatusCodes.Patching) { return; }
        			
        			status = newstatus;
					this.launcherProgressBar1.ForeColor = System.Drawing.Color.Green;
            		label1.ForeColor = Color.Aqua;
            		label1.Text = "Ready to play! (" + newstatus + ")";
            		pictureBox2.Image = null;
        			PlayButton.Disable = false;
        			PlayButton.Text = "Play";
					ScanButton.Disable = false;
        			break;
        	}
        	
        	this.Refresh();
        	
        	
        }
        

        
        

        private void MinimizeClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void CloseClick(object sender, EventArgs e)
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
	   
		        Dictionary<String,SWGFile> files = Controller.SWGFiles.SwgFileTable;
		            
		        float step = (float) 100 / (float) files.Count;
		        int i = 0;
		            
		        foreach (KeyValuePair<String,SWGFile> file in files) {
		        	i++;
		            	
		        	// don't download good files again.
		            if (Controller.SWGFiles.isGood(file.Key)) {
		            	continue;
		            }
		            	
		        	int progress = Convert.ToInt32(i * step);
		        	//backgroundWorker.ReportProgress( progress, "Debug " + files.Count + " " +progress + " " + i + " "+ step);
		        	bool dlstatus = false;
		        	String theKey = file.Key;
		        	backgroundWorker.ReportProgress( progress, "Debug " + file.Key);
		            backgroundWorker.ReportProgress( progress, file.Key);
					
					while (dlstatus == false && !backgroundWorker.CancellationPending) {
		            	dlstatus = DownloadFile(file.Key, file.Value, backgroundWorker, progress);
					}
		            
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
	        	
        		String s = e.UserState as String;
	        	
	        	//FIXME: this is a hack. got a better idea?
	        	String[] msg = s.Split(null,2);
	        	
	        	if (msg[0] == "Debug") {
	        		Controller.AddDebugMessage(msg[1]);
	        		return;
	        	}
	        	
	        	this.launcherProgressBar1.Text = s;
	        	this.launcherProgressBar1.Refresh();

	        	if (msg[0] == "Error") {
	        		errorcounter++;
	        		UpdateErrors();
	        	}
	        	
	        	if (msg[0] == "Patched" || msg[0] == "Read" || msg[0] == "OK") {
	        		
	        		if (Controller.SWGFiles.SwgFileTable.ContainsKey(msg[1])) {

	        			Controller.SWGFiles.AddGoodFile(msg[1]);
	        			//Controller.SWGFiles.SwgFileTable.Remove(msg[1]);
	        		}
	        		
	        		RefreshButtonState();
	        		
	        	}
        	}
        	

        	
        	launcherProgressBar1.Value = ((e.ProgressPercentage > 100) ? 100 : e.ProgressPercentage );
        	RefreshButtonState();

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
        	
        	bool scanned = false;
        	//this needs to be checked if interruptions are possible.
        	if (ForceChecksums) {
        		
        		scanned = true;
        		ForceChecksums = false;
        	}
        	
	       	if (e.Cancelled || e.Error != null) {
        		
        		Debug.WriteLine("DL cancel/fail: " + (e.Cancelled ? "cancelled" : "") + (e.Error != null ? e.ToString() : ""));
        		this.launcherProgressBar1.Text = "";
        		UpdateStatus((int) StatusCodes.PatchingFailed);
        	} else {
        		
        		if (checkForCompleteness()) {
        			if (scanned) {
        				Controller.SaveScanComplete();
        			}
        			UpdateStatus((int) StatusCodes.PatchingComplete);
        		} else {
        			UpdateStatus((int) StatusCodes.PatchingFailed);
        		}
        		
        		
        	}
           
            
        }
        
        private bool checkForCompleteness() {
        	
        	foreach (KeyValuePair<String,SWGFile> file in Controller.SWGFiles.SwgFileTable) {
        		
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
        		Directory.CreateDirectory(Controller.SwgSavePath + @"\" + m1.Groups[1].Value);
        	} catch (Exception e) {
        		
        		return false;
        	}
        	
        	return true;
        	
        }
        
        
        private bool DownloadFile(String file, SWGFile swgfile, BackgroundWorker backgroundWorker, int progress) {
        	
        	String path = Controller.SwgSavePath + @"\" + file;
        	String localsrc = Controller.SwgDir + @"\" + file;
        	String remotesrc = GuiController.MAINURL + "/" + file;
        	long offset = 0;
        	
        	String checksum = swgfile.Checksum;
        	
        	if (file.Contains("/")) {
	        	if (!this.MakeDirIfRequired(file)) {
	        		backgroundWorker.ReportProgress( progress, "Debug " + "Couldnt create dir for file " + file);
	        		return false;
	        	}
        	}

	        backgroundWorker.ReportProgress(progress, "Checking " + file);
	        
	        // empty files just need to have their dir created.
	        if (swgfile.Filesize <= 0) {
	        	
		        backgroundWorker.ReportProgress( progress, "Debug " + "empty file is good: " + file);
		        backgroundWorker.ReportProgress(progress, "OK " + file);
    	
	        	return true;
	        }

        	try {
	        
		        if (Controller.SWGFiles.isGood(file)) {
		        	backgroundWorker.ReportProgress( progress, "Debug " + "was already good: " + file);
		        	backgroundWorker.ReportProgress(progress, "OK " + file);
		        	return true;
		        }
		        
		        if (File.Exists(path)) {
		        	if (!swgfile.Strict) {
			        	backgroundWorker.ReportProgress( progress, "Debug " + "file exists and is not strict, skipping checksum for " + file);
			        	backgroundWorker.ReportProgress(progress, "OK " + file);
			        	return true;
		        	}
		        	
		        	FileInfo f = new FileInfo(path);
		        	
		        	if (f.Length == swgfile.Filesize) {
		        		
		        		if (!Controller.checksumOption && !this.ForceChecksums) {
			        		backgroundWorker.ReportProgress( progress, "Debug " + "file exists, skipping checksum for " + file);
			        		backgroundWorker.ReportProgress(progress, "OK " + file);		        			
		        			return true;
		        		}
		        		

			        	if (compareCheckSum(path, checksum)) {
			        		backgroundWorker.ReportProgress( progress, "Debug " + "is good: " + file);
			        		backgroundWorker.ReportProgress(progress, "OK " + file);
			        		return true;
		        		} else {
		        			backgroundWorker.ReportProgress( progress, "Debug " + "checksum check failed for " + file);
		        		}
		        		
		        		
		        	} else if (f.Length < swgfile.Filesize) {
		        		if (Controller.ResumeOption) {
		        			offset = f.Length;
		        		}
		        	}
		        	
		        }
	        
	        	
	        	// only get tre files from local storage
	        	if (Regex.IsMatch(file, @"\.tre$", RegexOptions.IgnoreCase)) {
	        		
	        		backgroundWorker.ReportProgress(progress, "Checking SWGDir for " + file);
	        		
	        		if (File.Exists(localsrc)) {
	        		
		        		if (compareCheckSum(localsrc, checksum)) {
	
		        			backgroundWorker.ReportProgress(progress, "Debug " + "Reading " + file);
		        			backgroundWorker.ReportProgress(progress, "Reading " + file);
		        			
							//File.Copy(localsrc, swgdirsave);
							File.Copy(@localsrc, @path, true);
		        			
		        			
		        			if (compareCheckSum(path,checksum) ) {
		        				backgroundWorker.ReportProgress(progress, "Debug " + "Read " + file);
		        				backgroundWorker.ReportProgress(progress, "Read " + file);
		        				return true;
		        			}
		        			
							backgroundWorker.ReportProgress(progress, "Debug " + "Postcopy checksum mismatch (rare!) " + file);
		        			return false;
		        		} else {
		        			backgroundWorker.ReportProgress(progress, "Debug " + "found locally but checksum mismatch. " + file);
		        		}
	        		}
	        	}
	        	
	        	backgroundWorker.ReportProgress(progress, "Debug " + "Patching " + file);
				backgroundWorker.ReportProgress(progress, "Patching " + file);
				
				long Filesize = 0;
				
				
				while (Filesize < swgfile.Filesize) {
					
					HTTPDownload(swgfile, remotesrc, offset, backgroundWorker, progress);
					
					if (File.Exists(path)) {
						FileInfo f = new FileInfo(path);
						Filesize = f.Length;
						offset = Filesize;
					}
					
				}
	        	
				
	        	if (compareCheckSum(path,checksum) ) {
	        		backgroundWorker.ReportProgress(progress, "Debug " + "Patched " + file);
	        		backgroundWorker.ReportProgress(progress, "Patched " + file);
	        		return true;
				} else {
					backgroundWorker.ReportProgress(progress, "Debug " + "Postpatch checksum mismatch " + file);
				}
        			
	        } catch (Exception ex) {
        		
	        	backgroundWorker.ReportProgress(progress, "Debug " + "Error patching " + file + " " + ex);
        	}
        	
        	backgroundWorker.ReportProgress(progress, "Debug " + "Error patching " + file);
        	backgroundWorker.ReportProgress(progress, "Error patching " + file);
        	
        	return false;
        }
        
        
        private void HTTPDownload(SWGFile swgfile, String remoteURL, long offset, BackgroundWorker backgroundWorker, int progress) {

        	bool append = false;
        	FileMode mode;
        	
			if (Controller.ResumeOption && offset > 0) {
        		
				backgroundWorker.ReportProgress(progress, "Debug " + "Resume from " + offset);
				append = true;
				mode = FileMode.Append;
				
			} else {
				mode = FileMode.Create;
			}
        	
        	using ( FileStream OutputStream = new FileStream(swgfile.Filename, mode) ) {
        		
        		HttpWebRequest WebReq = (HttpWebRequest) HttpWebRequest.Create(new Uri(remoteURL));

        		if ( append ) {
        			WebReq.AddRange(offset);
			    }
        		
				WebReq.Credentials = Controller.GetNetworkCredential();
        		
        		using (HttpWebResponse response = (HttpWebResponse) WebReq.GetResponse()) {
        			using (Stream webStream = response.GetResponseStream()) {
						
						long length = response.ContentLength;
						int bufsiz = 2048;
						byte[] buffer = new byte[bufsiz];
										
						int readcount = webStream.Read(buffer, 0, bufsiz);
										
						while (readcount > 0) {
							
							OutputStream.Write(buffer,0,readcount);
							
							readcount = webStream.Read(buffer, 0, bufsiz);
							
						} 
        			}
        		}
        	}
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
        private bool ProcessChecksums(WebClient wc) {
	
        	bool read = false;
        	
        	try {
        		
        		using (StreamReader sr = new StreamReader(wc.OpenRead(GuiController.MAINURL + "/launcherS.dl.dat"))) {
				
        			read = Controller.SWGFiles.CreateFileList(sr,true);
        		}

        		
        	} catch {}
        	
        	if (!read) {
        		Controller.AddDebugMessage("chksum exception/incomplete while reading from server.");
        	}
        	
        	if (!Controller.SWGFiles.HasFileList) {
        		Controller.AddDebugMessage("can't use local checksums");
        		return false;
        	}
        	
        	Controller.AddDebugMessage("using local checksums");

        	Controller.SWGFiles.WriteConfig(Controller.LocalFilelist);
        	
        	return true;
        	
        }
        


        private void PLAY_Click_1(object sender, EventArgs e) {
        	
        	if (status == (int) StatusCodes.ChecksumFailed || status == (int) StatusCodes.PatchingFailed) {
        		Process();
        		return;
        	}

        	
        	if (status != (int) StatusCodes.PatchingComplete) {
        		Controller.PlaySound("Sound_Error");
        		return;
        	}
        	
        	/*
        	 * this needs some tweaking :P
        	SWGFile swgcl;
        	Controller.SWGFiles.SwgFileTable.TryGetValue("SWGClient_r.exe", out swgcl);
        	Controller.AddDebugMessage(swgcl.Checksum);
        	if (swgcl == null || !compareCheckSum(swgdirsave + @"\SwgClient_r.exe", swgcl.Checksum)) {
        		
        		MessageBox.Show("SwgClient_r.exe is not OK. Please use the scan button to fix.","SwgClient not OK", MessageBoxButtons.OK, MessageBoxIcon.Error);
        		
        		return;
        	}*/
        	

        	// this is actually "play" here now.
        	Controller.PlaySound("Sound_Play");

        	String address = "login1.projectswg.com";
        	String port = "44453";
       	
        	if (Controller.LocalhostOption) {
        		address = "127.0.0.1";
        		port = "44453";

        	String args = String.Format("-- -s Station subscriptionFeatures=1 gameFeatures=34374193 -s ClientGame loginServerPort0={0} loginServerAddress0={1}", port, address);
        	
        	
        	this.Hide();
            Directory.SetCurrentDirectory(swgdirsave);
            System.Threading.Thread.Sleep(200);

        	try {
	        	this.Hide();
        		System.Diagnostics.Process.Start(Controller.FileSWGClient, args);
	            Application.Exit();
        			
        	} catch {
        		
        		GuiController.ShowErrorPermissions("Error starting swgclient_r.exe");
        	}


            return;
        	
        	
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


        private void button2_Click(object sender, EventArgs e)
        {
        	
        	Controller.PlaySound("Sound_Click");
			
        	if (OptionWindow != null) {
        		OptionWindow.RefreshButtonState();
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

        
        private void Donate_Click(object sender, EventArgs e)
        {
        	Controller.PlaySound("Sound_Click");
            Donate donate = new Donate(Controller);
            donate.Show();
        }
        

    }
}
       
       

        


       
    

        
    


