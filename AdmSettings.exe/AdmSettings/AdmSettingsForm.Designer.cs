/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 16.03.2013
 * Time: 10:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AdmSettings
{
	partial class AdmSettingsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdmSettingsForm));
			this.radioRunNormal = new System.Windows.Forms.RadioButton();
			this.radioRunElevated = new System.Windows.Forms.RadioButton();
			this.radioHome = new System.Windows.Forms.RadioButton();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.LocateSettingsLabel = new System.Windows.Forms.Label();
			this.radioNoSetting = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// radioRunNormal
			// 
			this.radioRunNormal.Location = new System.Drawing.Point(12, 68);
			this.radioRunNormal.Name = "radioRunNormal";
			this.radioRunNormal.Size = new System.Drawing.Size(630, 82);
			this.radioRunNormal.TabIndex = 0;
			this.radioRunNormal.TabStop = true;
			this.radioRunNormal.UseVisualStyleBackColor = true;
			// 
			// radioRunElevated
			// 
			this.radioRunElevated.Location = new System.Drawing.Point(12, 156);
			this.radioRunElevated.Name = "radioRunElevated";
			this.radioRunElevated.Size = new System.Drawing.Size(631, 82);
			this.radioRunElevated.TabIndex = 1;
			this.radioRunElevated.TabStop = true;
			this.radioRunElevated.UseVisualStyleBackColor = true;
			// 
			// radioHome
			// 
			this.radioHome.Location = new System.Drawing.Point(13, 244);
			this.radioHome.Name = "radioHome";
			this.radioHome.Size = new System.Drawing.Size(631, 82);
			this.radioHome.TabIndex = 2;
			this.radioHome.TabStop = true;
			this.radioHome.UseVisualStyleBackColor = true;
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonSave.Location = new System.Drawing.Point(12, 383);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 3;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(93, 383);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// LocateSettingsLabel
			// 
			this.LocateSettingsLabel.Location = new System.Drawing.Point(12, 329);
			this.LocateSettingsLabel.Name = "LocateSettingsLabel";
			this.LocateSettingsLabel.Size = new System.Drawing.Size(631, 48);
			this.LocateSettingsLabel.TabIndex = 5;
			// 
			// radioNoSetting
			// 
			this.radioNoSetting.Location = new System.Drawing.Point(12, 12);
			this.radioNoSetting.Name = "radioNoSetting";
			this.radioNoSetting.Size = new System.Drawing.Size(630, 50);
			this.radioNoSetting.TabIndex = 6;
			this.radioNoSetting.TabStop = true;
			this.radioNoSetting.Text = "Unconfigured";
			this.radioNoSetting.UseVisualStyleBackColor = true;
			// 
			// AdmSettingsForm
			// 
			this.AcceptButton = this.buttonCancel;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(655, 418);
			this.Controls.Add(this.radioNoSetting);
			this.Controls.Add(this.LocateSettingsLabel);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.radioHome);
			this.Controls.Add(this.radioRunElevated);
			this.Controls.Add(this.radioRunNormal);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AdmSettingsForm";
			this.Text = "PSWG Launcher Admin Settings";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.RadioButton radioNoSetting;
		private System.Windows.Forms.Label LocateSettingsLabel;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.RadioButton radioHome;
		private System.Windows.Forms.RadioButton radioRunElevated;
		private System.Windows.Forms.RadioButton radioRunNormal;
	}
}
