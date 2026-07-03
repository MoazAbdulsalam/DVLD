using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.LocalDrivingLicense
{
    public partial class frmLocalDrivingLicenseDetails : Form
    {
        public frmLocalDrivingLicenseDetails(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            ctrlLocalDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
