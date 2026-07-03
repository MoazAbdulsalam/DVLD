using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests.TestTypes
{
    public partial class frmUpdateTestType : Form
    {
        clsTestType.enTestType _ID =clsTestType.enTestType.VisionTest;
        clsTestType _clsTestType;

        public frmUpdateTestType(clsTestType.enTestType ID)
        {
            InitializeComponent();
            _ID = ID;
        }

        private void txtbxFees_Validating(object sender, CancelEventArgs e)
        {
            if (txtbxFees.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtbxFees, "Requared");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtbxFees, "");

            }
        }

        private void txtbxFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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
        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _clsTestType = clsTestType.Find((clsTestType.enTestType)_ID);
            if (_clsTestType == null)
            {
                MessageBox.Show("Did Not Find Application Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            txtbxTitle.Text = _clsTestType.TestTypeName;
            txtbxFees.Text = _clsTestType.TestTypeFees.ToString("0.00");
            txtbxDescription.Text = _clsTestType.TestTypeDescription;
            lblID.Text = _clsTestType.TestTypeID.ToString();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Fill Requared Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            _clsTestType.TestTypeName = txtbxTitle.Text;
            _clsTestType.TestTypeDescription = txtbxDescription.Text;
            _clsTestType.TestTypeFees = Convert.ToSingle(txtbxFees.Text);
            if (_clsTestType.Save())
            {
                MessageBox.Show("Saved Succefully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Save Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
