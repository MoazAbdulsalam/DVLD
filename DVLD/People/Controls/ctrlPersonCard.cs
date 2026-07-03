using DVLD.People;
using DVLD.Properties;
using DVLDBusinessLayer;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        clsPerson _Person;
        int _PersonID = -1;
        public int PersonID {  get { return _PersonID; } }
        public clsPerson SelectedPersonInfo {  get { return _Person; } }



        public ctrlPersonCard()
        {
            InitializeComponent ();
           
        }
        private void  _ResetPersonInfo()
        {
            lblPersonID.Text = "[???]";
            lblName.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblGendor.Text = "[???]";
            lblEmail.Text = "[???]";
            lblAddress.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblPhone.Text = "[???]";
            lblCountry.Text = "[???]";
            pbPersonImage.Image = Resources.Male_512;
            lnklblEdit.Enabled = false;

        }
        private void _LoadPersonImage()
        {
            pbGendorImage.Image = _Person.Gendor == 0 ? Resources.Man_32 : Resources.Woman_32;
            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Coud not Find this image: " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                pbPersonImage.Image = _Person.Gendor == 0 ? Resources.Male_512 : Resources.Female_512; ;
        }
        private void _FillPersonInfo()
        {
            lnklblEdit.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.FullName();
            lblNationalNo.Text = _Person.NationalNo.ToString();
            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email ?? "";
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("d");
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = _Person.CountryInfo.CountryName;
            _LoadPersonImage();
        }

        public void LoadPerson(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if( _Person == null )
            {
                _ResetPersonInfo();
                MessageBox.Show("No Person With PersonID =" + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();
        }
        public void LoadPerson(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No Person With NationalNo =" + NationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _FillPersonInfo();
        }

        private void lnklblEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(_PersonID);
            frm.ShowDialog();
            LoadPerson(_PersonID);

        }

        private void gbPersonInformation_Enter(object sender, EventArgs e)
        {

        }
    }
}
