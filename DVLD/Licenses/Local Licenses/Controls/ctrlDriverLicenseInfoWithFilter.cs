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
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID);
            }
        }
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set { _FilterEnabled = value; gbFilter.Enabled = _FilterEnabled; }
        }
        public int LicenseID { get { return ctrlDriverLicenseInfo1.LicenseID; } }
        public clsLicense SelectedLicenseInfo { get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; } }
        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void txtbxSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtbxSearch.Text.Length>0)
            {
                btnSearch.Enabled = true;
            }
            else
                btnSearch.Enabled= false;
        }
        private void txtbxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {


            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
          
            if (e.KeyChar == (char)13)
                if(btnSearch.Enabled)
                     btnSearch.PerformClick();
            
        }
        private void ctrlDriverLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
        }
     
        void _FindNow()
        {
            ctrlDriverLicenseInfo1.LoadLicenseInfo(Convert.ToInt32(txtbxSearch.Text));
            if (OnLicenseSelected != null && gbFilter.Enabled)
                OnLicenseSelected(ctrlDriverLicenseInfo1.LicenseID);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _FindNow();
        }
        public void LoadInfo(int licenseID)
        {
            txtbxSearch.Text = licenseID.ToString();
            ctrlDriverLicenseInfo1.LoadLicenseInfo(licenseID);
            if (OnLicenseSelected != null && gbFilter.Enabled)
                OnLicenseSelected(ctrlDriverLicenseInfo1.LicenseID);

        }
        public void SetTextBoxFocus()
        {
            txtbxSearch.Focus(); 
        }
    }
}
