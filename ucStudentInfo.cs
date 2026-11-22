using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class ucStudentInfo : UserControl
    {
        private int _StudentID = -1;
        private clsStudent _Student;

        public int StudentID
        {
            get { return _StudentID; }
        }
        public clsStudent SelectedStudentInfo
        {
            get { return _Student; }
        }
        public ucStudentInfo()
        {
            InitializeComponent();
        }
        public bool LoadStudentInfo(int StudentID)
        {
            _Student = clsStudent.FindByStudentID(StudentID);

            if (_Student == null || !ucPersonInfo1.LoadPersonInfo(_Student.PersonID))
            {
                ResetStudentInfo();
                ucPersonInfo1.ResetPersonInfo();
                return false;
            }

            FillStudentInfo(_Student);
            return true;
        }

        private void FillStudentInfo(clsStudent Student)
        {
            _StudentID = Student.StudentID;
            lblStudentID.Text = Student.StudentID.ToString();
            lblStatus.Text = Student.Status.ToString();
            lblCreatedBy.Text = Student.CreatedByUserInfo.UserName;
        }
        private void ResetStudentInfo()
        {
            _StudentID = -1;
            lblStudentID.Text = "???";
            lblStatus.Text = "???";
            lblCreatedBy.Text = "???";
        }
    }
}
