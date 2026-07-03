using DVLD.Classes;
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
    public partial class frmADDUpdateLocalDrivingLicense : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _SelectedPersonID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public frmADDUpdateLocalDrivingLicense()
        {
            InitializeComponent();
            _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
            _Mode = enMode.AddNew;
        }
        public frmADDUpdateLocalDrivingLicense(int AppID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = AppID;
            _Mode =enMode.Update;
        }
        void _FillLicenseClassData()
        {
            DataTable dt = clsLicenseClass.GetAllLicenseClasses();
            foreach(DataRow row in dt.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }
        void _LoadData()
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if(_LocalDrivingLicenseApplication == null )
            {
                MessageBox.Show("Application not Found");
                this.Close();
                return;
            }
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            lblHeader.Text = "   Update Local Driving License Application";
            lblApplicationID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToString("d");
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString( clsLicenseClass.Find( _LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblApplicationFees.Text =_LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedByUserID.Text = clsGlobals.CurrentUser.UserName;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            _SelectedPersonID = ctrlPersonCardWithFilter1.PersonID;



        }
        void _ResetDefualtValues()
        {
            _FillLicenseClassData();
            lblHeader.Text = "New Local Driving License Application";
            this.Text = "New Local Driving License Application";
            tpApplicationInfo.Enabled = false;
            lblCreatedByUserID.Text = clsGlobals.CurrentUser.UserName;
            lblApplicationDate.Text = DateTime.Now.ToString("d");
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationTypeFees.ToString();
            cbLicenseClass.SelectedIndex = 2;

        }
        private void frmADDUpdateLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_Mode == enMode.Update)
            {
                _LoadData();
                return;
            }

        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }
        private void btnNext_Click(object sender, EventArgs e)

        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tabControl1.SelectedIndex = 1;

                return;
            }
            if (_SelectedPersonID == -1)
            {
                MessageBox.Show("Applicant Must Be a Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tpApplicationInfo.Enabled = true;
            tabControl1.SelectedIndex = 1;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (_SelectedPersonID == -1)
            {
                MessageBox.Show("Applicant Must Be a Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int Age = DateTime.Now.Year - clsPerson.Find(_SelectedPersonID).DateOfBirth.Year;

            clsLicenseClass LicenseClass = clsLicenseClass.Find(cbLicenseClass.SelectedItem.ToString());
            if(Age<LicenseClass.MinimumAllowedAge)
            {
                MessageBox.Show("Person Is Younger Than The Minimum Allowed Age", "Age Restriction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClass.LicenseClassID);
            if(ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }

            
            if(clsLicense.IsLicenseExistByPersonID(_SelectedPersonID, LicenseClass.LicenseClassID) )
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID; 

            if(_Mode == enMode.AddNew)
                _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;

            _LocalDrivingLicenseApplication.ApplicationTypeID = 1;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblApplicationFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobals.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClass.LicenseClassID;


            if (_LocalDrivingLicenseApplication.Save())
            {
                lblApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();                
                _Mode = enMode.Update;
                lblHeader.Text = "Update Local Driving License Application";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
