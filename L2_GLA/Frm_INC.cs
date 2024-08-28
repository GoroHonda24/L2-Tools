using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace L2_GLA
{
    public partial class Frm_INC : Form
    {
        DBconnect conn = new DBconnect();
        public Frm_INC()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent(); 
        }

        public void load_FC()
        {
            cmbFC.Items.Clear();
            using (MySqlCommand cmd = new MySqlCommand("SELECT fc FROM tbl_cms_smart WHERE fc IS NOT NULL order by fc asc", conn.connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbFC.Items.Add(reader["fc"]);
                    }
                }
            }
        }
        public void load_owner()
        {
            cmbOwner.Items.Clear();
            using (MySqlCommand cmd = new MySqlCommand("SELECT owner FROM tbl_cms_smart WHERE owner IS NOT NULL order by owner asc", conn.connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbOwner.Items.Add(reader["owner"]);
                    }
                }
            }
        }
        public void load_status()
        {
            cmbStatus.Items.Clear();
            using (MySqlCommand cmd = new MySqlCommand("SELECT status FROM tbl_cms_smart WHERE status IS NOT NULL order by status asc", conn.connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbStatus.Items.Add(reader["status"]);
                    }
                }
            }
        }
        private void Frm_INC_Load(object sender, EventArgs e)
        {
            load_FC();
            load_owner();
            load_status();
            Load_inc();
        }

        private string ValidateAndFormatPhoneNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            if (input.StartsWith("09"))
            {
                input = "639" + input.Substring(2);
            }
            else if (input.StartsWith("9"))
            {
                input = "63" + input;
            }

            // Check if the input starts with "639" and has exactly 12 digits
            if (input.StartsWith("639") && input.Length == 12)
            {
                return input;
            }

            // If input is invalid, return null
            return null;
        }
   
        public void Data_clear()
        {
            cmbFC.SelectedIndex = -1;
            cmbOwner.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            txtINC.Clear();
            txtMIN.Clear();
            txtnotes.Clear();
        }
        public void Load_inc()
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_inc_logs WHERE created_at >= NOW() - INTERVAL 12 HOUR and user=@user", conn.connection))
            {
               // cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                dgvINC.Rows.Clear();
                dgvINC.AllowUserToAddRows = true;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvINC.Rows[0].Clone();
                        row.Cells[0].Value = reader["inc"];
                        row.Cells[1].Value = reader["fc"];
                        row.Cells[2].Value = reader["min"];
                        row.Cells[3].Value = reader["owner"];
                        row.Cells[4].Value = reader["status"];
                        row.Cells[5].Value = reader["notes"];
                        dgvINC.Rows.Add(row);
                    }
                    dgvINC.AllowUserToAddRows = false;
                }
            }
        }
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtINC.Text))
            {
                MessageBox.Show("Incident # is required");
                return;
            }

            if (string.IsNullOrEmpty(cmbStatus.Text))
            {
                MessageBox.Show("Status is required");
                return;
            }

            string input = txtMIN.Text.Trim();
            string formattedNumber = ValidateAndFormatPhoneNumber(input);
            string inc = txtINC.Text.Trim();

            if (formattedNumber != null)
            {
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO brand_synch_2.tbl_inc_logs (inc, min, fc, owner, status, notes, created_at,user) " +
                                       "VALUES (@inc, @min, @fc, @owner, @status, @notes, @created_at,@user)", conn.connection))
                {
                    cmd.Parameters.AddWithValue("@inc", inc);
                    cmd.Parameters.AddWithValue("@min", formattedNumber);
                    cmd.Parameters.AddWithValue("@fc", cmbFC.Text);
                    cmd.Parameters.AddWithValue("@owner", cmbOwner.Text);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                    cmd.Parameters.AddWithValue("@notes", txtnotes.Text);
                    cmd.Parameters.AddWithValue("@created_at", DateTime.Now);
                    cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"INC #: {inc} successfully save");
            }
            else
            {
                // Invalid number, show error
                MessageBox.Show("Invalid number");
                return;
            }
            Data_clear();
            Load_inc();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbStatus.Text == "RESOLVED / REJECTED"  || cmbStatus.Text == "RESOLVED / CLOSED")
            {
                cmbOwner.Text = "Smart App";
            }
            else
            {
                cmbOwner.SelectedIndex = -1;
            }
        }
    }
}
