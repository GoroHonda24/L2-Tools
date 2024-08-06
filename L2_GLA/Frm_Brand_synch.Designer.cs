
namespace L2_GLA
{
    partial class Frm_Brand_synch
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
            this.bca = new System.Windows.Forms.PictureBox();
            this.brand_series = new System.Windows.Forms.PictureBox();
            this.metadata = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bca)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brand_series)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.metadata)).BeginInit();
            this.SuspendLayout();
            // 
            // bca
            // 
            this.bca.Image = global::L2_GLA.Properties.Resources.bca_file;
            this.bca.Location = new System.Drawing.Point(619, 111);
            this.bca.Name = "bca";
            this.bca.Size = new System.Drawing.Size(237, 115);
            this.bca.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bca.TabIndex = 2;
            this.bca.TabStop = false;
            // 
            // brand_series
            // 
            this.brand_series.Image = global::L2_GLA.Properties.Resources.brand_series;
            this.brand_series.Location = new System.Drawing.Point(47, 111);
            this.brand_series.Name = "brand_series";
            this.brand_series.Size = new System.Drawing.Size(237, 115);
            this.brand_series.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.brand_series.TabIndex = 1;
            this.brand_series.TabStop = false;
            this.brand_series.Click += new System.EventHandler(this.brand_series_Click);
            // 
            // metadata
            // 
            this.metadata.Image = global::L2_GLA.Properties.Resources.min_metadata;
            this.metadata.Location = new System.Drawing.Point(335, 111);
            this.metadata.Name = "metadata";
            this.metadata.Size = new System.Drawing.Size(237, 115);
            this.metadata.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.metadata.TabIndex = 0;
            this.metadata.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Brand Series";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(331, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Min Metadata";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(615, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "BCA File";
            // 
            // Frm_Brand_synch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1795, 814);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bca);
            this.Controls.Add(this.brand_series);
            this.Controls.Add(this.metadata);
            this.Name = "Frm_Brand_synch";
            this.Text = "Frm_Brand_synch";
            ((System.ComponentModel.ISupportInitialize)(this.bca)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brand_series)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.metadata)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox metadata;
        private System.Windows.Forms.PictureBox brand_series;
        private System.Windows.Forms.PictureBox bca;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}