/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 22.12.2012
 * Time: 16:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using PswgLauncher;

namespace PswgLauncher
{
	/// <summary>
	/// Description of PatchCheckerDialog.
	/// </summary>
	public partial class PatchCheckerDialog : Form
	{
		
		private GuiController Controller;
		private Timer timer;

		bool update = false;
		
		public PatchCheckerDialog(GuiController gc)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			
			this.Controller = gc;
			
			
			InitializeComponent();
			this.Icon= Controller.GetAppIcon();
			this.setTimer();
		
		}
		



       
       	void ButtonRunClick(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
       	
       	
       	void ButtonUpdateClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Yes;
		}
       	
       			
		void ButtonCancelClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
       	
       
		void ButtonRetryClick(object sender, EventArgs e)
		{
			
			label2.Text = "Checking...";

			buttonRun.Enabled = false;
			buttonUpdate.Enabled = false;
			buttonRetry.Enabled = false;
			
			this.setTimer();
			
		}
		
		// need to use this, otherwise the dialog won't close 
		private void setTimer() {
			
			timer = new Timer();
			timer.Interval = 100;
        	timer.Tick += TimerCheckUpdate;
	        timer.Start();
			
		}
		
		private void TimerCheckUpdate(object sender, EventArgs e)
	    {
	        timer.Stop();
	        timer.Dispose();
	        
	        if (checkForUpdate()) {
	        	if (update) {
	        		this.DialogResult = DialogResult.Yes;
	        	} else {
	        		this.DialogResult = DialogResult.OK;
	        	}
	        }
    	}
		
		
		private bool checkForUpdate() {
			
			//FIXME: this is not really pretty. maybe the whole method should be in the controller.
			// --darkk
			PatchChecker patch = new PatchChecker(Controller);
			update = patch.UpdateNeeded;
			
			
			if (patch.remoteError) {
				label2.Text = "Remote error while checking for updates.";
				buttonRun.Enabled = true;
				buttonRetry.Enabled = true;
			} else if (patch.localError) {
				label2.Text = "Local error while checking for updates, lpatchusr.cfg write problems?";
				buttonRun.Enabled = true;
				buttonRetry.Enabled = true;
			} else {
			
				if (update) {
					label2.Text = "There's an Update available. We recommend you update by clicking the Update Button below.";
					
					buttonRun.Enabled = true;
					buttonUpdate.Enabled = true;
					return true;

				} else {
					label2.Text = "The Launcher is uptodate. There is no need to run the Launcher Patcher at this time.";
					buttonRun.Enabled = true;
					buttonUpdate.Enabled = true;
					return true;
					
				}
			
			}
			
			return false;
		}
		

		


	}
}
