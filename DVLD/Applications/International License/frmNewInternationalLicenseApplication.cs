using DVLD.Classes;
using DVLD.Drivers;
using DVLD.Licenses.International_Liceenses;
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

namespace DVLD.Applications.International_License
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        int _InternationalLicenseID = -1;
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnIssue.Enabled = false;
            lblApplicationDate.Text = DateTime.Now.ToString("D");
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString("D");
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationTypeFees.ToString();
            lblCreatedBy.Text = clsGlobals.CurrentUser.UserName;

        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if (obj == -1)
            {
                return;
            }
            if(!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("License Is Not Active", "Not Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpireDate < DateTime.Now)
            {
                MessageBox.Show("License Is Expired", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassID!=3)
            {
                MessageBox.Show("License Must Be From Class 3", "Not Class 3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int ID = clsInternationalLicense.GetActiveInternationalLicense(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);
            if (ID!=-1)
            {
                MessageBox.Show("Person Already Has International License with ID = "+ ID, "License Exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblLocalLicenseID.Text = obj.ToString();
            llShowLicenseHistory.Enabled = true;
            btnIssue.Enabled = true;

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want To Issue ?","Issue",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
                return;
            
            clsInternationalLicense InternationalLicnse = new clsInternationalLicense();
            InternationalLicnse.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicnse.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicnse.ApplicationDate = DateTime.Now;
            InternationalLicnse.LastStatusDate = DateTime.Now;
            InternationalLicnse.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationTypeFees;
            InternationalLicnse.CreatedByUserID = clsGlobals.CurrentUser.UserID;
            InternationalLicnse.DriverID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicnse.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.LicenseID;
            InternationalLicnse.IssueDate = DateTime.Now;
            InternationalLicnse.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicnse.IsActive = true;
            if (!InternationalLicnse.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            lblInternationalAppIicationID.Text = InternationalLicnse.ApplicationID.ToString();
            lblInternationalLicenseID.Text = InternationalLicnse.InternationalLicenseID.ToString();
            _InternationalLicenseID = InternationalLicnse.InternationalLicenseID;
            MessageBox.Show("International License Issued Successfully with ID=" + _InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            llShowLicenseInfo.Enabled = true;
            btnIssue.Enabled = true;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmListDriverLicenses frm = new frmListDriverLicenses(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverInternationalLicense frm = new frmShowDriverInternationalLicense(_InternationalLicenseID);
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
