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
    public partial class frmFilePaths : Form
    {
        public frmFilePaths()
        {
            InitializeComponent();
        }


        private void btnSaveFiles_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSaveFiles.Text = folderBrowserDialog1.SelectedPath;

            }
        }

        private void btnBackupFiles_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtBackupFiles.Text = folderBrowserDialog1.SelectedPath;

            }
        }
    }
}
