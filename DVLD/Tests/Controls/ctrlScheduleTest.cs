using DVLD.Classes;
using DVLD.Properties;
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
using static DVLDBusinessLayer.clsApplication;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public enum enMode { AddNew, Update }
         enMode _Mode;
        public enum enCreationMode { FirstTimeSchedule, RetakeTestSchedule }
         enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        int _LocalDrivingLicenseApplicationID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        public clsTestType.enTestType TestTypeID
        {
            get { return _TestTypeID; }
            set
            {
                _TestTypeID = value;
                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                        pbHeader.Image = Resources.Vision_512;
                        gbScheduleTest.Text = "Vision Test";
                        break;
                    case clsTestType.enTestType.StreetTest:
                        pbHeader.Image = Resources.driving_test_512;
                        gbScheduleTest.Text = "Street Test";
                        break;
                    case clsTestType.enTestType.WrittenTest:
                        pbHeader.Image = Resources.Written_Test_512;
                        gbScheduleTest.Text = "Writtin Test";
                        break;
                    default:
                        break;
                }
            }
        }
        clsTestAppointment _TestAppointment;
        int _TestAppointmentID =-1; 


        
        
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }
        public void LoadInfo(int LocalDriverLicenseApplicationID,int AppointmentID=-1)
        {
            if (AppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = LocalDriverLicenseApplicationID;
            _TestAppointmentID=AppointmentID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Local Driving License Application with ID= " + _LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnSave.Enabled = false;
                return;
            }

            if (_LocalDrivingLicenseApplication.DoesAttendTest(_TestTypeID))

                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;

            if (_LocalDrivingLicenseApplication.DoesAttendTest(TestTypeID))
            {
                lblRetakeFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationTypeFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblHeader.Text = "Schdule Retake Test";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled=false;
                lblHeader.Text = "Schdule Test";
                lblRetakeFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
                
            }

            lblDriverLicenseApplicationID.Text = _LocalDrivingLicenseApplication.ApplicationID.ToString();
            lblLicenseClass.Text = clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;
            lblApplicantName.Text = _LocalDrivingLicenseApplication.ApplicantFullName;
            lblTrialCount.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(TestTypeID).ToString();
            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestType.Find(TestTypeID).TestTypeFees.ToString();
                dateTimePicker1.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";

                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }
            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeFees.Text)).ToString();



            if (!HandleActiveTestAppointmentConstrtaint())
                return ;

            if (!HandleAppointmentLockedConstraint())
                return ;

            if (!HandlePrivousTestConstraint())
                return ;



        }
        bool HandleActiveTestAppointmentConstrtaint()
        {
            if(_Mode == enMode.AddNew && _LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestTypeID))
            {
                MessageBox.Show("Person Already Have an Active Test Appointment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker1.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }
            return true;
        }
        bool HandleAppointmentLockedConstraint()
        {
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Text = "Person Already sat for the test,Appointment Locked";
                lblUserMessage.Visible = true;
                dateTimePicker1.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }else
                lblUserMessage.Visible = false;

            return true;
        }
        bool HandlePrivousTestConstraint()
        {
            switch (_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblUserMessage.Visible=false;
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    if(!_LocalDrivingLicenseApplication.DoesPassTest(clsTestType.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Schedule,Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        dateTimePicker1.Enabled = false;
                        btnSave.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dateTimePicker1.Enabled = true;
                    }

                    return true;
                case clsTestType.enTestType.StreetTest:
                    if (!_LocalDrivingLicenseApplication.DoesPassTest(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Schedule,Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        dateTimePicker1.Enabled = false;
                        btnSave.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dateTimePicker1.Enabled = true;
                    }
                    return true;
                default:
                    return false;
                    
            }
        }
        bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.GetTestAppointmentByID(_TestAppointmentID);
            if (_TestAppointment == null)
            {
                MessageBox.Show("No Appintment with ID= " + _TestAppointmentID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            
            if( DateTime.Compare(DateTime.Now,_TestAppointment.AppointmentDate)<0)
                dateTimePicker1.MinDate = DateTime.Now;
            else
                dateTimePicker1.MinDate = _TestAppointment.AppointmentDate;
            
            if(_TestAppointment.RetakeTestApplicationID!=-1)
            {
                lblRetakeFees.Text = _TestAppointment.RetakeTestApplication.PaidFees.ToString();
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplication.ApplicationID.ToString();
                lblHeader.Text = "Schedule Retake Test";
                gbRetakeTestInfo.Enabled = true;
            }
            btnSave.Enabled = !_TestAppointment.IsLocked;

            return true;
        }
        bool HandleRetakeTestApplication()
        {
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                //incase the mode is add new and creation mode is retake test we should create a seperate application for it.
                //then we linke it with the appointment.

                //First Create Applicaiton 
                clsApplication Application = new clsApplication();

                Application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationTypeFees;
                Application.CreatedByUserID = clsGlobals.CurrentUser.UserID;

                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;

            }
            return true;
        }
        //----



       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!HandleRetakeTestApplication())
                return;
            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dateTimePicker1.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobals.CurrentUser.UserID;
           
            if(_TestAppointment.Save())
            {
                _Mode =enMode.Update;
                MessageBox.Show("Saved Succefuly");
                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Save Filed.");

            }

        }
    }
}
