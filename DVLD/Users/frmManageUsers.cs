using System;
using DVLDBusinessLayer;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmManageUsers : Form
    {
        DataTable dtUsers = clsUser.GetAllUsers();
        public frmManageUsers()
        {
            InitializeComponent();
        }
        private void _RefreshUsersData()
        {
            DataTable dt = clsUser.GetAllUsers();
            dtUsers.Clear();
            dtUsers.Merge(dt);
            lblRecordsCount.Text = dgvListUsers.Rows.Count.ToString();
        }
        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            dgvListUsers.DataSource = dtUsers;
            cbFilter.SelectedIndex = 0;
            if (dgvListUsers.Rows.Count > 0)
            {
                dgvListUsers.Columns[0].HeaderText = "User ID";
                dgvListUsers.Columns[0].Width = 140;

                dgvListUsers.Columns[1].HeaderText = "Person ID";
                dgvListUsers.Columns[1].Width = 140;

                dgvListUsers.Columns[2].HeaderText = "Full Name";
                dgvListUsers.Columns[2].Width = 350;

                dgvListUsers.Columns[3].HeaderText = "UserName";
                dgvListUsers.Columns[3].Width = 150;

                dgvListUsers.Columns[4].HeaderText = "Is Active";
                dgvListUsers.Columns[4].Width = 120;

            }
            lblRecordsCount.Text = dgvListUsers.Rows.Count.ToString();

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxSearch.Visible = cbFilter.SelectedIndex != 0 && cbFilter.SelectedIndex != 5;
            cbActiveFilter.Visible = cbFilter.SelectedIndex == 5;
            txtbxSearch.Text = "";
            dtUsers.DefaultView.RowFilter = "";
           // _RefreshUsersData();
        }

        private void cbActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbActiveFilter.SelectedItem)
            {
                case "All":
                    dtUsers.DefaultView.RowFilter = "";
                    break;
                case "Yes":
                    dtUsers.DefaultView.RowFilter = "[IsActive] = 'True'";
                    break;
                case "No":
                    dtUsers.DefaultView.RowFilter = "[IsActive] = 'False'";
                    break;
            }
            lblRecordsCount.Text = dgvListUsers.RowCount.ToString();

        }

        private void txtbxSearch_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "User Name":
                    FilterColumn = "UserName";

                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtbxSearch.Text.Trim() == "" || FilterColumn == "None")
            {
                dtUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvListUsers.Rows.Count.ToString();
                return;
            }
            if (FilterColumn == "PersonID" || FilterColumn=="UserID")
                dtUsers.DefaultView.RowFilter = $"[{FilterColumn}] ={txtbxSearch.Text.Trim()}";
            else
                dtUsers.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{txtbxSearch.Text.Trim()}%'";



            lblRecordsCount.Text = dgvListUsers.RowCount.ToString();
        }

        private void txtbxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "Person ID" || cbFilter.SelectedItem.ToString() == "User ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true; return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void tsmChangePassword_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword(Convert.ToInt32(dgvListUsers.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshUsersData();
        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            Form frm = new frmUserInfo(Convert.ToInt32(dgvListUsers.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshUsersData();

        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are You Sure You Want To Delete This Person With ID " + Convert.ToInt32(dgvListUsers.CurrentRow.Cells[0].Value), "Deleting Person", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                if (!clsUser.DeleteUser(Convert.ToInt32(dgvListUsers.CurrentRow.Cells[0].Value)))
                {
                    MessageBox.Show("Delete Failed");
                }
                else
                    MessageBox.Show("Deleted Succefully");

            }
            else
                MessageBox.Show("Delete Failed Because this Person Has Data Linked To Him");
            _RefreshUsersData();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser();
            frm.ShowDialog();
            _RefreshUsersData();
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser(Convert.ToInt32(dgvListUsers.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshUsersData();
        }

        private void tsmAddNewUser_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser();
            frm.ShowDialog();
            _RefreshUsersData();
        }
    }
}
