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

namespace DVLD.People
{
    public partial class frmManagePeople : Form
    {

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        //only select the columns that you want to show in the grid
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "GendorCaption", "DateOfBirth", "CountryName",
                                                         "Phone", "Email");


        public frmManagePeople()
        {
            InitializeComponent();
        }
   
        private void _RefreshPeopleData()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "GendorCaption", "DateOfBirth", "CountryName",
                                                         "Phone", "Email");
            dgvListPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvListPeople.RowCount.ToString();            
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            dgvListPeople.DataSource = _dtPeople;

            cbFilter.SelectedIndex = 0;
            if (dgvListPeople.Rows.Count > 0)
            {

                dgvListPeople.Columns[0].HeaderText = "Person ID";
                dgvListPeople.Columns[0].Width = 100;

                dgvListPeople.Columns[1].HeaderText = "National No.";
                dgvListPeople.Columns[1].Width = 100;


                dgvListPeople.Columns[2].HeaderText = "First Name";
                dgvListPeople.Columns[2].Width = 120;

                dgvListPeople.Columns[3].HeaderText = "Second Name";
                dgvListPeople.Columns[3].Width = 120;


                dgvListPeople.Columns[4].HeaderText = "Third Name";
                dgvListPeople.Columns[4].Width = 120;

                dgvListPeople.Columns[5].HeaderText = "Last Name";
                dgvListPeople.Columns[5].Width = 120;

                dgvListPeople.Columns[6].HeaderText = "Gendor";
                dgvListPeople.Columns[6].Width = 90;

                dgvListPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvListPeople.Columns[7].Width = 150;

                dgvListPeople.Columns[8].HeaderText = "Nationality";
                dgvListPeople.Columns[8].Width = 100;


                dgvListPeople.Columns[9].HeaderText = "Phone";
                dgvListPeople.Columns[9].Width = 130;


                dgvListPeople.Columns[10].HeaderText = "Email";
                dgvListPeople.Columns[10].Width = 160;
            }
            lblRecordsCount.Text = dgvListPeople.RowCount.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            
            frmPersonDetails frm = new frmPersonDetails((int)dgvListPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleData();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            txtbxSearch.Visible=cbFilter.SelectedIndex!=0;
            txtbxSearch.Text = "";
            _RefreshPeopleData();


        }

        private void txtbxSearch_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtbxSearch.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvListPeople.Rows.Count.ToString();
                return;
            }
            if (FilterColumn == "PersonID")
                _dtPeople.DefaultView.RowFilter = $"[PersonID] ={txtbxSearch.Text.Trim()}";
            else
                _dtPeople.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{txtbxSearch.Text.Trim()}%'";



            lblRecordsCount.Text = dgvListPeople.RowCount.ToString();
        }

        private void txtbxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.SelectedItem.ToString() == "Person ID")
            {
                if(!char.IsDigit(e.KeyChar)&&e.KeyChar!=8)
                {
                    e.Handled = true; return; 
                }
            }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
            _RefreshPeopleData();
        }

        private void tsmAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
            _RefreshPeopleData();


        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(Convert.ToInt32(dgvListPeople.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshPeopleData();

        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvListPeople.CurrentRow.Cells[0].Value);
            if (MessageBox.Show("Are You Sure You Want To Delete This Person With ID " + PersonID, "Deleting Person", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                if (!clsPerson.DeletePerson(PersonID))
                {
                    MessageBox.Show("Delete Failed");
                }
                else
                    MessageBox.Show("Deleted Succefully");

            }
            else
                MessageBox.Show("Delete Failed Because this Person Has Data Linked To Him");
            _RefreshPeopleData();


        }


    }
}
