using CsvHelper;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CsvHelper.Configuration;
using System.Collections.Generic;
using OfficeOpenXml;
using Oracle.ManagedDataAccess.Client;
namespace L2_GLA
{
    public partial class Frm_maya : Form
    {

        MySqlCommand cmd;
        DBconnect conn = new DBconnect();
        MySqlDataReader Reader;

        List<string> appTransactionNumbers = new List<string>();
        List<string> merchant = new List<string>();
        List<string> ELPTransactionNumbers = new List<string>();
        List<string> DBStatus = new List<string>();
        List<string> SplunkStatus = new List<string>();
        List<string> SplunkStatus1 = new List<string>();

        public Frm_maya()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
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

                        var records = csv.GetRecords<mydb>().ToList(); // Replace YourDataClass with your custom class to map CSV columns.
                                                                       //  csv.Configuration.IgnoreHeaderWhiteSpace = true;

                        foreach (var record in records)
                        {
                            // Adjust the SQL INSERT statement to match your table structure.
                            cmd = new MySqlCommand("INSERT INTO `myDb` (`App_transaction_number`, `Elp_transaction_number`,`Status`,`created_at`)" +
                                " VALUES (@Value2, @Value3, @Value1, @Value4)", conn.connection);

                            // Add parameters to the command.
                            cmd.Parameters.AddWithValue("@Value1", record.status);
                            cmd.Parameters.AddWithValue("@Value2", record.app_transaction_number);
                            cmd.Parameters.AddWithValue("@Value3", record.elp_transaction_number);
                            cmd.Parameters.AddWithValue("@Value4", record.created_at);
                            cmd.ExecuteNonQuery();

                            //using (MySqlCommand up_sql = new MySqlCommand("UPDATE `brand_synch_2`.`investigation` SET `MERCHANT_TRANS_ID` = @app WHERE replace(investigation.ELP_Reference_Number, ' | success', '') = @elp  ", conn.connection))
                            //{
                            //    up_sql.Parameters.AddWithValue("@app", record.app_transaction_number);
                            //    up_sql.Parameters.AddWithValue("@elp", record.elp_transaction_number);
                            //    up_sql.ExecuteNonQuery();
                            //}

                            ////using (MySqlCommand up_sql = new MySqlCommand("UPDATE `brand_synch_2`.`investigation` SET `ELP_Reference_Number` = @elp WHERE replace(investigation.MERCHANT_TRANS_ID, ' | success', '') = @app  ", conn.connection))
                            ////{
                            ////    up_sql.Parameters.AddWithValue("@app", record.app_transaction_number);
                            ////    up_sql.Parameters.AddWithValue("@elp", record.elp_transaction_number);
                            ////    up_sql.ExecuteNonQuery();
                            ////}                            

                            //if(record.status == "ELP_SUCCESSFUL")
                            //{
                            //    using (MySqlCommand up_sql = new MySqlCommand("UPDATE `brand_synch_2`.`investigation` SET `Status` = @stat, Remarks = @stat WHERE replace(investigation.MERCHANT_TRANS_ID, ' | success', '') = @app  ", conn.connection))
                            //    {
                            //        up_sql.Parameters.AddWithValue("@app", record.app_transaction_number);
                            //        up_sql.Parameters.AddWithValue("@stat", record.status);
                            //        up_sql.ExecuteNonQuery();
                            //    }
                            //}
                            //else
                            //{
                            //    using (MySqlCommand up_sql = new MySqlCommand("UPDATE `brand_synch_2`.`investigation` SET `Status` = @stat WHERE replace(investigation.MERCHANT_TRANS_ID, ' | success', '') = @app  ", conn.connection))
                            //    {
                            //        up_sql.Parameters.AddWithValue("@app", record.app_transaction_number);
                            //        up_sql.Parameters.AddWithValue("@stat", record.status);
                            //        up_sql.ExecuteNonQuery();
                            //    }
                            //}

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

        public sealed class CsvConfiguration : ClassMap<maya>
        {
            public CsvConfiguration()
            {
                Map(m => m.Transaction).Index(0);
                Map(m => m.MERCHANT_TRANS_ID).Index(1);
                Map(m => m.ELP_Reference_Number).Index(2);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var date = new List<string>();
            var app = new List<string>();
            var elp = new List<string>();
            var action = new List<string>();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;


                try
                {
                    string[] csvLines = File.ReadAllLines(selectedFilePath);

                    // Process CSV data
                    for (int i = 2; i < csvLines.Length; i++)
                    {
                        string[] rowData = csvLines[i].Split(',');

                        if (rowData.Length > 25 && (rowData[25] == "Refund to customer" || rowData[25] == "Load Reversal or Retry Payment Charging"))
                        {
                            // date.Add(rowData[0]);
                            app.Add(rowData[22]);
                            //elp.Add(item: rowData[23].Replace(" | success", " ").Replace(" |  | failed", " "));
                            elp.Add(item: rowData[23]);
                            action.Add(rowData[25]);
                        }

                    }

                    for (int j = 0; j < action.Count; j++)
                    {
                        cmd = new MySqlCommand("INSERT INTO `brand_synch_2`.`investigation` (`MERCHANT_TRANS_ID`, `ELP_Reference_Number`,`Action`,`user`) " +
                        "VALUES(@MerchantTransID, @ELPReference,@action,@user)", conn.connection);

                        cmd.Parameters.AddWithValue("@MerchantTransID", app[j]);
                        cmd.Parameters.AddWithValue("@ELPReference", elp[j]);
                        cmd.Parameters.AddWithValue("@action", action[j]);
                        cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("CSV data uploaded successfully.");
                    display();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

            }

        }
        List<string> iload = new List<string>();

        private void Frm_maya_Load(object sender, EventArgs e)
        {
            dtpto.Value = DateTime.Today;
            display();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("truncate table mydb", conn.connection);
            cmd.ExecuteNonQuery();
            MessageBox.Show("successfully.");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("Delete from investigation where user = @user", conn.connection);
            cmd.Parameters.AddWithValue("@user", GlobalVar.user);
            cmd.ExecuteNonQuery();

            display();
        }

        public void load_data()
        {
            cmd = new MySqlCommand("Select id,MERCHANT_TRANS_ID,ELP_Reference_Number from investigation where user = @user and file_id = @fileID AND Action in ('Refund to customer' ,'Load Reversal or Retry Payment Charging')", conn.connection);
            cmd.Parameters.AddWithValue("@user", GlobalVar.user);
            cmd.Parameters.AddWithValue("@fileID", GlobalVar.gfile_id);

            Reader = cmd.ExecuteReader();
            dgvlist.Rows.Clear();
            dgvlist.AllowUserToAddRows = true;
            while (Reader.Read())
            {
                DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();
               // row.Cells[0].Value = Reader["id"];
                row.Cells[0].Value = "'" + Reader["MERCHANT_TRANS_ID"] + "',";
                row.Cells[1].Value = "'" + Reader["ELP_Reference_Number"].ToString().Replace(" | success", " ") + "',";
                dgvlist.Rows.Add(row);
            }
            Reader.Close();
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            load_data();

        }

        private List<Tuple<DataGridViewComboBoxCell, string>> comboBoxCellsToUpdate = new List<Tuple<DataGridViewComboBoxCell, string>>();

        public void display()
        {
            cmd = new MySqlCommand("SELECT investigation.*, tbl_variance_file.File_name,tbl_variance_file.Status as file_status from investigation join tbl_variance_file ON tbl_variance_file.id = investigation.file_id where tbl_variance_file.Status = 'Ongoing' and Action in ('Refund to customer' , 'Load Reversal or Retry Payment Charging') and user = @user", conn.connection);
            cmd.Parameters.AddWithValue("@user", GlobalVar.user);
            Reader = cmd.ExecuteReader();



            dgvlist.Rows.Clear();
            dgvlist.AllowUserToAddRows = true;
           
                BtnInvestigation.Enabled = false;

                while (Reader.Read())
                {
                    if(Reader["file_status"].ToString() == "Ongoing")
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();

                        //row.Cells[0].Value = Reader["id"];
                        row.Cells[0].Value = Reader["MERCHANT_TRANS_ID"];
                        row.Cells[1].Value = Reader["ELP_Reference_Number"].ToString().Replace(" | success","");
                        row.Cells[2].Value = Reader["Status"];
                        row.Cells[3].Value = Reader["Splunk_Status"];
                        row.Cells[4].Value = Reader["iload"];
                    if(Reader["Remarks"].ToString() is null || string.IsNullOrEmpty(Reader["Remarks"].ToString()))
                    {
                        DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
                        comboBoxCell.Items.Add("Not Subject for Refund");
                        comboBoxCell.Items.Add("Failed - For Refund");
                        comboBoxCell.Items.Add("ELP_SUCCESSFUL");
                        row.Cells[5] = comboBoxCell;

                        comboBoxCellsToUpdate.Add(new Tuple<DataGridViewComboBoxCell, string>(comboBoxCell, Reader["id"].ToString()));
                    }
                    else
                    {
                        row.Cells[5].Value = Reader["Remarks"];
                    }
                       
                        dgvlist.Rows.Add(row);

                        GlobalVar.gfile_id = Reader["file_id"].ToString();
                        GlobalVar.gfile_name = Reader["file_name"].ToString();
                    }
                lblname.Text = Reader["File_name"].ToString();
                }

                dgvlist.AllowUserToAddRows = false;
            BtnInvestigation.Enabled = dgvlist.Rows.Count == 0;
         
            Reader.Close();
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




        private void button8_Click(object sender, EventArgs e)
        {
            display();
            MessageBox.Show("successfully.");
            Reader.Close();
        }
        public void view_list()
        {

            
            using (MySqlCommand sql_view = new MySqlCommand("SELECT * from investigation where Action IN ('Refund to customer', 'Load Reversal or Retry Payment Charging') and file_id = @fileID", conn.connection))
            {               
                sql_view.Parameters.AddWithValue("@fileID", GlobalVar.gfile_id);
                using (MySqlDataReader reader = sql_view.ExecuteReader())
                {
                    dgvlist.Rows.Clear();
                    dgvlist.AllowUserToAddRows = true;
                   
                    while (reader.Read())
                    {                        
                            DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();

                            //row.Cells[0].Value = Reader["id"];
                            row.Cells[0].Value = reader["MERCHANT_TRANS_ID"];
                            row.Cells[1].Value = reader["ELP_Reference_Number"].ToString().Replace(" | success", "");
                            row.Cells[2].Value = reader["Status"];
                            row.Cells[3].Value = reader["Splunk_Status"];
                            row.Cells[4].Value = reader["iload"];
                        if (reader["Remarks"].ToString() is null || string.IsNullOrEmpty(reader["Remarks"].ToString()))
                        {
                            DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
                            comboBoxCell.Items.Add("Not Subject for Refund");
                            comboBoxCell.Items.Add("Failed - For Refund");
                            comboBoxCell.Items.Add("ELP_SUCCESSFUL");

                            row.Cells[5] = comboBoxCell;

                            comboBoxCellsToUpdate.Add(new Tuple<DataGridViewComboBoxCell, string>(comboBoxCell, reader["id"].ToString()));
                        }
                        else
                        {
                            row.Cells[5].Value = reader["Remarks"];
                        }

                        dgvlist.Rows.Add(row);
                        lblname.Text = GlobalVar.gfile_name;
                    }

                    dgvlist.AllowUserToAddRows = false;
                }                                                              
            }
         
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            var date = new List<string>();
            var app = new List<string>();
            var elp = new List<string>();
            var action = new List<string>();
            string file_name = "";
            int file_id = 0;
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
                          DialogResult messresult =  MessageBox.Show(reader["File_name"].ToString() + " is already " + reader["Status"].ToString() + " by " + reader["Assigned_to"].ToString() + "\n\r" + "Do you want to Extract?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (messresult == DialogResult.Yes)
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
                        cmd2.Parameters.AddWithValue("@vartype", "maya");
                        cmd2.Parameters.AddWithValue("@assigned", GlobalVar.user);
                        cmd2.Parameters.AddWithValue("@status", "Ongoing");
                        cmd2.Parameters.AddWithValue("@created", DateTime.Now);
                        cmd2.ExecuteNonQuery();
                    }
                }

                try
                {
                    using (ExcelPackage package = new ExcelPackage(new FileInfo(selectedFilePath)))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first sheet

                        int rowCount = worksheet.Dimension.Rows;

                        for (int i = 3; i <= rowCount; i++)
                        {
                            // Assuming column indexes are same as in your CSV file
                            string dateValue = worksheet.Cells[i, 1].Value?.ToString(); // Assuming date is in column 1
                            string appValue = worksheet.Cells[i, 23].Value?.ToString(); // Assuming app is in column 23
                            string elpValue = worksheet.Cells[i, 24].Value?.ToString(); // Assuming elp is in column 24
                            string actionValue = worksheet.Cells[i, 26].Value?.ToString(); // Assuming action is in column 26

                            app.Add(appValue);
                            elp.Add(elpValue);
                            action.Add(actionValue);

                        }
                    }

                    for (int j = 0; j < action.Count; j++)
                    {
                        cmd = new MySqlCommand("INSERT INTO `brand_synch_2`.`investigation` (`MERCHANT_TRANS_ID`, `ELP_Reference_Number`,`Action`,`user`,`file_id`) " +
                        "VALUES(@MerchantTransID, @ELPReference,@action,@user,@fileID)", conn.connection);

                        if (action[j] == "Refund to customer" || action[j] == "Load Reversal or Retry Payment Charging")
                        {
                            if (app[j] == "Not Found")
                            {
                                cmd.Parameters.AddWithValue("@MerchantTransID", "Not Found in DB");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@MerchantTransID", app[j]);
                            }
                            if (elp[j] == "Not Found")
                            {
                                cmd.Parameters.AddWithValue("@ELPReference", "Not Found in DB");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ELPReference", elp[j]);
                            }
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MerchantTransID", app[j]);
                            cmd.Parameters.AddWithValue("@ELPReference", elp[j]);
                        }



                        cmd.Parameters.AddWithValue("@action", action[j]);
                        cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                        cmd.Parameters.AddWithValue("@fileID", file_id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Excel data uploaded successfully.");
                    display();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }

        }
        string db_File_name = "";
        private void updateStatus(object sender, Frm_Var_update_Status.UpdateEventArgs args)
        {
            display();
            dgvnoc.Visible = false;
            dgvnoc.Rows.Clear();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {                     
                DialogResult result = MessageBox.Show("To process the New MAYA variance\n\r Please Update the Status of " + db_File_name, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Frm_Var_update_Status objfrm = new Frm_Var_update_Status(null, this);
                    objfrm.UpdateEventHandler += updateStatus;
                    objfrm.ShowDialog();
                }
                else
                {
                    //  e.Cancel = true;
                }            
        }

        private void btnApp_Click(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("SELECT mydb.App_transaction_number, investigation.ELP_Reference_Number FROM mydb INNER JOIN investigation ON REPLACE(investigation.ELP_Reference_Number, ' | success', '') = mydb.Elp_transaction_number " +
                "WHERE investigation.Action IN('Refund to customer', 'Load Reversal or Retry Payment Charging') and investigation.user = @user and file_id = @fileID", conn.connection);
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
            for (int i = 0; i < ELPTransactionNumbers.Count; i++)
            {
                if (appTransactionNumbers[i] is null || appTransactionNumbers[i] == "NULL")
                {
                    cmd = new MySqlCommand("UPDATE investigation SET MERCHANT_TRANS_ID = 'Not Found in DB' WHERE ELP_Reference_Number = '" + ELPTransactionNumbers[i] + "'", conn.connection);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new MySqlCommand("UPDATE investigation SET MERCHANT_TRANS_ID = '" + appTransactionNumbers[i] + "' WHERE ELP_Reference_Number = '" + ELPTransactionNumbers[i] + "'", conn.connection);
                    cmd.ExecuteNonQuery();
                }

            }

            appTransactionNumbers.Clear();
            ELPTransactionNumbers.Clear();
            MessageBox.Show("successfully.");
            display();
        }

        private void btnElp_Click(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("SELECT  mydb.App_transaction_number,mydb.Elp_transaction_number FROM mydb inner join  investigation ON mydb.App_transaction_number = investigation.MERCHANT_TRANS_ID where investigation.user = @user and file_id = @fileID", conn.connection);
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
                if (ELPTransactionNumbers[i] is null || ELPTransactionNumbers[i] == "NULL")
                {
                    cmd = new MySqlCommand("UPDATE investigation SET ELP_Reference_Number  = 'Not Found in DB' WHERE MERCHANT_TRANS_ID= '" + appTransactionNumbers[i] + "'", conn.connection);
                    cmd.ExecuteNonQuery();

                }
                else
                {
                    cmd = new MySqlCommand("UPDATE investigation SET ELP_Reference_Number  = '" + ELPTransactionNumbers[i] + "' WHERE MERCHANT_TRANS_ID= '" + appTransactionNumbers[i] + "'", conn.connection);
                    cmd.ExecuteNonQuery();
                }

            }

            appTransactionNumbers.Clear();
            ELPTransactionNumbers.Clear();
            MessageBox.Show("successfully.");
            display();
        }

        private void btnDB_Click(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("SELECT  investigation.MERCHANT_TRANS_ID, mydb.Status FROM mydb inner join investigation ON investigation.MERCHANT_TRANS_ID = mydb.App_transaction_number where investigation.user = @user and file_id = @fileID", conn.connection);
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
                    cmd = new MySqlCommand("UPDATE investigation SET Remarks  = '" + DBStatus[i] + "' WHERE MERCHANT_TRANS_ID= '" + appTransactionNumbers[i] + "'", conn.connection);
                    cmd.ExecuteNonQuery();
                }

                cmd = new MySqlCommand("UPDATE investigation SET Status  = '" + DBStatus[i] + "' WHERE MERCHANT_TRANS_ID= '" + appTransactionNumbers[i] + "'", conn.connection);
                cmd.ExecuteNonQuery();
            }

            appTransactionNumbers.Clear();
            DBStatus.Clear();
            MessageBox.Show("successfully.");
            display();
        }

        public void app_noc()
        {
            dgvnoc.Visible = true;
            using (MySqlCommand select_cmd = new MySqlCommand("Select REPLACE(investigation.MERCHANT_TRANS_ID, ' | success', '') as MERCHANT_TRANS_ID from investigation where Action in ('Refund to customer'," +
                "'Load Reversal or Retry Payment Charging') and investigation.file_id = @file_id and Remarks is null and user = @user and MERCHANT_TRANS_ID != 'Not Found in DB'; ", conn.connection))
            {
                select_cmd.Parameters.AddWithValue("@file_id", GlobalVar.gfile_id);
                select_cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                using (MySqlDataReader reader = select_cmd.ExecuteReader())
                {
                    dgvnoc.Rows.Clear();
                    dgvnoc.AllowUserToAddRows = true;
                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvnoc.Rows[0].Clone();
                        row.Cells[0].Value = reader["MERCHANT_TRANS_ID"];
                        dgvnoc.Rows.Add(row);
                    }

                }
            }
        }
        private void btnsplunk_Click(object sender, EventArgs e)
        {
            string remarks = "";

            cmd = new MySqlCommand("select MERCHANT_TRANS_ID from  investigation where Status !='ELP_SUCCESSFUL' and file_id = @fileID", conn.connection);
          //  cmd.Parameters.AddWithValue("@user", GlobalVar.user);
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

                // Fetch data from tbl_splunk
                cmd = new MySqlCommand("SELECT app_transaction_number, state FROM tbl_splunk WHERE app_transaction_number = '" + appTransactionNumbers[i] + "' and time like @to", conn.connection);
                cmd.Parameters.AddWithValue("@to", "%" + dtpto.Value.ToString("ddd MMM") + "%");
                Console.WriteLine("DATE:" + dtpto.Value.ToString("ddd MMM"));
                Reader = cmd.ExecuteReader();

                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        merchant.Add(Reader["app_transaction_number"].ToString());
                        SplunkStatus.Add(Reader["state"].ToString());
                        SplunkStatus1.Add(Reader["state"].ToString());
                    }

                    Reader.Close();


                    string updateQuery = "UPDATE investigation SET Splunk_Status = @SplunkStatus WHERE MERCHANT_TRANS_ID = @MerchantTransID";

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
                        else if (SplunkStatus1.Contains("PAYMENT_SUCCESS"))
                        {

                            SplunkStatus1.Clear();
                            SplunkStatus1.Add("Failed - For Refund");
                        }
                        else
                        {
                            SplunkStatus1.Clear();
                            SplunkStatus1.Add("Not Subject for Refund");
                        }

                        string updateQuery1 = "UPDATE investigation SET Remarks = @Remarks WHERE MERCHANT_TRANS_ID = @MerchantTransID";

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
                    cmd = new MySqlCommand("UPDATE investigation SET Splunk_Status = 'Not Found in Splunk file' WHERE MERCHANT_TRANS_ID = @MerchantTransID", conn.connection);
                    cmd.Parameters.AddWithValue("@MerchantTransID", appTransactionNumbers[i]);
                    cmd.ExecuteNonQuery();
                }

                Reader.Close();
            }

            Reader.Close();


            appTransactionNumbers.Clear();
            SplunkStatus.Clear();
            display();
            app_noc();
        }

        private void btn_iload_Click(object sender, EventArgs e)
        {
            using (MySqlCommand select_cmd = new MySqlCommand("Select REPLACE(investigation.ELP_Reference_Number, ' | success', '') as ELP_Reference_Number from investigation where Action in ('Refund to customer','Load Reversal or Retry Payment Charging') and  investigation.file_id = @file_id and Remarks is null and Splunk_Status is null and user = @user", conn.connection))
            {
                select_cmd.Parameters.AddWithValue("@file_id", GlobalVar.gfile_id);
                select_cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                using (MySqlDataReader reader = select_cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        iload.Add(reader["ELP_Reference_Number"].ToString());
                        //label1.Text =  reader["ELP_Reference_Number"].ToString();
                    }

                }
            }

            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.109.183.200)(PORT=1521)))(CONNECT_DATA=(SID=vloltp11)));User Id=t_amagarang;Password=angelALODIA@@12";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    foreach (string elp in iload)
                    {
                        using (OracleCommand cmd = new OracleCommand("SELECT DECODE(voidcode, '0000', 'SUCCESSFUL', 'FAILED') AS \"STATUS\", (SELECT de.denom_name FROM oltp_eload_user.EDB_DENOMS_v de WHERE de.plancode = lg.plancode)" +
                            " AS \"DENOM_NAME\", (SELECT cf.val1 FROM oltp_eload_user.EDB_APPLICATION_CONFIGS_v cf WHERE cf.application_code = 'EDB' AND cf.name = 'System Channel ID' AND cf.key = NVL(SUBSTR(NULL, 1, 3), SUBSTR(txn_rrn, 1, 3)))" +
                            " AS \"CHANNEL\", NVL(SUBSTR(evc_rrn, 4), txn_rrn) AS \"REFERENCE_NUMBER\" FROM oltp_eload_user.rtl_txn_logs lg WHERE txn_end BETWEEN to_timestamp(:start_timestamp, 'YYYYMMDDHH24MISS.FF6')" +
                            " AND to_timestamp(:end_timestamp, 'YYYYMMDDHH24MISS.FF6') AND SUBSTR(evc_rrn, 4) in (:elp)", connection))
                        {
                            // Format the dateTimePicker1 value
                            string formattedDateTime = dtpto.Value.ToString("yyyyMMdd") + "000000.000000";
                            cmd.Parameters.Add(":start_timestamp", OracleDbType.Varchar2).Value = formattedDateTime;
                            //    label3.Text = formattedDateTime;
                            formattedDateTime = dtpto.Value.AddDays(2).ToString("yyyyMMdd") + "235959.999999";
                            cmd.Parameters.Add(":end_timestamp", OracleDbType.Varchar2).Value = formattedDateTime;
                            //  label4.Text = formattedDateTime;

                            cmd.Parameters.Add(":elp", OracleDbType.Varchar2).Value = elp;
                            using (OracleDataReader Reader = cmd.ExecuteReader())
                            {
                                while (Reader.Read())
                                {
                                    //     label5.Text = Reader["STATUS"].ToString();
                                    using (MySqlCommand update_query = new MySqlCommand("UPDATE `brand_synch_2`.`investigation` SET `iload` = @status , `Remarks` = @remarks  WHERE REPLACE(investigation.ELP_Reference_Number, ' | success', '') = @elp", conn.connection))
                                    {
                                        update_query.Parameters.AddWithValue("@status", Reader["STATUS"]);
                                        if (Reader["STATUS"].ToString() == "SUCCESSFUL")
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
                    display();

                    // Perform database operations here
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            foreach (var comboBoxCellToUpdate in comboBoxCellsToUpdate)
            {
                if (comboBoxCellToUpdate.Item1 != null && comboBoxCellToUpdate.Item1.Value != null)
                {
                    string selectedValue = comboBoxCellToUpdate.Item1.Value.ToString();
                    string databaseID = comboBoxCellToUpdate.Item2;

                    using (MySqlCommand sql_update = new MySqlCommand("UPDATE `brand_synch_2`.`investigation` SET `Remarks` = @remarks  WHERE `id` = @id", conn.connection))
                    {
                        sql_update.Parameters.AddWithValue("@remarks", selectedValue);
                        sql_update.Parameters.AddWithValue("@id", databaseID);
                        sql_update.ExecuteNonQuery();

                    }
                }
                else
                {
                    // Handle the case where comboBoxCellToUpdate.Item1 is null
                }

            }

            // Clear the list after updating the database
            comboBoxCellsToUpdate.Clear();

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
                    using (MySqlCommand cmdGetData = new MySqlCommand("SELECT * FROM brand_synch_2.investigation where file_id = @file_id", conn.connection))
                    {
                        cmdGetData.Parameters.AddWithValue("@file_id", GlobalVar.gfile_id);
                        using (MySqlDataReader reader = cmdGetData.ExecuteReader())
                        {
                            int row = 3; // Start inserting data from row 3
                            while (reader.Read())
                            {
                                // Write the updated data to specific cells
                                worksheet.Cells[row, 23].Value = reader["MERCHANT_TRANS_ID"].ToString();
                                worksheet.Cells[row, 24].Value = reader["ELP_Reference_Number"].ToString();
                                worksheet.Cells[row, 27].Value = reader["Remarks"].ToString();
                                row++;
                            }
                        }
                    }

                    // Save the changes to the Excel file
                    package.Save();
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
                        display();
                        dgvnoc.Visible = false;
                        dgvnoc.Rows.Clear();
                    }
                }
            }
            else
            {
                // Handle case where file path is not found
                MessageBox.Show("File path not found in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}







