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
using CsvHelper.Configuration;
using System.IO;
using CsvHelper;
using System.Globalization;
using OfficeOpenXml;
using Oracle.ManagedDataAccess.Client;

namespace L2_GLA
{
    public partial class Frm_Gcash : Form
    {
        MySqlCommand cmd;
        MySqlCommand cmd1;
        DBconnect conn = new DBconnect();
        MySqlDataReader Reader;

        List<string> appTransactionNumbers = new List<string>();
        List<string> merchant = new List<string>();
        List<string> ELPTransactionNumbers = new List<string>();
        List<string> DBStatus = new List<string>();
        List<string> SplunkStatus = new List<string>();
        List<string> SplunkStatus1 = new List<string>();
        int file_id = 0;
      
        public Frm_Gcash()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
  

        }

        private void button1_Click(object sender, EventArgs e)
        {

            var date = new List<string>();
            var app = new List<string>();
            var elp = new List<string>();
            string file_name = "";
            int file_id = 0;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                file_name = Path.GetFileNameWithoutExtension(openFileDialog.SafeFileName);

                using (MySqlCommand sql = new MySqlCommand("SELECT id, File_name, Assigned_to, created_at, Status FROM brand_synch_2.tbl_variance_file WHERE File_name = @filename", conn.connection))
                {
                    sql.Parameters.AddWithValue("@filename", file_name);

                    using (MySqlDataReader reader = sql.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MessageBox.Show(reader["File_name"].ToString() + " is already " + reader["Status"].ToString() + " by " + reader["Assigned_to"].ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //file_name = ""; // Not sure why you're resetting file_name here, but leaving it as is.
                            return;
                        }
                        else
                        {
                            // No matching record found, proceed with inserting new record
                            reader.Close(); // Close the reader before reusing it
                                            // Now, you can access the id since there are no rows matching the condition
                            sql.CommandText = "SELECT MAX(id) AS max_id FROM brand_synch_2.tbl_variance_file";
                            using (MySqlDataReader datareader = sql.ExecuteReader())
                            {
                                if (datareader.Read() && !datareader.IsDBNull(datareader.GetOrdinal("max_id")))
                                {
                                    file_id = datareader.GetInt32("max_id") + 1; // Increment the max id by 1
                                    
                                }
                                else
                                {
                                    file_id = 1; // If no existing records, set file_id to 1
                                    
                                }
                                datareader.Close(); // Close the reader after getting the id
                            }
                            
                        }
                    }

                    using (MySqlCommand cmd2 = new MySqlCommand("INSERT INTO `brand_synch_2`.`tbl_variance_file`(`file_id`,`File_name`,`Var_type`,`Assigned_to`,`Status`,`created_at`)" +
                        "VALUES(@fileID,@file_name, @vartype, @assigned, @status, @created)", conn.connection))
                    {

                        cmd2.Parameters.AddWithValue("@file_name", file_name);
                        cmd2.Parameters.AddWithValue("@fileID", selectedFilePath);
                        cmd2.Parameters.AddWithValue("@vartype", "gcash");
                        cmd2.Parameters.AddWithValue("@assigned", GlobalVar.user);
                        cmd2.Parameters.AddWithValue("@status", "Ongoing");
                        cmd2.Parameters.AddWithValue("@created", DateTime.Now);
                        cmd2.ExecuteNonQuery();
                    }
                }


                try
                {
                    

                    string[] csvLines = File.ReadAllLines(selectedFilePath);

                    // Process CSV data
                    for (int i = 2; i < csvLines.Length; i++)
                    {
                        string[] rowData = csvLines[i].Split(',');

                        date.Add(rowData[0]);
                        app.Add(rowData[1]);
                        elp.Add(rowData[2]);
                    }
                    int c = 0;
                    for (int j = 0; j < date.Count; j++)
                    {
                        c++;
                        cmd = new MySqlCommand("INSERT INTO `brand_synch_2`.`gcash` (`count`,`Transaction date`, `MERCHANT_TRANS_ID`, `ELP_Reference_Number`,`user`,`file_id`) " +
                        "VALUES(@count, @Date, @MerchantTransID, @ELPReference, @user, @file_id)", conn.connection);
                        cmd.Parameters.AddWithValue("@count", c);
                        cmd.Parameters.AddWithValue("@Date", date[j]);
                        cmd.Parameters.AddWithValue("@MerchantTransID", app[j]);
                        cmd.Parameters.AddWithValue("@ELPReference", elp[j]);
                        cmd.Parameters.AddWithValue("@user",GlobalVar.user);
                        cmd.Parameters.AddWithValue("@file_id", file_id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Investigation uploaded successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

            }
            display_data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("SELECT mydb.App_transaction_number, gcash.ELP_Reference_Number FROM mydb INNER JOIN gcash ON mydb.Elp_transaction_number = gcash.ELP_Reference_Number where gcash.user = @user and file_id = @fileID", conn.connection);
            cmd.Parameters.AddWithValue("@user", GlobalVar.user);
            cmd.Parameters.AddWithValue("@fileID", GlobalVar.gfile_id);
            Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    appTransactionNumbers.Add(Reader["App_transaction_number"].ToString());
                    ELPTransactionNumbers.Add(Reader["ELP_Reference_Number"].ToString());
                }

                Reader.Close();

            // Process the updates using the values from the list
            for (int i = 0; i < appTransactionNumbers.Count; i++)
            {
                if (appTransactionNumbers[i] == "NUll" || appTransactionNumbers[i] == "")
                {
                    cmd = new MySqlCommand("UPDATE gcash SET MERCHANT_TRANS_ID = 'Not Found in DB' WHERE ELP_Reference_Number = '" + ELPTransactionNumbers[i] + "'", conn.connection);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new MySqlCommand("UPDATE gcash SET MERCHANT_TRANS_ID = '" + appTransactionNumbers[i] + "' WHERE ELP_Reference_Number = '" + ELPTransactionNumbers[i] + "'", conn.connection);
                    cmd.ExecuteNonQuery();
                }
            }

                appTransactionNumbers.Clear();
                ELPTransactionNumbers.Clear();

            MessageBox.Show("successfully.");
            display_data();
            btnapp.Enabled = false;
        }
        public void load_data()
        {
            cmd = new MySqlCommand("Select id,count,MERCHANT_TRANS_ID,ELP_Reference_Number from gcash where user = @user and file_id = @fileID", conn.connection);
            cmd.Parameters.AddWithValue("@user", GlobalVar.user);
            cmd.Parameters.AddWithValue("@fileID", GlobalVar.gfile_id);
            Reader = cmd.ExecuteReader();
            dgvlist.Rows.Clear();
            dgvlist.AllowUserToAddRows = true;
            while (Reader.Read())
            {
                DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();
                row.Cells[0].Value = Reader["count"];
                row.Cells[2].Value = "'" + Reader["MERCHANT_TRANS_ID"] + "',";
                row.Cells[3].Value = "'" + Reader["ELP_Reference_Number"] + "',";
                dgvlist.Rows.Add(row);
            }
            Reader.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            load_data();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (cmd = new MySqlCommand("TRUNCATE TABLE mydb", conn.connection))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Table successfully truncated.");
                    }
                    else
                    {
                        MessageBox.Show("Truncation operation did not affect any rows.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error truncating table: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void Frm_Gcash_Load(object sender, EventArgs e)
        {
            display_data();
            dateTimePicker1.Value = DateTime.Now;
            dtpto.Value = DateTime.Now;

            
        }

        private void btnopenfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            int csvcount = 0;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                try
                {
                    // Read and parse the CSV file using CsvHelper.
                    using (var reader = new StreamReader(selectedFilePath))

                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {

                        var records = csv.GetRecords<mydb>().ToList(); // Replace YourDataClass with your custom class to map CSV columns.
                                                                       //  csv.Configuration.IgnoreHeaderWhiteSpace = true;
                        //progressBar1.Visible = true;
                        //lblprog.Visible = true;                                  // Loop through the records and insert them into the database.
                        //progressBar1.Maximum = records.Count();
                        foreach (var record in records)
                        {
                            // Adjust the SQL INSERT statement to match your table structure.
                            cmd = new MySqlCommand("INSERT INTO `myDb` (`App_transaction_number`, `Elp_transaction_number`,`Status`,`created_at`,`user`,`variance`)" +
                                " VALUES (@Value2, @Value3, @Value1, @Value4,@user, @var)", conn.connection);

                            // Add parameters to the command.
                            cmd.Parameters.AddWithValue("@Value1", record.status);
                            cmd.Parameters.AddWithValue("@Value2", record.app_transaction_number);
                            cmd.Parameters.AddWithValue("@Value3", record.elp_transaction_number);
                            cmd.Parameters.AddWithValue("@Value4", record.created_at);
                            cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                            cmd.Parameters.AddWithValue("@var", "gcash");

                            cmd.ExecuteNonQuery();
                           // progressBar1.Value++;

                            csvcount++;
                            //lblprog.Text = csvcount.ToString() + " - " + records.Count().ToString();
                            //lblprog.Refresh(); // or lblprog.Invalidate();
                        }
                    }


                    MessageBox.Show("CSV data uploaded successfully.");
                    csvcount = 0;
                    display_data();
                   // progressBar1.Value = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //----------------ELP REFERENCE-------------------------------------------

            cmd = new MySqlCommand("SELECT  mydb.App_transaction_number,mydb.Elp_transaction_number FROM mydb inner join  gcash ON mydb.App_transaction_number = gcash.MERCHANT_TRANS_ID where gcash.user = @user and file_id = @fileID", conn.connection);
            cmd.Parameters.AddWithValue("@user", GlobalVar.user);
            cmd.Parameters.AddWithValue("@fileID", GlobalVar.gfile_id);
            Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                appTransactionNumbers.Add(Reader["App_transaction_number"].ToString());
                ELPTransactionNumbers.Add(Reader["Elp_transaction_number"].ToString());
            }

            Reader.Close();

            // Process the updates using the values from the list
            for (int i = 0; i < appTransactionNumbers.Count; i++)
            {


                if(ELPTransactionNumbers[i] == "NULL" || ELPTransactionNumbers[i] == "")
                {
                    cmd = new MySqlCommand("UPDATE gcash SET ELP_Reference_Number  = 'Not Found in DB' WHERE MERCHANT_TRANS_ID= '" + appTransactionNumbers[i] + "'", conn.connection);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new MySqlCommand("UPDATE gcash SET ELP_Reference_Number  = '" + ELPTransactionNumbers[i] + "' WHERE MERCHANT_TRANS_ID= '" + appTransactionNumbers[i] + "'", conn.connection);
                    cmd.ExecuteNonQuery();
                }
                
            }

            appTransactionNumbers.Clear();
            ELPTransactionNumbers.Clear();
            MessageBox.Show("successfully.");
            display_data();
            btnalp.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ////--------------GET APP TO DB-------

            cmd = new MySqlCommand("SELECT  gcash.MERCHANT_TRANS_ID, mydb.Status FROM mydb inner join gcash ON gcash.MERCHANT_TRANS_ID = mydb.App_transaction_number where gcash.user = @user and file_id = @fileID", conn.connection);
            cmd.Parameters.AddWithValue("@user", GlobalVar.user);
            cmd.Parameters.AddWithValue("@fileID", GlobalVar.gfile_id);
            Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                appTransactionNumbers.Add(Reader["MERCHANT_TRANS_ID"].ToString());
                DBStatus.Add(Reader["Status"].ToString());
            }

            Reader.Close();

            // Process the updates using the values from the list

            for (int i = 0; i < appTransactionNumbers.Count; i++)
            {
                if (DBStatus[i] == "ELP_SUCCESSFUL")
                {
                    cmd1 = new MySqlCommand("UPDATE gcash SET Remarks  = '" + DBStatus[i] + "' WHERE MERCHANT_TRANS_ID= '" + appTransactionNumbers[i] + "'", conn.connection);
                    cmd1.ExecuteNonQuery();
                }
                cmd = new MySqlCommand("UPDATE gcash SET Status  = '" + DBStatus[i] + "' WHERE MERCHANT_TRANS_ID= '" + appTransactionNumbers[i] + "'", conn.connection);
                cmd.ExecuteNonQuery();
            }

            appTransactionNumbers.Clear();
            DBStatus.Clear();
            MessageBox.Show("successfully.");
            display_data();
            btndbstats.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string remarks="";

            cmd = new MySqlCommand("SELECT MERCHANT_TRANS_ID FROM gcash WHERE Status != 'ELP_SUCCESSFUL' and user = @user and file_id = @fileID", conn.connection);
            cmd.Parameters.AddWithValue("@user", GlobalVar.user);
            cmd.Parameters.AddWithValue("@fileID", GlobalVar.gfile_id);
            Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                appTransactionNumbers.Add(Reader["MERCHANT_TRANS_ID"].ToString());
                
            }
          
            Reader.Close();

            // Process the updates using the values from the list
            for (int i = 0; i < appTransactionNumbers.Count; i++)
            {
                SplunkStatus.Clear();
                merchant.Clear();

                // Fetch data from tbl_merchant
                cmd = new MySqlCommand("SELECT MERCHANT_TRANS_ID, TRANSACTION_TYPE FROM tbl_merchant WHERE MERCHANT_TRANS_ID like @transID AND TRANSACTION_DATETIME >= @date AND TRANSACTION_DATETIME <= @to", conn.connection);
                cmd.Parameters.AddWithValue("@transID", "%" + appTransactionNumbers[i]);

                cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00"));
                cmd.Parameters.AddWithValue("@to", dtpto.Value.ToString("yyyy-MM-dd 23:59:59"));

                Reader = cmd.ExecuteReader();

                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        merchant.Add(Reader["MERCHANT_TRANS_ID"].ToString());
                        SplunkStatus.Add(Reader["TRANSACTION_TYPE"].ToString());
                        SplunkStatus1.Add(Reader["TRANSACTION_TYPE"].ToString());
                    }

                    Reader.Close(); 


                    string updateQuery = "UPDATE gcash SET Splunk_Status = @SplunkStatus WHERE MERCHANT_TRANS_ID = @MerchantTransID";

                    using (MySqlCommand cmdUpdate = new MySqlCommand(updateQuery, conn.connection))
                    {
                        cmdUpdate.Parameters.AddWithValue("@SplunkStatus", string.Join(",", SplunkStatus));
                        cmdUpdate.Parameters.AddWithValue("@MerchantTransID", appTransactionNumbers[i]);

                        cmdUpdate.ExecuteNonQuery();
                    }
            

                    for (int n = 0; n < merchant.Count; n++)
                    {
                        bool isDuplicate = merchant.Count(item => item == merchant[n]) > 1;

                        if (isDuplicate)
                        {
                            SplunkStatus1.Clear();
                            SplunkStatus1.Add("Not Subject for Refund");
                        }
                        else if (SplunkStatus1.Contains("PAYMENT"))
                        {

                            SplunkStatus1.Clear();
                            SplunkStatus1.Add("Failed - For Refund");
                        }

                        string updateQuery1 = "UPDATE gcash SET Remarks = @Remarks WHERE MERCHANT_TRANS_ID = @MerchantTransID";

                        using (MySqlCommand cmdUpdate1 = new MySqlCommand(updateQuery1, conn.connection))
                        {
                            cmdUpdate1.Parameters.AddWithValue("@Remarks", SplunkStatus1.Count > 0 ? SplunkStatus1[0] : "");
                            cmdUpdate1.Parameters.AddWithValue("@SplunkStatus", SplunkStatus1.Count > 0 ? remarks : "");
                            cmdUpdate1.Parameters.AddWithValue("@MerchantTransID", appTransactionNumbers[i]);

                            cmdUpdate1.ExecuteNonQuery();
                        }
                    }
                }

                else
                {
                    Reader.Close();
                    cmd = new MySqlCommand("UPDATE gcash SET Splunk_Status = 'Not Found in Splunk file' WHERE MERCHANT_TRANS_ID = @MerchantTransID", conn.connection);
                    cmd.Parameters.AddWithValue("@MerchantTransID", appTransactionNumbers[i]);
                    cmd.ExecuteNonQuery();
                }

                Reader.Close();
            }

            Reader.Close();
            appTransactionNumbers.Clear();
            SplunkStatus.Clear();
            MessageBox.Show("successfully.");
            display_data();
            btnsplunk.Enabled = false;
        }
        //  string db_file_id = "";
        private List<Tuple<DataGridViewComboBoxCell, string>> comboBoxCellsToUpdate = new List<Tuple<DataGridViewComboBoxCell, string>>();
        public void display_data()
        {
            cmd = new MySqlCommand("SELECT gcash.*, tbl_variance_file.File_name from gcash join tbl_variance_file ON tbl_variance_file.id = gcash.file_id where tbl_variance_file.Status = 'Ongoing' and gcash.user = @user", conn.connection);
            cmd.Parameters.AddWithValue("@user", GlobalVar.user);
            Reader = cmd.ExecuteReader();

            dgvlist.Rows.Clear();
            dgvlist.AllowUserToAddRows = true;

            if (Reader.HasRows)
            {
                btniLoad.Enabled = false;
                while (Reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();

                    row.Cells[0].Value = Reader["count"];
                    row.Cells[1].Value = Reader["Transaction date"];                    
                    row.Cells[2].Value = Reader["MERCHANT_TRANS_ID"];                    
                    row.Cells[3].Value = Reader["ELP_Reference_Number"];                   
                    row.Cells[4].Value = Reader["Status"];
                    row.Cells[5].Value = Reader["Splunk_Status"];
                    row.Cells[6].Value = Reader["iload"];

                    if (!string.IsNullOrEmpty(Reader["Remarks"].ToString()))
                    {
                        row.Cells[7].Value = Reader["Remarks"];
                    }
                    else // If Remarks is null or empty, create a ComboBoxCell and add it to the list for future updates
                    {
                        DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
                        comboBoxCell.Items.Add("Not Subject for Refund");
                        comboBoxCell.Items.Add("Failed - For Refund");
                        comboBoxCell.Items.Add("ELP_SUCCESSFUL");
                        row.Cells[7] = comboBoxCell;
                        comboBoxCellsToUpdate.Add(new Tuple<DataGridViewComboBoxCell, string>(comboBoxCell, Reader["id"].ToString()));
                    }

                    dgvlist.Rows.Add(row);
                    

                    //   db_file_id = Reader["file_id"].ToString();
                    GlobalVar.gfile_name = Reader["file_name"].ToString();
                    GlobalVar.gfile_id = Reader["file_id"].ToString();
                    lblname.Text = Reader["File_name"].ToString();
                }
                
                dgvlist.AllowUserToAddRows = false;
            }
            else
            {
                btniLoad.Enabled = true;
            }
                
            
            Reader.Close();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            using (cmd = new MySqlCommand("Delete from mydb where user = @user and variance = 'gcash'", conn.connection))
                {
                    cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                    cmd.ExecuteNonQuery();                  
                    MessageBox.Show("Table successfully truncated.");                   
                }
            display_data();
            }

        private void ComboBoxCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected ComboBox cell and its corresponding database ID
            DataGridViewComboBoxCell comboBox = sender as DataGridViewComboBoxCell;
            string databaseID = comboBoxCellsToUpdate.Find(x => x.Item1 == comboBox)?.Item2;

            // If the ComboBox cell exists in the list, update its value
            if (!string.IsNullOrEmpty(databaseID))
            {
                comboBoxCellsToUpdate.Remove(comboBoxCellsToUpdate.Find(x => x.Item1 == comboBox));
                comboBoxCellsToUpdate.Add(new Tuple<DataGridViewComboBoxCell, string>(comboBox, databaseID));
            }
            else
            {
                comboBoxCellsToUpdate.Add(new Tuple<DataGridViewComboBoxCell, string>(comboBox, databaseID));
            }
        }

        public void view_list()
        {
            using (MySqlCommand sql_view = new MySqlCommand("SELECT gcash.*, tbl_variance_file.File_name FROM brand_synch_2.gcash join tbl_variance_file ON tbl_variance_file.id = gcash.file_id where gcash.file_id = @fileID", conn.connection))
            {
                Console.WriteLine(GlobalVar.gfile_id);
                sql_view.Parameters.AddWithValue("@fileID", GlobalVar.gfile_id);
                using (MySqlDataReader reader = sql_view.ExecuteReader())
                {
                    dgvlist.Rows.Clear();
                    dgvlist.AllowUserToAddRows = true;
                    DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();

                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();

                        //row.Cells[0].Value = Reader["id"];
                        row.Cells[0].Value = reader["count"];
                        row.Cells[1].Value = reader["Transaction date"];
                        row.Cells[2].Value = reader["MERCHANT_TRANS_ID"];
                        row.Cells[3].Value = reader["ELP_Reference_Number"];
                        row.Cells[4].Value = reader["Status"];
                        row.Cells[5].Value = reader["Splunk_Status"];
                        row.Cells[6].Value = reader["iload"];

                        if (reader["Remarks"].ToString() is null || string.IsNullOrEmpty(reader["Remarks"].ToString()))
                        {
                            
                            comboBoxCell.Items.Add("Not Subject for Refund");
                            comboBoxCell.Items.Add("Failed - For Refund");
                            comboBoxCell.Items.Add("ELP_SUCCESSFUL");
                            row.Cells[7] = comboBoxCell;

                            comboBoxCellsToUpdate.Add(new Tuple<DataGridViewComboBoxCell, string>(comboBoxCell, reader["id"].ToString()));
                            Console.WriteLine(comboBoxCellsToUpdate);
                        }
                        else
                        {
                            row.Cells[7].Value = reader["Remarks"];
                        }

                        dgvlist.Rows.Add(row);
                        lblname.Text = reader["File_name"].ToString();
                    }

                    dgvlist.AllowUserToAddRows = false;
                }
            }

        }

        
        private void btniLoad_Click(object sender, EventArgs e)
        {
            var date = new List<string>();
            var app = new List<string>();
            var elp = new List<string>();
            string file_name = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                file_name = Path.GetFileNameWithoutExtension(openFileDialog.SafeFileName);

                using (MySqlCommand sql = new MySqlCommand("SELECT id, File_name, Assigned_to, created_at, Status FROM brand_synch_2.tbl_variance_file WHERE File_name = @filename", conn.connection))
                {
                    sql.Parameters.AddWithValue("@filename", file_name);

                    using (MySqlDataReader reader = sql.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                          DialogResult view_result =  MessageBox.Show(reader["File_name"].ToString() + " is already " + reader["Status"].ToString() + " by " + reader["Assigned_to"].ToString() + "\n\r" + "Do you want to Extract?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                           if(view_result == DialogResult.Yes)
                            {

                                GlobalVar.gfile_id = reader["id"].ToString();
                                using (MySqlCommand sql_update = new MySqlCommand("UPDATE `brand_synch_2`.`tbl_variance_file` SET `File_id` = @newfileID WHERE `id` = @id", conn.connection))
                                {
                                    sql_update.Parameters.AddWithValue("@newfileID", selectedFilePath);
                                    sql_update.Parameters.AddWithValue("@id", reader["id"].ToString());
                                    reader.Close();
                                    sql_update.ExecuteNonQuery();
                                }
                                view_list();
                                return;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            // No matching record found, proceed with inserting new record
                            reader.Close(); // Close the reader before reusing it
                                            // Now, you can access the id since there are no rows matching the condition
                            sql.CommandText = "SELECT MAX(id) AS max_id FROM brand_synch_2.tbl_variance_file";
                            using (MySqlDataReader datareader = sql.ExecuteReader())
                            {
                                if (datareader.Read() && !datareader.IsDBNull(datareader.GetOrdinal("max_id")))
                                {
                                    file_id = datareader.GetInt32("max_id") + 1; // Increment the max id by 1

                                }
                                else
                                {
                                    file_id = 1; // If no existing records, set file_id to 1

                                }
                                datareader.Close(); // Close the reader after getting the id
                            }

                        }
                    }
                
                    using (MySqlCommand cmd2 = new MySqlCommand("INSERT INTO `brand_synch_2`.`tbl_variance_file`(`file_id`,`File_name`,`Var_type`,`Assigned_to`,`Status`,`created_at`)" +
                        "VALUES(@fileID,@file_name, @vartype, @assigned, @status, @created)", conn.connection))
                    {
                        cmd2.Parameters.AddWithValue("@file_name", file_name);
                        cmd2.Parameters.AddWithValue("@fileID", selectedFilePath);
                        cmd2.Parameters.AddWithValue("@vartype", "gcash");
                        cmd2.Parameters.AddWithValue("@assigned", GlobalVar.user);
                        cmd2.Parameters.AddWithValue("@status", "Ongoing");
                        cmd2.Parameters.AddWithValue("@created", DateTime.Now);
                        cmd2.ExecuteNonQuery();
                    }
                }

                try
                {
                    FileInfo fileInfo = new FileInfo(selectedFilePath);
                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming your data is in the first worksheet

                        int rowCount = worksheet.Dimension.Rows;

                        // Process Excel data
                        for (int i = 3; i <= rowCount; i++)
                        {
                            date.Add(worksheet.Cells[i, 1].Value?.ToString()); // Assuming date is in the first column
                            app.Add(worksheet.Cells[i, 2].Value?.ToString()); // Assuming app is in the second column
                            elp.Add(worksheet.Cells[i, 3].Value?.ToString()); // Assuming elp is in the third column
                        }
                    }

                    int c = 0;
                    for (int j = 0; j < date.Count; j++)
                    {
                        using (MySqlCommand sql_select = new MySqlCommand("",conn.connection))
                        {

                        }
                            c++;
                        cmd = new MySqlCommand("INSERT INTO `brand_synch_2`.`gcash` (`count`,`Transaction date`, `MERCHANT_TRANS_ID`, `ELP_Reference_Number`,`user`,`file_id`) " +
                        "VALUES(@count, @Date, @MerchantTransID, @ELPReference, @user, @file_id)", conn.connection);
                        cmd.Parameters.AddWithValue("@count", c);
                        cmd.Parameters.AddWithValue("@Date", date[j]);
                        Console.WriteLine("ins App: " + app[j]);
                        if (app[j] == "" || app[j] == "NULL" || app[j] is null)
                        {
                            cmd.Parameters.AddWithValue("@MerchantTransID","Not Found in DB");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MerchantTransID", app[j]);
                        }
                        
                        cmd.Parameters.AddWithValue("@ELPReference", elp[j]);
                        cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                        cmd.Parameters.AddWithValue("@file_id", file_id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Investigation uploaded successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

            }
            display_data();

            btnalp.Enabled = true;
            btnapp.Enabled = true;
            btndbstats.Enabled = true;
            btnformat.Enabled = true;
            btnsplunk.Enabled = true;
            
            
        }
        string db_File_name = "";
        private void button4_Click_1(object sender, EventArgs e)
        {
           
            foreach (var comboBoxCellToUpdate in comboBoxCellsToUpdate)
            {
                if (comboBoxCellToUpdate.Item1 != null && comboBoxCellToUpdate.Item1.Value != null)
                {
                    string selectedValue = comboBoxCellToUpdate.Item1.Value.ToString();
                    string databaseID = comboBoxCellToUpdate.Item2;
                    using (MySqlCommand sql_update = new MySqlCommand("UPDATE `brand_synch_2`.`gcash` SET `Remarks` = @remarks  WHERE `id` = @id", conn.connection))
                    {
                        sql_update.Parameters.AddWithValue("@remarks", selectedValue);
                        sql_update.Parameters.AddWithValue("@id", databaseID);
                        sql_update.ExecuteNonQuery();
                       
                    }
                }
                else
                {
                   
                }

            }

            // Clear the list after updating the database
            comboBoxCellsToUpdate.Clear();


            // Retrieve the file path from the database
            string selectedFilePath = "";

            using (MySqlCommand cmdGetData = new MySqlCommand("SELECT File_id,file_name FROM brand_synch_2.tbl_variance_file where id = @file_id", conn.connection))
            {
                cmdGetData.Parameters.AddWithValue("@file_id", GlobalVar.gfile_id);
                using (MySqlDataReader reader = cmdGetData.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        selectedFilePath = reader["File_id"].ToString();
                        db_File_name = reader["file_name"].ToString();
                    }
                    
                }                 
            }

            // Check if file path is retrieved successfully
            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                // Open the Excel file using the retrieved file path
                FileInfo fileInfo = new FileInfo(selectedFilePath);

                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming your data is in the first worksheet

                    // Retrieve the updated data from the database
                    using (MySqlCommand cmdGetData = new MySqlCommand("SELECT * FROM brand_synch_2.gcash where file_id = @file_id", conn.connection))
                    {
                        cmdGetData.Parameters.AddWithValue("@file_id", GlobalVar.gfile_id);
                        using (MySqlDataReader reader = cmdGetData.ExecuteReader())
                        {
                            int row = 3; // Start inserting data from row 3
                            while (reader.Read())
                            {
                                // Write the updated data to specific cells
                                worksheet.Cells[row, 2].Value = reader["MERCHANT_TRANS_ID"].ToString();
                                worksheet.Cells[row, 3].Value = reader["ELP_Reference_Number"].ToString();
                                worksheet.Cells[row, 10].Value = reader["Remarks"].ToString();
                                row++;
                            }
                        }
                    }

                    // Save the changes to the Excel file
                    package.Save();
                    //if (!string.IsNullOrEmpty(db_File_name))
                    //{
                    //    fileInfo.MoveTo(db_File_name);
                    //}
                    MessageBox.Show("Exporting Sucessfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult upstats = MessageBox.Show("Do you want to Update the status into DONE?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (upstats == DialogResult.Yes)
                    {
                        using (MySqlCommand sql = new MySqlCommand("UPDATE `brand_synch_2`.`tbl_variance_file` SET `Status` = @status, `Done_by` = @doneby,`end_at` = @enddt WHERE `id` = @id", conn.connection))
                        {

                            sql.Parameters.AddWithValue("@status", "Done");
                            sql.Parameters.AddWithValue("@doneby", GlobalVar.user);
                            sql.Parameters.AddWithValue("@enddt", DateTime.Now);
                            sql.Parameters.AddWithValue("@id", GlobalVar.gfile_id);
                            sql.ExecuteNonQuery();
                            //  Console.WriteLine("2" + file_name);
                        }
                        display_data();
                        lblname.Text = GlobalVar.gfile_name;
                    }
                }
            }
            else
            {
                // Handle case where file path is not found
                MessageBox.Show("File path not found in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void updateStatus(object sender, Frm_Var_update_Status.UpdateEventArgs args)
        {
            display_data();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show("To process the New GCASH variance\n\r Please Update the Status of " + db_File_name, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Frm_Var_update_Status objfrm = new Frm_Var_update_Status(this, null);
                objfrm.UpdateEventHandler += updateStatus;
                objfrm.ShowDialog();
            }
            else
            {
              //  e.Cancel = true;
            }
            
        }
        List<string> Iload_elp = new List<string>();
       
        private void button2_Click_1(object sender, EventArgs e)
        {
            using (MySqlCommand sql_select = new MySqlCommand("select ELP_Reference_Number from gcash where Remarks is null and file_id = @file_id and user = @user and Splunk_Status is null", conn.connection))
            {
                sql_select.Parameters.AddWithValue("@user", GlobalVar.user);
                sql_select.Parameters.AddWithValue("@file_id", GlobalVar.gfile_id);
                using (MySqlDataReader reader = sql_select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Iload_elp.Add(reader["ELP_Reference_Number"].ToString());
                    }
                }
            }
            foreach(string item in Iload_elp)
            {
                Console.WriteLine(item);
            }
            
            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.109.183.200)(PORT=1521)))(CONNECT_DATA=(SID=vloltp11)));User Id=t_amagarang;Password=angelALODIA@@12";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    foreach (string elp in Iload_elp)
                    {
                        using (OracleCommand cmd = new OracleCommand("SELECT DECODE(voidcode, '0000', 'SUCCESSFUL', 'FAILED') AS \"STATUS\", (SELECT de.denom_name FROM oltp_eload_user.EDB_DENOMS_v de WHERE de.plancode = lg.plancode)" +
                            " AS \"DENOM_NAME\", (SELECT cf.val1 FROM oltp_eload_user.EDB_APPLICATION_CONFIGS_v cf WHERE cf.application_code = 'EDB' AND cf.name = 'System Channel ID' AND cf.key = NVL(SUBSTR(NULL, 1, 3), SUBSTR(txn_rrn, 1, 3)))" +
                            " AS \"CHANNEL\", NVL(SUBSTR(evc_rrn, 4), txn_rrn) AS \"REFERENCE_NUMBER\" FROM oltp_eload_user.rtl_txn_logs lg WHERE txn_end BETWEEN to_timestamp(:start_timestamp, 'YYYYMMDDHH24MISS.FF6')" +
                            " AND to_timestamp(:end_timestamp, 'YYYYMMDDHH24MISS.FF6') AND SUBSTR(evc_rrn, 4) in (:elp)", connection))
                        {
                            // Format the dateTimePicker1 value
                            string formattedDateTime = dateTimePicker1.Value.ToString("yyyyMMdd") + "000000.000000";
                            cmd.Parameters.Add(":start_timestamp", OracleDbType.Varchar2).Value = formattedDateTime;

                            formattedDateTime = dateTimePicker1.Value.AddDays(2).ToString("yyyyMMdd") + "235959.999999";
                            cmd.Parameters.Add(":end_timestamp", OracleDbType.Varchar2).Value = formattedDateTime;

                            cmd.Parameters.Add(":elp", OracleDbType.Varchar2).Value = elp;
                            using (OracleDataReader Reader = cmd.ExecuteReader())
                            {                             
                                while (Reader.Read())
                                {
                                    using (MySqlCommand update_query = new MySqlCommand("UPDATE `brand_synch_2`.`gcash` SET `iload` = @status , `Remarks` = @remarks  WHERE ELP_Reference_Number = @elp", conn.connection))
                                    {
                                        update_query.Parameters.AddWithValue("@status", Reader["STATUS"]);
                                        if(Reader["STATUS"].ToString() == "SUCCESSFUL")
                                        {
                                            update_query.Parameters.AddWithValue("@remarks", "Not Subject for Refund");
                                        }
                                        else
                                        {
                                            update_query.Parameters.AddWithValue("@remarks", "Failed - For Refund");
                                        }                                        
                                        update_query.Parameters.AddWithValue("@elp", Reader["REFERENCE_NUMBER"]);
                                        update_query.ExecuteNonQuery();
                                    }
                                }
                              
                            }
                            
                        }
                    }
                    connection.Close();
                    MessageBox.Show("successfully.");
                    display_data();

                    // Perform database operations here
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
