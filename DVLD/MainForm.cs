using DVLD.Applications;
using DVLD.Applications.Detain;
using DVLD.Applications.International_License;
using DVLD.Applications.LocalDrivingLicense;
using DVLD.Applications.RenewLicenseApplication;
using DVLD.Applications.Replace;
using DVLD.Classes;
using DVLD.Drivers;
using DVLD.Licenses.International_Liceenses;
using DVLD.People;
using DVLD.Tests.TestTypes;
using DVLD.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class MainForm : Form
    {
        frmLogin _Login;
        public MainForm(frmLogin login)
        {
            InitializeComponent();
            _Login = login;
        }
        frmManagePeople frmManagePeople= null;
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmManagePeople = new frmManagePeople();
            frmManagePeople.StartPosition = FormStartPosition.CenterParent;
            frmManagePeople.ShowDialog(this);

            //if (frmManagePeople == null || frmManagePeople.IsDisposed)
            //{
            //    frmManagePeople = new frmManagePeople();
            //    frmManagePeople.MdiParent = this;
            //    frmManagePeople.Show();






            //}
            //else
            //{
            //    frmManagePeople.Focus();
            //}
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmManagePeople = new frmManageUsers();
            frmManagePeople.StartPosition = FormStartPosition.CenterParent;
            frmManagePeople.ShowDialog(this);
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUserInfo(clsGlobals.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword(clsGlobals.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobals.CurrentUser = null;
            _Login.Clear();
            _Login.Show();
            

            this.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(clsGlobals.CurrentUser != null) 
                 Application.Exit();
        }

        private void tsmManageApplicationTypes_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageApplicationTypes();
            frm.ShowDialog();   
        }

        private void tsmManageTestTypes_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageTestTypes();
            frm.ShowDialog();
        }

        private void LocalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form frm = new frmManageLocalDrivingLicense();
            frm.ShowDialog();

        }

        private void newLocalDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmADDUpdateLocalDrivingLicense();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void newInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void internationalDrivingLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicensesApplications frm = new frmListInternationalLicensesApplications();
            frm.ShowDialog();
        }

        private void renewLocalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLicenseApplication frm = new frmRenewLicenseApplication();
            frm.ShowDialog();
        }

        private void replacmentForDammegedLostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLicense frm = new frmReplaceLicense();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void listDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDetainedLicenses frm = new frmManageDetainedLicenses();
            frm.ShowDialog();
        }
    }
}
