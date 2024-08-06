
namespace L2_GLA
{
    partial class Frm_Dashboard
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
            this.pieChart1 = new LiveCharts.WinForms.PieChart();
            this.dtpfrom = new System.Windows.Forms.DateTimePicker();
            this.dgvlist = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpto = new System.Windows.Forms.DateTimePicker();
            this.cbDate = new System.Windows.Forms.CheckBox();
            this.dgvSelected_user = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbbUser = new System.Windows.Forms.ComboBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected_user)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pieChart1
            // 
            this.pieChart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pieChart1.Location = new System.Drawing.Point(4, 26);
            this.pieChart1.Margin = new System.Windows.Forms.Padding(1);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.Size = new System.Drawing.Size(643, 236);
            this.pieChart1.TabIndex = 0;
            this.pieChart1.Text = "pieChart1";
            // 
            // dtpfrom
            // 
            this.dtpfrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpfrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfrom.Location = new System.Drawing.Point(1158, 9);
            this.dtpfrom.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.dtpfrom.Name = "dtpfrom";
            this.dtpfrom.Size = new System.Drawing.Size(153, 26);
            this.dtpfrom.TabIndex = 2;
            this.dtpfrom.Value = new System.DateTime(2024, 1, 9, 22, 58, 11, 0);
            this.dtpfrom.ValueChanged += new System.EventHandler(this.dtpfrom_ValueChanged);
            // 
            // dgvlist
            // 
            this.dgvlist.AllowUserToAddRows = false;
            this.dgvlist.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvlist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvlist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvlist.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column8,
            this.Column4,
            this.Column5,
            this.Column7,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column6});
            this.dgvlist.Location = new System.Drawing.Point(23, 351);
            this.dgvlist.Name = "dgvlist";
            this.dgvlist.ReadOnly = true;
            this.dgvlist.RowHeadersVisible = false;
            this.dgvlist.Size = new System.Drawing.Size(1300, 372);
            this.dgvlist.TabIndex = 9;
            this.dgvlist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvlist_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column1.HeaderText = "Uts ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 62;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column2.HeaderText = "Call ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 63;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column3.HeaderText = "System";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 66;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "FC";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column4.HeaderText = "Status";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 62;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column5.HeaderText = "Reason";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 69;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.Column7.HeaderText = "Concern";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 5;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.Column9.HeaderText = "Error";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 5;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.Column10.HeaderText = "Work Around";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Visible = false;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.Column11.HeaderText = "Findings";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Visible = false;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Bulk";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Visible = false;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column6.HeaderText = "User";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 54;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1400, 351);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "Select User:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1330, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Date To:";
            // 
            // dtpto
            // 
            this.dtpto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpto.Location = new System.Drawing.Point(1404, 12);
            this.dtpto.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.dtpto.Name = "dtpto";
            this.dtpto.Size = new System.Drawing.Size(180, 26);
            this.dtpto.TabIndex = 14;
            this.dtpto.ValueChanged += new System.EventHandler(this.dtpto_ValueChanged);
            // 
            // cbDate
            // 
            this.cbDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDate.AutoSize = true;
            this.cbDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDate.Location = new System.Drawing.Point(987, 11);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(167, 24);
            this.cbDate.TabIndex = 15;
            this.cbDate.Text = "Filter by Date From:";
            this.cbDate.UseVisualStyleBackColor = true;
            this.cbDate.CheckedChanged += new System.EventHandler(this.cbDate_CheckedChanged);
            // 
            // dgvSelected_user
            // 
            this.dgvSelected_user.AllowUserToAddRows = false;
            this.dgvSelected_user.AllowUserToDeleteRows = false;
            this.dgvSelected_user.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSelected_user.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected_user.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6});
            this.dgvSelected_user.Location = new System.Drawing.Point(1345, 411);
            this.dgvSelected_user.Name = "dgvSelected_user";
            this.dgvSelected_user.ReadOnly = true;
            this.dgvSelected_user.RowHeadersVisible = false;
            this.dgvSelected_user.Size = new System.Drawing.Size(239, 253);
            this.dgvSelected_user.TabIndex = 16;
            this.dgvSelected_user.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSelected_user_CellMouseDown);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "User";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // cbbUser
            // 
            this.cbbUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbUser.FormattingEnabled = true;
            this.cbbUser.Location = new System.Drawing.Point(1345, 379);
            this.cbbUser.Name = "cbbUser";
            this.cbbUser.Size = new System.Drawing.Size(239, 28);
            this.cbbUser.TabIndex = 17;
            this.cbbUser.SelectedIndexChanged += new System.EventHandler(this.cbbUser_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(1345, 670);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(117, 45);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pieChart1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(31, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(662, 274);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Tickets";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1468, 670);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 45);
            this.button1.TabIndex = 20;
            this.button1.Text = "EXPORT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cartesianChart1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(699, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(885, 274);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tickets Status";
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cartesianChart1.Location = new System.Drawing.Point(59, 29);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(804, 233);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // Frm_Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1624, 743);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.cbbUser);
            this.Controls.Add(this.dgvSelected_user);
            this.Controls.Add(this.cbDate);
            this.Controls.Add(this.dtpto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvlist);
            this.Controls.Add(this.dtpfrom);
            this.Name = "Frm_Dashboard";
            this.Text = "Frm_Dashboard";
            this.Load += new System.EventHandler(this.Frm_Dashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvlist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected_user)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LiveCharts.WinForms.PieChart pieChart1;
        private System.Windows.Forms.DateTimePicker dtpfrom;
        private System.Windows.Forms.DataGridView dgvlist;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpto;
        private System.Windows.Forms.CheckBox cbDate;
        private System.Windows.Forms.DataGridView dgvSelected_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.ComboBox cbbUser;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}