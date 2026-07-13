using DVLD.Classes;
using DVLDBusinessLayer;
using System;
using Shared;
using System.ComponentModel;

using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        int _UserID = -1;
        clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void btnColse_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _User = clsUser.FindByUserID(_UserID);
            if(_User == null)
            {
                MessageBox.Show("No Users With UserID "+_UserID,"No Users Found",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
            ctrlUserInfo1.LoadUser(_UserID);
        }


        private void txtbxCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if(clsCryptography.ComputeHash( txtbxCurrentPassword.Text.Trim()) !=_User.Password)
            {
                errorProvider1.SetError(txtbxCurrentPassword, "Wrong Password");
                e.Cancel = true;
            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txtbxCurrentPassword, "");
            }
        }

        private void txtbxConfirmPassword_Validating(object sender, CancelEventArgs e)
        {

            if(txtbxConfirmPassword.Text.Trim()!=txtbxNewPassword.Text.Trim())
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

        private void txtbxNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtbxNewPassword.Text.Trim().Length < 4)
            {
                errorProvider1.SetError(txtbxNewPassword, "Password Must be > 4");
                e.Cancel = true;

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtbxNewPassword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Complete Requaired Fields","Error",MessageBoxButtons.OK, MessageBoxIcon.Error) ;
                return;
            }

            if(_User.changePassword(clsCryptography.ComputeHash(txtbxNewPassword.Text.Trim())))
            {
                MessageBox.Show("Password Changed Succefully", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Password Change Filed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
