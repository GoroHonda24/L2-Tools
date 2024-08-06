namespace L2_GLA
{
    partial class Frm_maya
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
            this.button1 = new System.Windows.Forms.Button();
            this.lblprog = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.dgvlist = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnInvestigation = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.lblname = new System.Windows.Forms.Label();
            this.btn_iload = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpto = new System.Windows.Forms.DateTimePicker();
            this.btnsplunk = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnDB = new System.Windows.Forms.Button();
            this.btnElp = new System.Windows.Forms.Button();
            this.btnApp = new System.Windows.Forms.Button();
            this.dgvnoc = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvnoc)).BeginInit();
            this.SuspendLayout();
            // 
            // btnopenfile
            // 
            this.btnopenfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnopenfile.BackColor = System.Drawing.Color.Transparent;
            this.btnopenfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnopenfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnopenfile.ForeColor = System.Drawing.Color.White;
            this.btnopenfile.Location = new System.Drawing.Point(1305, 12);
            this.btnopenfile.Name = "btnopenfile";
            this.btnopenfile.Size = new System.Drawing.Size(188, 32);
            this.btnopenfile.TabIndex = 14;
            this.btnopenfile.Text = "Smart DB";
            this.btnopenfile.UseVisualStyleBackColor = false;
            this.btnopenfile.Click += new System.EventHandler(this.btnopenfile_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(992, 613);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(245, 32);
            this.button1.TabIndex = 15;
            this.button1.Text = "Uplaod Investigation File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblprog
            // 
            this.lblprog.AutoSize = true;
            this.lblprog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprog.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblprog.Location = new System.Drawing.Point(518, -21);
            this.lblprog.Name = "lblprog";
            this.lblprog.Size = new System.Drawing.Size(0, 23);
            this.lblprog.TabIndex = 31;
            this.lblprog.UseCompatibleTextRendering = true;
            this.lblprog.Visible = false;
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Location = new System.Drawing.Point(992, 575);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(245, 32);
            this.button9.TabIndex = 42;
            this.button9.Text = "Truncate table Maya";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Visible = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // dgvlist
            // 
            this.dgvlist.AllowUserToAddRows = false;
            this.dgvlist.AllowUserToDeleteRows = false;
            this.dgvlist.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvlist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvlist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvlist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            this.dgvlist.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgvlist.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.dgvlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvlist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column6,
            this.Column1,
            this.Column7});
            this.dgvlist.Location = new System.Drawing.Point(67, 50);
            this.dgvlist.Name = "dgvlist";
            this.dgvlist.RowHeadersVisible = false;
            this.dgvlist.Size = new System.Drawing.Size(1426, 376);
            this.dgvlist.TabIndex = 48;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "App Transaction Number";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Elp Transaction Number";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "DB Status";
            this.Column4.Name = "Column4";
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.HeaderText = "Splunk Status";
            this.Column6.Name = "Column6";
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Iload Status";
            this.Column1.Name = "Column1";
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column7.HeaderText = "L2 Remarks";
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // BtnInvestigation
            // 
            this.BtnInvestigation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnInvestigation.BackColor = System.Drawing.Color.Transparent;
            this.BtnInvestigation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnInvestigation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnInvestigation.ForeColor = System.Drawing.Color.White;
            this.BtnInvestigation.Location = new System.Drawing.Point(1111, 12);
            this.BtnInvestigation.Name = "BtnInvestigation";
            this.BtnInvestigation.Size = new System.Drawing.Size(188, 32);
            this.BtnInvestigation.TabIndex = 49;
            this.BtnInvestigation.Text = "Investigation";
            this.BtnInvestigation.UseVisualStyleBackColor = false;
            this.BtnInvestigation.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Transparent;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(67, 12);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(134, 32);
            this.btnNew.TabIndex = 51;
            this.btnNew.Text = "Update Status";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // lblname
            // 
            this.lblname.AutoSize = true;
            this.lblname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblname.ForeColor = System.Drawing.Color.White;
            this.lblname.Location = new System.Drawing.Point(309, 16);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(0, 20);
            this.lblname.TabIndex = 52;
            // 
            // btn_iload
            // 
            this.btn_iload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_iload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_iload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_iload.ForeColor = System.Drawing.Color.White;
            this.btn_iload.Location = new System.Drawing.Point(1249, 580);
            this.btn_iload.Name = "btn_iload";
            this.btn_iload.Size = new System.Drawing.Size(245, 36);
            this.btn_iload.TabIndex = 83;
            this.btn_iload.Text = "ILOAD STATUS";
            this.btn_iload.UseVisualStyleBackColor = true;
            this.btn_iload.Click += new System.EventHandler(this.btn_iload_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(1249, 620);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(245, 36);
            this.btnExport.TabIndex = 82;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(1056, 546);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 81;
            this.label2.Text = "Date: ";
            // 
            // dtpto
            // 
            this.dtpto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpto.CalendarForeColor = System.Drawing.Color.White;
            this.dtpto.CalendarMonthBackground = System.Drawing.Color.Transparent;
            this.dtpto.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpto.Location = new System.Drawing.Point(1112, 543);
            this.dtpto.Name = "dtpto";
            this.dtpto.Size = new System.Drawing.Size(125, 26);
            this.dtpto.TabIndex = 80;
            this.dtpto.Value = new System.DateTime(2023, 11, 5, 12, 26, 22, 0);
            // 
            // btnsplunk
            // 
            this.btnsplunk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnsplunk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsplunk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsplunk.ForeColor = System.Drawing.Color.White;
            this.btnsplunk.Location = new System.Drawing.Point(1249, 540);
            this.btnsplunk.Name = "btnsplunk";
            this.btnsplunk.Size = new System.Drawing.Size(245, 36);
            this.btnsplunk.TabIndex = 79;
            this.btnsplunk.Text = "SPLUNK STATUS";
            this.btnsplunk.UseVisualStyleBackColor = true;
            this.btnsplunk.Click += new System.EventHandler(this.btnsplunk_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Enabled = false;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(998, 432);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(245, 32);
            this.button4.TabIndex = 78;
            this.button4.Text = "Clear";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button3_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(998, 468);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(245, 32);
            this.button3.TabIndex = 77;
            this.button3.Text = "Format";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDB
            // 
            this.btnDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDB.ForeColor = System.Drawing.Color.White;
            this.btnDB.Location = new System.Drawing.Point(1249, 504);
            this.btnDB.Name = "btnDB";
            this.btnDB.Size = new System.Drawing.Size(245, 32);
            this.btnDB.TabIndex = 76;
            this.btnDB.Text = "DB STATUS";
            this.btnDB.UseVisualStyleBackColor = true;
            this.btnDB.Click += new System.EventHandler(this.btnDB_Click);
            // 
            // btnElp
            // 
            this.btnElp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnElp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnElp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnElp.ForeColor = System.Drawing.Color.White;
            this.btnElp.Location = new System.Drawing.Point(1249, 468);
            this.btnElp.Name = "btnElp";
            this.btnElp.Size = new System.Drawing.Size(245, 32);
            this.btnElp.TabIndex = 75;
            this.btnElp.Text = "ELP";
            this.btnElp.UseVisualStyleBackColor = true;
            this.btnElp.Click += new System.EventHandler(this.btnElp_Click);
            // 
            // btnApp
            // 
            this.btnApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApp.ForeColor = System.Drawing.Color.White;
            this.btnApp.Location = new System.Drawing.Point(1249, 432);
            this.btnApp.Name = "btnApp";
            this.btnApp.Size = new System.Drawing.Size(245, 32);
            this.btnApp.TabIndex = 74;
            this.btnApp.Text = "App";
            this.btnApp.UseVisualStyleBackColor = true;
            this.btnApp.Click += new System.EventHandler(this.btnApp_Click);
            // 
            // dgvnoc
            // 
            this.dgvnoc.AllowUserToAddRows = false;
            this.dgvnoc.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvnoc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvnoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvnoc.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.dgvnoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvnoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvnoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5});
            this.dgvnoc.Location = new System.Drawing.Point(518, 432);
            this.dgvnoc.Name = "dgvnoc";
            this.dgvnoc.ReadOnly = true;
            this.dgvnoc.RowHeadersVisible = false;
            this.dgvnoc.Size = new System.Drawing.Size(345, 223);
            this.dgvnoc.TabIndex = 84;
            this.dgvnoc.Visible = false;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "App Transaction";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Frm_maya
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.ClientSize = new System.Drawing.Size(1506, 667);
            this.Controls.Add(this.dgvnoc);
            this.Controls.Add(this.btn_iload);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpto);
            this.Controls.Add(this.btnsplunk);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnDB);
            this.Controls.Add(this.btnElp);
            this.Controls.Add(this.btnApp);
            this.Controls.Add(this.lblname);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.BtnInvestigation);
            this.Controls.Add(this.dgvlist);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.lblprog);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnopenfile);
            this.Name = "Frm_maya";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Frm_maya_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvlist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvnoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnopenfile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblprog;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.DataGridView dgvlist;
        private System.Windows.Forms.Button BtnInvestigation;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label lblname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Button btn_iload;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpto;
        private System.Windows.Forms.Button btnsplunk;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnDB;
        private System.Windows.Forms.Button btnElp;
        private System.Windows.Forms.Button btnApp;
        private System.Windows.Forms.DataGridView dgvnoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}