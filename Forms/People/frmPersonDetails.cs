using System;
using System.Windows.Forms;

namespace Training_Center_Management_System
{
    public partial class frmPersonDetails : Form
    {
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            if (!ucPersonInfo1.LoadPersonInfo(PersonID))
                MessageBox.Show("Person with ID " + PersonID + " is not found!");

        }
        public frmPersonDetails(string NationalNo)
        {
            InitializeComponent();
            if (!ucPersonInfo1.LoadPersonInfo(NationalNo))
                MessageBox.Show("Person with National No " + NationalNo + " is not found!");


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
