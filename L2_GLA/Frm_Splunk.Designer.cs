namespace L2_GLA
{
    partial class Frm_Splunk
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbltag = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvlist = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.btnopenfile = new System.Windows.Forms.Button();
            this.txtspiel = new System.Windows.Forms.TextBox();
            this.cmbdbstats = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtnocstats = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblprog = new System.Windows.Forms.Label();
            this.dgvfile = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvfile)).BeginInit();
            this.SuspendLayout();
            // 
            // lbltag
            // 
            this.lbltag.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbltag.AutoSize = true;
            this.lbltag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltag.Location = new System.Drawing.Point(491, 248);
            this.lbltag.Name = "lbltag";
            this.lbltag.Size = new System.Drawing.Size(0, 20);
            this.lbltag.TabIndex = 18;
            this.lbltag.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(409, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(233, 32);
            this.button1.TabIndex = 17;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvlist
            // 
            this.dgvlist.AllowUserToAddRows = false;
            this.dgvlist.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvlist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvlist.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvlist.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.dgvlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column1,
            this.Column3,
            this.Column4});
            this.dgvlist.Location = new System.Drawing.Point(140, 172);
            this.dgvlist.Name = "dgvlist";
            this.dgvlist.ReadOnly = true;
            this.dgvlist.RowHeadersVisible = false;
            this.dgvlist.Size = new System.Drawing.Size(799, 214);
            this.dgvlist.TabIndex = 16;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Created_at";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "App_Transaction_number";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Status";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "Remarks";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(159, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 40);
            this.label1.TabIndex = 15;
            this.label1.Text = "      App\r\nTransaction";
            // 
            // txtsearch
            // 
            this.txtsearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(268, 12);
            this.txtsearch.Multiline = true;
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtsearch.Size = new System.Drawing.Size(541, 116);
            this.txtsearch.TabIndex = 14;
            // 
            // btnopenfile
            // 
            this.btnopenfile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnopenfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnopenfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnopenfile.ForeColor = System.Drawing.Color.White;
            this.btnopenfile.Location = new System.Drawing.Point(815, 12);
            this.btnopenfile.Name = "btnopenfile";
            this.btnopenfile.Size = new System.Drawing.Size(135, 32);
            this.btnopenfile.TabIndex = 13;
            this.btnopenfile.Text = "Upload File";
            this.btnopenfile.UseVisualStyleBackColor = true;
            this.btnopenfile.Click += new System.EventHandler(this.btnopenfile_Click);
            // 
            // txtspiel
            // 
            this.txtspiel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtspiel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtspiel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtspiel.Location = new System.Drawing.Point(1089, 96);
            this.txtspiel.Multiline = true;
            this.txtspiel.Name = "txtspiel";
            this.txtspiel.ReadOnly = true;
            this.txtspiel.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtspiel.Size = new System.Drawing.Size(184, 197);
            this.txtspiel.TabIndex = 23;
            this.txtspiel.Visible = false;
            // 
            // cmbdbstats
            // 
            this.cmbdbstats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbdbstats.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbdbstats.FormattingEnabled = true;
            this.cmbdbstats.Items.AddRange(new object[] {
            "AUTHENTICATION_EXPIRED",
            "AUTHENTICATION_FAILED",
            "CARD_UNVERIFIED",
            "ELP_SUCCESSFUL",
            "FOR_AUTHENTICATION",
            "FOR_VERIFICATION",
            "GCASH_EXPIRED",
            "GCASH_VOIDED",
            "PAYMAYA_VOIDED",
            "VERIFICATION_SUCCESSFUL",
            "WALLET_PENDING"});
            this.cmbdbstats.Location = new System.Drawing.Point(1214, 162);
            this.cmbdbstats.Name = "cmbdbstats";
            this.cmbdbstats.Size = new System.Drawing.Size(10, 28);
            this.cmbdbstats.TabIndex = 24;
            this.cmbdbstats.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1101, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 25;
            this.label2.Text = "DB Status:  ";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1100, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 27;
            this.label3.Text = "Noc Status :";
            this.label3.Visible = false;
            // 
            // txtnocstats
            // 
            this.txtnocstats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtnocstats.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnocstats.FormattingEnabled = true;
            this.txtnocstats.Items.AddRange(new object[] {
            "PAYMENT_SUCCESS",
            "FAILED"});
            this.txtnocstats.Location = new System.Drawing.Point(1214, 196);
            this.txtnocstats.Name = "txtnocstats";
            this.txtnocstats.Size = new System.Drawing.Size(10, 28);
            this.txtnocstats.TabIndex = 28;
            this.txtnocstats.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.Red;
            this.progressBar1.Location = new System.Drawing.Point(1104, 240);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(150, 29);
            this.progressBar1.TabIndex = 29;
            this.progressBar1.Visible = false;
            // 
            // lblprog
            // 
            this.lblprog.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblprog.AutoSize = true;
            this.lblprog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprog.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblprog.Location = new System.Drawing.Point(683, 252);
            this.lblprog.Name = "lblprog";
            this.lblprog.Size = new System.Drawing.Size(0, 23);
            this.lblprog.TabIndex = 30;
            this.lblprog.UseCompatibleTextRendering = true;
            this.lblprog.Visible = false;
            // 
            // dgvfile
            // 
            this.dgvfile.AllowUserToAddRows = false;
            this.dgvfile.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvfile.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvfile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvfile.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.dgvfile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvfile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvfile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvfile.Location = new System.Drawing.Point(140, 392);
            this.dgvfile.Name = "dgvfile";
            this.dgvfile.ReadOnly = true;
            this.dgvfile.RowHeadersVisible = false;
            this.dgvfile.Size = new System.Drawing.Size(799, 220);
            this.dgvfile.TabIndex = 31;
            this.dgvfile.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvfile_CellContentClick);
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
            // Frm_Splunk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.ClientSize = new System.Drawing.Size(1035, 663);
            this.Controls.Add(this.dgvfile);
            this.Controls.Add(this.lblprog);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtnocstats);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbdbstats);
            this.Controls.Add(this.txtspiel);
            this.Controls.Add(this.lbltag);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvlist);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtsearch);
            this.Controls.Add(this.btnopenfile);
            this.Name = "Frm_Splunk";
            this.Text = "Frm_Splunk";
            this.Load += new System.EventHandler(this.Frm_Splunk_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvlist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvfile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbltag;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvlist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Button btnopenfile;
        private System.Windows.Forms.TextBox txtspiel;
        private System.Windows.Forms.ComboBox cmbdbstats;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox txtnocstats;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblprog;
        private System.Windows.Forms.DataGridView dgvfile;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}