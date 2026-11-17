using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class ucPersonInfo : UserControl
    {
        private int _PersonID = -1;
        private clsPerson Person;

        public int PersonID
        {
            get { return _PersonID; }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return Person; }
        }
        public ucPersonInfo()
        {
            InitializeComponent();
        }
        public bool LoadPersonInfo(int PersonID)
        {
            Person = clsPerson.Find(PersonID);
            if (Person != null)
            {
                FillPersonInfo(Person);
                return true;
            }
            else
            {
                ResetPersonInfo();
                return false;
            }
        }
        public bool LoadPersonInfo(string NationalNo)
        {
            Person = clsPerson.Find(NationalNo);
            if (Person != null)
            {
                FillPersonInfo(Person);
                return true;
            }
            else
            {
                ResetPersonInfo();
                return false;
            }
        }

        private void FillPersonInfo(clsPerson Person)
        {
            lblEditPersonInfo.Enabled = true;

            _PersonID = Person.PersonID;
            lblPersonID.Text = Person.PersonID.ToString();
            lblName.Text = Person.FullName;
            lblNationalNo.Text = Person.NationalNo;
            lblGender.Text = (Person.Gender == false) ? "Male" : "Female";
            lblDateOfBirth.Text = Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblPhone.Text = Person.Phone;
            lblEmail.Text = Person.Email;
        }
        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "????";
            lblName.Text = "????";
            lblNationalNo.Text = "????";
            lblGender.Text = "????";
            lblDateOfBirth.Text = "????";
            lblPhone.Text = "????";
            lblEmail.Text = "????";
            lblEditPersonInfo.Enabled = false;
        }

        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }
    }
}
