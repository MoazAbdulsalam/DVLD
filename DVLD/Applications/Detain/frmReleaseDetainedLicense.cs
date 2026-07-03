using DVLD.Classes;
using DVLD.Drivers;
using DVLD.Licenses.Local_Licenses;
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

namespace DVLD.Applications.Detain
{
    public partial class frmReleaseDetainedLicense : Form
    {
        int _ApplicationID = -1 ;
        int _LicenseID = -1;
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }
        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }
        void _Load()
        {
            lblCreatedBy.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.CreatedByUserID.ToString();
            lblDetainDate.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainDate.ToString("d");
            lblDetainID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblFineFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID.ToString();
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationTypeFees.ToString();
            lblTotalFees.Text = (
                                Convert.ToInt32(lblApplicationFees.Text.Trim()) + Convert.ToInt32(lblFineFees.Text.Trim())
                                ).ToString();
        }
        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            btnRelease.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            if(_LicenseID != -1)
            {

                ctrlDriverLicenseInfoWithFilter1.LoadInfo(_LicenseID);
                ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            }
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {

            if (obj == -1)
            {
                frmReleaseDetainedLicense_Load(null, null);
                return;
            }
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;
            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("License Is Not Detained", "Not Detained", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }
            _Load();
            btnRelease.Enabled = true;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobals.CurrentUser.UserID, ref _ApplicationID))
            {
                MessageBox.Show("Couldn't Release", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Releasd Successfuly", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblApplicationID.Text = _ApplicationID.ToString();
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled=false;
            btnRelease.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(ctrlDriverLicenseInfoWithFilter1.LicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmListDriverLicenses frm = new frmListDriverLicenses(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
