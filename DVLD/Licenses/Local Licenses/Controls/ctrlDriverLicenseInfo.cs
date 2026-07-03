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

namespace DVLD.Licenses.Local_Licenses.Controls
{

    public partial class ctrlDriverLicenseInfo : UserControl
    {
        int _LicenseID = -1;
        clsLicense _License;
        public int LicenseID { get { return _LicenseID; } }
        public clsLicense SelectedLicenseInfo { get { return _License; } }
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }
     

        void _LoadImage()
        {
            pbGendor.Image = _License.DriverInfo.PersonInfo.Gendor == 0 ? Resources.Man_32 : Resources.Woman_32;
            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonPicture.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Coud not Find this image: " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                pbPersonPicture.Image = _License.DriverInfo.PersonInfo.Gendor == 0 ? Resources.Male_512 : Resources.Female_512;

        }
        void _FillLicenseCard()
        {
            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblName.Text = _License.DriverInfo.PersonInfo.FullName();
            lblLicenseID.Text = _LicenseID.ToString();
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _License.DriverInfo.PersonInfo.Gendor == 0? "Male":"Female";
            lblIssueDate.Text = _License.IssueDate.ToString("d");
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == ""? "No Notes " : _License.Notes;
            lblIsActive.Text = _License.IsActive? "Yes" : "No";
            lblExpirationDate .Text = _License.ExpireDate.ToString("d");
            lblDateOfBirth.Text = _License.DriverInfo.PersonInfo.DateOfBirth.ToString("d");
            lblDriverID.Text = _License.DriverID.ToString();
            lblIsDetained.Text = _License.IsDetained.ToString();
            _LoadImage();
        }
        public void LoadLicenseInfo(int LicenseID)
        {
            _License = clsLicense.GetLicenseByLicenseID(LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Could not find License ID = " + LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                ctrlDriverLicenseInfo_Load(null, null);

                return;
               
            }
            _LicenseID = LicenseID;
            _FillLicenseCard();

        }
        public void LoadLicenseInfoByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplication)
        {
            clsLocalDrivingLicenseApplication _LicenseApplication =clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplication);
            if( _LicenseApplication == null )
            {
                ctrlDriverLicenseInfo_Load(null, null);
                return;
            }
            _License = clsLicense.GetLicenseByLicenseID(_LicenseApplication.GetActiveLicenseID());
            if ( _License == null )
            {
                ctrlDriverLicenseInfo_Load(null, null);

                return;
            }

            _LicenseID = _License.LicenseID;
            _FillLicenseCard();
        }

        private void ctrlDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            lblClass.Text ="???";
            lblName.Text = "???";
            lblLicenseID.Text = "???";
            lblNationalNo.Text = "???";
            lblGendor.Text =  "Male";
            lblIssueDate.Text = "???";
            lblIssueReason.Text = "???";
            lblNotes.Text = "???";
            lblIsActive.Text = "???";
            lblExpirationDate.Text = "???";
            lblDateOfBirth.Text = "???";
            lblDriverID.Text = "???";
            lblIsDetained.Text = "???";
            pbPersonPicture.Image = Resources.Male_512 ;

        }
    }
}
