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
    public partial class Frm_Var_Report : Form
    {
        DBconnect conn = new DBconnect();
        public Frm_Var_Report()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
            dgvtblreport.RowPrePaint += dgvtblreport_RowPrePaint;
        }

        private void Frm_Var_Report_Load(object sender, EventArgs e)
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_variance_file order by created_at desc", conn.connection))
            {
                using (MySqlDataReader Reader = cmd.ExecuteReader())
                {
                    dgvtblreport.Rows.Clear();
                    dgvtblreport.AllowUserToAddRows = true;
                    while (Reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvtblreport.Rows[0].Clone();
                        row.Cells[0].Value = Reader["File_name"];
                        row.Cells[1].Value = Reader["Var_type"];
                       // row.Cells[2].Value = Reader["Assigned_to"];
                        row.Cells[2].Value = Reader["Status"];
                        row.Cells[3].Value = Reader["Done_by"];
                        row.Cells[4].Value = Reader["created_at"];
                        row.Cells[5].Value = Reader["end_at"].ToString();
                        dgvtblreport.Rows.Add(row);

                    }
                    dgvtblreport.AllowUserToAddRows = false;
                }
            }
            
        }
        string file_name = "";
        public void Edit(object sender, EventArgs e)
        {
           // string status = dgvtblreport.Rows[dgvtblreport.CurrentCell.RowIndex].Cells[0].Value.ToString();
            file_name = dgvtblreport.Rows[dgvtblreport.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtVarName.Text = dgvtblreport.Rows[dgvtblreport.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtassigned.Text = dgvtblreport.Rows[dgvtblreport.CurrentCell.RowIndex].Cells[2].Value.ToString();
            txtvarType.Text = dgvtblreport.Rows[dgvtblreport.CurrentCell.RowIndex].Cells[1].Value.ToString();
            cbbStatus.Text = dgvtblreport.Rows[dgvtblreport.CurrentCell.RowIndex].Cells[3].Value.ToString();



        }
        public void Extract(object sender, EventArgs e)
        {
            GlobalVar.varName = dgvtblreport.Rows[dgvtblreport.CurrentCell.RowIndex].Cells[0].Value.ToString();
            GlobalVar.vartype = dgvtblreport.Rows[dgvtblreport.CurrentCell.RowIndex].Cells[1].Value.ToString();
            frm_Var_view frmobj = new frm_Var_view();
            frmobj.ShowDialog();

        }
        private void dgvtblreport_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            if (dgvtblreport.Rows.Count != 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (e.RowIndex == -1) return;
                    if (e.ColumnIndex == -1) return;

                    dgvtblreport.CurrentCell = dgvtblreport.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvtblreport.Rows[e.RowIndex].Selected = true;
                    dgvtblreport.Focus();                   
                    ToolStripMenuItem mnuextract= new ToolStripMenuItem("View");
                   // mnuremove.Click += new EventHandler(Edit);
                    mnuextract.Click += new EventHandler(Extract);

                 //   mnu.Items.AddRange(new ToolStripItem[] { mnuremove });
                    mnu.Items.AddRange(new ToolStripItem[] { mnuextract });
                   // dgvtblreport.ContextMenuStrip = mnu;
                    dgvtblreport.ContextMenuStrip = mnu;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Console.WriteLine("1" + file_name);
            if (cbbStatus.Text == "Done")
            {
                using (MySqlCommand sql = new MySqlCommand("UPDATE `brand_synch_2`.`tbl_variance_file` SET `File_name` = @filename, `Status` = @status, `Done_by` = @doneby,`end_at` = @enddt WHERE `File_name` = @id", conn.connection))
                {
                    sql.Parameters.AddWithValue("@filename", txtVarName.Text);
                    sql.Parameters.AddWithValue("@status", cbbStatus.Text);
                    sql.Parameters.AddWithValue("@doneby", GlobalVar.user);
                    sql.Parameters.AddWithValue("@enddt", DateTime.Now.ToString(""));
                    sql.Parameters.AddWithValue("@id", file_name);
                    sql.ExecuteNonQuery();
                    Console.WriteLine("2" + file_name);
                }
            }
            else
            {
                using (MySqlCommand sql = new MySqlCommand("UPDATE `brand_synch_2`.`tbl_variance_file` SET `File_name` = @filename , `Status` = @status WHERE `File_name` = @id", conn.connection))
                {
                    sql.Parameters.AddWithValue("@filename", txtVarName.Text);
                    sql.Parameters.AddWithValue("@status", cbbStatus.Text);                    
                    sql.Parameters.AddWithValue("@id", file_name);
                    sql.ExecuteNonQuery();
                    Console.WriteLine("3" + file_name);
                }
            }
        }
        private void dgvtblreport_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = dgvtblreport.Rows[e.RowIndex];
            if (row.Cells["Column3"].Value != null)
            {
                string status = row.Cells["Column3"].Value.ToString();
                if(status== "Done")
                {
                    row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }else if (status == "Pending to NOC" || status == "Pending to PayOnline")
                {
                    row.DefaultCellStyle.BackColor = Color.OrangeRed;
                }
                else if (status == "Ongoing")
                {
                    row.DefaultCellStyle.BackColor = Color.Orange;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "All")
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_variance_file order by created_at desc", conn.connection))
                {
                    cmd.Parameters.AddWithValue("@stat", comboBox1.Text);
                    using (MySqlDataReader Reader = cmd.ExecuteReader())
                    {
                        dgvtblreport.Rows.Clear();
                        dgvtblreport.AllowUserToAddRows = true;
                        while (Reader.Read())
                        {
                            DataGridViewRow row = (DataGridViewRow)dgvtblreport.Rows[0].Clone();
                            row.Cells[0].Value = Reader["File_name"];
                            row.Cells[1].Value = Reader["Var_type"];
                            row.Cells[2].Value = Reader["Assigned_to"];
                            row.Cells[3].Value = Reader["Status"];
                            row.Cells[4].Value = Reader["Done_by"];
                            row.Cells[5].Value = Reader["created_at"];
                            row.Cells[6].Value = Reader["end_at"].ToString();
                            dgvtblreport.Rows.Add(row);

                        }
                        dgvtblreport.AllowUserToAddRows = false;
                    }
                }
            }
            else
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_variance_file where status = @stat order by created_at desc", conn.connection))
                {
                    cmd.Parameters.AddWithValue("@stat", comboBox1.Text);
                    using (MySqlDataReader Reader = cmd.ExecuteReader())
                    {
                        dgvtblreport.Rows.Clear();
                        dgvtblreport.AllowUserToAddRows = true;
                        while (Reader.Read())
                        {
                            DataGridViewRow row = (DataGridViewRow)dgvtblreport.Rows[0].Clone();
                            row.Cells[0].Value = Reader["File_name"];
                            row.Cells[1].Value = Reader["Var_type"];
                            row.Cells[2].Value = Reader["Assigned_to"];
                            row.Cells[3].Value = Reader["Status"];
                            row.Cells[4].Value = Reader["Done_by"];
                            row.Cells[5].Value = Reader["created_at"];
                            row.Cells[6].Value = Reader["end_at"].ToString();
                            dgvtblreport.Rows.Add(row);

                        }
                        dgvtblreport.AllowUserToAddRows = false;
                    }
                }
            }
            
        }
    }
}
