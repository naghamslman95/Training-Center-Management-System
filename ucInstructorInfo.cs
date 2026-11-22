using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class ucInstructorInfo : UserControl
    {
        private int _InstructorID = -1;
        private clsInstructor _Instructor;

        public int InstructorID
        {
            get { return _InstructorID; }
        }
        public clsInstructor SelectedInstructorInfo
        {
            get { return _Instructor; }
        }
        public ucInstructorInfo()
        {
            InitializeComponent();
        }
        public bool LoadInstructorInfo(int InstructorID)
        {
            _Instructor = clsInstructor.FindByInstructorID(InstructorID);

            if (_Instructor == null || !ucPersonInfo1.LoadPersonInfo(_Instructor.PersonID))
            {
                ResetInstructorInfo();
                ucPersonInfo1.ResetPersonInfo();
                return false;
            }

            FillInstructorInfo(_Instructor);
            return true;
        }

        private void FillInstructorInfo(clsInstructor Instructor)
        {
            _InstructorID = Instructor.InstructorID;
            lblInstructorID.Text = Instructor.InstructorID.ToString();
            lblQualifications.Text = Instructor.Qualifications;
            lblCreatedBy.Text = Instructor.CreatedByUserInfo.UserName;
        }
        private void ResetInstructorInfo()
        {
            _InstructorID = -1;
            _Instructor = null;
            lblInstructorID.Text = "???";
            lblQualifications.Text = "???";
            lblCreatedBy.Text = "???";
        }
    }
}
