namespace DVLD.Applications.LocalDrivingLicense
{
    partial class frmManageLocalDrivingLicense
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtbxSearch = new System.Windows.Forms.TextBox();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvListLocalDrivingLicenseApplication = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmScheduleTests = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmScheduleVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmScheduleWrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmScheduleStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmIssueDrivingLicenseFirstTime = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAddNewLocalDrivingApplication = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListLocalDrivingLicenseApplication)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtbxSearch
            // 
            this.txtbxSearch.Location = new System.Drawing.Point(288, 241);
            this.txtbxSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtbxSearch.Name = "txtbxSearch";
            this.txtbxSearch.Size = new System.Drawing.Size(297, 21);
            this.txtbxSearch.TabIndex = 20;
            this.txtbxSearch.TextChanged += new System.EventHandler(this.txtbxSearch_TextChanged);
            this.txtbxSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbxSearch_KeyPress);
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(105, 603);
            this.lblRecordsCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(34, 21);
            this.lblRecordsCount.TabIndex = 18;
            this.lblRecordsCount.Text = "???";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 603);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 21);
            this.label3.TabIndex = 17;
            this.label3.Text = "# Records";
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "None",
            "L.D.L.A ID",
            "Class Name",
            "National No",
            "Full Name",
            "Passed Test Count",
            "Status"});
            this.cbFilter.Location = new System.Drawing.Point(113, 241);
            this.cbFilter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(150, 23);
            this.cbFilter.TabIndex = 15;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Firebrick;
            this.label2.Location = new System.Drawing.Point(335, 185);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(592, 48);
            this.label2.TabIndex = 13;
            this.label2.Text = "Local Driving License Application";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 241);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "Filter By :";
            // 
            // dgvListLocalDrivingLicenseApplication
            // 
            this.dgvListLocalDrivingLicenseApplication.AllowUserToAddRows = false;
            this.dgvListLocalDrivingLicenseApplication.AllowUserToDeleteRows = false;
            this.dgvListLocalDrivingLicenseApplication.AllowUserToOrderColumns = true;
            this.dgvListLocalDrivingLicenseApplication.BackgroundColor = System.Drawing.Color.White;
            this.dgvListLocalDrivingLicenseApplication.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListLocalDrivingLicenseApplication.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvListLocalDrivingLicenseApplication.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvListLocalDrivingLicenseApplication.Location = new System.Drawing.Point(9, 270);
            this.dgvListLocalDrivingLicenseApplication.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvListLocalDrivingLicenseApplication.Name = "dgvListLocalDrivingLicenseApplication";
            this.dgvListLocalDrivingLicenseApplication.ReadOnly = true;
            this.dgvListLocalDrivingLicenseApplication.RowHeadersWidth = 51;
            this.dgvListLocalDrivingLicenseApplication.RowTemplate.Height = 26;
            this.dgvListLocalDrivingLicenseApplication.Size = new System.Drawing.Size(1149, 293);
            this.dgvListLocalDrivingLicenseApplication.TabIndex = 11;
            this.dgvListLocalDrivingLicenseApplication.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.detailesToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDetails,
            this.toolStripMenuItem1,
            this.tsmEdit,
            this.tsmDelete,
            this.toolStripMenuItem2,
            this.tsmCancel,
            this.toolStripMenuItem3,
            this.tsmScheduleTests,
            this.toolStripSeparator1,
            this.tsmIssueDrivingLicenseFirstTime,
            this.toolStripSeparator2,
            this.tsmShowLicense,
            this.toolStripSeparator3,
            this.tsmLicenseHistory});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(289, 276);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsmDetails
            // 
            this.tsmDetails.Image = global::DVLD.Properties.Resources.PersonDetails_321;
            this.tsmDetails.Name = "tsmDetails";
            this.tsmDetails.Size = new System.Drawing.Size(288, 26);
            this.tsmDetails.Text = "Detailes";
            this.tsmDetails.Click += new System.EventHandler(this.detailesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(285, 6);
            // 
            // tsmEdit
            // 
            this.tsmEdit.Image = global::DVLD.Properties.Resources.Manage_Applications_322;
            this.tsmEdit.Name = "tsmEdit";
            this.tsmEdit.Size = new System.Drawing.Size(288, 26);
            this.tsmEdit.Text = "Edit Application";
            this.tsmEdit.Click += new System.EventHandler(this.editApplicationToolStripMenuItem_Click);
            // 
            // tsmDelete
            // 
            this.tsmDelete.Image = global::DVLD.Properties.Resources.Delete_32_2;
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.Size = new System.Drawing.Size(288, 26);
            this.tsmDelete.Text = "Delete Application";
            this.tsmDelete.Click += new System.EventHandler(this.deleteApplicationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(285, 6);
            // 
            // tsmCancel
            // 
            this.tsmCancel.Image = global::DVLD.Properties.Resources.Delete_321;
            this.tsmCancel.Name = "tsmCancel";
            this.tsmCancel.Size = new System.Drawing.Size(288, 26);
            this.tsmCancel.Text = "Cancel Application";
            this.tsmCancel.Click += new System.EventHandler(this.cancelApplicationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(285, 6);
            // 
            // tsmScheduleTests
            // 
            this.tsmScheduleTests.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmScheduleVisionTest,
            this.tsmScheduleWrittenTest,
            this.tsmScheduleStreetTest});
            this.tsmScheduleTests.Image = global::DVLD.Properties.Resources.Schedule_Test_512;
            this.tsmScheduleTests.Name = "tsmScheduleTests";
            this.tsmScheduleTests.Size = new System.Drawing.Size(288, 26);
            this.tsmScheduleTests.Text = "Schedule Tests";
            // 
            // tsmScheduleVisionTest
            // 
            this.tsmScheduleVisionTest.Image = global::DVLD.Properties.Resources.Vision_Test_32;
            this.tsmScheduleVisionTest.Name = "tsmScheduleVisionTest";
            this.tsmScheduleVisionTest.Size = new System.Drawing.Size(231, 26);
            this.tsmScheduleVisionTest.Text = "Schedule Vision Test";
            this.tsmScheduleVisionTest.Click += new System.EventHandler(this.tsmScheduleVisionTest_Click);
            // 
            // tsmScheduleWrittenTest
            // 
            this.tsmScheduleWrittenTest.Image = global::DVLD.Properties.Resources.Written_Test_32;
            this.tsmScheduleWrittenTest.Name = "tsmScheduleWrittenTest";
            this.tsmScheduleWrittenTest.Size = new System.Drawing.Size(231, 26);
            this.tsmScheduleWrittenTest.Text = "Schedule Writtin Test";
            this.tsmScheduleWrittenTest.Click += new System.EventHandler(this.tsmScheduleWrittenTest_Click);
            // 
            // tsmScheduleStreetTest
            // 
            this.tsmScheduleStreetTest.Image = global::DVLD.Properties.Resources.Street_Test_32;
            this.tsmScheduleStreetTest.Name = "tsmScheduleStreetTest";
            this.tsmScheduleStreetTest.Size = new System.Drawing.Size(231, 26);
            this.tsmScheduleStreetTest.Text = "Schedule Street Test";
            this.tsmScheduleStreetTest.Click += new System.EventHandler(this.tsmScheduleStreetTest_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(285, 6);
            // 
            // tsmIssueDrivingLicenseFirstTime
            // 
            this.tsmIssueDrivingLicenseFirstTime.Image = global::DVLD.Properties.Resources.IssueDrivingLicense_32;
            this.tsmIssueDrivingLicenseFirstTime.Name = "tsmIssueDrivingLicenseFirstTime";
            this.tsmIssueDrivingLicenseFirstTime.Size = new System.Drawing.Size(288, 26);
            this.tsmIssueDrivingLicenseFirstTime.Text = "Issue Driving License(FirstTime)";
            this.tsmIssueDrivingLicenseFirstTime.Click += new System.EventHandler(this.tsmIssueDrivingLicenseFirstTime_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(285, 6);
            // 
            // tsmShowLicense
            // 
            this.tsmShowLicense.Image = global::DVLD.Properties.Resources.License_View_32;
            this.tsmShowLicense.Name = "tsmShowLicense";
            this.tsmShowLicense.Size = new System.Drawing.Size(288, 26);
            this.tsmShowLicense.Text = "Show License";
            this.tsmShowLicense.Click += new System.EventHandler(this.tsmShowLicense_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(285, 6);
            // 
            // tsmLicenseHistory
            // 
            this.tsmLicenseHistory.Image = global::DVLD.Properties.Resources.License_View_321;
            this.tsmLicenseHistory.Name = "tsmLicenseHistory";
            this.tsmLicenseHistory.Size = new System.Drawing.Size(288, 26);
            this.tsmLicenseHistory.Text = "Show Person License History";
            this.tsmLicenseHistory.Click += new System.EventHandler(this.tsmLicenseHistory_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button1.Image = global::DVLD.Properties.Resources.Close_32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(1000, 580);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.button1.Size = new System.Drawing.Size(160, 44);
            this.button1.TabIndex = 19;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAddNewLocalDrivingApplication
            // 
            this.btnAddNewLocalDrivingApplication.Image = global::DVLD.Properties.Resources.New_Application_64;
            this.btnAddNewLocalDrivingApplication.Location = new System.Drawing.Point(1056, 165);
            this.btnAddNewLocalDrivingApplication.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAddNewLocalDrivingApplication.Name = "btnAddNewLocalDrivingApplication";
            this.btnAddNewLocalDrivingApplication.Size = new System.Drawing.Size(104, 100);
            this.btnAddNewLocalDrivingApplication.TabIndex = 16;
            this.btnAddNewLocalDrivingApplication.UseVisualStyleBackColor = true;
            this.btnAddNewLocalDrivingApplication.Click += new System.EventHandler(this.btnAddNewLocalDrivingApplication_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Applications;
            this.pictureBox1.Location = new System.Drawing.Point(477, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(266, 170);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // frmManageLocalDrivingLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1170, 631);
            this.Controls.Add(this.txtbxSearch);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddNewLocalDrivingApplication);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvListLocalDrivingLicenseApplication);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "frmManageLocalDrivingLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Local Driving License";
            this.Load += new System.EventHandler(this.frmManageLocalDrivingLicense_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListLocalDrivingLicenseApplication)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbxSearch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddNewLocalDrivingApplication;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvListLocalDrivingLicenseApplication;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmScheduleTests;
        private System.Windows.Forms.ToolStripMenuItem tsmScheduleVisionTest;
        private System.Windows.Forms.ToolStripMenuItem tsmScheduleWrittenTest;
        private System.Windows.Forms.ToolStripMenuItem tsmScheduleStreetTest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmIssueDrivingLicenseFirstTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmShowLicense;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmLicenseHistory;
    }
}