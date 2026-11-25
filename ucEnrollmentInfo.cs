using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class ucEnrollmentInfo : UserControl
    {
        private int _EnrollmentID = -1;
        private clsEnrollment _Enrollment;
        public int EnrollmentID
        {
            get { return _EnrollmentID; }
        }
        public clsEnrollment SelectedEnrollmentInfo
        {
            get { return _Enrollment; }
        }
        public ucEnrollmentInfo()
        {
            InitializeComponent();
        }
        public bool LoadEnrollmentInfo(int EnrollmentID)
        {
             _Enrollment = clsEnrollment.FindByEnrollmentID(EnrollmentID);

            if (_Enrollment == null)
            {
                ResetEnrollmentInfo();
                return false;
            }

            FillEnrollmentInfo(_Enrollment);
            return true;
        }

        private void FillEnrollmentInfo(clsEnrollment Enrollment)
        {
            lblCourseDetails.Enabled = true;
            lblStudentDetails.Enabled = true;

            _EnrollmentID = Enrollment.EnrollmentID;
            lblEnrollmentID.Text = Enrollment.EnrollmentID.ToString();
            lblCourse.Text = Enrollment.CourseInfo.Title;
            lblStudent.Text = Enrollment.StudentInfo.PersonInfo.FullName;
            lblEnrollmentDate.Text = Enrollment.EnrollmentDate.ToShortDateString();
            lblTotalPrice.Text = Enrollment.TotalPrice.ToString();
            lblPaidAmount.Text = clsPayment.GetTotalPaidByEnrollmentID(Enrollment.EnrollmentID).ToString();
            lblEnrollmentStatus.Text = Enrollment.EnrollmentStatus.ToString();
            lblCreatedBy.Text = Enrollment.CreatedByUserInfo.UserName;
        }
        private void ResetEnrollmentInfo()
        {
            lblCourseDetails.Enabled = false;
            lblStudentDetails.Enabled = false;

            _EnrollmentID = -1;
            _Enrollment = null;
            lblEnrollmentID.Text = "???";
            lblCourse.Text = "???";
            lblStudent.Text = "???";
            lblEnrollmentDate.Text = "???";
            lblTotalPrice.Text = "???";
            lblPaidAmount.Text = "???";
            lblEnrollmentStatus.Text = "???";
            lblCreatedBy.Text = "???";
        }

        private void lblCourseDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsCourse _Course = clsCourse.FindByCourseID(_Enrollment.CourseID);
            if (_Course != null)
            {
                frmCourseDetails frm = new frmCourseDetails(_Course.CourseID);
                frm.ShowDialog();
            }
        }

        private void lblStudentDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           clsStudent _Student = clsStudent.FindByStudentID(_Enrollment.StudentID);
            if (_Student != null)
            {
                frmStudentDetails frm = new frmStudentDetails(_Student.StudentID);
                frm.ShowDialog();
            }
        }
    }
}
