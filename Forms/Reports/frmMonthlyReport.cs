using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class frmMonthlyReport : Form
    {
        public frmMonthlyReport()
        {
            InitializeComponent();
        }

        private void frmMonthlyReport_Load(object sender, EventArgs e)
        {
            cbMonths.SelectedIndex = DateTime.Now.Month -1;
            nudYears.Minimum = 2000;
            nudYears.Maximum = DateTime.Now.Year;
            nudYears.Value = DateTime.Now.Year;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            DateTime Start = new DateTime(Convert.ToInt32(nudYears.Value), cbMonths.SelectedIndex+1, 1);
            DateTime NextMonthStart = Start.AddMonths(1);
            lblNewEnrollments.Text = clsEnrollment.GetEnrollmentsPerMonth(Start, NextMonthStart).ToString();
            lblActive.Text = clsEnrollment.GetEnrollmentStatusCountPerMonth(Start, NextMonthStart, clsEnrollment.enEnrollmentStatus.Active).ToString();
            lblCancelled.Text = clsEnrollment.GetEnrollmentStatusCountPerMonth(Start, NextMonthStart, clsEnrollment.enEnrollmentStatus.Cancelled).ToString();
            lblCompleted.Text = clsEnrollment.GetEnrollmentStatusCountPerMonth(Start, NextMonthStart, clsEnrollment.enEnrollmentStatus.Completed).ToString();

            lblCollectedRevenue.Text = clsPayment.GetCollectedRevenuePerMonth(Start, NextMonthStart).ToString();
            lblOutstanding.Text = clsEnrollment.GetOutstandingPerMonth(Start, NextMonthStart).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
