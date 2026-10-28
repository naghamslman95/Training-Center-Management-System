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
    public partial class frmAppearance : Form
    {
        public frmAppearance()
        {
            InitializeComponent();
        }


        private void btnBackground_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = colorDialog1.Color;
                //change all forms' backcolor
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ForeColor = colorDialog1.Color;
                btnBackground.ForeColor = colorDialog1.Color;
                btnColor.ForeColor = colorDialog1.Color;
                btnFontAndSize.ForeColor = colorDialog1.Color;
                //change all forms' forecolor
            }
        }

        private void btnFontAndSize_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                btnBackground.Font = fontDialog1.Font;
                btnColor.Font = fontDialog1.Font;
                btnFontAndSize.Font = fontDialog1.Font;

                //change all form's fonts
            }
        }
    }
}
