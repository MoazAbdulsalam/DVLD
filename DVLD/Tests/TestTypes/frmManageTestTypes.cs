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

namespace DVLD.Tests.TestTypes
{
    public partial class frmManageTestTypes : Form
    {
        DataTable dtTestTypes;
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            dtTestTypes = clsTestType.GetAllAplications();
            dgvListTestTypes.DataSource = dtTestTypes;
            if (dgvListTestTypes.Rows.Count > 0)
            {
                dgvListTestTypes.Columns[0].HeaderText = "ID";
                dgvListTestTypes.Columns[0].Width = 80;

                dgvListTestTypes.Columns[1].HeaderText = "Title";
                dgvListTestTypes.Columns[1].Width = 150;

                dgvListTestTypes.Columns[2].HeaderText = "Description";
                dgvListTestTypes.Columns[2].Width = 380;

                dgvListTestTypes.Columns[3].HeaderText = "Fees";
                dgvListTestTypes.Columns[3].Width = 90;

            }
            lblRecordsCount.Text = dgvListTestTypes.RowCount.ToString();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUpdateTestType((clsTestType.enTestType)Convert.ToInt32(dgvListTestTypes.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            frmManageTestTypes_Load(null,null);
        }
    }
}
