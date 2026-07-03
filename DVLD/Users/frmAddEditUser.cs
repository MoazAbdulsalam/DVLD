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

namespace DVLD.Users
{
    public partial class frmAddEditUser : Form
    {
        public enum enMode { AddNew,Update}
        public enMode eMode;
        clsUser _User;
        int _PersonID = -1;
        int _UserID = -1;
        public frmAddEditUser()
        {
            InitializeComponent();
            _User = new clsUser();
            eMode= enMode.AddNew;
        }
        public frmAddEditUser(int UserID)
        {

            InitializeComponent();
            eMode= enMode.Update;
            _UserID = UserID;
        }
        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                MessageBox.Show("User not Found");
                this.Close();
                return;
            }
            lblHeader.Text = "   Update User";
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
            txtbxUserName.Text = _User.UserName;
            txtbxPassword.Text = _User.Password;
            txtbxConfirmPassword.Text = _User.Password;
            lblUserID.Text = _UserID.ToString();
            chbIsActive.Checked = _User.IsActive;

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            if (eMode == enMode.Update)
            {
                _LoadData();
                ctrlPersonCardWithFilter1.FilterEnabled = false;
            }
                
            else
            tpLoginInfo.Enabled = false;

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_PersonID==-1)
            {
                MessageBox.Show("User Must Be a Person","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(clsUser.IsUserExistForPersonID(_PersonID) && eMode==enMode.AddNew)
            {
                MessageBox.Show("This Person Already a User", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tpLoginInfo.Enabled = true;
            tabControl1.SelectedIndex = 1;
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;
        }


        //Validating
        private void txtbxUserName_Validating(object sender, CancelEventArgs e)
        {
            if(txtbxUserName.Text.Length < 4)
            {
                errorProvider1.SetError(txtbxUserName, "UserName Must be > 4");
                e.Cancel = true;
                return;
            }
            else
            {
                errorProvider1.SetError(txtbxUserName, "");
                e.Cancel = false;
            }
        
            if(clsUser.IsUserExistByUserName(txtbxUserName.Text) && txtbxUserName.Text!=_User.UserName)
            {
                errorProvider1.SetError(txtbxUserName, "UserName Taken");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtbxUserName, "");
                e.Cancel = false;
            }
        }

        private void txtbxPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtbxPassword.Text.Length < 4)
            {
                errorProvider1.SetError(txtbxPassword, "Password Must be > 4");
                e.Cancel = true;

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtbxPassword, "");
            }
        }

        private void txtbxConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtbxConfirmPassword.Text != txtbxPassword.Text)
            {
                errorProvider1.SetError(txtbxConfirmPassword, "Wrong Password");
                e.Cancel = true;
                return;

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtbxConfirmPassword, "");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Complete Requaired Fields","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            _User.UserName = txtbxUserName.Text.Trim();
            _User.PersonID = _PersonID;
            _User.Password = txtbxPassword.Text.Trim();
            _User.IsActive = chbIsActive.Checked;

            if(_User.Save())
            {
                MessageBox.Show("Saved Succefully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                eMode = enMode.Update;
                lblHeader.Text = "   Update User";
                lblUserID.Text = _User.UserID.ToString();
            }
            else
                MessageBox.Show("Save Failed", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
