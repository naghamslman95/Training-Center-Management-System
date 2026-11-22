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
    public partial class frmInstructorDetails : Form
    {
        private int _InstructorID;
        public frmInstructorDetails(int instructorID)
        {
            InitializeComponent();
            _InstructorID = instructorID;
        }

        private void frmInstructorDetails_Load(object sender, EventArgs e)
        {
            if (!ucInstructorInfo1.LoadInstructorInfo(_InstructorID))
            {
                MessageBox.Show("Instructor is not found!");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
