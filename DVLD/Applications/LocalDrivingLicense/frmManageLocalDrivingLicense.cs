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

namespace DVLD.Applications.LocalDrivingLicense
{
    public partial class frmManageLocalDrivingLicense : Form
    {
        DataTable dtListApplications;

        public frmManageLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            dtListApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvListLocalDrivingLicenseApplication.DataSource = dtListApplications;
            cbFilter.SelectedIndex = 0;
            if (dgvListLocalDrivingLicenseApplication.Rows.Count > 0)
            {
                dgvListLocalDrivingLicenseApplication.Columns[0].HeaderText = "L.D.L.A ID";
                dgvListLocalDrivingLicenseApplication.Columns[0].Width = 120;

                dgvListLocalDrivingLicenseApplication.Columns[1].HeaderText = "Class Name";
                dgvListLocalDrivingLicenseApplication.Columns[1].Width = 220;



                dgvListLocalDrivingLicenseApplication.Columns[2].HeaderText = "National No";
                dgvListLocalDrivingLicenseApplication.Columns[2].Width = 150;

                dgvListLocalDrivingLicenseApplication.Columns[3].HeaderText = "Full Name";
                dgvListLocalDrivingLicenseApplication.Columns[3].Width = 200;

                dgvListLocalDrivingLicenseApplication.Columns[4].HeaderText = "Application Date";
                dgvListLocalDrivingLicenseApplication.Columns[4].Width = 200;



                dgvListLocalDrivingLicenseApplication.Columns[5].HeaderText = "Passed Tests";
                dgvListLocalDrivingLicenseApplication.Columns[5].Width = 100;

                dgvListLocalDrivingLicenseApplication.Columns[6].HeaderText = "Status";
                dgvListLocalDrivingLicenseApplication.Columns[6].Width = 100;
            }
            lblRecordsCount.Text = dgvListLocalDrivingLicenseApplication.Rows.Count.ToString();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxSearch.Visible = cbFilter.SelectedIndex != 0;
            txtbxSearch.Text = "";
            dtListApplications.DefaultView.RowFilter = "";
        }

        private void txtbxSearch_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "L.D.L.A ID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "Class Name":
                    FilterColumn = "ClassName";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";

                    break;
                case "Passed Test Count":
                    FilterColumn = "PassedTestCount";
                    break;
                case "Status":
                    FilterColumn = "Status";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtbxSearch.Text.Trim() == "" || FilterColumn == "None")
            {
                dtListApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dtListApplications.Rows.Count.ToString();
                return;
            }
            if (FilterColumn == "LocalDrivingLicenseApplicationID" || FilterColumn == "PassedTestCount")
                dtListApplications.DefaultView.RowFilter = $"[{FilterColumn}] ={txtbxSearch.Text.Trim()}";
            else
                dtListApplications.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{txtbxSearch.Text.Trim()}%'";



            lblRecordsCount.Text = dgvListLocalDrivingLicenseApplication.RowCount.ToString();

        }

        private void txtbxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "L.D.L.A ID" || cbFilter.SelectedItem.ToString() == "Passed Test Count")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true; return;
                }
            }
        }

        private void btnAddNewLocalDrivingApplication_Click(object sender, EventArgs e)
        {
            Form frm = new frmADDUpdateLocalDrivingLicense();
            frm.ShowDialog();
            frmManageLocalDrivingLicense_Load(null, null);
        }

        private void detailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmLocalDrivingLicenseDetails(Convert.ToInt32(dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            frmManageLocalDrivingLicense_Load(null, null);

        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmADDUpdateLocalDrivingLicense(Convert.ToInt32(dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            frmManageLocalDrivingLicense_Load(null, null);
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want To Delete this Application","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(Convert.ToInt32(dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value)))
            { 
                MessageBox.Show("Deleted Successfully", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmManageLocalDrivingLicense_Load(null, null);
            }
            else
                MessageBox.Show("Delete Field Because it has data Related To it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Cancel this Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            clsLocalDrivingLicenseApplication App = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(Convert.ToInt32(dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value));
            if(App!=null)
            {
                if(App.Cancel())
                {
                    MessageBox.Show("Canceled Successfully", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmManageLocalDrivingLicense_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Delete Field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseID = Convert.ToInt32(dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseID);
            bool IsNewApplication = localDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;
            bool LicenseExists = localDrivingLicenseApplication.IsLicenseIssued();
           
            tsmEdit.Enabled =   IsNewApplication && !LicenseExists && (localDrivingLicenseApplication.GetPassedTestCount() == 0);
            tsmDelete.Enabled = IsNewApplication && !LicenseExists;
            tsmCancel.Enabled = IsNewApplication && !LicenseExists;
           
            bool PassedVisionTest = clsLocalDrivingLicenseApplication.DoesPassTest(LocalDrivingLicenseID, clsTestType.enTestType.VisionTest);
            bool PassedWrittenTest = clsLocalDrivingLicenseApplication.DoesPassTest(LocalDrivingLicenseID, clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = clsLocalDrivingLicenseApplication.DoesPassTest(LocalDrivingLicenseID, clsTestType.enTestType.StreetTest);
           
            tsmScheduleTests.Enabled = (!PassedStreetTest || !PassedVisionTest || !PassedWrittenTest) && IsNewApplication;
            tsmIssueDrivingLicenseFirstTime.Enabled = (PassedStreetTest && PassedVisionTest && PassedWrittenTest) && IsNewApplication && !LicenseExists;
            tsmShowLicense.Enabled = LicenseExists;
           
            if(tsmScheduleTests.Enabled)
            {
                tsmScheduleVisionTest.Enabled = !PassedVisionTest;
                tsmScheduleWrittenTest.Enabled = PassedVisionTest && !PassedWrittenTest;
                tsmScheduleStreetTest.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;
            }
        }

        private void tsmScheduleVisionTest_Click(object sender, EventArgs e)
        {
            Form frm = new Tests.frmListTestAppointments((int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value,clsTestType.enTestType.VisionTest);
            frm.ShowDialog();
            frmManageLocalDrivingLicense_Load(null, null);
        }

        private void tsmScheduleWrittenTest_Click(object sender, EventArgs e)
        {

            Form frm = new Tests.frmListTestAppointments((int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value, clsTestType.enTestType.WrittenTest);
            frm.ShowDialog();
            frmManageLocalDrivingLicense_Load(null, null);
        }

        private void tsmScheduleStreetTest_Click(object sender, EventArgs e)
        {

            Form frm = new Tests.frmListTestAppointments((int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value, clsTestType.enTestType.StreetTest);
            frm.ShowDialog();
            frmManageLocalDrivingLicense_Load(null, null);
        }

        private void tsmIssueDrivingLicenseFirstTime_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicenseForFirstTime frm = new frmIssueDriverLicenseForFirstTime((int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManageLocalDrivingLicense_Load(null, null);

        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication App = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID((int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            frmShowLicenseInfo frm = new frmShowLicenseInfo(App.GetActiveLicenseID());
            frm.ShowDialog();
            frmManageLocalDrivingLicense_Load(null, null);

        }

        private void tsmLicenseHistory_Click(object sender, EventArgs e)
        {
            frmListDriverLicenses frm = new frmListDriverLicenses(clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID((int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value).ApplicantPersonID);
            frm.ShowDialog();
            frmManageLocalDrivingLicense_Load(null, null);

        }
    }
}
