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
using CsvHelper;
using System.Globalization;
using System.IO;
using CsvHelper.Configuration;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;

namespace L2_GLA
{
    public partial class Frm_variance : Form
    {
        MySqlCommand cmd;
        DBconnect conn = new DBconnect();
        MySqlDataReader Reader;
        List<string> appTransactionNumbers = new List<string>();

        public Frm_variance()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
        }

    private string RemoveSingleQuotes (string input)
        {
            if(input != null)
            {
                return input.Replace("'", string.Empty);

            }
            return input;
        }
        private void btnopenfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                String filename = Path.GetFileName(selectedFilePath);
                //lbltag.Text = filename;

                try
                {

                   

                    int csvcount = 0;


                        // Read and parse the CSV file using CsvHelper.
                        using (var reader = new StreamReader(selectedFilePath))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        // using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                        {
                            var records = csv.GetRecords<merchant>().ToList();

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
                                cmd = new MySqlCommand("INSERT INTO `Tbl_merchant` (`SETTLEMENT_TXN_ID`, `MERCHANT_ID`, `MERCHANT_NAME`, `SETTLE_DATE`, `MERCHANT_TRANS_ID`, `ACQUIREMENT_ID`, `TRANSACTION_TYPE`, `TRANSACTION_DATETIME`, `MERCHANT_REFUND_REQUEST_ID`, `REFUND_ID`, `TRANSACTION_AMOUNT`, `NET_MDR`, `SETTLE_AMOUNT`) VALUES" +
                                    " (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9, @Value10, @Value11, @Value12, @Value13)", conn.connection);

                                // Add parameters to the command.
                                cmd.Parameters.AddWithValue("@Value1", record.SETTLEMENT_TXN_ID);
                                cmd.Parameters.AddWithValue("@Value2", record.MERCHANT_ID);
                                cmd.Parameters.AddWithValue("@Value3", record.MERCHANT_NAME);
                                cmd.Parameters.AddWithValue("@Value4", record.SETTLE_DATE);
                                cmd.Parameters.AddWithValue("@Value5", record.MERCHANT_TRANS_ID);
                                cmd.Parameters.AddWithValue("@Value6", record.ACQUIREMENT_ID);
                                cmd.Parameters.AddWithValue("@Value7", record.TRANSACTION_TYPE);
                                cmd.Parameters.AddWithValue("@Value8", record.TRANSACTION_DATETIME);
                                cmd.Parameters.AddWithValue("@Value9", record.MERCHANT_REFUND_REQUEST_ID);
                                cmd.Parameters.AddWithValue("@Value10", record.REFUND_ID);
                                cmd.Parameters.AddWithValue("@Value11", record.TRANSACTION_AMOUNT);
                                cmd.Parameters.AddWithValue("@Value12", record.NET_MDR);
                                cmd.Parameters.AddWithValue("@Value13", record.SETTLE_AMOUNT);
                                // Add parameters for each column in your table.

                                // Execute the SQL command.
                                cmd.ExecuteNonQuery();
                                //progressBar1.Value++;


                            csvcount++;
                         
                        }
                        }

                   
                    

                    MessageBox.Show("Total of " + csvcount + " Merchant Settlement record has been save");
                    csvcount = 0;
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
           // int loop = 0;
            string searchTerm = txtsearch.Text;
            string[] searchTerms = searchTerm.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Assuming connection is a MySqlConnection object already created and opened

            if(txtsearch.Text == "" || txtsearch.Text is null)
            {
                return;
            }

            string query = "SELECT TRANSACTION_DATETIME, MERCHANT_TRANS_ID, TRANSACTION_TYPE, MERCHANT_REFUND_REQUEST_ID,REFUND_ID,TRANSACTION_AMOUNT  FROM tbl_merchant WHERE ";

            for (int i = 0; i < searchTerms.Length; i++)
            {
                query += "MERCHANT_TRANS_ID LIKE @param" + i;
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
                        row.Cells[0].Value = reader["TRANSACTION_DATETIME"];
                        row.Cells[1].Value = reader["MERCHANT_TRANS_ID"];
                        row.Cells[2].Value = reader["TRANSACTION_TYPE"];
                        row.Cells[3].Value = reader["MERCHANT_REFUND_REQUEST_ID"];
                        row.Cells[4].Value = reader["REFUND_ID"];
                        row.Cells[5].Value = reader["TRANSACTION_AMOUNT"];

                        rowcount++;
                        appTransactionNumbers.Add(reader["MERCHANT_TRANS_ID"].ToString());
                        status.Add(reader["TRANSACTION_TYPE"].ToString());
                        dgvlist.Rows.Add(row);
                    }

                    dgvlist.AllowUserToAddRows = false;

                    for (int i = 0; i < appTransactionNumbers.Count; i++)
                    {
                        // Check for duplicates
                        bool isDuplicate = appTransactionNumbers.Count(item => item == appTransactionNumbers[i]) > 1;

                        if (isDuplicate)
                        {
                            if (status[i] == "REFUND")
                            {
                                dgvlist.Rows[i].Cells[6].Value = "Not Subject for Refund - GCash has already processed the refund with the amount of " + reader["TRANSACTION_AMOUNT"].ToString();
                            }
                            else
                            {
                                dgvlist.Rows[i].Cells[6].Value = "Not Subject for Refund";
                            }
                        }
                        else
                        {
                            if (status[i] == "PAYMENT")
                            {
                                dgvlist.Rows[i].Cells[6].Value = "Failed - For Refund";
                            }
                            else
                            {
                                dgvlist.Rows[i].Cells[6].Value = "Not Subject for Refund";
                            }
                        }
                    }

                    if (rowcount == 0)
                    {
                        MessageBox.Show("Transaction not found.\r\nPlease email PayOnline for the status of this transaction", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }



        }
        public void LOAD_FILE()
        {
            cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_file where file_name like 'MERCHANT%' order by id desc", conn.connection);
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

        private void Frm_variance_Load(object sender, EventArgs e)
        {
            LOAD_FILE();
        }
    }
}



