using DVLD.Properties;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.International_Liceenses.Controls
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        int _InternationalLicenseID = -1;
        clsInternationalLicense _InternationalLicense;
        public int InternationalLicenseID { get { return _InternationalLicenseID; } }
        public clsInternationalLicense InternationalLicense { get { return _InternationalLicense; } }
        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }
        void _LoadImage()
        {
            pbGendor.Image = _InternationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? Resources.Man_32 : Resources.Woman_32;
            string ImagePath = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonPicture.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Coud not Find this image: " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                pbPersonPicture.Image = _InternationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? Resources.Male_512 : Resources.Female_512;

        }
        void _FillCard()
        {
            lblName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName();
            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _InternationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToString("D");
            lblApplicationID.Text =_InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _InternationalLicense.DriverInfo.PersonInfo.DateOfBirth.ToString("D");
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToString("D");
            _LoadImage();
        }
        public void Load(int InternationalLicenseID)
        {
            _InternationalLicense = clsInternationalLicense.Find(InternationalLicenseID);
            if( _InternationalLicense == null )
            {
                MessageBox.Show("No International License With ID = " + InternationalLicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _InternationalLicenseID = InternationalLicenseID;
            _FillCard();
        }

    }
}
