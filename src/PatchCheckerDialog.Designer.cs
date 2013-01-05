/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 22.12.2012
 * Time: 16:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace PswgLauncher
{
	partial class PatchCheckerDialog
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatchCheckerDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonRun = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonRetry = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(320, 25);
			this.label1.TabIndex = 0;
			this.label1.Text = "Please wait while we\'re checking for Launcher Updates.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(318, 78);
			this.label2.TabIndex = 1;
			this.label2.Text = "Checking...";
			// 
			// buttonRun
			// 
			this.buttonRun.Enabled = false;
			this.buttonRun.Location = new System.Drawing.Point(13, 119);
			this.buttonRun.Name = "buttonRun";
			this.buttonRun.Size = new System.Drawing.Size(75, 23);
			this.buttonRun.TabIndex = 2;
			this.buttonRun.Text = "Run";
			this.buttonRun.UseVisualStyleBackColor = true;
			this.buttonRun.Click += new System.EventHandler(this.ButtonRunClick);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Enabled = false;
			this.buttonUpdate.Location = new System.Drawing.Point(94, 119);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
			this.buttonUpdate.TabIndex = 3;
			this.buttonUpdate.Text = "Update";
			this.buttonUpdate.UseVisualStyleBackColor = true;
			this.buttonUpdate.Click += new System.EventHandler(this.ButtonUpdateClick);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(256, 119);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// buttonRetry
			// 
			this.buttonRetry.Enabled = false;
			this.buttonRetry.Location = new System.Drawing.Point(175, 119);
			this.buttonRetry.Name = "buttonRetry";
			this.buttonRetry.Size = new System.Drawing.Size(75, 23);
			this.buttonRetry.TabIndex = 4;
			this.buttonRetry.Text = "Retry";
			this.buttonRetry.UseVisualStyleBackColor = true;
			this.buttonRetry.Click += new System.EventHandler(this.ButtonRetryClick);
			// 
			// PatchCheckerDialog
			// 
			this.ClientSize = new System.Drawing.Size(345, 153);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonRetry);
			this.Controls.Add(this.buttonUpdate);
			this.Controls.Add(this.buttonRun);
			
			this.Name = "PatchCheckerDialog";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button buttonRetry;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonRun;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		

	}
}
