namespace PswgLauncher
{
    partial class DirSearch
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
        	this.BrowseButton = new System.Windows.Forms.Button();
        	this.textBox1 = new System.Windows.Forms.TextBox();
        	this.NextButton1 = new System.Windows.Forms.Button();
        	this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
        	this.richTextBox1 = new System.Windows.Forms.RichTextBox();
        	this.buttonClose = new System.Windows.Forms.Button();
        	this.buttonMinimize = new System.Windows.Forms.Button();
        	this.textBox2 = new System.Windows.Forms.TextBox();
        	this.SuspendLayout();
        	// 
        	// BrowseButton
        	// 
        	this.BrowseButton.BackColor = System.Drawing.Color.DimGray;
        	this.BrowseButton.FlatAppearance.BorderSize = 0;
        	this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.BrowseButton.ForeColor = System.Drawing.SystemColors.WindowFrame;
        	this.BrowseButton.Location = new System.Drawing.Point(104, 309);
        	this.BrowseButton.Margin = new System.Windows.Forms.Padding(2);
        	this.BrowseButton.Name = "BrowseButton";
        	this.BrowseButton.Size = new System.Drawing.Size(157, 63);
        	this.BrowseButton.TabIndex = 0;
        	this.BrowseButton.UseVisualStyleBackColor = false;
        	this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
        	// 
        	// textBox1
        	// 
        	this.textBox1.Location = new System.Drawing.Point(41, 246);
        	this.textBox1.Margin = new System.Windows.Forms.Padding(2);
        	this.textBox1.Name = "textBox1";
        	this.textBox1.Size = new System.Drawing.Size(291, 20);
        	this.textBox1.TabIndex = 2;
        	this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        	this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
        	// 
        	// NextButton1
        	// 
        	this.NextButton1.BackColor = System.Drawing.SystemColors.WindowFrame;
        	this.NextButton1.FlatAppearance.BorderSize = 0;
        	this.NextButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.NextButton1.ForeColor = System.Drawing.SystemColors.WindowFrame;
        	this.NextButton1.Location = new System.Drawing.Point(282, 361);
        	this.NextButton1.Margin = new System.Windows.Forms.Padding(2);
        	this.NextButton1.Name = "NextButton1";
        	this.NextButton1.Size = new System.Drawing.Size(84, 36);
        	this.NextButton1.TabIndex = 3;
        	this.NextButton1.UseVisualStyleBackColor = false;
        	this.NextButton1.Click += new System.EventHandler(this.NextButton1_Click);
        	// 
        	// folderBrowserDialog1
        	// 
        	this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
        	// 
        	// richTextBox1
        	// 
        	this.richTextBox1.BackColor = System.Drawing.Color.White;
        	this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
        	this.richTextBox1.Location = new System.Drawing.Point(69, 249);
        	this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
        	this.richTextBox1.Name = "richTextBox1";
        	this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
        	this.richTextBox1.Size = new System.Drawing.Size(230, 15);
        	this.richTextBox1.TabIndex = 4;
        	this.richTextBox1.Text = "                PENDING DIRECTORY SEARCH";
        	this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
        	// 
        	// buttonClose
        	// 
        	this.buttonClose.BackColor = System.Drawing.SystemColors.WindowFrame;
        	this.buttonClose.FlatAppearance.BorderSize = 0;
        	this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.buttonClose.ForeColor = System.Drawing.SystemColors.WindowFrame;
        	this.buttonClose.Location = new System.Drawing.Point(335, 10);
        	this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
        	this.buttonClose.Name = "buttonClose";
        	this.buttonClose.Size = new System.Drawing.Size(22, 22);
        	this.buttonClose.TabIndex = 5;
        	this.buttonClose.UseVisualStyleBackColor = false;
        	this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        	// 
        	// buttonMinimize
        	// 
        	this.buttonMinimize.BackColor = System.Drawing.SystemColors.WindowFrame;
        	this.buttonMinimize.FlatAppearance.BorderSize = 0;
        	this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.buttonMinimize.ForeColor = System.Drawing.SystemColors.WindowFrame;
        	this.buttonMinimize.Location = new System.Drawing.Point(316, 11);
        	this.buttonMinimize.Margin = new System.Windows.Forms.Padding(2);
        	this.buttonMinimize.Name = "buttonMinimize";
        	this.buttonMinimize.Size = new System.Drawing.Size(15, 19);
        	this.buttonMinimize.TabIndex = 6;
        	this.buttonMinimize.UseVisualStyleBackColor = false;
        	this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
        	// 
        	// textBox2
        	// 
        	this.textBox2.Location = new System.Drawing.Point(41, 275);
        	this.textBox2.Margin = new System.Windows.Forms.Padding(2);
        	this.textBox2.Name = "textBox2";
        	this.textBox2.Size = new System.Drawing.Size(290, 20);
        	this.textBox2.TabIndex = 7;
        	this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        	// 
        	// DirSearch
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
        	this.BackColor = System.Drawing.SystemColors.Control;
        	this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        	this.ClientSize = new System.Drawing.Size(375, 406);
        	this.Controls.Add(this.textBox2);
        	this.Controls.Add(this.buttonMinimize);
        	this.Controls.Add(this.buttonClose);
        	this.Controls.Add(this.richTextBox1);
        	this.Controls.Add(this.NextButton1);
        	this.Controls.Add(this.textBox1);
        	this.Controls.Add(this.BrowseButton);
        	this.DoubleBuffered = true;
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        	this.Margin = new System.Windows.Forms.Padding(2);
        	this.MaximumSize = new System.Drawing.Size(375, 406);
        	this.MinimumSize = new System.Drawing.Size(375, 406);
        	this.Name = "DirSearch";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "Please select your Star Wars Galxies folder";
        	this.Load += new System.EventHandler(this.Form1_Load);
        	this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LAUNCHOPTIONS_MouseClick);
        	this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LAUNCHOPTIONS_MouseDown);
        	this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LAUNCHOPTIONS_MouseMove);
        	this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LAUNCHOPTIONS_MouseUp);
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button NextButton1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.TextBox textBox2;
        
    }
}

