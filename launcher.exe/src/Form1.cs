using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Media;



namespace WindowsFormsApplication1
{

    public partial class DirSearch : Form
    {
        public DirSearch()
        {
            InitializeComponent();
        }
      


        private void Form1_Load(object sender, EventArgs e)
        {




            if (File.Exists(Application.StartupPath + "/swgdir.cfg"))  //checks if the directory has already been defined
            {
                launcher2 launcher2 = new launcher2();  //skips browse path if directory has been defined
                launcher2.Show();  // loads launcher if directory has been defined
                this.WindowState = FormWindowState.Minimized;  //minimizes browse window
                this.Hide();  //hides browse window
                this.Visible = false; //makes browse window no longer visible
                this.ShowInTaskbar = false;  //removes browse window from taskbar
                try
                {
                }

                catch (FileNotFoundException)
                {
                }
            }
        }
        
       

        private void textBox1_TextChanged(object sender, EventArgs e)  //used to display dir path
        {

        }



        private void BrowseButton_Click(object sender, EventArgs e)  //to browse for SWG install dir
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
            player.Play();
            folderBrowserDialog1.ShowDialog();  //opens the file browse window
            string swggetdir = folderBrowserDialog1.SelectedPath; // gathers data and writes it to swggetdir
            textBox2.Text = swggetdir;  // displays data to textbox1
            try
            {
                try
                {
                    try  //all try/catch statements intended to prevent launcher crashes (out of range, etc)
                    {
                        string[] files = Directory.GetFiles(swggetdir, "BugTool.exe", SearchOption.AllDirectories);
                        
                        string isvalid = files[0];
                       

                        
                       
                        

                        try
                        {
                            bool isvaliddirectory = isvalid.EndsWith("BugTool.exe");
                            
                           
                           
                            if (isvaliddirectory) 
                            //this if is for valid install dir
                            {
                                string writefile = Application.StartupPath + "/SWGDIR.cfg";
                                richTextBox1.ForeColor = Color.Green;
                                richTextBox1.Text = "SWG INSTALLATION FOUND";
                                File.Create(Application.StartupPath + "/SWGDIR.cfg").Close();
                                File.WriteAllText(writefile, swggetdir);




                            }
                            else  //all else statements result in invalid directory error (also intended to prevent out of range, etc)
                            {
                                richTextBox1.ForeColor = Color.Red;
                                richTextBox1.Text = "   INVALID STAR WARS GALAXIES DIRECTORY";

                            }
                            
                           
                        }
                        catch (IndexOutOfRangeException)
                        {
                            richTextBox1.ForeColor = Color.Red;
                            richTextBox1.Text = "   INVALID STAR WARS GALAXIES DIRECTORY";
                        }
                    }

                    catch (UnauthorizedAccessException)
                    {
                        richTextBox1.ForeColor = Color.Red;
                        richTextBox1.Text = "   INVALID STAR WARS GALAXIES DIRECTORY";

                    }

                }
                catch (IndexOutOfRangeException)
                {
                    richTextBox1.ForeColor = Color.Red;
                    richTextBox1.Text = "   INVALID STAR WARS GALAXIES DIRECTORY";
                }
              
            }
            catch (ArgumentException) { }
          
        }
        


        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)//used when searching directory
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)//used to display directory data to the user
        {

        }
        private void NextButton1_Click(object sender, EventArgs e)  //used to get to launcher if directory is valid
        {
            if (richTextBox1.Text ==  "SWG INSTALLATION FOUND")
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
                player.Play();
                launcher2 launcher2 = new launcher2();//loads launcher if valid directory
                launcher2.Show();
                this.Hide(); //hides browse window
               

            }
            else  //this prevents non valid directories from working
            {
                errordir errordir = new errordir(); //brings up invalid directory error window
                errordir.Show();
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
             
        }

       
       
    }
}

// int filecheck1; filecheck1 = swgclient_r.exe; if filecheck1 = swggetdir/SwgClient_r.exe