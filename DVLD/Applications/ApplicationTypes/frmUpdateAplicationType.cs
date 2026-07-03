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

namespace DVLD.Applications
{
    public partial class frmUpdateAplicationType : Form
    {
        int _ID = -1;
        clsApplicationType _AppType;
        public frmUpdateAplicationType(int ID)
        {
            InitializeComponent();
            _ID = ID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtbxFees_Validating(object sender, CancelEventArgs e)
        {
            if(txtbxFees.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtbxFees, "Requared");
                e.Cancel = true;
            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txtbxFees, "");

            }
        }

        private void txtbxFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtbxTitle_Validating(object sender, CancelEventArgs e)
        {
            if (txtbxTitle.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtbxTitle, "Requared");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtbxTitle, "");

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Fill Requared Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return; 
            }
            _AppType.ApplicationTypeName = txtbxTitle.Text;
            _AppType.ApplicationTypeFees = Convert.ToSingle(txtbxFees.Text);
            if(_AppType.Save())
            {
                MessageBox.Show("Saved Succefully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Save Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void frmUpdateAplicationType_Load(object sender, EventArgs e)
        {
            _AppType = clsApplicationType.Find(_ID);
            if( _AppType == null )
            {
                MessageBox.Show("Did Not Find Application Type","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }
            txtbxTitle.Text = _AppType.ApplicationTypeName;
            txtbxFees.Text = _AppType.ApplicationTypeFees.ToString("0.00");
            lblID.Text = _AppType.ApplicationTypeID.ToString();
        }
    }
}
