using DVLD.Classes;
using DVLDBusinessLayer;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmLogin : Form
    {
    
        
        clsUser _User;
        public frmLogin()
        {
            InitializeComponent();

        }
        

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            _User = clsUser.FindByUserNameAndPassword( txtbxUserName.Text,clsCryptography.ComputeHash( txtbxPassword.Text));
            if (_User == null)
            {
                MessageBox.Show("UserName Or Password Is wrong","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(!_User.IsActive)
            {
                MessageBox.Show("User IS Not Active ,Contact Your Admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (chbRememberMe.Checked)
                clsGlobals.RememberUserNameAndPassword(txtbxUserName.Text.Trim(), txtbxPassword.Text.Trim());
            else
                clsGlobals.RememberUserNameAndPassword("","");




            
            clsGlobals.CurrentUser = _User;
            Form frm = new MainForm(this);
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string USerName = "", Password = "";
            if(clsGlobals.GetStoredCredential(ref USerName, ref Password))
            {
                txtbxPassword.Text = Password;
                txtbxUserName.Text = USerName;
                chbRememberMe.Checked = true;
            }
            else
            chbRememberMe.Checked = false;




        }
        public  void Clear()
        {
            txtbxPassword.Text = "";
            txtbxUserName.Text = "";
            chbRememberMe.Checked= false;
        }
    }
}
