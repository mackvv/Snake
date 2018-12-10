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
    public partial class FormEnd : Form
    {
        public static int points;       //player points

        public FormEnd()
        {
            InitializeComponent();
        }

        private void FormEnd_Load(object sender, EventArgs e)
        {
            //puts points in label 
            lblPoints.Text = "FINAL POINTS: " + points;

        }
    }
}
