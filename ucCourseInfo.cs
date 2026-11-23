using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class ucCourseInfo : UserControl
    {
        private int _CourseID = -1;
        private clsCourse _Course;

        public int CourseID
        {
            get { return _CourseID; }
        }
        public clsCourse SelectedCourseInfo
        {
            get { return _Course; }
        }
        public ucCourseInfo()
        {
            InitializeComponent();
        }

        public bool LoadCourseInfo(int CourseID)
        {
            _Course = clsCourse.FindByCourseID(CourseID);

            if (_Course == null )
            {
                ResetCourseInfo();
                return false;
            }

            FillCourseInfo(_Course);
            return true;
        }

        private void FillCourseInfo(clsCourse Course)
        {
            _CourseID = Course.CourseID;
            lblCourseID.Text = Course.CourseID.ToString();
            lblTitle.Text = Course.Title;
            lblInstructor.Text = Course.InstructorInfo.PersonInfo.FullName;
            lblDuration.Text = Course.Duration.ToString();
            lblPrice.Text = Course.Price.ToString();
            lblTotalSeats.Text = Course.TotalSeats.ToString();
            lblAvailableSeats.Text = (Course.TotalSeats - clsEnrollment.ActiveEnrollmentsPerCourse(Course.CourseID)).ToString();
            lblCreatedBy.Text = Course.CreatedByUserInfo.UserName;
        }
        private void ResetCourseInfo()
        {
            _CourseID = -1;
            _Course = null;
            lblCourseID.Text = "???";
            lblInstructor.Text = "???";
            lblDuration.Text = "???";
            lblPrice.Text = "???";
            lblTotalSeats.Text = "???";
            lblAvailableSeats.Text = "???";
            lblCreatedBy.Text = "???";
        }
    }
}
