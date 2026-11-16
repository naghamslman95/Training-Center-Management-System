using System;
using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }



        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                clsUser User = clsUser.Login(txtUserName.Text.Trim(), txtPassword.Text);
                if (User != null)
                {
                    if (!User.IsActive)
                    {
                        txtUserName.Focus();
                        MessageBox.Show("Your account is not Active, Contact Admin.", "InActive Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    clsGlobal.CurrentUser = User;
                    this.Hide();

                    frmMain Main = new frmMain();
                    Main.ShowDialog();
                    this.Show();
                    clsGlobal.CurrentUser = null;
                    txtPassword.Text = "";
                    txtUserName.Focus();

                }
                else
                {
                    txtUserName.Focus();
                    MessageBox.Show("Invalid Username/Password.", "Wrong Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong while signing in. Please try again or contact the administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
