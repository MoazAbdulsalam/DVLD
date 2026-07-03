using DVLD.Licenses.International_Liceenses;
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

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        DataTable _dtLocalLicenses;
        DataTable _dtInternationalLicenses;
        
        int _DriverID;
        clsDriver _SelectedDriverInfo;
        public clsDriver DriverInfo { get { return _SelectedDriverInfo; } }
        public int DriverID { get { return _DriverID; } }


        public ctrlDriverLicenses()
        {
            InitializeComponent();
            
        }

        void _LoadLocalDrivingLicenses()
        {
            _dtLocalLicenses = clsLicense.GetDriverLicenses(_DriverID);
            dgvListLocalLicenses.DataSource = _dtLocalLicenses;
            if (dgvListLocalLicenses.Rows.Count > 0)
            {
                dgvListLocalLicenses.Columns[0].HeaderText = "Lic ID";
                dgvListLocalLicenses.Columns[0].Width = 100;

                dgvListLocalLicenses.Columns[1].HeaderText = "App ID";
                dgvListLocalLicenses.Columns[1].Width = 100;

                dgvListLocalLicenses.Columns[2].HeaderText = "Class Name";
                dgvListLocalLicenses.Columns[2].Width = 250;

                dgvListLocalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvListLocalLicenses.Columns[3].Width = 200;

                dgvListLocalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvListLocalLicenses.Columns[4].Width = 200;

                dgvListLocalLicenses.Columns[5].HeaderText = "Is Active";
                dgvListLocalLicenses.Columns[5].Width = 100;

            }
            lblRecordsLocalCount.Text =dgvListLocalLicenses.Rows.Count.ToString();
        }
        void _LoadInternationalLicenses()
        {
            _dtInternationalLicenses = clsInternationalLicense.GetDriverlInternationalLicenses(DriverID);
            dgvListInternationalLicense.DataSource = _dtInternationalLicenses;
            if (dgvListInternationalLicense.Rows.Count > 0)
            {
                dgvListInternationalLicense.Columns[0].HeaderText = "Int.License ID";
                dgvListInternationalLicense.Columns[0].Width = 170;

                dgvListInternationalLicense.Columns[1].HeaderText = "App ID";
                dgvListInternationalLicense.Columns[1].Width = 160;

                dgvListInternationalLicense.Columns[2].HeaderText = "L.License ID";
                dgvListInternationalLicense.Columns[2].Width = 160;

                dgvListInternationalLicense.Columns[3].HeaderText = "Issue Date";
                dgvListInternationalLicense.Columns[3].Width = 200;

                dgvListInternationalLicense.Columns[4].HeaderText = "Expiration Date";
                dgvListInternationalLicense.Columns[4].Width = 200;
    
                dgvListInternationalLicense.Columns[5].HeaderText = "Is Active";
                dgvListInternationalLicense.Columns[5].Width = 100;

            }
            lblRecourdInternationalCount.Text = dgvListInternationalLicense.Rows.Count.ToString();
        }
        public void LoadByDriverID(int DriverID)
        {
            _DriverID = DriverID;
            _SelectedDriverInfo = clsDriver.FindDriverByDriverID(DriverID);
            if (_SelectedDriverInfo == null)
            {
                MessageBox.Show("Driver Does not Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadLocalDrivingLicenses();
            _LoadInternationalLicenses();
        }
        public void LoadByPersonID(int PersonID)
        {
            _SelectedDriverInfo = clsDriver.FindDriverByPersonID(PersonID);
            if (_SelectedDriverInfo == null)
            {
                MessageBox.Show("There is no Driver linked with  this Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _DriverID = _SelectedDriverInfo.DriverID;
            _LoadLocalDrivingLicenses();
            _LoadInternationalLicenses();
        }
        public void Clear()
        {
            _dtLocalLicenses.Clear();
        }
        private void dgvListLocalLicenses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmShowLicenseInfo frm  = new frmShowLicenseInfo((int)dgvListLocalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void dgvListInternationalLicense_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmShowDriverInternationalLicense frm = new frmShowDriverInternationalLicense((int)dgvListInternationalLicense.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
