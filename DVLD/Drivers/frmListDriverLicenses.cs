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
    public partial class frmListDriverLicenses : Form
    {
        int _PersonID = -1;
       
        public frmListDriverLicenses(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void frmListDriverLicenses_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlDriverLicenses1.LoadByPersonID(_PersonID);
                ctrlPersonCardWithFilter1.FilterEnabled = false;

            }
            else
            {
                ctrlPersonCardWithFilter1.FilterEnabled = false;

                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close ();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;
            if (_PersonID == -1)
            {
                ctrlDriverLicenses1.Clear();
            }
            else
                ctrlDriverLicenses1.LoadByPersonID(_PersonID);
        }
    }
}
