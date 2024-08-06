namespace L2_GLA
{
    partial class Frm_variance
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
            this.btnopenfile = new System.Windows.Forms.Button();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvlist = new System.Windows.Forms.DataGridView();
            this.dgvfile = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvfile)).BeginInit();
            this.SuspendLayout();
            // 
            // btnopenfile
            // 
            this.btnopenfile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnopenfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnopenfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnopenfile.ForeColor = System.Drawing.Color.White;
            this.btnopenfile.Location = new System.Drawing.Point(848, 12);
            this.btnopenfile.Name = "btnopenfile";
            this.btnopenfile.Size = new System.Drawing.Size(135, 32);
            this.btnopenfile.TabIndex = 7;
            this.btnopenfile.Text = "Upload File";
            this.btnopenfile.UseVisualStyleBackColor = true;
            this.btnopenfile.Click += new System.EventHandler(this.btnopenfile_Click);
            // 
            // txtsearch
            // 
            this.txtsearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(284, 12);
            this.txtsearch.Multiline = true;
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtsearch.Size = new System.Drawing.Size(558, 115);
            this.txtsearch.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(175, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 40);
            this.label1.TabIndex = 9;
            this.label1.Text = "      App\r\nTransaction";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(448, 143);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(244, 32);
            this.button1.TabIndex = 11;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvlist
            // 
            this.dgvlist.AllowUserToAddRows = false;
            this.dgvlist.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvlist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvlist.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvlist.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.dgvlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Column1,
            this.Column2,
            this.Column3,
            this.dataGridViewTextBoxColumn4});
            this.dgvlist.Location = new System.Drawing.Point(138, 196);
            this.dgvlist.Name = "dgvlist";
            this.dgvlist.ReadOnly = true;
            this.dgvlist.RowHeadersVisible = false;
            this.dgvlist.Size = new System.Drawing.Size(845, 230);
            this.dgvlist.TabIndex = 17;
            // 
            // dgvfile
            // 
            this.dgvfile.AllowUserToAddRows = false;
            this.dgvfile.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvfile.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvfile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvfile.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.dgvfile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvfile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvfile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvfile.Location = new System.Drawing.Point(138, 432);
            this.dgvfile.Name = "dgvfile";
            this.dgvfile.ReadOnly = true;
            this.dgvfile.RowHeadersVisible = false;
            this.dgvfile.Size = new System.Drawing.Size(845, 220);
            this.dgvfile.TabIndex = 32;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "File Name";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column6.HeaderText = "Uploader";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 75;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column7.HeaderText = "Date uploaded";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 102;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.Red;
            this.progressBar1.Location = new System.Drawing.Point(758, 143);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(377, 29);
            this.progressBar1.TabIndex = 33;
            this.progressBar1.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "Created_at";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 84;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn2.HeaderText = "App_Transaction_number";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 154;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn3.HeaderText = "Status";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 62;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column1.HeaderText = "MERCHANT_REFUND_REQUEST_ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 219;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column2.HeaderText = "REFUND_ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 94;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column3.HeaderText = "AMOUNT";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 79;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn4.HeaderText = "Remarks";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 74;
            // 
            // Frm_variance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.ClientSize = new System.Drawing.Size(1248, 740);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dgvfile);
            this.Controls.Add(this.dgvlist);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtsearch);
            this.Controls.Add(this.btnopenfile);
            this.Name = "Frm_variance";
            this.Text = "Frm_variance";
            this.Load += new System.EventHandler(this.Frm_variance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvlist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvfile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnopenfile;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvlist;
        private System.Windows.Forms.DataGridView dgvfile;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}