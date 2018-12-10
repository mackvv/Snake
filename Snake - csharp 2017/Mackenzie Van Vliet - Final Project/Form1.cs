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
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            //sets difficulty
            if (radEasy.Checked == true)
            {
                GameForm.difficulty = 1;
            }
            else if (radMedium.Checked == true)
            {
                GameForm.difficulty = 2;
            }
            else if (radHard.Checked == true)
            {
                GameForm.difficulty = 3;
            }

            //opens game
            this.Hide();
            var gameForm = new GameForm();
            gameForm.Closed += (s, args) => this.Close();
            gameForm.Show();

        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            //opens about form
            this.Hide();
            var FormAbout = new FormAbout();
            FormAbout.Closed += (s, args) => this.Close();
            FormAbout.Show();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            //opens Help form
            this.Hide();
            var FormHelp = new FormHelp();
            FormHelp.Closed += (s, args) => this.Close();
            FormHelp.Show();
        }
    }
}
