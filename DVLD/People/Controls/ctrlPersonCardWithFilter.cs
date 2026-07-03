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

namespace DVLD.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID);
            }
        }


        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get { return _ShowAddPerson; }
            set { _ShowAddPerson = value;  btnAddNewPerson.Visible = _ShowAddPerson; }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set { _FilterEnabled = value; gbFilter.Enabled = _FilterEnabled; }
        }

        //int _PersonID=-1;
        public int PersonID { get { return ctrlPersonCard1.PersonID; } }
        public clsPerson SelectedPersonInfo { get { return ctrlPersonCard1.SelectedPersonInfo; } }
        
        
        enum enSearchBy { PersonID = 0 ,NationalNo =1}
        enSearchBy SearchBy;

        
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }
        public void LoadPersonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtbxSearch.Text=PersonID.ToString();
            FindNow();
        }
        private void FindNow()
        {
            switch (SearchBy)
            {
                case enSearchBy.PersonID:
                    ctrlPersonCard1.LoadPerson(Convert.ToInt32(txtbxSearch.Text));
                    break;
                case enSearchBy.NationalNo:
                    ctrlPersonCard1.LoadPerson(txtbxSearch.Text.Trim());
                    break;
            }
            if (OnPersonSelected != null && FilterEnabled)
                OnPersonSelected(ctrlPersonCard1.PersonID);
        }
        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            btnSearch.Enabled = false;
        }

        private void txtbxSearch_TextChanged(object sender, EventArgs e)
        {
            if(txtbxSearch.Text.Length > 0)
                 btnSearch.Enabled = true;
            else
                btnSearch.Enabled = false;

        }

        private void txtbxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if(SearchBy == enSearchBy.PersonID)
            {
                if(!char.IsDigit(e.KeyChar) &&!char.IsControl( e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchBy = (enSearchBy)cbFilterBy.SelectedIndex;
            txtbxSearch.Text = "";
            ctrlPersonCard1.Refresh();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FindNow();
        }
        private void DataBackEvent(object sender, int PersonID)
        {
            LoadPersonInfo(PersonID);
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frmAdd = new frmAddEditPersonInfo();
            frmAdd.DataBack += DataBackEvent;
            frmAdd.ShowDialog();
        }
        public void FilterFocus()
        {
            txtbxSearch.Focus();
        }
    }
}
