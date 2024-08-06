
namespace L2_GLA
{
    partial class Frm_Delete
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
            this.txtspiel = new System.Windows.Forms.TextBox();
            this.btnGen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtmin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.incid = new System.Windows.Forms.TextBox();
            this.mincount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtspiel
            // 
            this.txtspiel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtspiel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtspiel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtspiel.Location = new System.Drawing.Point(6, 28);
            this.txtspiel.Multiline = true;
            this.txtspiel.Name = "txtspiel";
            this.txtspiel.ReadOnly = true;
            this.txtspiel.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtspiel.Size = new System.Drawing.Size(857, 316);
            this.txtspiel.TabIndex = 22;
            // 
            // btnGen
            // 
            this.btnGen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(244)))), ((int)(((byte)(235)))));
            this.btnGen.FlatAppearance.BorderSize = 0;
            this.btnGen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(22)))), ((int)(((byte)(5)))));
            this.btnGen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGen.Font = new System.Drawing.Font("Segoe UI Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGen.Location = new System.Drawing.Point(573, 132);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(167, 50);
            this.btnGen.TabIndex = 21;
            this.btnGen.Text = "Generate";
            this.btnGen.UseVisualStyleBackColor = false;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(440, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 21);
            this.label1.TabIndex = 20;
            this.label1.Text = "MIN: ";
            // 
            // txtmin
            // 
            this.txtmin.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmin.Location = new System.Drawing.Point(494, 89);
            this.txtmin.MaxLength = 12;
            this.txtmin.Name = "txtmin";
            this.txtmin.Size = new System.Drawing.Size(325, 29);
            this.txtmin.TabIndex = 19;
            this.txtmin.TextChanged += new System.EventHandler(this.txtmin_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(423, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 21);
            this.label3.TabIndex = 24;
            this.label3.Text = "INC ID :";
            // 
            // incid
            // 
            this.incid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.incid.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.incid.Location = new System.Drawing.Point(494, 54);
            this.incid.MaxLength = 100;
            this.incid.Name = "incid";
            this.incid.Size = new System.Drawing.Size(325, 29);
            this.incid.TabIndex = 23;
            // 
            // mincount
            // 
            this.mincount.AutoSize = true;
            this.mincount.Location = new System.Drawing.Point(858, 98);
            this.mincount.Name = "mincount";
            this.mincount.Size = new System.Drawing.Size(0, 13);
            this.mincount.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtspiel);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(205, 212);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(868, 350);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SPIEL";
            // 
            // Frm_Delete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.ClientSize = new System.Drawing.Size(1349, 658);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mincount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.incid);
            this.Controls.Add(this.btnGen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtmin);
            this.Name = "Frm_Delete";
            this.Text = "Frm_Delete";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtspiel;
        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtmin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox incid;
        private System.Windows.Forms.Label mincount;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}