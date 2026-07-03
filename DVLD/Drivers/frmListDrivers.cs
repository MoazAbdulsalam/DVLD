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

namespace DVLD.Drivers
{
    public partial class frmListDrivers : Form
    {
        DataTable dtListDrivers = clsDriver.GetAllDrivers();
        public frmListDrivers()
        {
            InitializeComponent();
        }
        void _Refresh()
        {
            DataTable dt = clsDriver.GetAllDrivers();
            dtListDrivers.Clear();
            dtListDrivers.Merge(dt);
            lblRecordsCount.Text = dgvListDrivers.Rows.Count.ToString();

        }
        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            dgvListDrivers.DataSource = dtListDrivers;
            if(dgvListDrivers.Rows.Count > 0 )
            {
                dgvListDrivers.Columns[0].HeaderText = "Driver ID";
                dgvListDrivers.Columns[0].Width = 100;

                dgvListDrivers.Columns[1].HeaderText = "PersonID";
                dgvListDrivers.Columns[1].Width = 100;

                dgvListDrivers.Columns[2].HeaderText = "National No";
                dgvListDrivers.Columns[2].Width = 100;

                dgvListDrivers.Columns[3].HeaderText = "Full Name";
                dgvListDrivers.Columns[3].Width = 300;

                dgvListDrivers.Columns[4].HeaderText = "Date";
                dgvListDrivers.Columns[4].Width = 200;

                dgvListDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvListDrivers.Columns[5].Width = 100;
            }
            lblRecordsCount.Text = dgvListDrivers.Rows.Count.ToString();
        }

        private void txtbxSearch_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Active Licenses":
                    FilterColumn = "NumberOfActiveLicenses";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (txtbxSearch.Text.Trim() == "" || FilterColumn == "None")
            {
                dtListDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvListDrivers.Rows.Count.ToString();
                return;
            }
            if( FilterColumn == "PersonID" || FilterColumn == "DriverID" || FilterColumn == "NumberOfActiveLicenses" )
                dtListDrivers.DefaultView.RowFilter = $"[{FilterColumn}] ={txtbxSearch.Text.Trim()}";
            else
                dtListDrivers.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{txtbxSearch.Text.Trim()}%'";



            lblRecordsCount.Text = dgvListDrivers.RowCount.ToString();
        }

        private void txtbxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "Person ID" || cbFilter.SelectedItem.ToString() == "Driver ID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                {
                    e.Handled = true; return;
                }
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxSearch.Visible = cbFilter.SelectedIndex != 0;
            txtbxSearch.Text = "";
            _Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvListDrivers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmListDriverLicenses frm = new frmListDriverLicenses((int)dgvListDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _Refresh();
        }

        private void IssueInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void showLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDriverLicenses frm = new frmListDriverLicenses((int)dgvListDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _Refresh();

        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dgvListDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _Refresh();

        }
    }
}
