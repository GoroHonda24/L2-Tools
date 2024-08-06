
namespace L2_GLA
{
    partial class frm_account
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
            this.dgvlist = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtspiel = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlist)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvlist
            // 
            this.dgvlist.AllowUserToAddRows = false;
            this.dgvlist.AllowUserToDeleteRows = false;
            this.dgvlist.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvlist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvlist.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.dgvlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5});
            this.dgvlist.Location = new System.Drawing.Point(116, 154);
            this.dgvlist.Name = "dgvlist";
            this.dgvlist.ReadOnly = true;
            this.dgvlist.RowHeadersVisible = false;
            this.dgvlist.Size = new System.Drawing.Size(686, 429);
            this.dgvlist.TabIndex = 0;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "ID";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "MIN";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "App Transaction";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Account";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "Time Stamp";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(605, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "Import Paybill Logs";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(605, 51);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(197, 40);
            this.button4.TabIndex = 4;
            this.button4.Text = "Truncate Table";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(311, 606);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(317, 40);
            this.button5.TabIndex = 5;
            this.button5.Text = "Export to CSV file";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtspiel);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(830, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 177);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PAYBILL LOGS";
            // 
            // txtspiel
            // 
            this.txtspiel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtspiel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtspiel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.txtspiel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtspiel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtspiel.Location = new System.Drawing.Point(3, 25);
            this.txtspiel.Multiline = true;
            this.txtspiel.Name = "txtspiel";
            this.txtspiel.ReadOnly = true;
            this.txtspiel.Size = new System.Drawing.Size(262, 149);
            this.txtspiel.TabIndex = 7;
            this.txtspiel.Text = "SELECT number,app_transaction_number,created_at FROM consumer.paybill_logs where " +
    "app_transaction_number in (\'\') order by created_at desc;";
            // 
            // frm_account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.ClientSize = new System.Drawing.Size(1108, 669);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvlist);
            this.Name = "frm_account";
            this.Text = "frm_account";
            this.Load += new System.EventHandler(this.frm_account_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvlist)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvlist;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtspiel;
    }
}