using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class ucUserInfo : UserControl
    {
        private int _UserID = -1;
        private clsUser _User;

        public int UserID
        {
            get { return _UserID; }
        }
        public clsUser SelectedUserInfo
        {
            get { return _User; }
        }
        public ucUserInfo()
        {
            InitializeComponent();
        }
        public bool LoadUserInfo(int UserID)
        {
            _User = clsUser.FindByUserID(UserID);

            if (_User == null || !ucPersonInfo1.LoadPersonInfo(_User.PersonID))
            {
                ResetUserInfo();
                ucPersonInfo1.ResetPersonInfo();
                return false;
            }

            FillUserInfo(_User);
            return true;
        }

        private void FillUserInfo(clsUser User)
        {
            _UserID = User.UserID;
            lblUserID.Text = User.UserID.ToString();
            lblUserName.Text = User.UserName;
            lblRole.Text = User.Role.ToString();
            chbIsActive.Checked = User.IsActive;
        }
        public void ResetUserInfo()
        {
            _UserID = -1;
            lblUserID.Text = "???";
            lblUserName.Text = "???";
            lblRole.Text = "???";
            chbIsActive.Checked = false;
        }
    }
}
