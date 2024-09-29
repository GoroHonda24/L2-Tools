
namespace L2_GLA
{
    partial class Frm_Update_Brand
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
            this.mincount = new System.Windows.Forms.Label();
            this.txtformat = new System.Windows.Forms.TextBox();
            this.btnGen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBrand = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtmin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbold = new System.Windows.Forms.ComboBox();
            this.txtspiel = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.incid = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtsubject = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mincount
            // 
            this.mincount.AutoSize = true;
            this.mincount.Location = new System.Drawing.Point(839, 53);
            this.mincount.Name = "mincount";
            this.mincount.Size = new System.Drawing.Size(0, 13);
            this.mincount.TabIndex = 13;
            // 
            // txtformat
            // 
            this.txtformat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.txtformat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtformat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtformat.Location = new System.Drawing.Point(3, 25);
            this.txtformat.Multiline = true;
            this.txtformat.Name = "txtformat";
            this.txtformat.ReadOnly = true;
            this.txtformat.Size = new System.Drawing.Size(584, 322);
            this.txtformat.TabIndex = 12;
            // 
            // btnGen
            // 
            this.btnGen.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(244)))), ((int)(((byte)(235)))));
            this.btnGen.FlatAppearance.BorderSize = 0;
            this.btnGen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(22)))), ((int)(((byte)(5)))));
            this.btnGen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGen.Font = new System.Drawing.Font("Segoe UI Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGen.Location = new System.Drawing.Point(503, 244);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(167, 50);
            this.btnGen.TabIndex = 11;
            this.btnGen.Text = "Generate";
            this.btnGen.UseVisualStyleBackColor = false;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(332, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "NEW BRAND: ";
            // 
            // cmbBrand
            // 
            this.cmbBrand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.cmbBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBrand.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBrand.FormattingEnabled = true;
            this.cmbBrand.Items.AddRange(new object[] {
            "TNT PREPAID",
            "SMART PREPAID",
            "SMART POSTPAID",
            "BRO PREPAID",
            "SMART BRO POSTPAID",
            "HOME WIFI PREPAID"});
            this.cmbBrand.Location = new System.Drawing.Point(450, 209);
            this.cmbBrand.Name = "cmbBrand";
            this.cmbBrand.Size = new System.Drawing.Size(282, 29);
            this.cmbBrand.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(393, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 21);
            this.label1.TabIndex = 9;
            this.label1.Text = "MIN: ";
            // 
            // txtmin
            // 
            this.txtmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtmin.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmin.Location = new System.Drawing.Point(450, 139);
            this.txtmin.MaxLength = 12;
            this.txtmin.Name = "txtmin";
            this.txtmin.Size = new System.Drawing.Size(282, 29);
            this.txtmin.TabIndex = 7;
            this.txtmin.TextChanged += new System.EventHandler(this.txtmin_TextChanged);
            this.txtmin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtmin_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(336, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 21);
            this.label3.TabIndex = 15;
            this.label3.Text = "OLD BRAND: ";
            // 
            // cmbold
            // 
            this.cmbold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.cmbold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbold.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbold.FormattingEnabled = true;
            this.cmbold.Items.AddRange(new object[] {
            "TNT PREPAID",
            "SMART PREPAID",
            "SMART POSTPAID",
            "SMART FLP",
            "SMART BRO PREPAID",
            "SMART BRO POSTPAID",
            "HOME WIFI PREPAID",
            "DITO",
            "GLOBE",
            "ULTERA",
            "SUN"});
            this.cmbold.Location = new System.Drawing.Point(450, 174);
            this.cmbold.Name = "cmbold";
            this.cmbold.Size = new System.Drawing.Size(282, 29);
            this.cmbold.TabIndex = 14;
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
            this.txtspiel.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtspiel.Size = new System.Drawing.Size(642, 322);
            this.txtspiel.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(376, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 21);
            this.label4.TabIndex = 26;
            this.label4.Text = "INC ID :";
            // 
            // incid
            // 
            this.incid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.incid.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.incid.FormattingEnabled = true;
            this.incid.Items.AddRange(new object[] {
            "EMAIL"});
            this.incid.Location = new System.Drawing.Point(450, 44);
            this.incid.Name = "incid";
            this.incid.Size = new System.Drawing.Size(282, 29);
            this.incid.TabIndex = 27;
            this.incid.TextChanged += new System.EventHandler(this.incid_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(360, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 21);
            this.label5.TabIndex = 35;
            this.label5.Text = "SUBJECT :";
            // 
            // txtsubject
            // 
            this.txtsubject.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtsubject.Enabled = false;
            this.txtsubject.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.txtsubject.Location = new System.Drawing.Point(449, 79);
            this.txtsubject.Multiline = true;
            this.txtsubject.Name = "txtsubject";
            this.txtsubject.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtsubject.Size = new System.Drawing.Size(283, 54);
            this.txtsubject.TabIndex = 34;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.txtformat);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(6, 320);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 350);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MONGO DB";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.txtspiel);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(622, 320);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(648, 350);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SPIEL";
            // 
            // Frm_Update_Brand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.ClientSize = new System.Drawing.Size(1292, 681);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtsubject);
            this.Controls.Add(this.incid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbold);
            this.Controls.Add(this.mincount);
            this.Controls.Add(this.btnGen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbBrand);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtmin);
            this.Name = "Frm_Update_Brand";
            this.Text = "Frm_Update_Brand";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mincount;
        private System.Windows.Forms.TextBox txtformat;
        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBrand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtmin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbold;
        private System.Windows.Forms.TextBox txtspiel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox incid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtsubject;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}