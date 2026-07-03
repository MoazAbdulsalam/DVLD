using DVLD.Classes;
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

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID;
        private clsTestType.enTestType _TestType;

        private int _TestID = -1;
        private clsTest _Test;
        public frmTakeTest(int testAppointmentID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestAppointmentID = testAppointmentID;
            _TestType = TestTypeID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestTypeID = _TestType;
            ctrlScheduledTest1.LoadData(_TestAppointmentID);
            if(ctrlScheduledTest1.TestID!=-1)
            {
                _TestID = ctrlScheduledTest1.TestID;
                _Test = clsTest.FindByTestID(_TestID);
                if( _Test != null )
                {
                    if(_Test.TestResult)
                        rbPass.Checked = true;
                    else
                        rbFail.Checked = true;

                    txtbxNotes.Text = _Test.Notes;
                    rbFail.Enabled= false;
                    rbPass.Enabled= false;
                    txtbxNotes.Enabled= false;
                    btnSave.Enabled= false;
                    return;
                }
                
            }
            _Test = new clsTest();
            rbFail.Enabled = true;
            rbPass.Enabled = true;
            txtbxNotes.Enabled = true;
            btnSave.Enabled = true;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                      "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            _Test.TestResult = rbPass.Checked;
            _Test.Notes = txtbxNotes.Text;
            _Test.CreatedByUserID = clsGlobals.CurrentUser.UserID;
            _Test.TestAppointmentID =_TestAppointmentID;
            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
                txtbxNotes.Enabled = false;

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
