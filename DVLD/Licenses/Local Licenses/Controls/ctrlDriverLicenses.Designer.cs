namespace DVLD.Licenses.Local_Licenses.Controls
{
    partial class ctrlDriverLicenses
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcDriverLicenses = new System.Windows.Forms.TabControl();
            this.tpLocal = new System.Windows.Forms.TabPage();
            this.lblRecordsLocalCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvListLocalLicenses = new System.Windows.Forms.DataGridView();
            this.tpInternational = new System.Windows.Forms.TabPage();
            this.lblRecourdInternationalCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvListInternationalLicense = new System.Windows.Forms.DataGridView();
            this.gbDriverLicenses = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tcDriverLicenses.SuspendLayout();
            this.tpLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListLocalLicenses)).BeginInit();
            this.tpInternational.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListInternationalLicense)).BeginInit();
            this.gbDriverLicenses.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcDriverLicenses
            // 
            this.tcDriverLicenses.Controls.Add(this.tpLocal);
            this.tcDriverLicenses.Controls.Add(this.tpInternational);
            this.tcDriverLicenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDriverLicenses.Location = new System.Drawing.Point(3, 20);
            this.tcDriverLicenses.Name = "tcDriverLicenses";
            this.tcDriverLicenses.SelectedIndex = 0;
            this.tcDriverLicenses.Size = new System.Drawing.Size(1077, 338);
            this.tcDriverLicenses.TabIndex = 0;
            // 
            // tpLocal
            // 
            this.tpLocal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tpLocal.Controls.Add(this.lblRecordsLocalCount);
            this.tpLocal.Controls.Add(this.label3);
            this.tpLocal.Controls.Add(this.dgvListLocalLicenses);
            this.tpLocal.Location = new System.Drawing.Point(4, 25);
            this.tpLocal.Name = "tpLocal";
            this.tpLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tpLocal.Size = new System.Drawing.Size(1069, 309);
            this.tpLocal.TabIndex = 0;
            this.tpLocal.Text = "Local";
            // 
            // lblRecordsLocalCount
            // 
            this.lblRecordsLocalCount.AutoSize = true;
            this.lblRecordsLocalCount.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsLocalCount.Location = new System.Drawing.Point(96, 259);
            this.lblRecordsLocalCount.Name = "lblRecordsLocalCount";
            this.lblRecordsLocalCount.Size = new System.Drawing.Size(34, 21);
            this.lblRecordsLocalCount.TabIndex = 21;
            this.lblRecordsLocalCount.Text = "???";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 259);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 21);
            this.label3.TabIndex = 20;
            this.label3.Text = "# Records";
            // 
            // dgvListLocalLicenses
            // 
            this.dgvListLocalLicenses.AllowUserToAddRows = false;
            this.dgvListLocalLicenses.AllowUserToDeleteRows = false;
            this.dgvListLocalLicenses.AllowUserToOrderColumns = true;
            this.dgvListLocalLicenses.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvListLocalLicenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListLocalLicenses.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvListLocalLicenses.Location = new System.Drawing.Point(6, 6);
            this.dgvListLocalLicenses.Name = "dgvListLocalLicenses";
            this.dgvListLocalLicenses.ReadOnly = true;
            this.dgvListLocalLicenses.RowHeadersWidth = 51;
            this.dgvListLocalLicenses.RowTemplate.Height = 26;
            this.dgvListLocalLicenses.Size = new System.Drawing.Size(1044, 250);
            this.dgvListLocalLicenses.TabIndex = 19;
            this.dgvListLocalLicenses.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListLocalLicenses_CellDoubleClick);
            // 
            // tpInternational
            // 
            this.tpInternational.Controls.Add(this.lblRecourdInternationalCount);
            this.tpInternational.Controls.Add(this.label2);
            this.tpInternational.Controls.Add(this.dgvListInternationalLicense);
            this.tpInternational.Location = new System.Drawing.Point(4, 25);
            this.tpInternational.Name = "tpInternational";
            this.tpInternational.Padding = new System.Windows.Forms.Padding(3);
            this.tpInternational.Size = new System.Drawing.Size(1069, 309);
            this.tpInternational.TabIndex = 1;
            this.tpInternational.Text = "InterNational";
            this.tpInternational.UseVisualStyleBackColor = true;
            // 
            // lblRecourdInternationalCount
            // 
            this.lblRecourdInternationalCount.AutoSize = true;
            this.lblRecourdInternationalCount.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecourdInternationalCount.Location = new System.Drawing.Point(96, 260);
            this.lblRecourdInternationalCount.Name = "lblRecourdInternationalCount";
            this.lblRecourdInternationalCount.Size = new System.Drawing.Size(34, 21);
            this.lblRecourdInternationalCount.TabIndex = 21;
            this.lblRecourdInternationalCount.Text = "???";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 21);
            this.label2.TabIndex = 20;
            this.label2.Text = "# Records";
            // 
            // dgvListInternationalLicense
            // 
            this.dgvListInternationalLicense.AllowUserToAddRows = false;
            this.dgvListInternationalLicense.AllowUserToDeleteRows = false;
            this.dgvListInternationalLicense.AllowUserToOrderColumns = true;
            this.dgvListInternationalLicense.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvListInternationalLicense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListInternationalLicense.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvListInternationalLicense.Location = new System.Drawing.Point(6, 6);
            this.dgvListInternationalLicense.Name = "dgvListInternationalLicense";
            this.dgvListInternationalLicense.ReadOnly = true;
            this.dgvListInternationalLicense.RowHeadersWidth = 51;
            this.dgvListInternationalLicense.RowTemplate.Height = 26;
            this.dgvListInternationalLicense.Size = new System.Drawing.Size(1046, 251);
            this.dgvListInternationalLicense.TabIndex = 19;
            this.dgvListInternationalLicense.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListInternationalLicense_CellDoubleClick);
            // 
            // gbDriverLicenses
            // 
            this.gbDriverLicenses.Controls.Add(this.tcDriverLicenses);
            this.gbDriverLicenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDriverLicenses.Location = new System.Drawing.Point(0, 0);
            this.gbDriverLicenses.Name = "gbDriverLicenses";
            this.gbDriverLicenses.Size = new System.Drawing.Size(1083, 361);
            this.gbDriverLicenses.TabIndex = 1;
            this.gbDriverLicenses.TabStop = false;
            this.gbDriverLicenses.Text = "Driver Licenses";
            // 
            // ctrlDriverLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.gbDriverLicenses);
            this.Name = "ctrlDriverLicenses";
            this.Size = new System.Drawing.Size(1083, 361);
            this.tcDriverLicenses.ResumeLayout(false);
            this.tpLocal.ResumeLayout(false);
            this.tpLocal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListLocalLicenses)).EndInit();
            this.tpInternational.ResumeLayout(false);
            this.tpInternational.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListInternationalLicense)).EndInit();
            this.gbDriverLicenses.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcDriverLicenses;
        private System.Windows.Forms.TabPage tpLocal;
        private System.Windows.Forms.TabPage tpInternational;
        private System.Windows.Forms.GroupBox gbDriverLicenses;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblRecordsLocalCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvListLocalLicenses;
        private System.Windows.Forms.Label lblRecourdInternationalCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvListInternationalLicense;
    }
}
