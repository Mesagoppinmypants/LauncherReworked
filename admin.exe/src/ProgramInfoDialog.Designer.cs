/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 06.01.2013
 * Time: 20:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AdminRightsLauncher
{
	partial class ProgramInfoDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramInfoDialog));
			this.TheLabel = new System.Windows.Forms.Label();
			this.checkBoxSeen = new System.Windows.Forms.CheckBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TheLabel
			// 
			this.TheLabel.Location = new System.Drawing.Point(13, 13);
			this.TheLabel.Name = "TheLabel";
			this.TheLabel.Size = new System.Drawing.Size(267, 204);
			this.TheLabel.TabIndex = 0;
			this.TheLabel.Text = resources.GetString("TheLabel.Text");
			// 
			// checkBoxSeen
			// 
			this.checkBoxSeen.Location = new System.Drawing.Point(13, 221);
			this.checkBoxSeen.Name = "checkBoxSeen";
			this.checkBoxSeen.Size = new System.Drawing.Size(267, 24);
			this.checkBoxSeen.TabIndex = 1;
			this.checkBoxSeen.Text = "Do not show this message again.";
			this.checkBoxSeen.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(13, 252);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(94, 252);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// ProgramInfoDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 282);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.checkBoxSeen);
			this.Controls.Add(this.TheLabel);
			this.Name = "ProgramInfoDialog";
			this.Text = "Run As Administrator?";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.CheckBox checkBoxSeen;
		private System.Windows.Forms.Label TheLabel;
	}
}
