using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Net;
using System.Threading;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{

    public partial class launcher2 : Form
    {

        string imagepb = Application.StartupPath + "/resources/images/progressbar";
        string FTP = "ftp://173.242.114.16/files/";
        string swgdirsave = Application.StartupPath;  //loads the swg install directory from the patch file
        string[] userdir = Directory.GetFiles(System.IO.File.ReadAllText(Application.StartupPath + "/swgdir.cfg"));  //gets a list of all files in the SWG directory 
        string checks;
        int good = 0;
        
        public launcher2()


        {
            InitializeComponent();

        }

        
        private void launcher2_Load(object sender, EventArgs e)
        {
            Point mouseDownPoint = Point.Empty;
            backgroundWorker1.RunWorkerAsync();
            if ((good == 1))
            {
                              
                pictureBox2.Image = null;
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/complete.jpg");
                label1.ForeColor = Color.Aqua;
                label1.Text = "Ready to play!";
                PLAY.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/playgood.png");
            }
        
        
        
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) //displays current project status
        {
            

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            string FTP = "ftp://173.242.114.16/files/";
            string delsrvpatch = Application.StartupPath + "/lpatchsrv.cfg";
            File.Delete(delsrvpatch);
            string lpatchusr = System.IO.File.ReadAllText(Application.StartupPath + "/lpatchusr.cfg");
            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential("anonymous", "anonymous");
            wc.DownloadFile("ftp://173.242.114.16/files/lpatch.cfg", Application.StartupPath + "/lpatchsrv.cfg");
            
        }
         private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
         {
            string lpatchusr = System.IO.File.ReadAllText(Application.StartupPath + "/lpatchusr.cfg");
            string lpatchsrv = System.IO.File.ReadAllText(Application.StartupPath + "/lpatchsrv.cfg");


            if (lpatchsrv != lpatchusr)
            {
                this.Hide();
                Form3 form3 = new Form3();
                form3.Show();

            }

            else
            {
               
                
                {
                    backgroundWorker2.RunWorkerAsync();
                }




            }

            }
    

        

        private void options_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start(swgdirsave + "/swgclientsetup_r.exe"); //starts the swg client setup

        }

      
        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }



        private void PLAY_Click(object sender, EventArgs e)
        {

            {


            }



        }

        private void close_Click_1(object sender, EventArgs e)
        {
            if (good != 1)
            {
                Form5 form5 = new Form5();
                form5.Show();
            }
            else
            {
                Application.Exit();
            }
        }

        private void acct_Click_1(object sender, EventArgs e)
        {

            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
            player.Play();
            Form2 form2 = new Form2();
            form2.Show();
        }
        private void options_Click_1(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "/SwgClientSetup_r.exe"))
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
                player.Play();
                System.Diagnostics.Process.Start(swgdirsave + "/SwgClientSetup_r.exe");
            }
            else
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Error.wav");
                player.Play();
            }
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            good = 0;

                WebClient wc = new WebClient();
                wc.Credentials = new NetworkCredential("anonymous", "anonymous");
                PLAY.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/playbad.png");
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/small-loading.gif");

                if (File.Exists(swgdirsave + "/alut.dll"))
                {
                   
                }

                else
                {

                    wc.DownloadFile(FTP + "/alut.dll", swgdirsave + "/alut.dll");

                }
                if (File.Exists(swgdirsave + "/binkw32.dll"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar1.jpg");


                }
                else
                {
                    wc.DownloadFile(FTP + "/binkw32.dll", swgdirsave + "/binkw32.dll");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar1.jpg");

                }

                if (File.Exists(swgdirsave + "/bottom.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/bottom.tre", swgdirsave + "/bottom.tre");
                }

                if (File.Exists(swgdirsave + "/client.cfg"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/client.cfg", swgdirsave + "/client.cfg");
                }

                if (File.Exists(swgdirsave + "/data_animation_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_animation_00.tre", swgdirsave + "/data_animation_00.tre");
                }

                if (File.Exists(swgdirsave + "/data_music_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar2.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/data_music_00.tre", swgdirsave + "/data_music_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar2.jpg");
                }

                if (File.Exists(swgdirsave + "/data_other_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_other_00.tre", swgdirsave + "/data_other_00.tre");
                }
                if (File.Exists(swgdirsave + "/data_sample_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_sample_00.tre", swgdirsave + "/data_sample_00.tre");
                }
                if (File.Exists(swgdirsave + "/data_sample_01.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_sample_01.tre", swgdirsave + "/data_sample_01.tre");
                }
                if (File.Exists(swgdirsave + "/data_sample_02.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar3.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/data_sample_02.tre", swgdirsave + "/data_sample_02.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar3.jpg");
                }
                if (File.Exists(swgdirsave + "/data_sample_03.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_sample_03.tre", swgdirsave + "/data_sample_03.tre");
                }
                if (File.Exists(swgdirsave + "/data_sample_04.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_sample_04.tre", swgdirsave + "/data_sample_04.tre");
                }
                if (File.Exists(swgdirsave + "/data_skeletal_mesh_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_skeletal_mesh_00.tre", swgdirsave + "/data_skeletal_mesh_00.tre");
                }
                if (File.Exists(swgdirsave + "/data_skeletal_mesh_01.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar4.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/data_skeletal_mesh_01.tre", swgdirsave + "/data_skeletal_mesh_01.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar3.jpg");
                }
                if (File.Exists(swgdirsave + "/data_sku1_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_sku1_00.tre", swgdirsave + "/data_sku1_00.tre");
                }
                if (File.Exists(swgdirsave + "/data_sku1_01.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_sku1_01.tre", swgdirsave + "/data_sku1_01.tre");
                }
                if (File.Exists(swgdirsave + "/data_sku1_02.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_sku1_02.tre", swgdirsave + "/data_sku1_02.tre");
                }
                if (File.Exists(swgdirsave + "/data_sku1_03.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_sku1_03.tre", swgdirsave + "/data_sku1_03.tre");
                }
                if (File.Exists(swgdirsave + "/data_sku1_04.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar4.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/data_sku1_04.tre", swgdirsave + "/data_sku1_04.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar4.jpg");
                }
                if (File.Exists(swgdirsave + "/data_sku1_05.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_sku1_05.tre", swgdirsave + "/data_sku1_05.tre");
                }
                if (File.Exists(swgdirsave + "/data_sku1_06.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_sku1_06.tre", swgdirsave + "/data_sku1_06.tre");
                }
                if (File.Exists(swgdirsave + "/data_sku1_07.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_sku1_07.tre", swgdirsave + "/data_sku1_07.tre");
                }
                if (File.Exists(swgdirsave + "/data_static_mesh_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_static_mesh_00.tre", swgdirsave + "/data_static_mesh_00.tre");
                }
                if (File.Exists(swgdirsave + "/data_static_mesh_01.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar5.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/data_static_mesh_01.tre", swgdirsave + "/data_static_mesh_01.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar5.jpg");
                }
                if (File.Exists(swgdirsave + "/data_texture_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_texture_00.tre", swgdirsave + "/data_texture_00.tre");
                }
                if (File.Exists(swgdirsave + "/data_texture_01.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar6.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/data_texture_01.tre", swgdirsave + "/data_texture_01.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar6.jpg");
                }
                if (File.Exists(swgdirsave + "/data_texture_02.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_texture_02.tre", swgdirsave + "/data_texture_02.tre");
                }
                if (File.Exists(swgdirsave + "/data_texture_03.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_texture_03.tre", swgdirsave + "/data_texture_03.tre");
                }
                if (File.Exists(swgdirsave + "/data_texture_04.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar7.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/data_texture_04.tre", swgdirsave + "/data_texture_04.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar7.jpg");
                }
                if (File.Exists(swgdirsave + "/data_texture_05.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/data_texture_05.tre", swgdirsave + "/data_texture_05.tre");
                }
                if (File.Exists(swgdirsave + "/data_texture_06.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar8.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/data_texture_06.tre", swgdirsave + "/data_texture_06.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar8.jpg");
                }
                if (File.Exists(swgdirsave + "/data_texture_07.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar9.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/data_texture_07.tre", swgdirsave + "/data_texture_07.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar9.jpg");
                }

                if (File.Exists(swgdirsave + "/dbghelp.dll"))
                {
                   

                }
                else
                {
                    wc.DownloadFile(FTP + "/dbghelp.dll", swgdirsave + "/dbghelp.dll");
                }

                if (File.Exists(swgdirsave + "/gksvggdiplus.dll"))
                {

                }

                else
                {
                    wc.DownloadFile(FTP + "/gksvggdiplus.dll", swgdirsave + "/gksvggdiplus.dll");
                }
            
            
                if (File.Exists(swgdirsave + "/dbghelp_6.3.17.0.dll"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/dbghelp_6.3.17.0.dll", swgdirsave + "/dbghelp_6.3.17.0.dll");
                }
                if (File.Exists(swgdirsave + "/gl05_r.dll"))
                {
                    
                }

                else
                {
                    wc.DownloadFile(FTP + "/gl05_r.dll", swgdirsave + "/gl05_r.dll");


                }
               
            if (File.Exists(swgdirsave + "/gl06_r.dll"))
                {
                    
                }
                else
                {
                    wc.DownloadFile(FTP + "/gl06_r.dll", swgdirsave + "/gl06_r.dll");
                }

                if (File.Exists(swgdirsave + "/gl07_r.dll"))
                {
                    
                }
                else
                {
                    wc.DownloadFile(FTP + "/gl07_r.dll", swgdirsave + "/gl07_r.dll");
                }

                if (File.Exists(swgdirsave + "/hotfix_59_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/hotfix_59_client_00.tre", swgdirsave + "/hotfix_59_client_00.tre");
                }

                if (File.Exists(swgdirsave + "/hotfix_59_shared_00.tre"))
                {

                }

                else
                {
                    wc.DownloadFile(FTP + "/hotfix_59_shared_00.tre", swgdirsave + "/hotfix_59_shared_00.tre");
                        
                }

                if (File.Exists(swgdirsave + "/default_patch.tre"))
                {

                }

                else
                {
                    wc.DownloadFile(FTP + "/default_patch.tre", swgdirsave + "/default_patch.tre");
                }

                if (File.Exists(swgdirsave + "/dpvs.dll"))
                {
                   
                }

                else
                {
                    wc.DownloadFile(FTP + "/dpvs.dll", swgdirsave + "/dpvs.dll");
                }

                if (File.Exists(swgdirsave + "/js3250.dll"))
                {
                   
                }

                else
                {
                    wc.DownloadFile(FTP + "/js3250.dll", swgdirsave + "/js3250.dll");
                }

                if (File.Exists(swgdirsave + "/libsndfile-1.dll"))
                {

                  
                }

                else
                {
                    wc.DownloadFile(FTP + "/libsndfile-1.dll", swgdirsave + "/libsndfile-1.dll");
                }

                if (File.Exists(swgdirsave + "/live.cfg"))
                {

                }
                else
                {

                    wc.DownloadFile(FTP + "/live.cfg", swgdirsave + "/live.cfg");

                }

                if (File.Exists(swgdirsave + "/Mss32.dll"))
                {
                    
                }
                else
                {
                    wc.DownloadFile(FTP + "/Mss32.dll", swgdirsave + "/Mss32.dll");
                }

                if (File.Exists(swgdirsave + "/msvcr71.dll"))
                {
                    
                }
                else
                {
                    wc.DownloadFile(FTP + "/msvcr71.dll", swgdirsave + "/msvcr71.dll");
                }

              

                if (File.Exists(swgdirsave + "/nspr4.dll"))
                {
                    
                }
                else
                {
                    wc.DownloadFile(FTP + "/nspr4.dll", swgdirsave + "/nspr4.dll");
                }

                if (File.Exists(swgdirsave + "/nss3.dll"))
                {
                    
                }
                else
                {
                    wc.DownloadFile(FTP + "/nss3.dll", swgdirsave + "/nss3.dll");
                }

                if (File.Exists(swgdirsave + "/nssckbi.dll"))
                {
                   
                }
                else
                {
                    wc.DownloadFile(FTP + "/nssckbi.dll", swgdirsave + "/nssckbi.dll");
                }

                if (File.Exists(swgdirsave + "/options.cfg"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar10.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/options.cfg", swgdirsave + "/options.cfg");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar10.jpg");
                }

                if (File.Exists(swgdirsave + "/ortp.dll"))
                {
                   
                }
                else
                {
                    wc.DownloadFile(FTP + "/ortp.dll", swgdirsave + "/ortp.dll");
                }

                if (File.Exists(swgdirsave + "/patch_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_00.tre", swgdirsave + "/patch_00.tre");
                }

                if (File.Exists(swgdirsave + "/patch_01.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_01.tre", swgdirsave + "/patch_01.tre");
                }

                if (File.Exists(swgdirsave + "/patch_02.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_02.tre", swgdirsave + "/patch_02.tre");
                }
                if (File.Exists(swgdirsave + "/patch_03.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_03.tre", swgdirsave + "/patch_03.tre");
                }

                if (File.Exists(swgdirsave + "/patch_04.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_04.tre", swgdirsave + "/patch_04.tre");
                }
                if (File.Exists(swgdirsave + "/patch_05.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar11.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_05.tre", swgdirsave + "/patch_05.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar11.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_06.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_06.tre", swgdirsave + "/patch_06.tre");
                }

                if (File.Exists(swgdirsave + "/patch_07.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_07.tre", swgdirsave + "/patch_07.tre");
                }

                if (File.Exists(swgdirsave + "/patch_08.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_08.tre", swgdirsave + "/patch_08.tre");
                }
                if (File.Exists(swgdirsave + "/patch_09.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_09.tre", swgdirsave + "/patch_09.tre");
                }
                if (File.Exists(swgdirsave + "/patch_10.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_10.tre", swgdirsave + "/patch_10.tre");
                }
                if (File.Exists(swgdirsave + "/patch_11_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar12.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_11_00.tre", swgdirsave + "/patch_11_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar12.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_11_01.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_11_01.tre", swgdirsave + "/patch_11_01.tre");
                }
                if (File.Exists(swgdirsave + "/patch_11_02.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_11_02.tre", swgdirsave + "/patch_11_02.tre");
                }
                if (File.Exists(swgdirsave + "/patch_11_03.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_11_03.tre", swgdirsave + "/patch_11_03.tre");
                }
                if (File.Exists(swgdirsave + "/patch_12_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_12_00.tre", swgdirsave + "/patch_12_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_13_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_13_00.tre", swgdirsave + "/patch_13_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_14_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_14_00.tre", swgdirsave + "/patch_14_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_15_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar13.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_15_00.tre", swgdirsave + "/patch_15_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar13.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_15_01.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar14.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_15_01.tre", swgdirsave + "/patch_15_01.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar14.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_15_02.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar15.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_15_02.tre", swgdirsave + "/patch_15_02.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar15.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_16_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_16_00.tre", swgdirsave + "/patch_16_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_17_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_17_00.tre", swgdirsave + "/patch_17_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_18_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_18_client_00.tre", swgdirsave + "/patch_18_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_18_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_18_shared_00.tre", swgdirsave + "/patch_18_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_19_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_19_client_00.tre", swgdirsave + "/patch_19_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_19_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar16.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_19_shared_00.tre", swgdirsave + "/patch_19_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar16.jpg");
                }

                if (File.Exists(swgdirsave + "/patch_20_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_20_client_00.tre", swgdirsave + "/patch_20_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_20_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_20_shared_00.tre", swgdirsave + "/patch_20_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_23_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_23_client_00.tre", swgdirsave + "/patch_23_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_23_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar17.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_23_shared_00.tre", swgdirsave + "/patch_23_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar17.jpg");
                }

                if (File.Exists(swgdirsave + "/patch_24_client_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar18.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_24_client_00.tre", swgdirsave + "/patch_24_client_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar18.jpg");
                }

                if (!File.Exists(swgdirsave + "/patch_24_shared_00.tre"))
                {
                    wc.DownloadFile(FTP + "/patch_24_shared_00.tre", swgdirsave + "/patch_24_shared_00.tre");
                }
                else
                {
                  
                }

                if (File.Exists(swgdirsave + "/patch_24_client_01.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar19.jpg");
                }

                else
                {
                    wc.DownloadFile(FTP + "/patch_24_client_01.tre", swgdirsave + "/patch_24_client_01.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar19.jpg");

                }

                if (File.Exists(swgdirsave + "/patch_25_client_00.tre"))
                {

                }

                else
                {
                    wc.DownloadFile(FTP + "/patch_25_client_00.tre", swgdirsave + "/patch_25_client_00.tre");
                }

                if (File.Exists(swgdirsave + "/patch_25_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_25_shared_00.tre", swgdirsave + "/patch_25_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_26_client_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar20.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_26_client_00.tre", swgdirsave + "/patch_26_client_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar20.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_26_client_01.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_26_client_01.tre", swgdirsave + "/patch_26_client_01.tre");

                }
                if (File.Exists(swgdirsave + "/patch_26_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_26_shared_00.tre", swgdirsave + "/patch_26_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_27_client_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar20.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_27_client_00.tre", swgdirsave + "/patch_27_client_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar20.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_27_client_01.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar21.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_27_client_01.tre", swgdirsave + "/patch_27_client_01.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar21.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_27_client_02.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_27_client_02.tre", swgdirsave + "/patch_27_client_02.tre");
                }
                if (File.Exists(swgdirsave + "/patch_27_client_02.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_27_client_02.tre", swgdirsave + "/patch_27_client_02.tre");
                }
                if (File.Exists(swgdirsave + "/patch_27_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_27_shared_00.tre", swgdirsave + "/patch_27_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_28_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_28_client_00.tre", swgdirsave + "/patch_28_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_28_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_28_shared_00.tre", swgdirsave + "/patch_28_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_29_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_29_client_00.tre", swgdirsave + "/patch_29_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_29_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_29_shared_00.tre", swgdirsave + "/patch_29_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_30_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_30_client_00.tre", swgdirsave + "/patch_30_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_30_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar22.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_30_shared_00.tre", swgdirsave + "/patch_30_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar22.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_31_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_31_client_00.tre", swgdirsave + "/patch_31_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_31_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_31_shared_00.tre", swgdirsave + "/patch_31_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_32_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_32_client_00.tre", swgdirsave + "/patch_32_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_32_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar23.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_32_shared_00.tre", swgdirsave + "/patch_32_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar23.jpg");
                }

                if (File.Exists(swgdirsave + "/patch_33_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_33_client_00.tre", swgdirsave + "/patch_33_client_00.tre");
                }

                if (File.Exists(swgdirsave + "/patch_33_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_33_shared_00.tre", swgdirsave + "/patch_33_shared_00.tre");
                }

                if (File.Exists(swgdirsave + "/patch_34_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_34_client_00.tre", swgdirsave + "/patch_34_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_34_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_34_shared_00.tre", swgdirsave + "/patch_34_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_35_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_35_client_00.tre", swgdirsave + "/patch_35_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_35_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar24.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_35_shared_00.tre", swgdirsave + "/patch_35_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar24.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_36_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_36_client_00.tre", swgdirsave + "/patch_36_client_00.tre");
                }

                if (File.Exists(swgdirsave + "/patch_36_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_36_shared_00.tre", swgdirsave + "/patch_36_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_37_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_37_client_00.tre", swgdirsave + "/patch_37_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_37_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar25.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_37_shared_00.tre", swgdirsave + "/patch_37_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar25.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_38_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_38_client_00.tre", swgdirsave + "/patch_38_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_38_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_38_shared_00.tre", swgdirsave + "/patch_38_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_39_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_39_client_00.tre", swgdirsave + "/patch_39_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_39_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_39_shared_00.tre", swgdirsave + "/patch_39_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_40_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_40_client_00.tre", swgdirsave + "/patch_40_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_40_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar26.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_40_shared_00.tre", swgdirsave + "/patch_40_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar26.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_41_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_41_client_00.tre", swgdirsave + "/patch_41_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_41_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_41_shared_00.tre", swgdirsave + "/patch_41_shared_00.tre");

                }
                if (File.Exists(swgdirsave + "/patch_42_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_42_client_00.tre", swgdirsave + "/patch_42_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_42_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar27.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_42_shared_00.tre", swgdirsave + "/patch_42_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar27.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_43_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_43_client_00.tre", swgdirsave + "/patch_43_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_43_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar28.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_43_shared_00.tre", swgdirsave + "/patch_43_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar28.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_44_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_44_client_00.tre", swgdirsave + "/patch_44_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_44_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_44_shared_00.tre", swgdirsave + "/patch_44_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_45_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_45_client_00.tre", swgdirsave + "/patch_45_client_00.tre");
                }

                if (File.Exists(swgdirsave + "/patch_45_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_45_shared_00.tre", swgdirsave + "/patch_45_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_46_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_46_client_00.tre", swgdirsave + "/patch_46_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_46_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar29.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_46_shared_00.tre", swgdirsave + "/patch_46_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar29.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_47_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_47_client_00.tre", swgdirsave + "/patch_47_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_47_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_47_shared_00.tre", swgdirsave + "/patch_47_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_48_client_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar30.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_48_client_00.tre", swgdirsave + "/patch_48_client_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar30.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_48_client_01.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_48_client_01.tre", swgdirsave + "/patch_48_client_01.tre");
                }
                if (File.Exists(swgdirsave + "/patch_48_client_02.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar31.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_48_client_02.tre", swgdirsave + "/patch_48_client_02.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar31.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_48_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_48_shared_00.tre", swgdirsave + "/patch_48_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_49_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_49_client_00.tre", swgdirsave + "/patch_49_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_49_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_49_shared_00.tre", swgdirsave + "/patch_49_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_50_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_50_client_00.tre", swgdirsave + "/patch_50_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_50_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_50_shared_00.tre", swgdirsave + "/patch_50_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_51_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_51_client_00.tre", swgdirsave + "/patch_51_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_51_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_51_shared_00.tre", swgdirsave + "/patch_51_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_52_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_52_client_00.tre", swgdirsave + "/patch_52_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_52_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar32.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_52_shared_00.tre", swgdirsave + "/patch_52_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar32.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_53_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_53_client_00.tre", swgdirsave + "/patch_53_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_53_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_53_shared_00.tre", swgdirsave + "/patch_53_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_54_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_54_client_00.tre", swgdirsave + "/patch_54_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_54_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_54_shared_00.tre", swgdirsave + "/patch_54_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_55_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_55_client_00.tre", swgdirsave + "/patch_55_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_55_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar33.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_55_shared_00.tre", swgdirsave + "/patch_55_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar33.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_56_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_56_client_00.tre", swgdirsave + "/patch_56_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_56_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_56_shared_00.tre", swgdirsave + "/patch_56_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_57_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_57_client_00.tre", swgdirsave + "/patch_57_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_57_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_57_shared_00.tre", swgdirsave + "/patch_57_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_58_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_58_client_00.tre", swgdirsave + "/patch_58_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_58_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_58_shared_00.tre", swgdirsave + "/patch_58_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_12_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_12_00.tre", swgdirsave + "/patch_sku1_12_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_13_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_13_00.tre", swgdirsave + "/patch_sku1_13_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_14_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_14_00.tre", swgdirsave + "/patch_sku1_14_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_15_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_15_00.tre", swgdirsave + "/patch_sku1_15_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_16_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_16_00.tre", swgdirsave + "/patch_sku1_16_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_17_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar34.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_17_00.tre", swgdirsave + "/patch_sku1_17_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar34.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_18_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_18_client_00.tre", swgdirsave + "/patch_sku1_18_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_19_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_19_client_00.tre", swgdirsave + "/patch_sku1_19_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_19_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_19_shared_00.tre", swgdirsave + "/patch_sku1_19_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_23_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_23_client_00.tre", swgdirsave + "/patch_sku1_23_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_24_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_24_client_00.tre", swgdirsave + "/patch_sku1_24_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_24_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_24_shared_00.tre", swgdirsave + "/patch_sku1_24_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_26_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_26_client_00.tre", swgdirsave + "/patch_sku1_26_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_28_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_28_client_00.tre", swgdirsave + "/patch_sku1_28_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_28_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_28_shared_00.tre", swgdirsave + "/patch_sku1_28_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_29_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_29_client_00.tre", swgdirsave + "/patch_sku1_29_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_31_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_31_client_00.tre", swgdirsave + "/patch_sku1_31_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_32_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_32_client_00.tre", swgdirsave + "/patch_sku1_32_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_33_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_33_client_00.tre", swgdirsave + "/patch_sku1_33_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_34_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_34_client_00.tre", swgdirsave + "/patch_sku1_34_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_37_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_37_client_00.tre", swgdirsave + "/patch_sku1_37_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_37_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar35.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_37_shared_00.tre", swgdirsave + "/patch_sku1_37_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar35.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_39_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_39_client_00.tre", swgdirsave + "/patch_sku1_39_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_39_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_39_shared_00.tre", swgdirsave + "/patch_sku1_39_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_40_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_40_client_00.tre", swgdirsave + "/patch_sku1_40_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_40_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_40_shared_00.tre", swgdirsave + "/patch_sku1_40_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_41_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_41_client_00.tre", swgdirsave + "/patch_sku1_41_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_42_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_42_client_00.tre", swgdirsave + "/patch_sku1_42_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_44_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_44_client_00.tre", swgdirsave + "/patch_sku1_44_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_45_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_45_client_00.tre", swgdirsave + "/patch_sku1_45_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_53_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_53_client_00.tre", swgdirsave + "/patch_sku1_53_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_54_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_54_client_00.tre", swgdirsave + "/patch_sku1_54_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_55_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_55_client_00.tre", swgdirsave + "/patch_sku1_55_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_57_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_57_client_00.tre", swgdirsave + "/patch_sku1_57_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku1_58_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku1_58_client_00.tre", swgdirsave + "/patch_sku1_58_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku2_15_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar36.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_15_00.tre", swgdirsave + "/patch_sku2_15_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar36.jpg");
                }
                if (File.Exists(swgdirsave + "/patch_sku2_15_01.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_15_01.tre", swgdirsave + "/patch_sku2_15_01.tre");

                }
                if (File.Exists(swgdirsave + "/patch_sku2_16_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_16_00.tre", swgdirsave + "/patch_sku2_16_00.tre");

                }
                if (File.Exists(swgdirsave + "/patch_sku2_17_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_17_00.tre", swgdirsave + "/patch_sku2_17_00.tre");

                }
                if (File.Exists(swgdirsave + "/patch_sku2_23_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_23_client_00.tre", swgdirsave + "/patch_sku2_23_client_00.tre");

                }
                if (File.Exists(swgdirsave + "/patch_sku2_24_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_24_client_00.tre", swgdirsave + "/patch_sku2_24_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku2_24_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_24_shared_00.tre", swgdirsave + "/patch_sku2_24_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku2_26_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_26_client_00.tre", swgdirsave + "/patch_sku2_26_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku2_33_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_33_client_00.tre", swgdirsave + "/patch_sku2_33_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku2_33_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_33_shared_00.tre", swgdirsave + "/patch_sku2_33_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku2_34_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_34_client_00.tre", swgdirsave + "/patch_sku2_34_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku2_34_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_34_shared_00.tre", swgdirsave + "/patch_sku2_34_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku2_44_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_44_client_00.tre", swgdirsave + "/patch_sku2_44_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku2_44_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_44_shared_00.tre", swgdirsave + "/patch_sku2_44_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku2_56_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku2_56_client_00.tre", swgdirsave + "/patch_sku2_56_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku3_24_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku3_24_client_00.tre", swgdirsave + "/patch_sku3_24_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku3_24_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku3_24_shared_00.tre", swgdirsave + "/patch_sku3_24_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku3_25_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku3_25_client_00.tre", swgdirsave + "/patch_sku3_25_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku3_25_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku3_25_shared_00.tre", swgdirsave + "/patch_sku3_25_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku3_27_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku3_27_client_00.tre", swgdirsave + "/patch_sku3_27_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku3_27_shared_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku3_27_shared_00.tre", swgdirsave + "/patch_sku3_27_shared_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku3_33_client_00.tre"))
                {

                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku3_33_client_00.tre", swgdirsave + "/patch_sku3_33_client_00.tre");
                }
                if (File.Exists(swgdirsave + "/patch_sku3_33_shared_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar37.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/patch_sku3_33_shared_00.tre", swgdirsave + "/patch_sku3_33_shared_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar37.jpg");
                }
                if (File.Exists(swgdirsave + "/plc4.dll"))
                {
                    
                }
                else
                {
                    wc.DownloadFile(FTP + "/plc4.dll", swgdirsave + "/plc4.dll");
                }
                if (File.Exists(swgdirsave + "/plds4.dll"))
                {
                   
                }
                else
                {
                    wc.DownloadFile(FTP + "/plds4.dll", swgdirsave + "/plds4.dll");
                }
                if (File.Exists(swgdirsave + "/preload.cfg"))
                {
                   
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar38.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/preload.cfg", swgdirsave + "/preload.cfg");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar38.jpg");
                }
                if (File.Exists(swgdirsave + "/qt-mt305.dll"))
                {
                   
                }
                else
                {
                    wc.DownloadFile(FTP + "/qt-mt305.dll", swgdirsave + "/qt-mt305.dll");
                }
                if (File.Exists(swgdirsave + "/sku0_client.toc"))
                {
                    
                }
                else
                {
                    wc.DownloadFile(FTP + "/sku0_client.toc", swgdirsave + "/sku0_client.toc");
                }
                if (File.Exists(swgdirsave + "/sku1_client.toc"))
                {
                    
                }
                else
                {
                    wc.DownloadFile(FTP + "/sku1_client.toc", swgdirsave + "/sku1_client.toc");
                }
                if (File.Exists(swgdirsave + "/sku2_client.toc"))
                {
                    
                }
                else
                {
                    wc.DownloadFile(FTP + "/sku2_client.toc", swgdirsave + "/sku2_client.toc");
                }
                if (File.Exists(swgdirsave + "/sku3_client.toc"))
                {
                   
                }
                else
                {
                    wc.DownloadFile(FTP + "/sku3_client.toc", swgdirsave + "/sku3_client.toc");
                }
                if (File.Exists(swgdirsave + "/smime3.dll"))
                {
                    
                }
                else
                {
                    wc.DownloadFile(FTP + "/smime3.dll", swgdirsave + "/smime3.dll");
                }
                if (File.Exists(swgdirsave + "/softokn3.dll"))
                {
                   
                }
                else
                {
                    wc.DownloadFile(FTP + "/softokn3.dll", swgdirsave + "/softokn3.dll");
                }
                if (File.Exists(swgdirsave + "/ssl3.dll"))
                {
                   
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar39.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/ssl3.dll", swgdirsave + "/ssl3.dll");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar39.jpg");
                }
                if (File.Exists(swgdirsave + "/SwgClient_r.exe"))
                {
                    
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar40.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/SwgClient_r.exe", swgdirsave + "/SwgClient_r.exe");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar40.jpg");
                }
                if (File.Exists(swgdirsave + "/SwgClientSetup_r.exe"))
                {
                   

                }
                else
                {
                    wc.DownloadFile(FTP + "/SwgClientSetup_r.exe", swgdirsave + "/SwgClientSetup_r.exe");
                }
                if (File.Exists(swgdirsave + "/TREFix.exe"))
                {
                  

                }
                else
                {
                    wc.DownloadFile(FTP + "/TREFix.exe", swgdirsave + "/TREFix.exe");
                }
                if (File.Exists(swgdirsave + "/user.cfg"))
                {
                   

                }
                else
                {
                    wc.DownloadFile(FTP + "/user.cfg", swgdirsave + "/user.cfg");
                }
                if (File.Exists(swgdirsave + "/unicows.dll"))
                {
                   

                }
                else
                {
                    wc.DownloadFile(FTP + "/unicows.dll", swgdirsave + "/unicows.dll");
                }
                if (File.Exists(swgdirsave + "/vivoxplatform.dll"))
                {
                  
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar41.jpg");

                }
                else
                {
                    wc.DownloadFile(FTP + "/vivoxplatform.dll", swgdirsave + "/vivoxplatform.dll");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar41.jpg");
                }
                if (File.Exists(swgdirsave + "/vivoxsdk.dll"))
                {
                    

                }
                else
                {
                    wc.DownloadFile(FTP + "/vivoxsdk.dll", swgdirsave + "/vivoxsdk.dll");
                }
                if (File.Exists(swgdirsave + "/wrap_oal.dll"))
                {
                    

                }
                else
                {
                    wc.DownloadFile(FTP + "/wrap_oal.dll", swgdirsave + "/wrap_oal.dll");
                }
                if (File.Exists(swgdirsave + "/xpcom.dll"))
                {
                   

                }
                else
                {
                    wc.DownloadFile(FTP + "/xpcom.dll", swgdirsave + "/xpcom.dll");
                }
                if (File.Exists(swgdirsave + "/xul.dll"))
                {
                   

                }
                else
                {
                    wc.DownloadFile(FTP + "/xul.dll", swgdirsave + "/xul.dll");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar42.jpg");
                }

                if (File.Exists(swgdirsave + "/zlib1.dll"))
                {
                   
                   

                }
                else
                {
                    wc.DownloadFile(FTP + "/zlib1.dll", swgdirsave + "/zlib1.dll");
                  
                }
                if (File.Exists(swgdirsave + "/TREfix.exe"))
                {
                   


                }
                else
                {
                    wc.DownloadFile(FTP + "/TREFix.exe", swgdirsave + "/TREfix.exe");

                }
                if (File.Exists(swgdirsave + "/xul.dll"))
                {


                }
                else
                {
                    wc.DownloadFile(FTP + "/xul.dll", swgdirsave + "/xul.dll");
                   
                }
               
              
                if (File.Exists(swgdirsave + "/hotfix_sku1_59_client_00.tre"))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/complete.jpg");
                }
                else
                {
                    wc.DownloadFile(FTP + "/hotfix_sku1_59_client_00.tre", swgdirsave + "/hotfix_sku1_59_client_00.tre");
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/complete.jpg");

                   
                }
               
              

              

            
        }

        private void backgroundWorker2_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            good = 1;
            label1.ForeColor = Color.Aqua;
            label1.Text = "Ready to play!";
            pictureBox2.Image = null;
            PLAY.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/playgood.png");
           
            
        }

        private void PLAY_Click_1(object sender, EventArgs e)
        {
            if (good == 1)
            {
               
                
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Play.wav");
                    player.Play();
                    this.Hide();
                    WebClient wc = new WebClient();
                    wc.Credentials = new NetworkCredential("anonymous", "anonymous");
                    wc.DownloadFile(FTP + "/login.cfg", swgdirsave + "/login.cfg");
                    Directory.SetCurrentDirectory(swgdirsave);
                    System.Threading.Thread.Sleep(200);
                    System.Diagnostics.Process.Start(swgdirsave + "/SwgClient_r.exe");
                    System.Threading.Thread.Sleep(30000);
                    File.Delete(swgdirsave + "/login.cfg");
                    Application.Exit();
                   

                }
               
            else
            {

                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Error.wav");
                player.Play();
            }
        }

        private void options_SizeChanged(object sender, EventArgs e)
        {
               
                    backgroundWorker2.CancelAsync();
                
         
        }

        private void scan_Click(object sender, EventArgs e)
        {

            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
            player.Play();
            if (good == 1)
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/progressbar1.jpg");
                PLAY.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/playbad.png");
                backgroundWorker2.RunWorkerAsync();
                label1.ForeColor = Color.Red;
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "/resources/images/progressbar/small-loading.gif");
                label1.Text = "Downloading Patches";
            }

            else
            {

               
          
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "/resources/sounds/Click.wav");
            player.Play();
            LAUNCHOPTIONS launchoptions = new LAUNCHOPTIONS();
            launchoptions.Show();
        }


        Point mouseDownPoint = Point.Empty;
        private void launcher2_MouseDown(object sender, MouseEventArgs e)
        
        {
            mouseDownPoint = new Point(e.X, e.Y);
        
        }

        private void launcher2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void launcher2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownPoint = Point.Empty;
        }

        private void launcher2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDownPoint.IsEmpty)
                return;
            Form f = sender as Form;
            f.Location = new Point(f.Location.X + (e.X - mouseDownPoint.X), f.Location.Y + (e.Y - mouseDownPoint.Y));
        }

        }
        
       }

        


       
    

        
    

