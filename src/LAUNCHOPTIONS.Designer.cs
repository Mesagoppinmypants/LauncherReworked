namespace WindowsFormsApplication1
{
    partial class LAUNCHOPTIONS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LAUNCHOPTIONS));
            this.button1 = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.Support = new System.Windows.Forms.Button();
            this.Donate = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(670, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 23);
            this.button1.TabIndex = 8;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // close
            // 
            this.close.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.close.Image = ((System.Drawing.Image)(resources.GetObject("close.Image")));
            this.close.Location = new System.Drawing.Point(702, 15);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(26, 29);
            this.close.TabIndex = 7;
            this.close.UseVisualStyleBackColor = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // Support
            // 
            this.Support.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.Support.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Support.BackgroundImage")));
            this.Support.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Support.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Support.Image = ((System.Drawing.Image)(resources.GetObject("Support.Image")));
            this.Support.Location = new System.Drawing.Point(168, 157);
            this.Support.Name = "Support";
            this.Support.Size = new System.Drawing.Size(164, 75);
            this.Support.TabIndex = 12;
            this.Support.UseVisualStyleBackColor = false;
            this.Support.Click += new System.EventHandler(this.Support_Click);
            // 
            // Donate
            // 
            this.Donate.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.Donate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Donate.BackgroundImage")));
            this.Donate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Donate.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Donate.Image = ((System.Drawing.Image)(resources.GetObject("Donate.Image")));
            this.Donate.Location = new System.Drawing.Point(168, 270);
            this.Donate.Name = "Donate";
            this.Donate.Size = new System.Drawing.Size(164, 76);
            this.Donate.TabIndex = 13;
            this.Donate.UseVisualStyleBackColor = false;
            this.Donate.Click += new System.EventHandler(this.Donate_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(411, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(164, 76);
            this.button2.TabIndex = 14;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(411, 270);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(164, 76);
            this.button3.TabIndex = 15;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(251, 405);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Aqua;
            this.label2.Location = new System.Drawing.Point(451, 405);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 17;
            // 
            // LAUNCHOPTIONS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(750, 503);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Donate);
            this.Controls.Add(this.Support);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.close);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LAUNCHOPTIONS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.LAUNCHOPTIONS_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LAUNCHOPTIONS_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LAUNCHOPTIONS_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LAUNCHOPTIONS_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LAUNCHOPTIONS_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button Support;
        private System.Windows.Forms.Button Donate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}