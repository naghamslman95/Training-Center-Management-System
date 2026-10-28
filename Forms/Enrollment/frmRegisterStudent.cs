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
    public partial class frmRegisterStudent : Form
    {
        public frmRegisterStudent()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            
        }

       

        private void frmRegisterStudent_Load(object sender, EventArgs e)
        {
        }

        private void txtPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            decimal totalCost = 0;
            decimal paidAmount = 0;

            decimal.TryParse(txtTotalCost.Text, out totalCost);
            decimal.TryParse(txtPaymentAmount.Text, out paidAmount);

            txtRemainingAmount.Text = (totalCost - paidAmount).ToString();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

    }
}
