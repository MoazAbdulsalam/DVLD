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

namespace DVLD.Applications.Controls
{
    public partial class ctrlApplicationInfo : UserControl
    {
        int _ApplicationID = -1;
        public int ApplicationID { get { return _ApplicationID; } }
        clsApplication _ApplicationInfo;
        public clsApplication Application { get { return _ApplicationInfo; } }
        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }
        void _FillCard()
        {

            lblBaseAppID.Text = _ApplicationInfo.ApplicationID.ToString();
            lblStatus.Text = _ApplicationInfo.StatusText;
            lblFees.Text = clsApplicationType.Find(_ApplicationInfo.ApplicationTypeID).ApplicationTypeFees.ToString();
            lblType.Text = _ApplicationInfo.ApplicationTypeInfo.ApplicationTypeName;
            lblApplicant.Text = _ApplicationInfo.PersonInfo.FullName();
            lblDate.Text = _ApplicationInfo.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = _ApplicationInfo.LastStatusDate.ToShortDateString();
            lblCreatedBy.Text = _ApplicationInfo.CreatedByUserInfo.UserName;
        }
        public void LoadApplicationInfo(int ApplicationID)
        {
            _ApplicationInfo = clsApplication.FindBaseApplication(ApplicationID);
            if(_ApplicationInfo == null)
            {
                MessageBox.Show("No Applications with ID = "+ ApplicationID,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                
                return;
            }
            _ApplicationID = ApplicationID;
            _FillCard();
        }

        private void llShowPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new People.frmPersonDetails(_ApplicationInfo.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
