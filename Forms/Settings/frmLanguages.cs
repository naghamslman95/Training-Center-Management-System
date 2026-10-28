using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Training_Center_Management_System
{
    public partial class frmLanguages : Form
    {
        public frmLanguages()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Arabic")
            {
                MessageBox.Show("Arabic Language will be applied");
            }
            if (comboBox1.SelectedItem == "English")
            {
                MessageBox.Show("English Language will be applied");
            }
        }
    }
}
