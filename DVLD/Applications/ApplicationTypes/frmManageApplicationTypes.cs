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
    public partial class frmManageApplicationTypes : Form
    {
        DataTable dtApplicationTypes;
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            dtApplicationTypes= clsApplicationType.GetAllAplications();
            dgvListApplicationTypes.DataSource = dtApplicationTypes;
            if(dgvListApplicationTypes.Rows.Count > 0 )
            {
                dgvListApplicationTypes.Columns[0].HeaderText = "Application Type ID";
                dgvListApplicationTypes.Columns[0].Width = 150;

                dgvListApplicationTypes.Columns[1].HeaderText = "Application Type Name";
                dgvListApplicationTypes.Columns[1].Width = 320;

                dgvListApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvListApplicationTypes.Columns[2].Width = 90;

            }
            lblRecordsCount.Text=dgvListApplicationTypes.RowCount.ToString();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUpdateAplicationType(Convert.ToInt32(dgvListApplicationTypes.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            frmManageApplicationTypes_Load(null, null);
        }

        private void dgvListApplicationTypes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            updateToolStripMenuItem_Click(null, null);
        }
    }
}
