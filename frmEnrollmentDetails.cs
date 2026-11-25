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
    public partial class frmEnrollmentDetails : Form
    {
        private int _EnrollmentID;
        public frmEnrollmentDetails(int EnrollmentID)
        {
            InitializeComponent();
            _EnrollmentID = EnrollmentID;
        }

        private void frmEnrollmentDetails_Load(object sender, EventArgs e)
        {
            if (!ucEnrollmentInfo1.LoadEnrollmentInfo(_EnrollmentID))
            {
                MessageBox.Show("Enrollment is not found!");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
