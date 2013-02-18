/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 17.02.2013
 * Time: 18:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace LauncherPatcher
{
	partial class LauncherPatcher
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherPatcher));
			this.launcherProgressBar1 = new PswgLauncher.LauncherProgressBar();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.buttonRetry = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonForceDL = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// launcherProgressBar1
			// 
			this.launcherProgressBar1.Location = new System.Drawing.Point(12, 143);
			this.launcherProgressBar1.Name = "launcherProgressBar1";
			this.launcherProgressBar1.Size = new System.Drawing.Size(268, 23);
			this.launcherProgressBar1.TabIndex = 0;
			this.launcherProgressBar1.Text = "launcherProgressBar1";
			this.launcherProgressBar1.TextColor = System.Drawing.Color.Empty;
			// 
			// StatusLabel
			// 
			this.StatusLabel.Location = new System.Drawing.Point(13, 13);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(267, 127);
			this.StatusLabel.TabIndex = 1;
			// 
			// buttonRetry
			// 
			this.buttonRetry.Enabled = false;
			this.buttonRetry.Location = new System.Drawing.Point(12, 172);
			this.buttonRetry.Name = "buttonRetry";
			this.buttonRetry.Size = new System.Drawing.Size(75, 23);
			this.buttonRetry.TabIndex = 2;
			this.buttonRetry.Text = "Retry";
			this.buttonRetry.UseVisualStyleBackColor = true;
			this.buttonRetry.Click += new System.EventHandler(this.ButtonRetryClick);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(93, 172);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// buttonForceDL
			// 
			this.buttonForceDL.Enabled = false;
			this.buttonForceDL.Location = new System.Drawing.Point(175, 171);
			this.buttonForceDL.Name = "buttonForceDL";
			this.buttonForceDL.Size = new System.Drawing.Size(105, 23);
			this.buttonForceDL.TabIndex = 4;
			this.buttonForceDL.Text = "Force Download";
			this.buttonForceDL.UseVisualStyleBackColor = true;
			this.buttonForceDL.Click += new System.EventHandler(this.ButtonForceDLClick);
			// 
			// LauncherPatcher
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 207);
			this.Controls.Add(this.buttonForceDL);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonRetry);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.launcherProgressBar1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LauncherPatcher";
			this.Text = "LauncherPatcher";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LauncherPatcherFormClosing);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button buttonForceDL;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonRetry;
		private System.Windows.Forms.Label StatusLabel;
		private PswgLauncher.LauncherProgressBar launcherProgressBar1;
	}
}
