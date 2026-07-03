using DVLD.Applications.International_License;
using DVLD.Drivers;
using DVLD.People;
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

namespace DVLD.Licenses.International_Liceenses
{
    public partial class frmListInternationalLicensesApplications : Form
    {
        DataTable _dtInternationalLicenses;
        public frmListInternationalLicensesApplications()
        {
            InitializeComponent();
        }
        void _Refresh()
        {
            DataTable dt = clsInternationalLicense.GetAllInternationalLicenses();
            _dtInternationalLicenses.Clear();
            _dtInternationalLicenses.Merge(dt);
            lblRecordsCount.Text = dgvListInternationalLicense.Rows.Count.ToString();
        }
        private void frmListInternationalLicensesApplications_Load(object sender, EventArgs e)
        {
            _dtInternationalLicenses = clsInternationalLicense.GetAllInternationalLicenses();
            dgvListInternationalLicense.DataSource = _dtInternationalLicenses;
            if (dgvListInternationalLicense.Rows.Count > 0)
            {
                dgvListInternationalLicense.Columns[0].HeaderText = "Int.License ID";
                dgvListInternationalLicense.Columns[0].Width = 150;

                dgvListInternationalLicense.Columns[1].HeaderText = "App ID";
                dgvListInternationalLicense.Columns[1].Width = 150;

                dgvListInternationalLicense.Columns[2].HeaderText = "Driver ID";
                dgvListInternationalLicense.Columns[2].Width = 150;

                dgvListInternationalLicense.Columns[3].HeaderText = "L.License ID";
                dgvListInternationalLicense.Columns[3].Width = 160;

                dgvListInternationalLicense.Columns[4].HeaderText = "Issue Date";
                dgvListInternationalLicense.Columns[4].Width = 200;

                dgvListInternationalLicense.Columns[5].HeaderText = "Expiration Date";
                dgvListInternationalLicense.Columns[5].Width = 200;

                dgvListInternationalLicense.Columns[6].HeaderText = "Is Active";
                dgvListInternationalLicense.Columns[6].Width = 100;

            }
            cbFilter.SelectedIndex = 0;

            lblRecordsCount.Text = dgvListInternationalLicense.Rows.Count.ToString();
        }

        private void btnAddNewInternationalDrivingApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm  = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            _Refresh();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxSearch.Visible = cbFilter.SelectedIndex != 0;
            txtbxSearch.Text = "";
            _dtInternationalLicenses.DefaultView.RowFilter = "";
        }

        private void txtbxSearch_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Int.License ID
            //Application ID
            //Driver ID
            //L.License ID
            switch (cbFilter.Text)
            {
                case "Int.License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;

                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "L. License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

           
            if (txtbxSearch.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalLicenses.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtInternationalLicenses.Rows.Count.ToString();
                return;
            }

            _dtInternationalLicenses.DefaultView.RowFilter = $"[{FilterColumn}] ={txtbxSearch.Text.Trim()}";

            lblRecordsCount.Text = dgvListInternationalLicense.RowCount.ToString();

        }

        private void txtbxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true; return;
                
            
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(clsDriver.FindDriverByDriverID((int)dgvListInternationalLicense.CurrentRow.Cells[2].Value).PersonID);
            frm.ShowDialog();
            _Refresh();
        }

        private void showLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDriverLicenses frm = new frmListDriverLicenses(clsDriver.FindDriverByDriverID((int)dgvListInternationalLicense.CurrentRow.Cells[2].Value).PersonID);
            frm.ShowDialog();
            _Refresh();
        }

        private void IssueInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDriverInternationalLicense frm = new frmShowDriverInternationalLicense((int)dgvListInternationalLicense.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
