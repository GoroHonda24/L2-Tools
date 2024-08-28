using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Configuration;
using System.Data.Common;
using OfficeOpenXml;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using L2_GLA.Variance;
using CsvHelper;
using System.Globalization;
using OfficeOpenXml.Drawing.Slicer.Style;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using MySqlX.XDevAPI.Relational;
using Oracle.ManagedDataAccess.Client;

namespace L2_GLA.Model
{
    internal class variance
    {
        private readonly DBconnect _conn;
        private int count = 0;
        private int action = 0, app = 0, iload = 0, remark = 0;
        private string table = "";
        List<string> appTransactionNumbers = new List<string>();
        List<string> transStatus = new List<string>();
        public variance()
        {
            _conn = new DBconnect();
            if (_conn.connection.State != ConnectionState.Open) _conn.connection.Open();
        }

        #region maya

        public async Task<int> checkMayaFile(string mayaFileName)
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(file_name) FROM brand_synch_2.tbl_variance_file WHERE File_name = @file_name", _conn.connection))
            {
                cmd.Parameters.AddWithValue("@file_name", mayaFileName);
                object result = await cmd.ExecuteScalarAsync();

                // Check if result is not null and is convertible to an integer
                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    return count;
                }
                else
                {
                    return 0; // Return default value if conversion fails
                }
            }
        }

        public async Task fileconfig(string vartype)
        {
            using (MySqlCommand selectquery = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_variance_config where vartype = @vartype ", _conn.connection))
            {
                selectquery.Parameters.AddWithValue("@vartype", vartype);
                using (var reader = await selectquery.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        action = reader.GetInt32(1);
                        app = reader.GetInt32(2);
                        iload = reader.GetInt32(3);
                        remark = reader.GetInt32(4);
                    }
                }
            }
        }

        public async Task formattoquery(string filename, string vartype)
        {
            try
            {
                HashSet<string> app_value = new HashSet<string>();
                HashSet<string> iload_value = new HashSet<string>();

                if (string.IsNullOrEmpty(GlobalVar.filepath))
                {
                    FileInfo fileInfo = new FileInfo(filename);
                    GlobalVar.filepath = fileInfo.FullName;

                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                        {
                            throw new Exception("Worksheet is null. Please check the Excel file.");
                        }

                        if (worksheet.Dimension == null)
                        {
                            throw new Exception("Worksheet dimensions are null. Please check the Excel file.");
                        }

                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (vartype == "maya")
                            {
                                table = "tbl_variance_maya";
                                string columnZ = worksheet.Cells[row, action].Text; // Column Z
                                if (columnZ == "Refund to customer" || columnZ == "Load Reversal or Retry Payment Charging")
                                {
                                    string columnW = worksheet.Cells[row, app].Text; // Column W
                                    app_value.Add(columnW);

                                    string columnY = worksheet.Cells[row, iload].Text; // Column Y
                                    string[] columnYParts = columnY.Split('|');

                                    if (columnYParts.Length >= 1)
                                    {
                                        string iload = columnYParts[0].Trim();
                                        iload_value.Add(iload);
                                        await SaveToDatabaseAsync(columnW, iload, columnZ);
                                        Console.WriteLine($"Processed row {row}: columnW = {columnW}, iload = {iload}, action = {columnZ}");
                                    }
                                }
                            }
                            else if (vartype == "gcash")
                            {
                                table = "tbl_variance_gcash";
                                string columnZ = worksheet.Cells[row, action].Text; // Column Z
                                if (columnZ == "Refund to Customer" || columnZ == "Load Reversal or Retry Payment Charging")
                                {
                                    string afColumnValue = worksheet.Cells[row, app].Text; // AF column is the 32nd column
                                    string[] splitValues = afColumnValue.Split('|');
                                    if (splitValues.Length >= 0)
                                    {
                                        string app = splitValues[0].Trim();
                                        app_value.Add(app);
                                        string iload = splitValues[2].Trim();
                                        iload_value.Add(iload);

                                        await SaveToDatabaseAsync(app, iload, columnZ);
                                        // Console.WriteLine($"Processed row {row}: columnW = {app}, iload = {iload}, action = {columnZ}");
                                    }
                                }
                            }
                        }

                        string columnWList = string.Join("','", app_value);
                        string columnYList = string.Join("','", iload_value);
                        string query = "SELECT status, app_transaction_number, elp_transaction_number, created_at FROM gigapay.transaction_logs where app_transaction_number in ('" + columnWList + "')\r\n or elp_transaction_number in ('" + columnYList + "');";
                        await insertFileName();
                        GlobalVar.formatedQuery = query;
                        queryFormat queryFormat = new queryFormat();
                        await Task.Run(() => queryFormat.ShowDialog());
                    }
                }
                else if (!string.IsNullOrEmpty(GlobalVar.filepath))
                {
                    filename = GlobalVar.filepath;

                    FileInfo fileInfo = new FileInfo(filename);
                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                        {
                            throw new Exception("Worksheet is null. Please check the Excel file.");
                        }

                        if (worksheet.Dimension == null)
                        {
                            throw new Exception("Worksheet dimensions are null. Please check the Excel file.");
                        }

                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            string columnZ = worksheet.Cells[row, action].Text; // Column Z
                            if (columnZ == "Refund to customer" || columnZ == "Load Reversal or Retry Payment Charging")
                            {
                                string columnW = worksheet.Cells[row, app].Text; // Column W
                                app_value.Add(columnW);

                                string columnY = worksheet.Cells[row, iload].Text; // Column Y
                                string[] columnYParts = columnY.Split('|');

                                if (columnYParts.Length >= 1)
                                {
                                    string iload = columnYParts[0].Trim();
                                    string query = "SELECT remarks FROM brand_synch_2.tbl_variance_maya WHERE app_transaction = @columnW OR iload = @iload";

                                    using (MySqlCommand command = new MySqlCommand(query, _conn.connection))
                                    {
                                        command.Parameters.AddWithValue("@columnW", columnW);
                                        command.Parameters.AddWithValue("@iload", iload);

                                        using (var reader = await command.ExecuteReaderAsync())
                                        {
                                            if (await reader.ReadAsync())
                                            {
                                                string remarks = reader["remarks"].ToString();
                                                worksheet.Cells[row, remark].Value = remarks;
                                                Console.WriteLine($"Row {row}: Updated Column AA with remarks: {remarks}");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        package.Save();
                        GlobalVar.filepath = "";
                        await updateFileName();
                        System.Windows.MessageBox.Show("Process Completed", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    System.Data.DataTable databaseData = await GetMayaResult();
                    // dgvTicketDetails.DataSource = databaseData;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("An error occurred while processing the Excel file. Check the log for details." + ex.ToString());
                Console.WriteLine("error" + ex.ToString());
            }
        }

        public async Task<System.Data.DataTable> GetMayaResult()
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            try
            {
                string Query = $"SELECT app_transaction, iload, dbStatus, remarks FROM {GlobalVar.tableName} INNER JOIN tbl_variance_file ON tbl_variance_file.File_id = {GlobalVar.tableName}.file_id WHERE tbl_variance_file.File_name = @user";
                using (MySqlCommand selectQuery = new MySqlCommand(Query, _conn.connection))
                {
                    selectQuery.Parameters.AddWithValue("@user", GlobalVar.gfile_name);
                    using (var reader = await selectQuery.ExecuteReaderAsync())
                    {
                        dataTable.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.ToString());
            }

            return dataTable;
        }
        public async Task importSmartDatabase(string filePath, string variancetype)
        {
            try
            {
                // Read and parse the CSV file using CsvHelper.
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<mydb>().ToList();
                    var processedIds = new HashSet<string>(); // Track processed IDs to avoid duplicates

                    foreach (var record in records)
                    {
                        string selectQuery = $"SELECT id FROM {table} " +
                                             "WHERE app_transaction = @app OR iload = @iload";

                        var idsToUpdate = new List<string>();

                        using (var cmdSelect = new MySqlCommand(selectQuery, _conn.connection))
                        {
                            cmdSelect.Parameters.AddWithValue("@app", record.app_transaction_number);
                            cmdSelect.Parameters.AddWithValue("@iload", record.elp_transaction_number);

                            try
                            {
                                using (var dataReader = await cmdSelect.ExecuteReaderAsync())
                                {
                                    while (await dataReader.ReadAsync())
                                    {
                                        string id = dataReader.GetString(0);
                                        idsToUpdate.Add(id);
                                    }
                                }
                            }
                            catch (MySqlException ex)
                            {
                                // Log and rethrow the exception for the select command
                                Console.WriteLine($"MySQL error (select): {ex.Message}");
                                throw;
                            }
                        }

                        foreach (var id in idsToUpdate)
                        {
                            if (processedIds.Contains(id))
                            {
                                //Console.WriteLine($"Duplicate ID skipped: {id}");
                                continue; // Skip already processed IDs
                            }

                            processedIds.Add(id);
                            //Console.WriteLine($"Processing ID: {id}");
                            string newstatus = "";
                            if (variancetype == "maya")
                            {
                                if (record.status != "ELP_SUCCESSFUL")
                                {


                                    //newstatus = "Failed for Refund";
                                }
                                else if (record.status == "ELP_SUCCESSFUL")
                                {
                                    newstatus = record.status;
                                }
                                else
                                {
                                    newstatus = "Not Subject for Refund";
                                }
                            }
                            else
                            {
                                if (record.status != "ELP_SUCCESSFUL")
                                {
                                    using (MySqlCommand selectquery = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_merchant where TRANSACTION_DATETIME >= @created_at and MERCHANT_TRANS_ID like @apptrans", _conn.connection))
                                    {
                                        DateTime createdAtDateTime;
                                        if (DateTime.TryParseExact(record.created_at, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out createdAtDateTime))
                                        {
                                            selectquery.Parameters.AddWithValue("@created_at", createdAtDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                                            selectquery.Parameters.AddWithValue("@apptrans", "%" + record.app_transaction_number);
                                            using (var readerResult = await selectquery.ExecuteReaderAsync())
                                            {

                                            }
                                        }
                                        else
                                        {
                                            throw new FormatException("Invalid date format. Expected MM/dd/yyyy.");
                                        }

                                    }

                                    newstatus = "Failed for Refund";
                                }
                                else if (record.status == "ELP_SUCCESSFUL")
                                {
                                    newstatus = record.status;
                                }
                                else
                                {
                                    newstatus = "Not Subject for Refund";
                                }

                            }


                            // Update the existing record
                            string updateQuery = "UPDATE `brand_synch_2`.`tbl_variance_maya` " +
                                                 "SET `app_transaction` = @app, `iload` = @iload, `dbStatus` = @status, `remarks` = @remarks " +
                                                 "WHERE `id` = @id ";

                            using (var cmdUpdate = new MySqlCommand(updateQuery, _conn.connection))
                            {
                                cmdUpdate.Parameters.AddWithValue("@id", id);
                                cmdUpdate.Parameters.AddWithValue("@app", record.app_transaction_number);
                                cmdUpdate.Parameters.AddWithValue("@iload", record.elp_transaction_number);
                                cmdUpdate.Parameters.AddWithValue("@status", record.status);
                                cmdUpdate.Parameters.AddWithValue("@remarks", newstatus);

                                try
                                {
                                    await cmdUpdate.ExecuteNonQueryAsync();
                                }
                                catch (MySqlException ex)
                                {
                                    // Log and rethrow the exception for the update command
                                    Console.WriteLine($"MySQL error (update): {ex.Message}");
                                    throw;
                                }
                            }
                        }
                    }
                }
                using (MySqlCommand updateRemarks = new MySqlCommand("UPDATE brand_synch_2.tbl_variance_maya SET remarks = 'Not Subject for Refund' WHERE app_transaction = 'Not Found'", _conn.connection))
                {
                    await updateRemarks.ExecuteNonQueryAsync();
                }
                //using (MySqlCommand updateRemarks = new MySqlCommand("update brand_synch_2.tbl_variance_maya set remarks = 'Failed - for Refund' where iload = 'Not Found' ", _conn.connection))
                //{
                //    await updateRemarks.ExecuteNonQueryAsync();
                //}
                await formattoquery(filePath, null);
            }
            catch (MySqlException ex)
            {
                // Log MySQL-specific exceptions
                Console.WriteLine($"MySQL error: {ex.Message}");
                System.Windows.MessageBox.Show("A database error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Log general exceptions
                Console.WriteLine($"General error: {ex.Message}");
                System.Windows.MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        //public async Task iloadquery()
        //{
        //    string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.109.183.200)(PORT=1521)))(CONNECT_DATA=(SID=vloltp11)));User Id=t_amagarang;Password=angelALODIA@@12";

        //    using (OracleConnection connection = new OracleConnection(connectionString))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            using (OracleCommand cmd = new OracleCommand("SELECT DECODE(voidcode, '0000', 'SUCCESSFUL', 'FAILED') AS \"STATUS\", (SELECT de.denom_name FROM oltp_eload_user.EDB_DENOMS_v de WHERE de.plancode = lg.plancode)" +
        //                     " AS \"DENOM_NAME\", (SELECT cf.val1 FROM oltp_eload_user.EDB_APPLICATION_CONFIGS_v cf WHERE cf.application_code = 'EDB' AND cf.name = 'System Channel ID' AND cf.key = NVL(SUBSTR(NULL, 1, 3), SUBSTR(txn_rrn, 1, 3)))" +
        //                     " AS \"CHANNEL\", NVL(SUBSTR(evc_rrn, 4), txn_rrn) AS \"REFERENCE_NUMBER\" FROM oltp_eload_user.rtl_txn_logs lg WHERE txn_end BETWEEN to_timestamp(:start_timestamp, 'YYYYMMDDHH24MISS.FF6')" +
        //                     " AND to_timestamp(:end_timestamp, 'YYYYMMDDHH24MISS.FF6') AND SUBSTR(evc_rrn, 4) in (:elp)", connection))
        //            {
        //                // Format the dateTimePicker1 value
        //                //string formattedDateTime = dtpto.Value.ToString("yyyyMMdd") + "000000.000000";
        //                //cmd.Parameters.Add(":start_timestamp", OracleDbType.Varchar2).Value = formattedDateTime;
        //                ////    label3.Text = formattedDateTime;
        //                //formattedDateTime = dtpto.Value.AddDays(2).ToString("yyyyMMdd") + "235959.999999";
        //                //cmd.Parameters.Add(":end_timestamp", OracleDbType.Varchar2).Value = formattedDateTime;
        //                ////  label4.Text = formattedDateTime;

        //                //cmd.Parameters.Add(":elp", OracleDbType.Varchar2).Value = elp;
        //                //using (OracleDataReader Reader = cmd.ExecuteReader())
        //                //{
        //                //    while (Reader.Read())
        //                //    {
        //                //        //     label5.Text = Reader["STATUS"].ToString();
        //                //        using (MySqlCommand update_query = new MySqlCommand("UPDATE `brand_synch_2`.`investigation` SET `iload` = @status , `Remarks` = @remarks  WHERE REPLACE(investigation.ELP_Reference_Number, ' | success', '') = @elp", conn.connection))
        //                //        {
        //                //            update_query.Parameters.AddWithValue("@status", Reader["STATUS"]);
        //                //            if (Reader["STATUS"].ToString() == "SUCCESSFUL")
        //                //            {
        //                //                update_query.Parameters.AddWithValue("@remarks", "Not Subject for Refund");
        //                //            }
        //                //            else
        //                //            {
        //                //                update_query.Parameters.AddWithValue("@remarks", "Failed - For Refund");
        //                //            }
        //                //            update_query.Parameters.AddWithValue("@elp", Reader["REFERENCE_NUMBER"]);
        //                //            update_query.ExecuteNonQuery();
        //                //        }
        //                //    }

        //                //}

        //            }

        //            connection.Close();
        //            System.Windows.MessageBox.Show("successfully.");
                    
        //        }
        //        catch (Exception ex)
        //        {
        //            System.Windows.MessageBox.Show("Error: " + ex.Message);
        //        }
        //    }
        //}

        public async Task SaveToDatabaseAsync(string appTransactionData, string iloadData, string actionData)
        {
            try
            {               
                string query = $"INSERT INTO {table}(`file_id`,`app_transaction`,`iload`,`action`)VALUES(@maxid, @app_transaction, @iload, @action)";
                using (MySqlCommand cmd = new MySqlCommand(query, _conn.connection))
                {

                    cmd.Parameters.AddWithValue("@maxid", GlobalVar.maxID);
                    cmd.Parameters.AddWithValue("@app_transaction", appTransactionData);
                    cmd.Parameters.AddWithValue("@iload", iloadData);
                    cmd.Parameters.AddWithValue("@action", actionData);
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    
                }
               

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to database: {ex.Message}");
                throw; 
            }
        }
        public async Task insertFileName()
        {
            try
            {
                using (MySqlCommand inserquery = new MySqlCommand("INSERT INTO `brand_synch_2`.`tbl_variance_file`(`File_id`,`File_name`,`Var_type`,`Status`,`Done_by`,`created_at`)" +
                    "VALUES(@maxid,@fileName,@var,@status,@by,@created)", _conn.connection))
                {
                    Console.WriteLine(GlobalVar.gfile_name);
                    inserquery.Parameters.AddWithValue("@maxid", GlobalVar.maxID);
                    inserquery.Parameters.AddWithValue("@fileName", GlobalVar.gfile_name);
                    inserquery.Parameters.AddWithValue("@var", "maya");
                    inserquery.Parameters.AddWithValue("@status", "Ongoing");
                    inserquery.Parameters.AddWithValue("@by", GlobalVar.user);
                    inserquery.Parameters.AddWithValue("@created", DateTime.Now);
                    await inserquery.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show("insert file " + ex.Message + ex.ToString());
            }
        }

        public async Task updateFileName()
        {
            try
            {
                using (MySqlCommand inserquery = new MySqlCommand("UPDATE `brand_synch_2`.`tbl_variance_file` SET `Status` = @status, `end_at` = @created WHERE `file_id` = @maxid", _conn.connection))
                {       

                    inserquery.Parameters.AddWithValue("@maxid", GlobalVar.maxID);
                    inserquery.Parameters.AddWithValue("@status", "Done");
                    inserquery.Parameters.AddWithValue("@created", DateTime.Now);
                    await inserquery.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show("insert file " + ex.Message + ex.ToString());
            }
        }
        public async Task MaxId()
        {
            using (MySqlCommand selectmax = new MySqlCommand("SELECT MAX(id) AS max_id FROM brand_synch_2.tbl_variance_file", _conn.connection))
            {
                using (var reader = await selectmax.ExecuteReaderAsync())
                {
                    if (reader.Read() && !reader.IsDBNull(reader.GetOrdinal("max_id")))
                    {
                        GlobalVar.maxID = reader.GetInt32(reader.GetOrdinal("max_id")) + 1; // Increment the max id by 1
                    }
                    else
                    {
                        GlobalVar.maxID = 1;
                    }
                }         
            }

        }
        #endregion
        #region
        /*  public async Task ProcessExcelFileAsync(string filePath)
          {
              try
              {
                  Console.WriteLine("Starting to process Excel file.");
                  FileInfo fileInfo = new FileInfo(filePath);

                  using (ExcelPackage package = new ExcelPackage(fileInfo))
                  {
                      ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                      int rowCount = worksheet.Dimension.Rows;

                      for (int row = 2; row <= rowCount; row++)
                      {
                          string columnZ = worksheet.Cells[row, 26].Text; // Column Z
                          if (columnZ == "Refund to customer" || columnZ == "Load Reversal or Retry Payment Charging")
                          {
                              string columnW = worksheet.Cells[row, 23].Text; // Column W
                              string columnY = worksheet.Cells[row, 24].Text; // Column Y
                              string[] columnYParts = columnY.Split('|');

                              if (columnYParts.Length >= 1)
                              {
                                  string iload = columnYParts[0].Trim();
                                 // string remarks = GetRemarks(columnW, iload);

                                  await SaveToDatabaseAsync(columnW, iload, columnZ);
                                 // worksheet.Cells[row, 27].Value = remarks; // Update column AA with remarks

                                  Console.WriteLine($"Processed row {row}: columnW = {columnW}, iload = {iload}, action = {columnZ}");
                              }
                          }
                      }
                      package.Save();
                      Console.WriteLine("Excel file processed and saved.");
                  }
              }
              catch (Exception ex)
              {
                  Console.WriteLine($"Error processing Excel file: {ex.Message}");
                  MessageBox.Show("An error occurred while processing the Excel file. Check the log for details.");
              }
          }

      public string GetRemarks(string columnW, string iload)
        {
            if (columnW != "Not Found")
            {
                if (iload == "success")
                {
                    return "Not Subject for Refund";
                }
                else if (iload == "failed")
                {
                    return "Failed - for Refund";
                }
            }
            return "Not Subject for Refund";
        }*/


        #endregion

    }
}
