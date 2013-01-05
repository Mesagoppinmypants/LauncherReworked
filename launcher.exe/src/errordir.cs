using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PswgLauncher
{
    public partial class errordir : Form
    {
    	
    	private GuiController Controller;
    	
        public errordir(GuiController gc)
        {
        	this.Controller = gc;
            InitializeComponent();
            this.Icon= Controller.GetAppIcon();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
