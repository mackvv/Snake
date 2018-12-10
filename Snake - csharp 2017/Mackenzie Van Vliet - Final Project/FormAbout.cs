using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mackenzie_Van_Vliet___Final_Project
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            //goes back to home
            this.Hide();
            var frmHome = new frmHome();
            frmHome.Closed += (s, args) => this.Close();
            frmHome.Show();
        }

       
    }
}
