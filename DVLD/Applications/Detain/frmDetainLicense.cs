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
    public partial class frmDetainLicense : Form
    {

        int _LicenseID;
        int _DetainID;
        public frmDetainLicense()
        {
            InitializeComponent();
        }

  



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Complete Requaired Fields");
                return;
            }
            _DetainID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Detain(Convert.ToInt32(txtbxFineFees.Text.Trim()), clsGlobals.CurrentUser.UserID);
            if( _DetainID == -1)
            {
                MessageBox.Show("Couldn't Detain","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Detained Succefuly", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblDetainID.Text = _DetainID.ToString();
            btnDetain.Enabled = false;
            txtbxFineFees.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
            llShowLicenseInfo.Enabled = _LicenseID != -1;
            llShowLicenseHistory.Enabled = _LicenseID != -1;
            lblLicenseID.Text = obj.ToString();


            if (_LicenseID == -1)
                return;

            if(clsDetainedLicense.IsLicenseDetained(obj))
            {


                btnDetain.Enabled = false;
                
                MessageBox.Show("License is Already Detained", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnDetain.Enabled = true;
            txtbxFineFees.Focus(); 
              
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            btnDetain.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;

            lblDetainDate.Text = DateTime.Now.ToString("d");
            lblCreatedBy.Text = clsGlobals.CurrentUser.UserID.ToString();

        }

        private void txtbxFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
            }

        }

        private void txtbxFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtbxFineFees.Text.Trim()))
            {
                e.Cancel = true;
                txtbxFineFees.Focus();
                errorProvider1.SetError(txtbxFineFees, "Required Feild");
                return;

            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txtbxFineFees, "");
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmListDriverLicenses frm = new frmListDriverLicenses(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }
    }
}
