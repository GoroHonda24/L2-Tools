using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using MySql.Data.MySqlClient;

namespace L2_GLA
{
    public partial class Frm_export : Form
    {
        MySqlCommand cmd;
        DBconnect conn = new DBconnect();
        MySqlDataReader reader;
        public Frm_export()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DateTime dtpfrom1 = dtpfrom.Value.Date;
            DateTime unixEpoch = new DateTime(1970, 1, 1);
            TimeSpan timeSpan = dtpfrom1 - unixEpoch;

            DateTime dtpto1 = dtpto.Value;
            DateTime unixEpoch1 = new DateTime(1970, 1, 1);
            TimeSpan timeSpan1 = dtpto1 - unixEpoch1;

            long from = (long)timeSpan.TotalSeconds;
            long to = (long)timeSpan1.TotalSeconds;

            cmd = new MySqlCommand("select * from min_logs where timestamp >= '" + from + "' and timestamp <= '" + to + "'", conn.connection);
            reader = cmd.ExecuteReader();

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string dateTime = date + " 00:00:00";

            string fileName = "Min_Logs_Report_" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv"; // Set default file name
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            Directory.CreateDirectory(folderPath);

            // Save the CSV file with the full file path
            string filePath = Path.Combine(folderPath, fileName);

            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            using (var csvWriter = new CsvHelper.CsvWriter(streamWriter, System.Globalization.CultureInfo.InvariantCulture))
            {
                string[] fields = { "id", "user", "min", "timestamp", "old_brand", "new_brand", "action"};

                csvWriter.WriteField(fields);
                csvWriter.NextRecord();

                while (reader.Read())
                {
                    string[] lineData = { reader["id"].ToString(), reader["user"].ToString(), reader["min"].ToString(), reader["timestamp"].ToString(), reader["old_brand"].ToString(), reader["new_brand"].ToString(), reader["action"].ToString() };

                    csvWriter.WriteField(lineData);
                    csvWriter.NextRecord();
                }

                streamWriter.Flush();
                memoryStream.Position = 0;

                File.WriteAllBytes(filePath, memoryStream.ToArray());

                MessageBox.Show("CSV file saved successfully with filename: " + fileName);

                // Open the folder where the file is saved
              //  Process.Start("explorer.exe", "/select, \"" + filePath + "\"");
            }
            reader.Close();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnopenfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                try
                {
                        // Read and parse the CSV file using CsvHelper.
                        using (var reader = new StreamReader(selectedFilePath))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            var records = csv.GetRecords<merchant>(); // Replace YourDataClass with your custom class to map CSV columns.

                            // Loop through the records and insert them into the database.
                            foreach (var record in records)
                            {
                                // Adjust the SQL INSERT statement to match your table structure.
                                cmd = new MySqlCommand("INSERT INTO `Tbl_merchant` (`SETTLEMENT_TXN_ID`, `MERCHANT_ID`, `MERCHANT_NAME`, `SETTLE_DATE`, `MERCHANT_TRANS_ID`, `ACQUIREMENT_ID`, `TRANSACTION_TYPE`, `TRANSACTION_DATETIME`, `MERCHANT_REFUND_REQUEST_ID`, `REFUND_ID`, `TRANSACTION_AMOUNT`, `NET_MDR`, `SETTLE_AMOUNT`) VALUES" +
                                    " (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9, @Value10, @Value11, @Value12, @Value13)",conn.connection);

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
                            }
                        }
                    

                    MessageBox.Show("CSV data uploaded successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
