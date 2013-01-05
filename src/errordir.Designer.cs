namespace PswgLauncher
{
    partial class errordir
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	System.Windows.Forms.Label label3;
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(errordir));
        	this.label1 = new System.Windows.Forms.Label();
        	this.label2 = new System.Windows.Forms.Label();
        	this.button1 = new System.Windows.Forms.Button();
        	label3 = new System.Windows.Forms.Label();
        	this.SuspendLayout();
        	// 
        	// label3
        	// 
        	label3.AutoSize = true;
        	label3.ForeColor = System.Drawing.Color.Crimson;
        	label3.Location = new System.Drawing.Point(82, 98);
        	label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        	label3.Name = "label3";
        	label3.Size = new System.Drawing.Size(250, 13);
        	label3.TabIndex = 2;
        	label3.Text = "Please select a valid Star Wars Galaxies Installation";
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.ForeColor = System.Drawing.Color.Crimson;
        	this.label1.Location = new System.Drawing.Point(181, 16);
        	this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(46, 13);
        	this.label1.TabIndex = 0;
        	this.label1.Text = "ERROR";
        	// 
        	// label2
        	// 
        	this.label2.AutoSize = true;
        	this.label2.ForeColor = System.Drawing.Color.Crimson;
        	this.label2.Location = new System.Drawing.Point(104, 74);
        	this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(200, 13);
        	this.label2.TabIndex = 1;
        	this.label2.Text = "Star Wars Galaxies installation not found.";
        	// 
        	// button1
        	// 
        	this.button1.Location = new System.Drawing.Point(339, 154);
        	this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(56, 19);
        	this.button1.TabIndex = 3;
        	this.button1.Text = "OK";
        	this.button1.UseVisualStyleBackColor = true;
        	this.button1.Click += new System.EventHandler(this.button1_Click);
        	// 
        	// errordir
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(404, 182);
        	this.Controls.Add(this.button1);
        	this.Controls.Add(label3);
        	this.Controls.Add(this.label2);
        	this.Controls.Add(this.label1);
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        	this.Name = "errordir";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "SWG Dir Not Found";
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}