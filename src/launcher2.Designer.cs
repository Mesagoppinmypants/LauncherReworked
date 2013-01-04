namespace WindowsFormsApplication1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(launcher2));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.PLAY = new System.Windows.Forms.Button();
            this.scan = new System.Windows.Forms.Button();
            this.options = new System.Windows.Forms.Button();
            this.acct = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowNavigation = false;
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(478, 65);
            this.webBrowser1.MaximumSize = new System.Drawing.Size(277, 367);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(277, 367);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(277, 367);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("ftp://173.242.114.16/files/text.html", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // PLAY
            // 
            this.PLAY.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.PLAY.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PLAY.BackgroundImage")));
            this.PLAY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PLAY.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.PLAY.Image = ((System.Drawing.Image)(resources.GetObject("PLAY.Image")));
            this.PLAY.Location = new System.Drawing.Point(626, 538);
            this.PLAY.Name = "PLAY";
            this.PLAY.Size = new System.Drawing.Size(162, 50);
            this.PLAY.TabIndex = 1;
            this.PLAY.UseVisualStyleBackColor = false;
            this.PLAY.Click += new System.EventHandler(this.PLAY_Click_1);
            // 
            // scan
            // 
            this.scan.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.scan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("scan.BackgroundImage")));
            this.scan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scan.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.scan.Image = ((System.Drawing.Image)(resources.GetObject("scan.Image")));
            this.scan.Location = new System.Drawing.Point(321, 491);
            this.scan.Name = "scan";
            this.scan.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.scan.Size = new System.Drawing.Size(160, 60);
            this.scan.TabIndex = 2;
            this.scan.UseVisualStyleBackColor = false;
            this.scan.Click += new System.EventHandler(this.scan_Click);
            // 
            // options
            // 
            this.options.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.options.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("options.BackgroundImage")));
            this.options.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.options.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.options.Image = ((System.Drawing.Image)(resources.GetObject("options.Image")));
            this.options.Location = new System.Drawing.Point(165, 491);
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(160, 60);
            this.options.TabIndex = 3;
            this.options.UseVisualStyleBackColor = false;
            this.options.SizeChanged += new System.EventHandler(this.options_SizeChanged);
            this.options.Click += new System.EventHandler(this.options_Click_1);
            // 
            // acct
            // 
            this.acct.AutoEllipsis = true;
            this.acct.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.acct.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("acct.BackgroundImage")));
            this.acct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.acct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.acct.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.acct.Image = ((System.Drawing.Image)(resources.GetObject("acct.Image")));
            this.acct.Location = new System.Drawing.Point(25, 491);
            this.acct.Name = "acct";
            this.acct.Size = new System.Drawing.Size(144, 60);
            this.acct.TabIndex = 4;
            this.acct.UseVisualStyleBackColor = false;
            this.acct.Click += new System.EventHandler(this.acct_Click_1);
            // 
            // close
            // 
            this.close.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.close.Image = ((System.Drawing.Image)(resources.GetObject("close.Image")));
            this.close.Location = new System.Drawing.Point(749, 12);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(26, 29);
            this.close.TabIndex = 5;
            this.close.UseVisualStyleBackColor = false;
            this.close.Click += new System.EventHandler(this.close_Click_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(707, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 23);
            this.button1.TabIndex = 6;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
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
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 600);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(632, 498);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Downloading Patches...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(31, 555);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(589, 22);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(694, 518);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(18, 22);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(478, 496);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(156, 55);
            this.button2.TabIndex = 11;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // launcher2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.acct);
            this.Controls.Add(this.options);
            this.Controls.Add(this.scan);
            this.Controls.Add(this.PLAY);
            this.Controls.Add(this.webBrowser1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "launcher2";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.launcher2_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.launcher2_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.launcher2_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.launcher2_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.launcher2_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button scan;
        private System.Windows.Forms.Button options;
        private System.Windows.Forms.Button acct;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button PLAY;
        private System.Windows.Forms.Splitter splitter1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button2;

    }
}