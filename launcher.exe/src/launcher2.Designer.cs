﻿namespace PswgLauncher
{
    partial class launcher2
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
        	this.components = new System.ComponentModel.Container();
        	this.webBrowser1 = new System.Windows.Forms.WebBrowser();
        	this.imageList1 = new System.Windows.Forms.ImageList(this.components);
        	this.splitter1 = new System.Windows.Forms.Splitter();
        	this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
        	this.label1 = new System.Windows.Forms.Label();
        	this.pictureBox2 = new System.Windows.Forms.PictureBox();
        	this.launcherProgressBar1 = new PswgLauncher.LauncherProgressBar();
        	this.labelError = new System.Windows.Forms.Label();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// webBrowser1
        	// 
        	this.webBrowser1.AllowNavigation = false;
        	this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.webBrowser1.Location = new System.Drawing.Point(358, 53);
        	this.webBrowser1.Margin = new System.Windows.Forms.Padding(2);
        	this.webBrowser1.MaximumSize = new System.Drawing.Size(208, 298);
        	this.webBrowser1.MinimumSize = new System.Drawing.Size(208, 298);
        	this.webBrowser1.Name = "webBrowser1";
        	this.webBrowser1.Size = new System.Drawing.Size(208, 298);
        	this.webBrowser1.TabIndex = 0;
        	this.webBrowser1.Url = new System.Uri("http://www.projectswg.com/forums/update_notes.php", System.UriKind.Absolute);
        	// 
        	// imageList1
        	// 
        	this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
        	this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
        	this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
        	// 
        	// splitter1
        	// 
        	this.splitter1.Location = new System.Drawing.Point(0, 0);
        	this.splitter1.Margin = new System.Windows.Forms.Padding(2);
        	this.splitter1.Name = "splitter1";
        	this.splitter1.Size = new System.Drawing.Size(2, 488);
        	this.splitter1.TabIndex = 7;
        	this.splitter1.TabStop = false;
        	// 
        	// backgroundWorker2
        	// 
        	this.backgroundWorker2.WorkerReportsProgress = true;
        	this.backgroundWorker2.WorkerSupportsCancellation = true;
        	this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
        	this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
        	this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
        	// 
        	// label1
        	// 
        	this.label1.BackColor = System.Drawing.Color.Transparent;
        	this.label1.ForeColor = System.Drawing.Color.Maroon;
        	this.label1.Location = new System.Drawing.Point(23, 421);
        	this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(120, 13);
        	this.label1.TabIndex = 10;
        	this.label1.Text = "Downloading Patches...";
        	// 
        	// pictureBox2
        	// 
        	this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
        	this.pictureBox2.Location = new System.Drawing.Point(530, 415);
        	this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
        	this.pictureBox2.Name = "pictureBox2";
        	this.pictureBox2.Size = new System.Drawing.Size(16, 16);
        	this.pictureBox2.TabIndex = 9;
        	this.pictureBox2.TabStop = false;
        	// 
        	// launcherProgressBar1
        	// 
        	this.launcherProgressBar1.ForeColor = System.Drawing.Color.Red;
        	this.launcherProgressBar1.Location = new System.Drawing.Point(23, 452);
        	this.launcherProgressBar1.Name = "launcherProgressBar1";
        	this.launcherProgressBar1.Size = new System.Drawing.Size(441, 17);
        	this.launcherProgressBar1.TabIndex = 18;
        	this.launcherProgressBar1.TextColor = System.Drawing.Color.Gray;
        	// 
        	// labelError
        	// 
        	this.labelError.BackColor = System.Drawing.Color.Transparent;
        	this.labelError.Location = new System.Drawing.Point(23, 434);
        	this.labelError.Name = "labelError";
        	this.labelError.Size = new System.Drawing.Size(143, 13);
        	this.labelError.TabIndex = 18;
        	// 
        	// launcher2
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
        	this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        	this.ClientSize = new System.Drawing.Size(600, 488);
        	this.ControlBox = false;
        	this.Controls.Add(this.launcherProgressBar1);
        	this.Controls.Add(this.labelError);
        	this.Controls.Add(this.label1);
        	this.Controls.Add(this.pictureBox2);
        	this.Controls.Add(this.splitter1);
        	this.Controls.Add(this.webBrowser1);
        	this.DoubleBuffered = true;
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        	this.Margin = new System.Windows.Forms.Padding(2);
        	this.MaximumSize = new System.Drawing.Size(600, 488);
        	this.MinimumSize = new System.Drawing.Size(600, 488);
        	this.Name = "launcher2";
        	this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "ProjectSWG Launcher";
        	this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.launcher2_MouseClick);
        	this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.launcher2_MouseDown);
        	this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.launcher2_MouseMove);
        	this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.launcher2_MouseUp);
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
        	this.ResumeLayout(false);
        }
        private PswgLauncher.LauncherProgressBar launcherProgressBar1;
        private System.Windows.Forms.Label labelError;

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Splitter splitter1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;

    }
}
