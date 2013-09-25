/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 29.07.2013
 * Time: 15:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace PswgLauncher.GUI.Forms
{
	partial class FileListWindow
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
			this.checkBoxFont = new System.Windows.Forms.CheckBox();
			this.checkBoxHide = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// checkBoxFont
			// 
			this.checkBoxFont.Checked = true;
			this.checkBoxFont.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxFont.Location = new System.Drawing.Point(6, 19);
			this.checkBoxFont.Name = "checkBoxFont";
			this.checkBoxFont.Size = new System.Drawing.Size(127, 24);
			this.checkBoxFont.TabIndex = 0;
			this.checkBoxFont.Text = "Use Standard Font";
			this.checkBoxFont.UseVisualStyleBackColor = true;
			this.checkBoxFont.CheckedChanged += new System.EventHandler(this.CheckBoxFontCheckedChanged);
			// 
			// checkBoxHide
			// 
			this.checkBoxHide.Location = new System.Drawing.Point(6, 49);
			this.checkBoxHide.Name = "checkBoxHide";
			this.checkBoxHide.Size = new System.Drawing.Size(166, 24);
			this.checkBoxHide.TabIndex = 1;
			this.checkBoxHide.Text = "Hide Completed Files";
			this.checkBoxHide.UseVisualStyleBackColor = true;
			this.checkBoxHide.CheckedChanged += new System.EventHandler(this.CheckBoxHideCheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.checkBoxFont);
			this.groupBox1.Controls.Add(this.checkBoxHide);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(560, 83);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Display";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeColumns = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(13, 102);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(559, 347);
			this.dataGridView1.TabIndex = 3;
			// 
			// FileListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 461);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.groupBox1);
			this.Name = "FileListForm";
			this.Text = "File List";
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBoxHide;
		private System.Windows.Forms.CheckBox checkBoxFont;
	}
}
