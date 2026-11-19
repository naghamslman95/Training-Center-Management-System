using System;
using System.Windows.Forms;

namespace Training_Center_Management_System
{
    public partial class frmUserDetails : Form
    {
        private int _UserID;
        public frmUserDetails(int UserID)
        {
            InitializeComponent();
            _UserID= UserID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserDetails_Load(object sender, EventArgs e)
        {
            if (!ucUserInfo1.LoadUserInfo(_UserID))
            {
                MessageBox.Show("User is not found!");
            }
        }
    }
}
