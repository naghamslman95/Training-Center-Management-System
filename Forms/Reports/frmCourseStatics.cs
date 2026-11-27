using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class frmCourseStatics : Form
    {
        private clsCourse _Course;
        public frmCourseStatics()
        {
            InitializeComponent();
        }
        private bool _FillCoursesComboBox()
        {
            DataTable DT = clsCourse.GetAllCourses();
            if (DT.Rows.Count == 0)
                return false;
            else
            {
                cbCourses.DisplayMember = "Title";
                cbCourses.ValueMember = "CourseID";
                cbCourses.DataSource = DT;
                return true;
            }

        }
        private void _FillCourseStatics(clsCourse Course)
        {
            lblTotalEnrollments.Text = clsEnrollment.GetCourseEnrollments(Course.CourseID).ToString();
            lblActiveEnrollments.Text = clsEnrollment.GetCourseEnrollmentStatusCount(Course.CourseID, clsEnrollment.enEnrollmentStatus.Active).ToString();
            lblAvailableSeats.Text = (Course.TotalSeats - clsEnrollment.ActiveEnrollmentsPerCourse(Course.CourseID)).ToString();
            lblRevenue.Text = clsPayment.GetTotalPaidByCourseID(Course.CourseID).ToString();

            double Cancelled = clsEnrollment.GetCourseEnrollmentStatusCount(Course.CourseID, clsEnrollment.enEnrollmentStatus.Cancelled);
            double Completed = clsEnrollment.GetCourseEnrollmentStatusCount(Course.CourseID, clsEnrollment.enEnrollmentStatus.Completed);
            if (Completed + Cancelled == 0)
                lblCompletionRate.Text = "N/A";
            else
                lblCompletionRate.Text = (Completed / (Completed + Cancelled) * 100).ToString("N0") + "%";

        }
        private void frmCourseStatics_Load(object sender, EventArgs e)
        {
            if (_FillCoursesComboBox())
            {
                lblPopularCourse.Text = clsCourse.GetMostPopularCourse();
            }
        }

        private void cbCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Course = clsCourse.FindByCourseID(Convert.ToInt32(cbCourses.SelectedValue));
            if (_Course != null)
                _FillCourseStatics(_Course);
        }
    }
}
