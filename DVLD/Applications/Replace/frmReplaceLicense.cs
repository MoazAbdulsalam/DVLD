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

namespace DVLD.Applications.Replace
{
    public partial class frmReplaceLicense : Form
    {
        int _NewLicenseID = -1;

        public frmReplaceLicense()
        {
            InitializeComponent();
        }
        int _GetApplicationType()
        {
            if (rbDamagedLicense.Checked)
                return (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            return (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

        }
        clsLicense.enIssueReason _GetIssueReason()
        {
            if (rbDamagedLicense.Checked)
                return clsLicense.enIssueReason.DamagedReplacement;
            return clsLicense.enIssueReason.LostReplacement;
        }
        private void frmReplaceLicense_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Checked = true;
            lblApplicationDate.Text = DateTime.Now.ToString("D");
            lblCreatedBy.Text = clsGlobals.CurrentUser.UserName;
            llShowLicenseHistory.Enabled = false;
            llShowNewLicenseInfo.Enabled = false;
            btnReplace.Enabled = false;
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Replacement For Damaged License";
            lblHeader.Text = Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationType()).ApplicationTypeFees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {

            this.Text = "Replacement For Lost License";
            lblHeader.Text = Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationType()).ApplicationTypeFees.ToString();
        }
       private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if (obj == -1)
                return;
            lblOldLicenseID.Text = obj.ToString();
            llShowLicenseHistory.Enabled = true;

            if(!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License Is Not Active", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnReplace.Enabled = false;
                return;
            }
            btnReplace.Enabled = true;
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            clsLicense NewLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Replace(_GetIssueReason(), clsGlobals.CurrentUser.UserID);

            lblReplaceAppIicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblReplacedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Replaced Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnReplace.Enabled = false;
            gbReplacementFor.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowNewLicenseInfo.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmListDriverLicenses frm = new frmListDriverLicenses(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
