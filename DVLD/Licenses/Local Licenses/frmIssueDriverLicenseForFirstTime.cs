using DVLD.Classes;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmIssueDriverLicenseForFirstTime : Form
    {
        int _LocalDrivingLicenseApplicationID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public frmIssueDriverLicenseForFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void frmIssueDriverLicenseForFirstTime_Load(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null )
            {
                MessageBox.Show("No Application With ID=" + _LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if(!_LocalDrivingLicenseApplication.DoesPassedAllTests() )
            {
                MessageBox.Show("Must Pass All Test ","Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (_LocalDrivingLicenseApplication.IsLicenseIssued())
            {
                MessageBox.Show("License Already Exists" + _LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlLocalDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationByID(_LocalDrivingLicenseApplicationID);
            
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
           
           int LicenseID = _LocalDrivingLicenseApplication.IssueLicenseForTheFirstTime(txtbxNotes.Text, clsGlobals.CurrentUser.UserID);
            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Succeffuly With LicenseID =" + LicenseID, "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnIssue.Enabled = false;
                txtbxNotes.Enabled = false;
                this.Close();
                return;
            }
            MessageBox.Show("License Was not Issued ! ", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
