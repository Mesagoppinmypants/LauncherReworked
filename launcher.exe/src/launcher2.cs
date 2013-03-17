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
        
        public StatusProcessor StatusProcessor;
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
        
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorkerScan;
        private System.ComponentModel.BackgroundWorker backgroundWorkerScanManual;
        
        public launcher2(GuiController gc)
        {
        	
        	this.Controller = gc;
        	this.StatusProcessor = new StatusProcessor(gc);

        	this.AutoScaleMode = AutoScaleMode.None;
        	
        	InitializeComponent();
        	InitializeComponent2();
        	
        	this.Show();
        	
            Point mouseDownPoint = Point.Empty;
            this.Process();
            
        }
        
        public void InitializeComponent2() {
        	
        	this.backgroundWorkerScan = new BackgroundWorker();
       		this.backgroundWorkerScan.WorkerReportsProgress = true;
        	this.backgroundWorkerScan.WorkerSupportsCancellation = true;
        	this.backgroundWorkerScan.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerScan_DoWork);
        	//this.backgroundWorkerScan.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerScan_ProgressChanged);
        	//FIXME
        	this.backgroundWorkerScan.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
        	this.backgroundWorkerScan.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerScan_RunWorkerCompleted);        	

        	this.backgroundWorkerScanManual = new BackgroundWorker();
       		this.backgroundWorkerScanManual.WorkerReportsProgress = true;
        	this.backgroundWorkerScanManual.WorkerSupportsCancellation = true;
        	this.backgroundWorkerScanManual.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerScanManual_DoWork);
        	//this.backgroundWorkerScanManual.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerScanManual_ProgressChanged);
        	//FIXME
        	this.backgroundWorkerScanManual.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
        	this.backgroundWorkerScanManual.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerScanManual_RunWorkerCompleted);
        	
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
        	
        	label1 = Controller.SpawnLabel("", new Point(23, 415), new Size(160, 15));
        	this.Controls.Add(label1);
        	
        	labelError = Controller.SpawnLabel("", new Point(23, 430), new Size(160, 15));
        	this.Controls.Add(labelError);
        	
        }
        
        private void SetNextStatus() {
        	
        	int newstatus = StatusProcessor.GetNextStatus();
        	
        	if (newstatus >= 0) {
        		UpdateStatus(newstatus);
        	}
        	
        }
        

        private void Process() {
        	       	
        	this.SetNextStatus();
        	
        	//FIXME: well this is a bit dodgy. Maybe get rid of NoChecksum altogether.
        	if (StatusProcessor.Status == (int) StatusProcessor.StatusCodes.NoChecksum) {

        		this.SetNextStatus();
        	}
        	
            if (StatusProcessor.Status == (int) StatusProcessor.StatusCodes.UpdatingChecksum) {
        		
            	
            	WebClient wc = new WebClient();
            	wc.Encoding = System.Text.Encoding.UTF8;
            	wc.Credentials = Controller.GetNetworkCredential();

            	Controller.AddDebugMessage("Processing Checksums.");
            	
            	bool rv = false;
				int c = 0;
            	
				while (rv == false) {
					
            		if (c>0) {
						UpdateStatus((int) StatusProcessor.StatusCodes.ChecksumFailed);
            			errorcounter++;
            			UpdateErrors();

		            	DialogResult res = MessageBox.Show("File checksums could not be read from server. Unfortunately we don't have a local copy that we could use instead. You can retry with OK button now or try again later.", "Retry downloading?",MessageBoxButtons.OKCancel);
        		
			        	if (res == DialogResult.Cancel) {
			        		return;
			        	}
            			
            		}
					
					UpdateStatus((int) StatusProcessor.StatusCodes.UpdatingChecksum);
					
					c++;
					rv = this.ProcessChecksums(wc);
				}
            	
            	UpdateStatus((int) StatusProcessor.StatusCodes.ChecksumOK);
            	
            	this.SetNextStatus();

            }
            
            if (StatusProcessor.Status == (int) StatusProcessor.StatusCodes.Scanning) {
            	
            	Controller.AddDebugMessage("Dispatching Worker for Scanning.");
            	this.DispatchScanWorker();
            }
            
            if (StatusProcessor.Status == (int) StatusProcessor.StatusCodes.ScanningManual) {
            	Controller.AddDebugMessage("Dispatching Worker for Manual Scanning.");
            	this.DispatchScanManualWorker();
            }
            
            if (StatusProcessor.Status == (int) StatusProcessor.StatusCodes.Patching) {
            	
            	Controller.AddDebugMessage("Dispatching Worker for Patching.");
            	this.DispatchPatchWorker();
            }
            

        }
        
        
        private void UpdateErrors() {
        	if (errorcounter > 0) {
        		labelError.Text = "Download Errors: " + errorcounter;
        	} else {
        		labelError.Text = "";
        	}
        }



        private void DispatchWorker(BackgroundWorker bgworker, int targetstatus) {
        	
         	UpdateErrors();
         	
         	if (StatusProcessor.Status != targetstatus) {
        		return;
        	}
        	        	
        	launcherProgressBar1.Step = 100 / Controller.SWGFiles.SwgFileTable.Count;
        	launcherProgressBar1.Value = 0;
        	
        	
        	bgworker.RunWorkerAsync();

        }

        private void DispatchPatchWorker() {
        	
        	DispatchWorker(backgroundWorker2, (int)StatusProcessor.StatusCodes.Patching);

        }

        
        private void DispatchScanWorker() {
        	
        	DispatchWorker(backgroundWorkerScan, (int)StatusProcessor.StatusCodes.Scanning);

        }
        
        private void DispatchScanManualWorker() {
        	
        	DispatchWorker(backgroundWorkerScanManual, (int)StatusProcessor.StatusCodes.ScanningManual);
        	
        }
        

        private void RefreshButtonState() {
        	
        	OptButton.Disable = !File.Exists(Controller.SwgSavePath + @"\swgclientsetup_r.exe");
        	
        	if (OptionWindow != null) {
        		OptionWindow.RefreshButtonState();
        	}
        	
        }
        
        private bool UpdateStatus(int newstatus) {
        	
        	Controller.AddDebugMessage("Switching Status to: " + newstatus);
        	if (!StatusProcessor.SetNewState(newstatus)) {
        		return false;
        	}
        	Controller.AddDebugMessage("Switched Status to: " + newstatus);
        	
        	switch (newstatus) {
        		//checksums need downloading	
        		case (int) StatusProcessor.StatusCodes.NoChecksum:

        			PlayButton.Disable = false;
        			PlayButton.Text = "Get Checksums";
                	label1.ForeColor = Color.Blue;
                	pictureBox2.Image = null;
                	label1.Text = "Checksums need Checking (" + newstatus + ")";
                	launcherProgressBar1.Text = "";
                	ScanButton.Disable = true;
        			break;
        		case (int) StatusProcessor.StatusCodes.UpdatingChecksum:

        			PlayButton.Disable = true;
        			PlayButton.Text = "Get Checksums";
                	label1.ForeColor = Color.Blue;
                	pictureBox2.Image = Controller.GetResourceImage("small-loading");
                	label1.Text = "DL'ing Checksums (" + newstatus + ")";
                	launcherProgressBar1.Text = "";
					ScanButton.Disable = true;
        			break;


        		case (int) StatusProcessor.StatusCodes.ChecksumFailed:
        			
        			PlayButton.Disable = false;
        			PlayButton.Text = "Retry Checksums";
                	label1.ForeColor = Color.Red;
                	pictureBox2.Image = null;
                	label1.Text = "Checksum DL failed. (" + newstatus + ")";
                	launcherProgressBar1.Text = "";

                	ScanButton.Disable = true;
        			break;
        			
        		//checksums present and working
        		case (int) StatusProcessor.StatusCodes.ChecksumOK:
        		
        			PlayButton.Disable = false;
        			PlayButton.Text = "Scan";
                	label1.ForeColor = Color.Green;
                	pictureBox2.Image = null;
                	label1.Text = "Checksums loaded. (" + newstatus + ")";
                	launcherProgressBar1.Text = "";
                	ScanButton.Disable = false;
        			break;

        		case (int) StatusProcessor.StatusCodes.Scanning:
        		
        			PlayButton.Disable = true;
        			PlayButton.Text = "Scan";
                	label1.ForeColor = Color.Blue;
                	pictureBox2.Image = Controller.GetResourceImage("small-loading");
                	label1.Text = "Scanning (" + newstatus + ")";
					ScanButton.Disable = true;
        			break;
        			
        		case (int) StatusProcessor.StatusCodes.ScanningManual:
        		
        			PlayButton.Disable = true;
        			PlayButton.Text = "Scan";
                	label1.ForeColor = Color.Blue;
                	pictureBox2.Image = Controller.GetResourceImage("small-loading");
                	label1.Text = "Scanning (" + newstatus + ")";
					ScanButton.Disable = true;
        			break;
        			
        		case (int) StatusProcessor.StatusCodes.ScanningFailed:
        			PlayButton.Disable = false;
        			PlayButton.Text = "Retry Scan";
                	label1.ForeColor = Color.Red;
                	pictureBox2.Image = null;
                	label1.Text = "Scanning Failed (" + newstatus + ")";
                	ScanButton.Disable = false;
        			break;
        		
        		case (int) StatusProcessor.StatusCodes.ScanningOK:
        			
            		label1.ForeColor = Color.Blue;
            		label1.Text = "Scanning complete! (" + newstatus + ")";
            		pictureBox2.Image = null;
        			PlayButton.Disable = false;
        			PlayButton.Text = "Patch";
					ScanButton.Disable = false;
        			break;
        			
        		case (int) StatusProcessor.StatusCodes.Patching:
        		
        			PlayButton.Disable = true;
        			PlayButton.Text = "Patch";
                	label1.ForeColor = Color.Blue;
                	pictureBox2.Image = Controller.GetResourceImage("small-loading");
                	label1.Text = "Patching (" + newstatus + ")";
					ScanButton.Disable = true;
        			break;
        		
        		case (int) StatusProcessor.StatusCodes.PatchingFailed:
					//FIXME: add retry button
        			PlayButton.Disable = false;
        			PlayButton.Text = "Retry Patch";
                	label1.ForeColor = Color.Red;
                	pictureBox2.Image = null;
                	label1.Text = "Patching Failed (" + newstatus + ")";
                	ScanButton.Disable = false;
        			break;
        		
        		case (int) StatusProcessor.StatusCodes.PatchingOK:
        			
            		label1.ForeColor = Color.Aqua;
            		label1.Text = "Ready to play! (" + newstatus + ")";
            		pictureBox2.Image = null;
        			PlayButton.Disable = false;
        			PlayButton.Text = "Play";
					ScanButton.Disable = false;
        			break;
        	}
        	
        	//FIXME
        	this.launcherProgressBar1.ForeColor = ((StatusProcessor.Status == (int) StatusProcessor.StatusCodes.PatchingOK) ? System.Drawing.Color.Green : System.Drawing.Color.Red);
        	
        	this.Refresh();
        	
        	return true;
        	
        }

        private void backgroundWorkerChecksum_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {

        	// only run in scanning State
        	if (StatusProcessor.Status != (int) StatusProcessor.StatusCodes.UpdatingChecksum) {
		    	e.Cancel = true;
		        return;
		    }

        	BackgroundWorker backgroundWorker = sender as BackgroundWorker;
  			if(backgroundWorker != null) {
        		
	    		backgroundWorker.ReportProgress(0);
	    		
	    		
	    	}

	    	if(backgroundWorker.CancellationPending) {
			     e.Cancel = true;
        	}
			else {
            		backgroundWorker.ReportProgress(100,"Scanning finished.");
			}

        	
        }

        
        private void ScanWork(bool checksums, int checkstatus, object sender, System.ComponentModel.DoWorkEventArgs e) {

        	// only run in scanning State
		    if (StatusProcessor.Status != checkstatus) {
		    	e.Cancel = true;
		        return;
		    }

        	BackgroundWorker backgroundWorker = sender as BackgroundWorker;
  			if(backgroundWorker != null) {
        		
	    		backgroundWorker.ReportProgress(0);
	    		Controller.SWGFiles.Scan(checksums, backgroundWorker);
	    		
	    	}

	    	if(backgroundWorker.CancellationPending) {
			     e.Cancel = true;
        	}
			else {
            		backgroundWorker.ReportProgress(100,"Scanning finished.");
			}

        }
        
        

        
        
        private void backgroundWorkerScan_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {

        	ScanWork(Controller.checksumOption,(int)StatusProcessor.StatusCodes.Scanning, sender, e);
	    		
        }

        private void backgroundWorkerScanManual_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {

	    	ScanWork(true,(int)StatusProcessor.StatusCodes.ScanningManual, sender, e);
	    	
        }
        
        
        private void ScanComplete(bool checksum, bool DoContinue, object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {

        	if (e.Cancelled || e.Error != null) {
        		
        		Debug.WriteLine("Scan cancel/fail: " + (e.Cancelled ? "cancelled" : "") + (e.Error != null ? e.ToString() : ""));
        		this.launcherProgressBar1.Text = "";
        		
        		UpdateStatus((int) StatusProcessor.StatusCodes.ScanningFailed);
        		
        		return;
        	} 

        	UpdateStatus((int) StatusProcessor.StatusCodes.ScanningOK);

        	if (checksum) {
        		Controller.SaveScanComplete();
        	}

        	
        	if (DoContinue) {
        		Process();
        	}
        	
        }
        
        
        //FIXME: consolidate with manual
        private void backgroundWorkerScan_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
        	
        	ScanComplete(Controller.checksumOption, true, sender, e);
        	
        }


        
        //FIXME
        private void backgroundWorkerScanManual_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {

        	ScanComplete(true, true, sender, e);
        	
        }        
        

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
        	
        	// only run if checksums are present
		    if (StatusProcessor.Status != (int)StatusProcessor.StatusCodes.Patching) {
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
		            if (Controller.SWGFiles.IsGood(file.Key)) {
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
	        		
	        	}
        	}
        	

        	
        	launcherProgressBar1.Value = ((e.ProgressPercentage > 100) ? 100 : e.ProgressPercentage );
        	RefreshButtonState();

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
        	
	       	if (e.Cancelled || e.Error != null) {
        		
        		Debug.WriteLine("DL cancel/fail: " + (e.Cancelled ? "cancelled" : "") + (e.Error != null ? e.ToString() : ""));
        		this.launcherProgressBar1.Text = "";
        		UpdateStatus((int) StatusProcessor.StatusCodes.PatchingFailed);
        		
        	} else {

        		if (Controller.SWGFiles.IsComplete()) {
        			UpdateStatus((int) StatusProcessor.StatusCodes.PatchingOK);
        		} else {
        			UpdateStatus((int) StatusProcessor.StatusCodes.PatchingFailed);
        		}
        	}
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
	        
		        if (Controller.SWGFiles.IsGood(file)) {
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
		        		
		        		//if (!Controller.checksumOption && !this.ForceChecksums) {
		        		if (!Controller.checksumOption ) {
			        		backgroundWorker.ReportProgress( progress, "Debug " + "file exists, skipping checksum for " + file);
			        		backgroundWorker.ReportProgress(progress, "OK " + file);		        			
		        			return true;
		        		}
		        		

			        	if (swgfile.MatchChecksum(path)) {
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
	        		
		        		if (swgfile.MatchChecksum(localsrc)) {
	
		        			backgroundWorker.ReportProgress(progress, "Debug " + "Reading " + file);
		        			backgroundWorker.ReportProgress(progress, "Reading " + file);
		        			
							//File.Copy(localsrc, swgdirsave);
							File.Copy(@localsrc, @path, true);
		        			
		        			
		        			if (swgfile.MatchChecksum(path)) {
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
	        	
				
	        	if (swgfile.MatchChecksum(path) ) {
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
        	
        	//FIXME
        	if (StatusProcessor.Status == (int) StatusProcessor.StatusCodes.ChecksumFailed || StatusProcessor.Status == (int) StatusProcessor.StatusCodes.PatchingFailed) {
        		Process();
        		return;
        	}

        	//FIXME
        	if (StatusProcessor.Status != (int) StatusProcessor.StatusCodes.PatchingOK) {
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
        	}

        	String args = String.Format("-- -s Station subscriptionFeatures=1 gameFeatures=34374193 -s ClientGame loginServerPort0={0} loginServerAddress0={1}", port, address);
        	
        	this.Hide();
            Directory.SetCurrentDirectory(Controller.SwgSavePath);
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


        private void Scan_Click(object sender, EventArgs e)
        {
        	
        	//FIXME
            //only scan if complete
            if (StatusProcessor.Status != (int) StatusProcessor.StatusCodes.PatchingOK && StatusProcessor.Status != (int) StatusProcessor.StatusCodes.PatchingFailed)
            {
            	return;
            }
            
            Controller.PlaySound("Sound_Click");
			
			
			UpdateStatus((int) StatusProcessor.StatusCodes.ScanningManual);
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
        

        private void MinimizeClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void CloseClick(object sender, EventArgs e)
        {
        	
        	//FIXME
        	if (StatusProcessor.Status == (int) StatusProcessor.StatusCodes.UpdatingChecksum || StatusProcessor.Status == (int) StatusProcessor.StatusCodes.Patching)
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
        
        
    }
}
       
       

        


       
    

        
    


