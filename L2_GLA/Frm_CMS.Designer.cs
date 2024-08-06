
namespace L2_GLA
{
    partial class Frm_CMS
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvuser = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.cmbTeam = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFname = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmpID = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvFC = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.dgvReason = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSystem = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStatus = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.cmbmune = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textDetails = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.dgv_FC = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvOwner = new System.Windows.Forms.DataGridView();
            this.dgv_Status = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvuser)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOwner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Status)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(2, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1459, 739);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvuser);
            this.tabPage1.Controls.Add(this.cmbRole);
            this.tabPage1.Controls.Add(this.cmbTeam);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtFname);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtEmpID);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1451, 710);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "User Whitelisted";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // dgvuser
            // 
            this.dgvuser.AllowUserToAddRows = false;
            this.dgvuser.AllowUserToDeleteRows = false;
            this.dgvuser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvuser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dgvuser.Location = new System.Drawing.Point(64, 321);
            this.dgvuser.Name = "dgvuser";
            this.dgvuser.ReadOnly = true;
            this.dgvuser.RowHeadersVisible = false;
            this.dgvuser.Size = new System.Drawing.Size(853, 267);
            this.dgvuser.TabIndex = 21;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column2.HeaderText = "User ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 89;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "User Name";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column4.HeaderText = "Team";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 74;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column5.HeaderText = "Role";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 67;
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Items.AddRange(new object[] {
            "ADMIN",
            "TEAM LEAD",
            "USER"});
            this.cmbRole.Location = new System.Drawing.Point(262, 169);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(229, 28);
            this.cmbRole.TabIndex = 10;
            // 
            // cmbTeam
            // 
            this.cmbTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeam.FormattingEnabled = true;
            this.cmbTeam.Items.AddRange(new object[] {
            "L2 Smart App",
            "L2 Triage"});
            this.cmbTeam.Location = new System.Drawing.Point(262, 133);
            this.cmbTeam.Name = "cmbTeam";
            this.cmbTeam.Size = new System.Drawing.Size(229, 28);
            this.cmbTeam.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(212, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Role:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(205, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Team:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "First Name: \r\n";
            // 
            // txtFname
            // 
            this.txtFname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFname.Location = new System.Drawing.Point(262, 99);
            this.txtFname.Name = "txtFname";
            this.txtFname.Size = new System.Drawing.Size(229, 26);
            this.txtFname.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(320, 229);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 38);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Employee ID:";
            // 
            // txtEmpID
            // 
            this.txtEmpID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmpID.Location = new System.Drawing.Point(262, 65);
            this.txtEmpID.Name = "txtEmpID";
            this.txtEmpID.Size = new System.Drawing.Size(229, 26);
            this.txtEmpID.TabIndex = 0;
            this.txtEmpID.TextChanged += new System.EventHandler(this.txtEmpID_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgv_Status);
            this.tabPage2.Controls.Add(this.dgvOwner);
            this.tabPage2.Controls.Add(this.dgv_FC);
            this.tabPage2.Controls.Add(this.buttonSave);
            this.tabPage2.Controls.Add(this.textDetails);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.cmbmune);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1451, 710);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Smart App CMS";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvFC);
            this.tabPage3.Controls.Add(this.cmbStatus);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.txtDetails);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.cmbCategory);
            this.tabPage3.Controls.Add(this.dgvReason);
            this.tabPage3.Controls.Add(this.dgvSystem);
            this.tabPage3.Controls.Add(this.dgvStatus);
            this.tabPage3.Controls.Add(this.btnAdd);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1451, 710);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "L2 Triage";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // dgvFC
            // 
            this.dgvFC.AllowUserToAddRows = false;
            this.dgvFC.AllowUserToDeleteRows = false;
            this.dgvFC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3});
            this.dgvFC.Location = new System.Drawing.Point(898, 291);
            this.dgvFC.Name = "dgvFC";
            this.dgvFC.ReadOnly = true;
            this.dgvFC.RowHeadersVisible = false;
            this.dgvFC.Size = new System.Drawing.Size(430, 267);
            this.dgvFC.TabIndex = 30;
            this.toolTip1.SetToolTip(this.dgvFC, "Right Click to Remove System Name");
            this.dgvFC.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFC_CellMouseDown);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "FUNCTIONAL CATEGORY";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // cmbStatus
            // 
            this.cmbStatus.AllowDrop = true;
            this.cmbStatus.Location = new System.Drawing.Point(427, 88);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(389, 28);
            this.cmbStatus.Sorted = true;
            this.cmbStatus.TabIndex = 29;
            this.cmbStatus.TextChanged += new System.EventHandler(this.cmbStatus_TextChanged);
            this.cmbStatus.Click += new System.EventHandler(this.cmbStatus_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(344, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 20);
            this.label6.TabIndex = 28;
            this.label6.Text = "Details:";
            // 
            // txtDetails
            // 
            this.txtDetails.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDetails.Location = new System.Drawing.Point(427, 137);
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.Size = new System.Drawing.Size(389, 27);
            this.txtDetails.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(327, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 20);
            this.label5.TabIndex = 26;
            this.label5.Text = "Category:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Items.AddRange(new object[] {
            "STATUS",
            "SYSTEM NAME"});
            this.cmbCategory.Location = new System.Drawing.Point(427, 38);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(389, 28);
            this.cmbCategory.TabIndex = 25;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            this.cmbCategory.Click += new System.EventHandler(this.cmbCategory_Click);
            // 
            // dgvReason
            // 
            this.dgvReason.AllowUserToAddRows = false;
            this.dgvReason.AllowUserToDeleteRows = false;
            this.dgvReason.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReason.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2});
            this.dgvReason.Location = new System.Drawing.Point(335, 291);
            this.dgvReason.Name = "dgvReason";
            this.dgvReason.ReadOnly = true;
            this.dgvReason.RowHeadersVisible = false;
            this.dgvReason.Size = new System.Drawing.Size(257, 267);
            this.dgvReason.TabIndex = 24;
            this.toolTip1.SetToolTip(this.dgvReason, "Right Click to Remove Reason");
            this.dgvReason.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvReason_CellMouseDown);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "REASON";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dgvSystem
            // 
            this.dgvSystem.AllowUserToAddRows = false;
            this.dgvSystem.AllowUserToDeleteRows = false;
            this.dgvSystem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSystem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.dgvSystem.Location = new System.Drawing.Point(618, 291);
            this.dgvSystem.Name = "dgvSystem";
            this.dgvSystem.ReadOnly = true;
            this.dgvSystem.RowHeadersVisible = false;
            this.dgvSystem.Size = new System.Drawing.Size(257, 267);
            this.dgvSystem.TabIndex = 21;
            this.toolTip1.SetToolTip(this.dgvSystem, "Right Click to Remove System Name");
            this.dgvSystem.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSystem_CellMouseDown);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "SYSTEM";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dgvStatus
            // 
            this.dgvStatus.AllowUserToAddRows = false;
            this.dgvStatus.AllowUserToDeleteRows = false;
            this.dgvStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgvStatus.Location = new System.Drawing.Point(55, 291);
            this.dgvStatus.Name = "dgvStatus";
            this.dgvStatus.ReadOnly = true;
            this.dgvStatus.RowHeadersVisible = false;
            this.dgvStatus.Size = new System.Drawing.Size(257, 267);
            this.dgvStatus.TabIndex = 20;
            this.toolTip1.SetToolTip(this.dgvStatus, "Right Click to Remove Status");
            this.dgvStatus.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvStatus_CellMouseDown);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "STATUS";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(522, 182);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(130, 54);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(358, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 20);
            this.label8.TabIndex = 12;
            this.label8.Text = "Name:";
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(304, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Menu :";
            // 
            // cmbmune
            // 
            this.cmbmune.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbmune.FormattingEnabled = true;
            this.cmbmune.Items.AddRange(new object[] {
            "Functional Category",
            "Owner",
            "Status"});
            this.cmbmune.Location = new System.Drawing.Point(373, 67);
            this.cmbmune.Name = "cmbmune";
            this.cmbmune.Size = new System.Drawing.Size(317, 28);
            this.cmbmune.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(289, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 20);
            this.label9.TabIndex = 2;
            this.label9.Text = "Details :";
            // 
            // textDetails
            // 
            this.textDetails.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDetails.Location = new System.Drawing.Point(373, 101);
            this.textDetails.Name = "textDetails";
            this.textDetails.Size = new System.Drawing.Size(317, 27);
            this.textDetails.TabIndex = 3;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(441, 144);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(164, 42);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // dgv_FC
            // 
            this.dgv_FC.AllowUserToAddRows = false;
            this.dgv_FC.AllowUserToDeleteRows = false;
            this.dgv_FC.AllowUserToOrderColumns = true;
            this.dgv_FC.BackgroundColor = System.Drawing.Color.White;
            this.dgv_FC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_FC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_FC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6});
            this.dgv_FC.Location = new System.Drawing.Point(221, 209);
            this.dgv_FC.Name = "dgv_FC";
            this.dgv_FC.ReadOnly = true;
            this.dgv_FC.RowHeadersVisible = false;
            this.dgv_FC.Size = new System.Drawing.Size(223, 250);
            this.dgv_FC.TabIndex = 5;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.HeaderText = "Functional Category";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // dgvOwner
            // 
            this.dgvOwner.AllowUserToAddRows = false;
            this.dgvOwner.AllowUserToDeleteRows = false;
            this.dgvOwner.AllowUserToOrderColumns = true;
            this.dgvOwner.BackgroundColor = System.Drawing.Color.White;
            this.dgvOwner.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOwner.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOwner.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4});
            this.dgvOwner.Location = new System.Drawing.Point(445, 209);
            this.dgvOwner.Name = "dgvOwner";
            this.dgvOwner.ReadOnly = true;
            this.dgvOwner.RowHeadersVisible = false;
            this.dgvOwner.Size = new System.Drawing.Size(223, 250);
            this.dgvOwner.TabIndex = 6;
            // 
            // dgv_Status
            // 
            this.dgv_Status.AllowUserToAddRows = false;
            this.dgv_Status.AllowUserToDeleteRows = false;
            this.dgv_Status.AllowUserToOrderColumns = true;
            this.dgv_Status.BackgroundColor = System.Drawing.Color.White;
            this.dgv_Status.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_Status.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Status.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5});
            this.dgv_Status.Location = new System.Drawing.Point(669, 209);
            this.dgv_Status.Name = "dgv_Status";
            this.dgv_Status.ReadOnly = true;
            this.dgv_Status.RowHeadersVisible = false;
            this.dgv_Status.Size = new System.Drawing.Size(223, 250);
            this.dgv_Status.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "Owner";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.HeaderText = "Status";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // Frm_CMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1464, 738);
            this.Controls.Add(this.tabControl1);
            this.Name = "Frm_CMS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_CMS";
            this.Load += new System.EventHandler(this.Frm_CMS_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvuser)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOwner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Status)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.ComboBox cmbTeam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFname;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmpID;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvSystem;
        private System.Windows.Forms.DataGridView dgvStatus;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvReason;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridView dgvuser;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridView dgvFC;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbmune;
        private System.Windows.Forms.DataGridView dgv_FC;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textDetails;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgv_Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridView dgvOwner;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}