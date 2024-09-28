using CsvHelper;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace L2_GLA.NewForms
{
    public partial class AppTransChecking : Form
    {

        private readonly DBconnect conn = new DBconnect();

        public AppTransChecking()
        {
            if(conn.connection.State != ConnectionState.Open) conn.connection.Open();  
            InitializeComponent();
          
        }

        private async void BtnOpenFile_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog { Filter = "CSV Files (*.csv)|*.csv" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    string filename = Path.GetFileName(selectedFilePath);

                    try
                    {
                        List<merchant> records;
                        using (var reader = new StreamReader(selectedFilePath))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                           records = csv.GetRecords<merchant>().ToList();
                        }

                        if (await FileExisting(filename))
                        {
                            MessageBox.Show($"File '{filename}' already exists.");
                            return;
                        }

                        // int csvCount = await InsertMerchantRecordsAsync(records);
                        await UploadFile(selectedFilePath);
                        await InsertFileEntryAsync(filename);
                       // MessageBox.Show($"Total of {csvCount} Merchant Settlement records have been saved.");                        
                        await LOAD_FILE();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
            }
        }

        private async Task<bool> FileExisting(string FileName)
        {
            using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM tbl_file WHERE file_name = @fileName", conn.connection))
            {
                cmd.Parameters.AddWithValue("@fileName", FileName);
                return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
            }
        }

        private async Task InsertFileEntryAsync(string filename)
        {
            Console.WriteLine($"saving file name");
            using (var cmd = new MySqlCommand("INSERT INTO tbl_file (`file_name`, `user`, `created_at`) VALUES (@fileName, @user, @createdAt)", conn.connection))
            {
                cmd.Parameters.AddWithValue("@fileName", filename);
                cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                cmd.Parameters.AddWithValue("@createdAt", DateTime.Now);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        private async Task<string> UploadFile(string selectedFilePath)
        {
            try
            {
                // Assuming 'conn.connection' is your active MySQL connection object
                using (MySqlCommand cmd = new MySqlCommand($"LOAD DATA INFILE '{selectedFilePath.Replace("\\", "/")}' INTO TABLE tbl_merchant FIELDS TERMINATED BY ',' ENCLOSED BY '\"' LINES TERMINATED BY '\n' IGNORE 1 LINES" +
                    $"(SETTLEMENT_TXN_ID,MERCHANT_ID, MERCHANT_NAME, SETTLE_DATE, MERCHANT_TRANS_ID, ACQUIREMENT_ID, TRANSACTION_TYPE, TRANSACTION_DATETIME, MERCHANT_REFUND_REQUEST_ID, REFUND_ID, TRANSACTION_AMOUNT, NET_MDR, SETTLE_AMOUNT, WITHHOLDING_TAX);", conn.connection))
                {
                    await cmd.ExecuteNonQueryAsync();

                    Console.WriteLine($"Upload file: {selectedFilePath}");
                    return "Data uploaded successfully.";
                    
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return $"Error: {ex.Message}";
            }
        }

        public async Task LOAD_FILE()
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_file where file_name like 'MERCHANT%' order by id desc", conn.connection))
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dgvfile.Rows.Clear();
                    dgvfile.AllowUserToAddRows = true;
                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvfile.Rows[0].Clone();
                        row.Cells[0].Value = reader["file_name"];
                        row.Cells[1].Value = reader["user"];
                        row.Cells[2].Value = reader["created_at"];
                        dgvfile.Rows.Add(row);
                    }
                    reader.Close();
                }
            }
          
            
        }

        private async void AppTransChecking_Load(object sender, EventArgs e)
        {
            await LOAD_FILE();  
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Run both search operations asynchronously
            await merchantSearch();
            await appSearch();
        }

        private async Task merchantSearch()
        {
            string searchTerm = txtsearch.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Please enter a search term.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Split the search terms by commas
            string[] searchTerms = searchTerm.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Construct the query using StringBuilder for efficiency
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("SELECT TRANSACTION_DATETIME, MERCHANT_TRANS_ID, TRANSACTION_TYPE, MERCHANT_REFUND_REQUEST_ID, REFUND_ID, TRANSACTION_AMOUNT ");
            queryBuilder.Append("FROM tbl_merchant WHERE ");

            for (int i = 0; i < searchTerms.Length; i++)
            {
                queryBuilder.Append("MERCHANT_TRANS_ID LIKE @param" + i);
                if (i < searchTerms.Length - 1)
                    queryBuilder.Append(" OR ");
            }

            string query = queryBuilder.ToString();

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn.connection))
                {
                    // Add parameters dynamically based on the search terms
                    for (int i = 0; i < searchTerms.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@param" + i, "%" + searchTerms[i].Trim() + "%");
                    }

                    // Execute the query asynchronously and process the result
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        dgvlist.Rows.Clear();
                        dgvlist.AllowUserToAddRows = true;

                        Dictionary<string, string> transactionStatusMap = new Dictionary<string, string>();
                        List<string> appTransactionNumbers = new List<string>();

                        while (await reader.ReadAsync())
                        {
                            // Populate DataGridView with results
                            DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();
                            row.Cells[0].Value = reader["TRANSACTION_DATETIME"];
                            row.Cells[1].Value = reader["MERCHANT_TRANS_ID"];
                            row.Cells[2].Value = reader["TRANSACTION_TYPE"];
                            row.Cells[3].Value = reader["MERCHANT_REFUND_REQUEST_ID"];
                            row.Cells[4].Value = reader["REFUND_ID"];
                            row.Cells[5].Value = reader["TRANSACTION_AMOUNT"];

                            appTransactionNumbers.Add(reader["MERCHANT_TRANS_ID"].ToString());
                            transactionStatusMap[reader["MERCHANT_TRANS_ID"].ToString()] = reader["TRANSACTION_TYPE"].ToString();

                            dgvlist.Rows.Add(row);
                        }

                        dgvlist.AllowUserToAddRows = false;

                        // Handle refund logic and duplicates
                        ProcessDuplicates(appTransactionNumbers, transactionStatusMap);

                        if (appTransactionNumbers.Count == 0)
                        {
                            MessageBox.Show("Transaction not found.\r\nPlease email PayOnline for the status of this transaction", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Handle potential database errors
                MessageBox.Show($"Database Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task appSearch()
        {
            string searchTerm = txtsearch.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Please enter a search term.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Split the search terms by commas
            string[] searchTerms = searchTerm.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Construct the search query
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT a.app_transaction, a.iload, a.remarks, a.file_id, f.file_name, f.end_at
                          FROM (
                                SELECT app_transaction, iload, remarks, file_id
                                FROM tbl_variance_maya
                                WHERE ");

            // Dynamically add search terms for tbl_variance_maya
            for (int i = 0; i < searchTerms.Length; i++)
            {
                queryBuilder.Append("app_transaction LIKE @mayaParam" + i);
                if (i < searchTerms.Length - 1)
                    queryBuilder.Append(" OR ");
            }

            // Append UNION ALL for tbl_variance_gcash
            queryBuilder.Append(@" UNION ALL
                          SELECT app_transaction, iload, remarks, file_id
                          FROM tbl_variance_gcash
                          WHERE ");

            // Dynamically add search terms for tbl_variance_gcash
            for (int i = 0; i < searchTerms.Length; i++)
            {
                queryBuilder.Append("app_transaction LIKE @gcashParam" + i);
                if (i < searchTerms.Length - 1)
                    queryBuilder.Append(" OR ");
            }

            // Final join with tbl_variance_file
            queryBuilder.Append(@") a
                          INNER JOIN tbl_variance_file f ON a.file_id = f.file_id");

            string query = queryBuilder.ToString();

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn.connection))
                {
                    // Add parameters dynamically based on the search terms
                    for (int i = 0; i < searchTerms.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@mayaParam" + i, "%" + searchTerms[i].Trim() + "%");
                        cmd.Parameters.AddWithValue("@gcashParam" + i, "%" + searchTerms[i].Trim() + "%");
                    }

                    // Execute the query and process the result
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        DgvVariance.Rows.Clear();
                        DgvVariance.AllowUserToAddRows = true;

                        // Create a HashSet to track unique app_transactions
                        HashSet<string> uniqueAppTransactions = new HashSet<string>();

                        while (await reader.ReadAsync())
                        {
                            string appTransaction = reader["app_transaction"].ToString();

                            // Only add rows if the app_transaction is unique
                            if (!uniqueAppTransactions.Contains(appTransaction))
                            {
                                uniqueAppTransactions.Add(appTransaction);

                                // Populate DataGridView with unique results
                                DataGridViewRow row = (DataGridViewRow)DgvVariance.Rows[0].Clone();
                                row.Cells[0].Value = reader["app_transaction"];
                                row.Cells[1].Value = reader["remarks"];
                                row.Cells[2].Value = reader["file_name"];
                                row.Cells[3].Value = reader["end_at"];

                                DgvVariance.Rows.Add(row);
                            }
                        }

                        DgvVariance.AllowUserToAddRows = false;

                        // If no rows found, show a message
                        if (uniqueAppTransactions.Count == 0)
                        {
                            MessageBox.Show("No transactions found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessDuplicates(List<string> appTransactionNumbers, Dictionary<string, string> transactionStatusMap)
        {
            for (int i = 0; i < appTransactionNumbers.Count; i++)
            {
                string currentTransactionId = appTransactionNumbers[i];
                bool isDuplicate = appTransactionNumbers.Count(t => t == currentTransactionId) > 1;
                string transactionType = transactionStatusMap[currentTransactionId];

                if (isDuplicate)
                {
                    dgvlist.Rows[i].Cells[6].Value = transactionType == "REFUND"
                        ? $"Not Subject for Refund - GCash has already processed the refund with the amount of {dgvlist.Rows[i].Cells[5].Value}"
                        : "Not Subject for Refund";
                }
                else
                {
                    dgvlist.Rows[i].Cells[6].Value = transactionType == "PAYMENT"
                        ? "Failed - For Refund"
                        : "Not Subject for Refund";
                }
            }
        }

    }
}
