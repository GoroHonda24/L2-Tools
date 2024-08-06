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
using System.Net.Http;
using MySqlX.XDevAPI;
using L2_GLA.Model;

namespace L2_GLA
{
    public partial class Frm_Inc_logs : Form
    {
        DBconnect conn = new DBconnect();

        public Frm_Inc_logs()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
        }
        public void txtclear()
        {
            txtInc.Clear();
            cmb_Status.SelectedIndex = -1;
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            using (MySqlCommand check_sql = new MySqlCommand("select * from tbl_inc where inc = @inc and created_at >= @created", conn.connection))
            {
                check_sql.Parameters.AddWithValue("@inc", txtInc.Text);
                check_sql.Parameters.AddWithValue("@created", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                using (MySqlDataReader reader = check_sql.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        using (MySqlCommand update_sql = new MySqlCommand("Update tbl_inc set status = @Status where inc = @inc", conn.connection))
                        {
                            reader.Close();
                            update_sql.Parameters.AddWithValue("@inc", txtInc.Text);
                            update_sql.Parameters.AddWithValue("@Status", cmb_Status.Text);
                            update_sql.ExecuteNonQuery();
                        }
                       
                    }
                    else
                    {
                        reader.Close();
                        using (MySqlCommand cmd = new MySqlCommand("INSERT INTO `tbl_inc`(`inc`,`Status`,`created_at`)VALUES(@inc, @Status, @created)", conn.connection))
                        {
                            cmd.Parameters.AddWithValue("@inc", txtInc.Text);
                            cmd.Parameters.AddWithValue("@Status", cmb_Status.Text);
                            cmd.Parameters.AddWithValue("@created", DateTime.Now);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Success");
                            txtclear();
                        }
                    }
                    
                }
            }
            

            using (MySqlCommand cmd = new MySqlCommand("Select count(inc)as count, Status from tbl_inc where created_at >= @date group by Status",conn.connection))
            {
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                dataGridView1.Rows.Clear();
                dataGridView1.AllowUserToAddRows = true;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                        row.Cells[0].Value = reader["Status"];
                        row.Cells[1].Value = reader["Count"];
                        dataGridView1.Rows.Add(row);
                    }
                    dataGridView1.AllowUserToAddRows = false;
                }
            }
        }

        private void Frm_Inc_logs_Load(object sender, EventArgs e)
        {
            using (MySqlCommand cmd = new MySqlCommand("Select count(inc)as count, Status from tbl_inc where created_at >= @date group by Status",conn.connection))
            {
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                dataGridView1.Rows.Clear();
                dataGridView1.AllowUserToAddRows = true;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();

                        row.Cells[0].Value = reader["Status"];
                        row.Cells[1].Value = reader["Count"];
                        dataGridView1.Rows.Add(row);
                        
                    }
                    dataGridView1.AllowUserToAddRows = false;                    
                }                                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (textBox1.Text is null || textBox1.Text == "")
            {
                return;
            }
            else
            {
                double amount = double.Parse(textBox1.Text);
                textBox2.Text = (amount * 0.05).ToString();
            }
           
            

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            apiclient client = new apiclient();
            await client.CallAPI();
        }
    }
}
