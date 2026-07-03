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

namespace DVLD.Applications.RenewLicenseApplication
{
    public partial class frmRenewLicenseApplication : Form
    {
        int _NewLicenseID = -1;
        public frmRenewLicenseApplication()
        {
            InitializeComponent();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if(obj== -1)
            {
                btnRenew.Enabled = false;
                llShowNewLicenseInfo.Enabled = false;
                llShowLicenseHistory.Enabled = false;
                return;
            }
            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpireDate > DateTime.Now)
            {
                MessageBox.Show("Selected License Is Not Expired Yet , It Will Expire On " + ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpireDate, "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                llShowNewLicenseInfo.Enabled = false;


                return;
            }
            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License Is Not Active" , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                llShowNewLicenseInfo.Enabled = false;


                return;
            }
            btnRenew.Enabled = true;
            llShowLicenseHistory.Enabled = true;
            lblLicenseFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.PaidFees.ToString();
            lblOldLicenseID.Text = obj.ToString();
            lblExpirationDate.Text =DateTime.Now.AddYears( ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength).ToString("D");
            lblTotalFees.Text = (Convert.ToSingle(lblLicenseFees.Text)+Convert.ToSingle(lblApplicationFees.Text)).ToString();
            


        }

        private void frmRenewLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString("D");
            lblIssueDate.Text = DateTime.Now.ToString("D");
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationTypeFees.ToString();
            lblCreatedBy.Text = clsGlobals.CurrentUser.UserName;
            ctrlDriverLicenseInfoWithFilter1.SetTextBoxFocus();
            btnRenew.Enabled = false;
            llShowNewLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            

        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Renew ?", "Renew", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            clsLicense NewLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.RenewLicense(txtbxNotes.Text.Trim(),clsGlobals.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            lblRenewAppIicationID.Text = NewLicense.ApplicationID.ToString();
            lblRenewedLicenseID.Text = NewLicense.LicenseID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenew.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowNewLicenseInfo.Enabled = true;

        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmListDriverLicenses frm = new frmListDriverLicenses(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
