using DVLD.Applications.LocalDrivingLicense;
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

namespace DVLD.Applications.Detain
{
    public partial class frmManageDetainedLicenses : Form
    {
        DataTable dtDetain = clsDetainedLicense.GetALlDetainedLicenses();
        public frmManageDetainedLicenses()
        {
            InitializeComponent();
        }
        void _Refresh()
        {
            DataTable dt = clsDetainedLicense.GetALlDetainedLicenses();
            dtDetain.Clear();
            dtDetain.Merge(dt);
            lblRecordsCount.Text =dgvListDetain.Rows.Count.ToString();
        }

        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            dgvListDetain.DataSource = dtDetain;
            cbFilter.SelectedIndex = 0;
            if (dgvListDetain.Rows.Count > 0)
            {
                dgvListDetain.Columns[0].HeaderText = "D.ID";
                dgvListDetain.Columns[0].Width = 60;

                dgvListDetain.Columns[1].HeaderText = "L.ID";
                dgvListDetain.Columns[1].Width = 60;

                dgvListDetain.Columns[2].HeaderText = "Detain Date";
                dgvListDetain.Columns[2].Width = 200;

                dgvListDetain.Columns[3].HeaderText = "Is Released";
                dgvListDetain.Columns[3].Width = 60;

                dgvListDetain.Columns[4].HeaderText = "Fine Fees";
                dgvListDetain.Columns[4].Width = 120;

                dgvListDetain.Columns[5].HeaderText = "Release Date";
                dgvListDetain.Columns[5].Width = 200;

                dgvListDetain.Columns[6].HeaderText = "National No.";
                dgvListDetain.Columns[6].Width = 100;

                dgvListDetain.Columns[7].HeaderText = "Full Name";
                dgvListDetain.Columns[7].Width = 280;

                dgvListDetain.Columns[8].HeaderText = "Release App.ID";
                dgvListDetain.Columns[8].Width = 120;

            }
            lblRecordsCount.Text = dgvListDetain.Rows.Count.ToString();


        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxSearch.Visible = cbFilter.SelectedIndex != 0 && cbFilter.SelectedIndex!=2;
            txtbxSearch.Text = "";
            cbIsReleased.Visible = cbFilter.SelectedIndex == 2;

            dtDetain.DefaultView.RowFilter = "";
        }

        private void txtbxSearch_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilter.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;

                case "Is Released":
                    FilterColumn = "IsReleased";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";

                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (txtbxSearch.Text.Trim() == "" || FilterColumn == "None")
            {
                dtDetain.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvListDetain.Rows.Count.ToString();
                return;
            }
            if (FilterColumn == "DetainID")
                dtDetain.DefaultView.RowFilter = $"[{FilterColumn}] ={txtbxSearch.Text.Trim()}";
            else
                dtDetain.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{txtbxSearch.Text.Trim()}%'";



            lblRecordsCount.Text = dgvListDetain.RowCount.ToString();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsReleased.SelectedItem)
            {
                case "All":
                    dtDetain.DefaultView.RowFilter = "";
                    break;
                case "Yes":
                    dtDetain.DefaultView.RowFilter = "[IsReleased] = 1";
                    break;
                case "No":
                    dtDetain.DefaultView.RowFilter = "[IsReleased] = 0";
                    break;
            }
            lblRecordsCount.Text = dgvListDetain.RowCount.ToString();
        }

        private void txtbxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "Detain ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true; return;
                }
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm= new frmReleaseDetainedLicense();
            frm.ShowDialog();
            _Refresh();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            _Refresh();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(dgvListDetain.CurrentRow.Cells[6].Value.ToString());
            frm.ShowDialog();
        }

        private void showLicenseDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseDetails frm = new frmLocalDrivingLicenseDetails(Convert.ToInt32(dgvListDetain.CurrentRow.Cells[1].Value));
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(Convert.ToInt32(dgvListDetain.CurrentRow.Cells[1].Value));
            frm.ShowDialog();
            _Refresh();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = !Convert.ToBoolean(dgvListDetain.CurrentRow.Cells[3].Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
