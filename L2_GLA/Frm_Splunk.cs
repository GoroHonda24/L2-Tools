using CsvHelper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L2_GLA
{
    public partial class Frm_Splunk : Form
    {
        MySqlCommand cmd;
        DBconnect conn = new DBconnect();
        MySqlDataReader Reader;



        public Frm_Splunk()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
        }

        private void btnopenfile_Click(object sender, EventArgs e)
        {
            int csvcount = 0;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                String filename = Path.GetFileName(selectedFilePath);

                try
                {
                    // Read and parse the CSV file using CsvHelper.
                    using (var reader = new StreamReader(selectedFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<splunk>().ToList(); // Replace YourDataClass with your custom class to map CSV columns.

                    cmd = new MySqlCommand("Select * from tbl_file where file_name = '" + filename + "'", conn.connection);
                    Reader = cmd.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        MessageBox.Show("File " + filename + "is already existing");
                        Reader.Close();
                        return;
                    }
                    else
                    {
                        Reader.Close();
                        cmd = new MySqlCommand("Insert into tbl_file (`file_name`,`user`,`created_at`) values ('" + filename + "','" + GlobalVar.user + "', '" + DateTime.Now.ToString() + "')", conn.connection);
                        cmd.ExecuteNonQuery();
                    }
                    Reader.Close();
                   

                    foreach (var record in records)
                        {
                            // Adjust the SQL INSERT statement to match your table structure.
                            cmd = new MySqlCommand("INSERT INTO `Tbl_splunk` (`time`, `s_id`, `processor_ref_no`, `app_transaction_number`, `state`)" +
                                " VALUES (@Value1, @Value2, @Value3, @Value4, @Value5);", conn.connection);

                            // Add parameters to the command.
                            cmd.Parameters.AddWithValue("@Value1", record._time);
                            cmd.Parameters.AddWithValue("@Value2", record.id);
                            cmd.Parameters.AddWithValue("@Value3", record.processor_ref_no);
                            cmd.Parameters.AddWithValue("@Value4", record.app_transaction_number);
                            cmd.Parameters.AddWithValue("@Value5", record.state);
                            cmd.ExecuteNonQuery();
                            csvcount++;
                        }

                    }
                  
                    MessageBox.Show("Total of  " + csvcount + " Splunk record has been save");
                    csvcount = 0;
                   // progressBar1.Value = 0;
                    LOAD_FILE();
            }
                catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int rowcount = 0;
         //   int loop = 0;
            string searchTerm = txtsearch.Text;
            string[] searchTerms = searchTerm.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if(txtsearch.Text=="" || txtsearch.Text is null)
            {
                return;
            }

            // Assuming connection is a MySqlConnection object already created and opened
            string query = "SELECT time, app_transaction_number,state FROM tbl_splunk WHERE ";

            for (int i = 0; i < searchTerms.Length; i++)
            {
                query += "app_transaction_number LIKE @param" + i;
                if (i < searchTerms.Length - 1)
                    query += " OR ";
            }

            using (MySqlCommand cmd = new MySqlCommand(query, conn.connection))
            {
                for (int i = 0; i < searchTerms.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@param" + i, "%" + searchTerms[i].Trim() + "%");

                }

                // Execute the query and handle the result (execute, reader, etc.)
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dgvlist.Rows.Clear();
                    dgvlist.AllowUserToAddRows = true;
                    List<string> appTransactionNumbers = new List<string>();
                    List<string> status = new List<string>();

                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();
                        row.Cells[0].Value = reader["time"];
                        row.Cells[1].Value = reader["app_transaction_number"];
                        row.Cells[2].Value = reader["state"];

                        rowcount++;
                        appTransactionNumbers.Add(reader["app_transaction_number"].ToString());
                        status.Add(reader["state"].ToString());
                        dgvlist.Rows.Add(row);
                    }

                    dgvlist.AllowUserToAddRows = false;

                    for (int i = 0; i < appTransactionNumbers.Count; i++)
                    {
                        // Check for duplicates
                        bool isDuplicate = appTransactionNumbers.Count(item => item == appTransactionNumbers[i]) > 1;
                        //Console.WriteLine(appTransactionNumbers[i]);
                        if (isDuplicate)
                        {
                            dgvlist.Rows[i].Cells[3].Value = "Not Subject for Refund";
                        }
                        else
                        {
                            if (status[i] == ("PAYMENT_SUCCESS"))
                            {
                                dgvlist.Rows[i].Cells[3].Value = "Failed - For Refund";
                            }
                            else
                            {
                                dgvlist.Rows[i].Cells[3].Value = "Not Subject for Refund";
                            }
                        }
                    }

                    if (rowcount == 0)
                    {
                        MessageBox.Show("Transaction not found.\r\nPlease email Noc for Status of this transaction", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }


            //int rowcount = 0;
            //cmd = new MySqlCommand("SELECT time, app_transaction_number,state FROM tbl_splunk WHERE tbl_splunk.app_transaction_number = '" + txtsearch.Text + "'", conn.connection);
            //Reader = cmd.ExecuteReader();
            //dgvlist.Rows.Clear();
            //dgvlist.AllowUserToAddRows = true;
            //if (Reader.HasRows)
            //{

            //    while (Reader.Read())
            //    {
            //        DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();
            //        row.Cells[0].Value = Reader["time"];
            //        row.Cells[1].Value = Reader["app_transaction_number"];
            //        row.Cells[2].Value = Reader["state"];
            //        dgvlist.Rows.Add(row);
            //        rowcount++;
            //    }

            //    if (rowcount == 2)
            //    {
            //        lbltag.Text = "Not Subject for Refund";
            //        dgvlist.Rows[0].Cells[3].Value = "Not Subject for Refund";
            //        dgvlist.Rows[1].Cells[3].Value = "Not Subject for Refund";

            //        txtspiel.Text = ("As per checking the App Transaction Number, Status is (" + cmbdbstats.Text + " ) that the equivalent status in Splunk File is (" + dgvlist.Rows[0].Cells[2].Value + " and " + dgvlist.Rows[1].Cells[2].Value + " " + txtnocstats.Text + ")that the Subscriber is NOT subjected for Refund since transaction was not succesfull process from ELP side." +
            //  "\r\n" +
            //  "\r\nMIN: " +
            //  "\r\nTime Stamp: " + Reader["time"] + "" +
            //  "\r\nAvailed Load: " +
            //  "\r\nApp Transaction Number: " + txtsearch.Text + "" +
            //  "\r\nPayment Method: Paymaya");
            //    }

            //    else if  (Reader["state"].ToString() == "PAYMENT_SUCCESS")
            //    {
            //        lbltag.Text = "Failed - For Refund";
            //        dgvlist.Rows[0].Cells[3].Value = "Failed - For Refund";

            //        txtspiel.Text = ("As per checking the App Transaction Number, Status is (" + cmbdbstats.Text + " ) that the equivalent status in Splunk File is (" + dgvlist.Rows[0].Cells[2].Value + ")that the Subscriber is NOT subjected for Refund since transaction was not succesfull process from ELP side." +
            //  "\r\n" +
            //  "\r\nMIN: " +
            //  "\r\nTime Stamp: " + Reader["time"] + "" +
            //  "\r\nAvailed Load: " +
            //  "\r\nApp Transaction Number: " + txtsearch.Text + "" +
            //  "\r\nPayment Method: Paymaya");
            //    }
            //    else
            //    {
            //        lbltag.Text = "Not Subject for Refund";
            //        dgvlist.Rows[0].Cells[3].Value = "Not Subject for Refund";

            //        txtspiel.Text = ("As per checking the App Transaction Number, Status is (" + cmbdbstats.Text + " ) that the equivalent status in Splunk File is (" + dgvlist.Rows[0].Cells[2].Value + " and " + dgvlist.Rows[1].Cells[2].Value + " " + txtnocstats.Text + ")that the Subscriber is NOT subjected for Refund since transaction was not succesfull process from ELP side." +
            //  "\r\n" +
            //  "\r\nMIN: " +
            //  "\r\nTime Stamp: " + Reader["time"] + "" +
            //  "\r\nAvailed Load: " +
            //  "\r\nApp Transaction Number: " + txtsearch.Text + "" +
            //  "\r\nPayment Method: Paymaya");
            //    }

            //    dgvlist.AllowUserToAddRows = false;
            //}
            //else
            //{
            //    MessageBox.Show("Transaction not found " +
            //        "\r\n" +
            //        "Please email Noc for Status of this transaction", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            



            Reader.Close();
        }
        public void LOAD_FILE()
        {
            cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_file where file_name like 'ACQUIRING%' order by id desc;", conn.connection);
            Reader = cmd.ExecuteReader();
            dgvfile.Rows.Clear();
            dgvfile.AllowUserToAddRows = true;
            while (Reader.Read())
            {
                DataGridViewRow row = (DataGridViewRow)dgvfile.Rows[0].Clone();
                row.Cells[0].Value = Reader["file_name"];
                row.Cells[1].Value = Reader["user"];
                row.Cells[2].Value = Reader["created_at"];
                dgvfile.Rows.Add(row);
            }
            Reader.Close();
        }
        private void Frm_Splunk_Load(object sender, EventArgs e)
        {
            LOAD_FILE();


        }

        private void dgvfile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
 }

