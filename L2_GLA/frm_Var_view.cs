using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace L2_GLA
{
    public partial class frm_Var_view : Form
    {
        DBconnect conn = new DBconnect();
        string varName = "";
        public frm_Var_view()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void frm_Var_view_Load(object sender, EventArgs e)
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT id FROM brand_synch_2.tbl_variance_file where File_name =@file",conn.connection))
            {
                cmd.Parameters.AddWithValue("@file", GlobalVar.varName);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        varName = reader["id"].ToString();
                        Console.WriteLine(varName);
                    }
                }
            }

            if(GlobalVar.vartype == "maya")
            {
                using (MySqlCommand cmd = new MySqlCommand ("Select investigation.*, tbl_variance_file.File_Name from investigation join tbl_variance_file ON tbl_variance_file.id = investigation.file_id " +
                    "where Action in ('Refund to customer', 'Load Reversal or Retry Payment Charging') and  investigation.file_id = @id; ",conn.connection))
                {
                    cmd.Parameters.AddWithValue("@id", varName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        dgvlist.Rows.Clear();
                        dgvlist.AllowUserToAddRows = true;


                        while (reader.Read())
                        {
                                DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();
                                //row.Cells[0].Value = reader["id"];
                                row.Cells[0].Value = reader["MERCHANT_TRANS_ID"];
                                row.Cells[1].Value = reader["ELP_Reference_Number"].ToString().Replace(" | success", "");
                                row.Cells[2].Value = reader["Status"];
                                row.Cells[3].Value = reader["Splunk_Status"];
                                row.Cells[4].Value = reader["iload"];
                                row.Cells[5].Value = reader["Remarks"];
                                dgvlist.Rows.Add(row);
                            }
                        }
                        dgvlist.AllowUserToAddRows = false;
                    }
            }
            else
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM brand_synch_2.gcash where file_id = @id ", conn.connection))
                {
                    cmd.Parameters.AddWithValue("@id", varName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        dgvlist.Rows.Clear();
                        dgvlist.AllowUserToAddRows = true;


                        while (reader.Read())
                        {
                            DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();
                            //row.Cells[0].Value = reader["id"];
                            row.Cells[0].Value = reader["MERCHANT_TRANS_ID"];
                            row.Cells[1].Value = reader["ELP_Reference_Number"].ToString().Replace(" | success", "");
                            row.Cells[2].Value = reader["Status"];
                            row.Cells[3].Value = reader["Splunk_Status"];
                            row.Cells[4].Value = reader["iload"];
                            row.Cells[5].Value = reader["Remarks"];
                            dgvlist.Rows.Add(row);
                        }
                    }
                    dgvlist.AllowUserToAddRows = false;
                }
            }
            }
        
        }
    }
