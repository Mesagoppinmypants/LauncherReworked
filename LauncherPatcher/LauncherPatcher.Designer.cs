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
			this.StatusLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// StatusLabel
			// 
			this.StatusLabel.Location = new System.Drawing.Point(13, 13);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(267, 127);
			this.StatusLabel.TabIndex = 1;
			// 
			// LauncherPatcher
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 207);
			this.Controls.Add(this.StatusLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LauncherPatcher";
			this.Text = "LauncherPatcher";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LauncherPatcherFormClosing);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label StatusLabel;
	}
}
