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

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {
        DataTable dtAppointments;
        int LocalDrivingLicenseApplicationID = -1;
        clsTestType.enTestType TestTypeID;

        public frmListTestAppointments(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.TestTypeID = TestTypeID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void _LoadImage()
        {
            switch (TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    pbHeader.Image = Resources.Vision_512;
                    lblHeader.Text = "Vision Test Appointment";
                    break;
                case clsTestType.enTestType.WrittenTest:
                    pbHeader.Image = Resources.Written_Test_512;
                    lblHeader.Text = "Written Test Appointment";
                    break;
                case clsTestType.enTestType.StreetTest:
                    pbHeader.Image = Resources.driving_test_512;
                    lblHeader.Text = "Street Test Appointment";
                    break;
                default:

                    pbHeader.Image = Resources.Vision_512;
                    lblHeader.Text = "Unknowen";
                    break;

            }
        }
        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadImage();
            ctrlLocalDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID);
            dtAppointments = clsTestAppointment.GetAllTestAppointmentsForTestTypeForApplication(LocalDrivingLicenseApplicationID, (int)TestTypeID);
            dgvListAppointments.DataSource = dtAppointments;
            lblRecordsCount.Text = dgvListAppointments.Rows.Count.ToString();

            if (dgvListAppointments.Rows.Count > 0)
            {
                dgvListAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvListAppointments.Columns[0].Width = 150;

                dgvListAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvListAppointments.Columns[1].Width = 200;

                dgvListAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvListAppointments.Columns[2].Width = 150;

                dgvListAppointments.Columns[3].HeaderText = "Is Locked";
                dgvListAppointments.Columns[3].Width = 100;
            }
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest(TestTypeID))
            {
                MessageBox.Show(" This Person Have an Active Schedual Test Appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest lastTest = localDrivingLicenseApplication.GetLastTestPerTestType(TestTypeID);
            if (lastTest == null)
            {
                //Open AddNew Test Form
                frmScheduleTest frm = new frmScheduleTest(LocalDrivingLicenseApplicationID, TestTypeID);
                frm.ShowDialog(); ;
                frmListTestAppointments_Load(null, null);
                return;
            }
            if (lastTest.TestResult == true)
            {
                MessageBox.Show("This Person Passed This Test");


                return;
            }

            // open retake test
            frmScheduleTest frm2 = new frmScheduleTest(/*lastTest.TestAppointmentInfo.*/LocalDrivingLicenseApplicationID, TestTypeID);
            frm2.ShowDialog();

            frmListTestAppointments_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmScheduleTest frm2 = new frmScheduleTest(LocalDrivingLicenseApplicationID, TestTypeID, (int)dgvListAppointments.CurrentRow.Cells[0].Value);
            frm2.ShowDialog();
            frmListTestAppointments_Load(null, null);

        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest((int)dgvListAppointments.CurrentRow.Cells[0].Value, TestTypeID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool Enabel = !clsTestAppointment.GetTestAppointmentByID((int)dgvListAppointments.CurrentRow.Cells[0].Value).IsLocked;

           // editToolStripMenuItem.Enabled = Enabel;
            takeTestToolStripMenuItem.Enabled = Enabel;
            
        }
    }
}
