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

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
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
        public int TestID { get { return _TestID; } }
        int _TestID = -1;
        int _TestAppointmentID;
        public ctrlScheduledTest()
        {
            InitializeComponent();
        }
        public void LoadData(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            _TestAppointment =clsTestAppointment.GetTestAppointmentByID(TestAppointmentID);
            if(_TestAppointment == null )
            {
                MessageBox.Show("No Appointment With ID=" + TestAppointmentID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(_TestAppointment.LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _TestAppointment.LocalDrivingLicenseApplicationID.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _TestID = _TestAppointment.GetTestID();
            lblDriverLicenseApplicationID.Text = _TestAppointment.LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = _TestAppointment.LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblApplicantName.Text = _TestAppointment.LocalDrivingLicenseApplication.ApplicantFullName;
            lblTrialCount.Text = _TestAppointment.LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();
            lblDate.Text=_TestAppointment.AppointmentDate.ToString("d");
            lblFees.Text =_TestAppointment.PaidFees.ToString();

            lblTestID.Text = _TestID == -1?"Not Taken Yet" : _TestID.ToString();

        }

        private void gbScheduleTest_Enter(object sender, EventArgs e)
        {

        }
    }
}
