namespace L2_GLA.Variance
{
    partial class queryFormat
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
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtQuery
            // 
            this.txtQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.txtQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuery.ForeColor = System.Drawing.SystemColors.Window;
            this.txtQuery.Location = new System.Drawing.Point(94, 40);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtQuery.Size = new System.Drawing.Size(1428, 517);
            this.txtQuery.TabIndex = 0;
            // 
            // queryFormat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(193)))), ((int)(((byte)(108)))));
            this.ClientSize = new System.Drawing.Size(1610, 626);
            this.Controls.Add(this.txtQuery);
            this.Name = "queryFormat";
            this.Text = "queryFormat";
            this.Load += new System.EventHandler(this.queryFormat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQuery;
    }
}