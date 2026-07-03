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

namespace DVLD.Applications.LocalDrivingLicense
{
    public partial class ctrlLocalDrivingLicenseApplicationInfo : UserControl
    {
        int _LocalDrivingLicenseApplicationID = -1;
        public int LocalDrivingLicenseApplicationID { get { return _LocalDrivingLicenseApplicationID; } }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication;
        
        public ctrlLocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        void _FillCard()
        {
          
            lblLDLAppID.Text = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = clsLicenseClass.Find(LocalDrivingLicenseApplication.LicenseClassID).ClassName;
            lblPassedTest .Text=LocalDrivingLicenseApplication.GetPassedTestCount().ToString()+"/3";
            ctrlApplicationInfo1.LoadApplicationInfo(LocalDrivingLicenseApplication.ApplicationID);

        }
        public void LoadLocalDrivingLicenseApplicationByID(int LocalDrivingLicenseApplicationID)
        {
            LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            if(LocalDrivingLicenseApplication == null )
            {
                MessageBox.Show("This Local Driving License Aplication Does not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _FillCard();


        }
        public void LoadLocalDrivingLicenseApplicationByApplicationID(int ApplicationID)
        {
            LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);
            if (LocalDrivingLicenseApplication != null)
            {
                _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
                _FillCard();
            }

            MessageBox.Show("This Local Driving License Application Does not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;

        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
            if(LocalDrivingLicenseApplication.IsLicenseIssued())
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LocalDrivingLicenseApplication.GetActiveLicenseID());
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("License Has Not yet been Issued Or Is Not Active","No License",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void ctrlApplicationInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
