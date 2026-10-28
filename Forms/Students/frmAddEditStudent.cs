using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Training_Center_Management_System
{
    public partial class frmAddEditStudent : Form
    {
        public frmAddEditStudent()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Text = txtID.Text  = txtEmail.Text  = "";
            dateTimePicker1.Value = DateTime.Now;
            for (int i = 0; i < chkBoxEnrolledCourses.Items.Count; i++)
            {
                chkBoxEnrolledCourses.SetItemChecked(i, false);
            }
            cmbStatus.SelectedIndex = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool IsValid = true;
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider1.SetError(txtName, "Title can not be empty");
                IsValid = false;
            }
            else
            {
                errorProvider1.SetError(txtName, "");
            }

            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                errorProvider1.SetError(txtID, "ID can not be empty");
                IsValid = false;
            }
            else
            {
                errorProvider1.SetError(txtID, "");
            }

            if (string.IsNullOrWhiteSpace(cmbStatus.Text))
            {
                errorProvider1.SetError(cmbStatus, "status can not be empty");
                IsValid = false;
            }
            else
            {
                errorProvider1.SetError(cmbStatus, "");
            }
            if (IsValid)
            MessageBox.Show("Student Added Successfully!");
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
