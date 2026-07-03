using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.International_Liceenses
{
    public partial class frmShowDriverInternationalLicense : Form
    {
        int _InternationalLicenseID = -1;
        public frmShowDriverInternationalLicense(int internationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = internationalLicenseID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowDriverInternationalLicense_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicenseInfo1.Load(_InternationalLicenseID);
        }
    }
}
