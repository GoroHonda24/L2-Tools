namespace L2_GLA.Variance
{
    partial class gcash
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnImportSmartDB = new System.Windows.Forms.Button();
            this.dgvTicketDetails = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTicketDetails)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.Transparent;
            this.btnQuery.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnQuery.FlatAppearance.BorderSize = 0;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(0, 0);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(810, 61);
            this.btnQuery.TabIndex = 90;
            this.btnQuery.Text = "Investigation";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnImportSmartDB
            // 
            this.btnImportSmartDB.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnImportSmartDB.FlatAppearance.BorderSize = 0;
            this.btnImportSmartDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportSmartDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportSmartDB.ForeColor = System.Drawing.Color.White;
            this.btnImportSmartDB.Location = new System.Drawing.Point(815, 0);
            this.btnImportSmartDB.Name = "btnImportSmartDB";
            this.btnImportSmartDB.Size = new System.Drawing.Size(691, 61);
            this.btnImportSmartDB.TabIndex = 104;
            this.btnImportSmartDB.Text = "Smart DB";
            this.btnImportSmartDB.UseVisualStyleBackColor = true;
            this.btnImportSmartDB.Click += new System.EventHandler(this.btnImportSmartDB_Click);
            // 
            // dgvTicketDetails
            // 
            this.dgvTicketDetails.AllowUserToAddRows = false;
            this.dgvTicketDetails.AllowUserToDeleteRows = false;
            this.dgvTicketDetails.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvTicketDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTicketDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTicketDetails.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.dgvTicketDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTicketDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTicketDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTicketDetails.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTicketDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTicketDetails.Location = new System.Drawing.Point(0, 61);
            this.dgvTicketDetails.Name = "dgvTicketDetails";
            this.dgvTicketDetails.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTicketDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTicketDetails.RowHeadersVisible = false;
            this.dgvTicketDetails.Size = new System.Drawing.Size(1506, 606);
            this.dgvTicketDetails.TabIndex = 109;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.btnImportSmartDB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1506, 61);
            this.panel1.TabIndex = 108;
            // 
            // gcash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.ClientSize = new System.Drawing.Size(1506, 667);
            this.Controls.Add(this.dgvTicketDetails);
            this.Controls.Add(this.panel1);
            this.Name = "gcash";
            this.Text = "gcash";
            this.Load += new System.EventHandler(this.gcash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTicketDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnImportSmartDB;
        private System.Windows.Forms.DataGridView dgvTicketDetails;
        private System.Windows.Forms.Panel panel1;
    }
}