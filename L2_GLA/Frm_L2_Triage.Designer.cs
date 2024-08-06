
namespace L2_GLA
{
    partial class Frm_L2_Triage
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
            this.txtUts_id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.rdbYes = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbNo = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCall_id = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbsystem_name = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtConcern = new System.Windows.Forms.TextBox();
            this.txtError = new System.Windows.Forms.TextBox();
            this.txtWork_around = new System.Windows.Forms.TextBox();
            this.cmbReason = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtfindings = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbFc = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUts_id
            // 
            this.txtUts_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUts_id.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUts_id.Location = new System.Drawing.Point(196, 85);
            this.txtUts_id.Name = "txtUts_id";
            this.txtUts_id.Size = new System.Drawing.Size(644, 26);
            this.txtUts_id.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "UTS ID    :";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(196, 257);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(644, 28);
            this.cmbStatus.TabIndex = 4;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // rdbYes
            // 
            this.rdbYes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbYes.AutoSize = true;
            this.rdbYes.Location = new System.Drawing.Point(15, 33);
            this.rdbYes.Name = "rdbYes";
            this.rdbYes.Size = new System.Drawing.Size(58, 24);
            this.rdbYes.TabIndex = 6;
            this.rdbYes.TabStop = true;
            this.rdbYes.Text = "Yes";
            this.rdbYes.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbNo);
            this.groupBox1.Controls.Add(this.rdbYes);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(196, 355);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 75);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bulk?";
            // 
            // rdbNo
            // 
            this.rdbNo.AutoSize = true;
            this.rdbNo.Location = new System.Drawing.Point(97, 33);
            this.rdbNo.Name = "rdbNo";
            this.rdbNo.Size = new System.Drawing.Size(49, 24);
            this.rdbNo.TabIndex = 7;
            this.rdbNo.TabStop = true;
            this.rdbNo.Text = "No";
            this.rdbNo.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(57, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 20);
            this.label11.TabIndex = 21;
            this.label11.Text = "*";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Customer Call ID    :";
            // 
            // txtCall_id
            // 
            this.txtCall_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCall_id.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCall_id.Location = new System.Drawing.Point(196, 127);
            this.txtCall_id.Name = "txtCall_id";
            this.txtCall_id.Size = new System.Drawing.Size(644, 26);
            this.txtCall_id.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 261);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Status    :";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 304);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Status Reason    :";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "System Name    :";
            // 
            // cmbsystem_name
            // 
            this.cmbsystem_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbsystem_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbsystem_name.FormattingEnabled = true;
            this.cmbsystem_name.Location = new System.Drawing.Point(196, 169);
            this.cmbsystem_name.Name = "cmbsystem_name";
            this.cmbsystem_name.Size = new System.Drawing.Size(644, 28);
            this.cmbsystem_name.TabIndex = 3;
            this.cmbsystem_name.SelectedIndexChanged += new System.EventHandler(this.cmbsystem_name_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(883, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Concern :";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(908, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Error :";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(850, 281);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Work Around :";
            // 
            // txtConcern
            // 
            this.txtConcern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConcern.Location = new System.Drawing.Point(964, 94);
            this.txtConcern.Multiline = true;
            this.txtConcern.Name = "txtConcern";
            this.txtConcern.Size = new System.Drawing.Size(468, 76);
            this.txtConcern.TabIndex = 8;
            // 
            // txtError
            // 
            this.txtError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtError.Location = new System.Drawing.Point(964, 186);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(468, 76);
            this.txtError.TabIndex = 9;
            // 
            // txtWork_around
            // 
            this.txtWork_around.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWork_around.Location = new System.Drawing.Point(964, 278);
            this.txtWork_around.Multiline = true;
            this.txtWork_around.Name = "txtWork_around";
            this.txtWork_around.Size = new System.Drawing.Size(468, 76);
            this.txtWork_around.TabIndex = 10;
            // 
            // cmbReason
            // 
            this.cmbReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReason.FormattingEnabled = true;
            this.cmbReason.Location = new System.Drawing.Point(198, 301);
            this.cmbReason.Name = "cmbReason";
            this.cmbReason.Size = new System.Drawing.Size(644, 28);
            this.cmbReason.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(711, 542);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(170, 57);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtfindings
            // 
            this.txtfindings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtfindings.Location = new System.Drawing.Point(964, 369);
            this.txtfindings.Multiline = true;
            this.txtfindings.Name = "txtfindings";
            this.txtfindings.Size = new System.Drawing.Size(468, 76);
            this.txtfindings.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(861, 372);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 40);
            this.label9.TabIndex = 19;
            this.label9.Text = "Investigation\r\nFinding :";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(173, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 20);
            this.label10.TabIndex = 20;
            this.label10.Text = "*";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(175, 258);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 20);
            this.label12.TabIndex = 22;
            this.label12.Text = "*";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(177, 302);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 20);
            this.label13.TabIndex = 23;
            this.label13.Text = "*";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(175, 171);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(16, 20);
            this.label14.TabIndex = 24;
            this.label14.Text = "*";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(175, 129);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(16, 20);
            this.label15.TabIndex = 25;
            this.label15.Text = "*";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(41, 216);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(151, 20);
            this.label17.TabIndex = 27;
            this.label17.Text = "Functional Category";
            // 
            // cmbFc
            // 
            this.cmbFc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFc.FormattingEnabled = true;
            this.cmbFc.Location = new System.Drawing.Point(196, 213);
            this.cmbFc.Name = "cmbFc";
            this.cmbFc.Size = new System.Drawing.Size(644, 28);
            this.cmbFc.TabIndex = 26;
            // 
            // Frm_L2_Triage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1466, 1061);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cmbFc);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtfindings);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbReason);
            this.Controls.Add(this.txtWork_around);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.txtConcern);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbsystem_name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCall_id);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUts_id);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Frm_L2_Triage";
            this.Text = "Frm_L2_Triage";
            this.Load += new System.EventHandler(this.Frm_L2_Triage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUts_id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.RadioButton rdbYes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCall_id;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbsystem_name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtConcern;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.TextBox txtWork_around;
        private System.Windows.Forms.ComboBox cmbReason;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtfindings;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbFc;
    }
}