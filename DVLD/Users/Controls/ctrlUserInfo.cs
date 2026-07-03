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

namespace DVLD.Users.Controls
{
    public partial class ctrlUserInfo : UserControl
    {
        int _UserID = -1;
        clsUser _User;
        public clsUser SelectedUserInfo { get { return _User; }  }

        public ctrlUserInfo()
        {
            InitializeComponent();
        }
        private void _LoadUserInfo()
        {
            ctrlPersonCard1.LoadPerson(_User.PersonID);
            _UserID =_User.UserID;
            lblUserID.Text =_User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();
            lblIsActive.Text = _User.IsActive == true?"Yes":"No";

        }
        public void LoadUser(int UserID)
        {
            _User = clsUser.FindByUserID(UserID);
            if (_User == null)
            {
                MessageBox.Show("No Person With PersonID =" + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadUserInfo();
        }
       
        
        private void ctrlUserInfo_Load(object sender, EventArgs e)
        {
        }
    }
}
