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
using Microsoft.VisualBasic;
using System.Linq.Expressions;

namespace L2_GLA.Model
{
    internal class variance
    {
        private readonly DBconnect _conn;
       // private int count = 0;
        private int action = 0, app = 0, iload = 0, remark = 0;
       // private string table = "";
        //List<string> appTransactionNumbers = new List<string>();
        //List<string> transStatus = new List<string>();


        string IloadElp, typeofvariance="";

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
                        typeofvariance = reader.GetString(5);
                    }
                }
            }
        }

        public async Task formattoquery(string filename, string vartype)
        {
            try
            {
                //HashSet<string> app_value = new HashSet<string>();
                //HashSet<string> iload_value = new HashSet<string>();
                List<string> app_value = new List<string>();
                List<string> iload_value = new List<string>();


                if (string.IsNullOrEmpty(GlobalVar.filepath))
                {
                    FileInfo fileInfo = new FileInfo(filename);
                    
                   // Console.WriteLine($"{filename} : Updating file {vartype}");
                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                        {
                            System.Windows.MessageBox.Show("Please check the Excel file. Sheet is empty");
                            return;
                        }

                        if (worksheet.Dimension == null)
                        {
                            System.Windows.MessageBox.Show("Please check the Excel file. Sheet is empty");
                            return;
                        }
                        GlobalVar.filepath = fileInfo.FullName;
                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (vartype == "maya")
                            {
                                //table = "tbl_variance_maya";
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
                                        await SaveToDatabaseAsync(columnW, iload, columnZ, null);
                                       // Console.WriteLine($"Processed row {row}: columnW = {columnW}, iload = {iload}, action = {columnZ}");
                                    }
                                }
                            }
                            else if (vartype == "gcash")
                            {
                                // table = "tbl_variance_gcash";
                                string clmAction = worksheet.Cells[row, action].Text;
                                if (clmAction == "Refund to Customer" || clmAction == "Load Reversal or Retry Payment Charging")
                                {
                                    string refundcol = worksheet.Cells[row, iload].Text;
                                    string clmAppTrans = worksheet.Cells[row, app].Text; // AF column is the 32nd column
                                    string[] splitValues = clmAppTrans.Split('|');
                                    if (splitValues.Length >= 0)
                                    {
                                        string app = splitValues[0].Trim();
                                        app_value.Add(app);
                                        string iload = splitValues[2].Trim();
                                        iload_value.Add(iload);

                                        await SaveToDatabaseAsync(app, iload, clmAction, refundcol);
                                        //Console.WriteLine($"Processed row {row}: columnW = {app}, iload = {iload}, action = {clmAction}");
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
                    //Console.WriteLine($"{filename} : Updating file {typeofvariance}");
                    FileInfo fileInfo = new FileInfo(filename);
                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                        {
                            System.Windows.MessageBox.Show("Please check the Excel file. Sheet is empty");
                            return;
                        }

                        if (worksheet.Dimension == null)
                        {
                            System.Windows.MessageBox.Show("Please check the Excel file. Sheet is empty");
                            return;
                        }

                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            if(typeofvariance == "maya")
                            {
                                 string columnZ = worksheet.Cells[row, action].Text; // Column Z
                                if (columnZ == "Refund to customer" || columnZ == "Load Reversal or Retry Payment Charging")
                                {
                                    string columnW = worksheet.Cells[row, app].Text; // Column W
                                   // app_value.Add(columnW);

                                    string columnY = worksheet.Cells[row, iload].Text; // Column Y
                                    string[] columnYParts = columnY.Split('|');

                                    if (columnYParts.Length >= 1)
                                    {
                                        string iload = columnYParts[0].Trim();
                                        string query;
                                        if (columnW != "Not Found")
                                        {
                                             query = $"SELECT remarks FROM {GlobalVar.tableName} WHERE app_transaction = @columnW";
                                        }else
                                        {
                                            query = $"SELECT remarks FROM {GlobalVar.tableName} WHERE iload = @iload";
                                        }                                    

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
                                                    //System.Diagnostics.Debug.WriteLine($"Row {row} App {columnW} iload {iload}: Updated Column AA with remarks: {remarks}");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (typeofvariance == "gcash")
                            {
                                string clmAction = worksheet.Cells[row, action].Text;
                                if (clmAction == "Refund to Customer" || clmAction == "Load Reversal or Retry Payment Charging")
                                {
                                    string clmAppTrans = worksheet.Cells[row, app].Text; // AF column is the 32nd column
                                    string[] splitValues = clmAppTrans.Split('|');
                                    if (splitValues.Length >= 0)
                                    {
                                        string app = splitValues[0].Trim();                                      
                                        string iload = splitValues[2].Trim();
                                       // Console.WriteLine($"app : {app} iload : {iload}");
                                        string query;
                                        if (app.Length!=0)
                                        {
                                            query = $"SELECT remarks FROM {GlobalVar.tableName} WHERE app_transaction = @columnW";
                                        }
                                        else
                                        {
                                            query = $"SELECT remarks FROM {GlobalVar.tableName} WHERE iload = @iload";
                                        }

                                        using (MySqlCommand command = new MySqlCommand(query, _conn.connection))
                                        {
                                            command.Parameters.AddWithValue("@columnW", app);
                                            command.Parameters.AddWithValue("@iload", iload);

                                            using (var reader = await command.ExecuteReaderAsync())
                                            {
                                                if (await reader.ReadAsync())
                                                {
                                                    string remarks = reader["remarks"].ToString();
                                                    worksheet.Cells[row, remark].Value = remarks;
                                                    //System.Diagnostics.Debug.WriteLine($"table : {GlobalVar.tableName} : Row {row} App {app} iload {iload}: Updated Column AA with remarks: {remarks}");
                                                }
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
        //public async Task importSmartDatabase(string filePath, string variancetype)
        //{
        //    try
        //    {
        //        // Read and parse the CSV file using CsvHelper.
        //        using (var reader = new StreamReader(filePath))
        //        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        //        {
        //            var records = csv.GetRecords<mydb>().ToList();
        //            var processedIds = new HashSet<string>();

        //            foreach (var record in records)
        //            {
        //                string selectQuery = $"SELECT id FROM {GlobalVar.tableName} " +
        //                                     "WHERE app_transaction = @app OR iload = @iload";

        //                Console.WriteLine(GlobalVar.tableName + " : " + record.app_transaction_number + " : " + record.elp_transaction_number);

        //                var idsToUpdate = new List<string>();

        //                using (var cmdSelect = new MySqlCommand(selectQuery, _conn.connection))
        //                {
        //                    cmdSelect.Parameters.AddWithValue("@app", record.app_transaction_number);
        //                    cmdSelect.Parameters.AddWithValue("@iload", record.elp_transaction_number);

        //                    try
        //                    {
        //                        using (var dataReader = await cmdSelect.ExecuteReaderAsync())
        //                        {
        //                            while (await dataReader.ReadAsync())
        //                            {

        //                                string id = dataReader.GetString(0);
        //                                idsToUpdate.Add(id);

        //                            }
        //                        }
        //                    }
        //                    catch (MySqlException ex)
        //                    {                                
        //                        Console.WriteLine($"MySQL error (select): {ex.Message}");
        //                        throw;
        //                    }
        //                }

        //                foreach (var id in idsToUpdate)
        //                {


        //                    if (processedIds.Contains(id))
        //                    {
        //                        Console.WriteLine($"1st {variancetype} : {id} : {processedIds} : {record.app_transaction_number} : {record.status}");
        //                        continue;
        //                    }

        //                    processedIds.Add(id);
        //                    //Console.WriteLine($"Processing ID: {id}");
        //                    Console.WriteLine($"2nd {variancetype} : {id} : {processedIds} : {record.app_transaction_number} : {record.status}");
        //                    string newstatus = "";
        //                    if (variancetype == "maya")
        //                    {
        //                        if (record.status == "ELP_SUCCESSFUL")
        //                        {
        //                            // iload checking
        //                            newstatus = record.status;

        //                        }
        //                        else if (record.status != "ELP_SUCCESSFUL")
        //                        {
        //                            if (record.elp_transaction_number == "NULL" || record.elp_transaction_number is null)
        //                            {
        //                                newstatus = "Failed - for Refund";
        //                            }
        //                            else {
        //                                //iload checking
        //                                IloadElp = record.elp_transaction_number;

        //                                 await iloadquery();
        //                                 newstatus = iloadStatus;
        //                            }

        //                        }

        //                    }
        //                    else if (variancetype == "gcash")
        //                    {
        //                        Console.WriteLine(variancetype + " : " + record.status);
        //                        if (record.status != "ELP_SUCCESSFUL")
        //                        {                                    
        //                            if (record.elp_transaction_number == "NULL" || record.elp_transaction_number is null)
        //                            {
        //                                using (MySqlCommand selectquery = new MySqlCommand("SELECT MERCHANT_TRANS_ID, TRANSACTION_TYPE FROM brand_synch_2.tbl_merchant where TRANSACTION_DATETIME >= @from and TRANSACTION_DATETIME <= @to and MERCHANT_TRANS_ID like @apptrans", _conn.connection))
        //                                {
        //                                    selectquery.Parameters.AddWithValue("@from", DatetimeModal.datefrom.ToString("yyyy-MM-dd 00:00:00"));
        //                                    selectquery.Parameters.AddWithValue("@to", DatetimeModal.dateto.ToString("yyyy-MM-dd 23:59:59"));
        //                                   Console.WriteLine( DatetimeModal.datefrom.ToString("yyyy-MM-dd 00:00:00"));
        //                                    Console.WriteLine(DatetimeModal.dateto.ToString("yyyy-MM-dd 23:59:59"));

        //                                    selectquery.Parameters.AddWithValue("@apptrans", "%" + record.app_transaction_number);
        //                                    using (var readerResult = await selectquery.ExecuteReaderAsync())
        //                                    {
        //                                        while (await readerResult.ReadAsync())
        //                                        {
        //                                            string StatusType = readerResult["TRANSACTION_TYPE"].ToString();

        //                                            if (StatusType == "PAYMENT")
        //                                            {
        //                                                newstatus = "Failed for Refund";
        //                                            }
        //                                            else if (StatusType == "REFUND")
        //                                            {
        //                                                newstatus = "Not Subject for Refund";
        //                                            }
        //                                            else
        //                                            {
        //                                                newstatus = "Need more Investigation";
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                //iload checking
        //                                IloadElp = record.elp_transaction_number;

        //                                await iloadquery();
        //                                newstatus = iloadStatus;
        //                            }
        //                        }
        //                        else if (record.status == "ELP_SUCCESSFUL")
        //                        {
        //                            newstatus = record.status;
        //                        }
        //                        else
        //                        {
        //                            newstatus = "Not Subject for Refund";
        //                        }

        //                    }


        //                    // Update the existing record
        //                    string updateQuery = $"UPDATE {GlobalVar.tableName} " +
        //                                         "SET `app_transaction` = @app, `iload` = @iload, `dbStatus` = @status, `remarks` = @remarks " +
        //                                         "WHERE `id` = @id ";

        //                    using (var cmdUpdate = new MySqlCommand(updateQuery, _conn.connection))
        //                    {
        //                        cmdUpdate.Parameters.AddWithValue("@id", id);
        //                        cmdUpdate.Parameters.AddWithValue("@app", record.app_transaction_number);
        //                        cmdUpdate.Parameters.AddWithValue("@iload", record.elp_transaction_number);
        //                        cmdUpdate.Parameters.AddWithValue("@status", record.status);
        //                        cmdUpdate.Parameters.AddWithValue("@remarks", newstatus);

        //                        try
        //                        {
        //                            await cmdUpdate.ExecuteNonQueryAsync();
        //                        }
        //                        catch (MySqlException ex)
        //                        {
        //                            // Log and rethrow the exception for the update command
        //                            Console.WriteLine($"MySQL error (update): {ex.Message}");
        //                            throw;
        //                        }
        //                    }

        //                }
        //            }
        //        }
        //        using (MySqlCommand updateRemarks = new MySqlCommand($"UPDATE {GlobalVar.tableName} SET remarks = 'Not Subject for Refund' WHERE app_transaction = '' or app_transaction = 'Not Found'", _conn.connection))
        //        {
        //            await updateRemarks.ExecuteNonQueryAsync();
        //        }

        //        await formattoquery(filePath, null);
        //    }
        //    catch (MySqlException ex)
        //    {
        //        // Log MySQL-specific exceptions
        //        Console.WriteLine($"MySQL error: {ex.Message}");
        //        System.Windows.MessageBox.Show("A database error occurred: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log general exceptions
        //        Console.WriteLine($"General error: {ex.Message}");
        //        System.Windows.MessageBox.Show("An error occurred: " + ex.Message);
        //    }
        //}

        public async Task ImportSmartDatabaseAsync(string filePath, string variancetype)
        {
            try
            {
                // Read and parse the CSV file using CsvHelper.
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<mydb>().ToList();
                    var processedIds = new HashSet<string>();
               
                    foreach (var record in records)
                    {
                        var idsToUpdate = await GetIdsToUpdate(record);

                        foreach (var id in idsToUpdate)
                        {
                            if (processedIds.Contains(id))
                            {
                                continue;
                            }

                            processedIds.Add(id);
                            string newstatus = await GetNewStatusAsync(record, variancetype);

                            await UpdateDatabaseRecord(id, record, newstatus);
                        }
                    }

                    // Update remarks for records without a specific transaction
                    await UpdateRemarksAsync();
                }

                // Additional processing
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

        private async Task<List<string>> GetIdsToUpdate(mydb record)
        {
            var idsToUpdate = new List<string>();
            string selectQuery = $"SELECT id FROM {GlobalVar.tableName} WHERE app_transaction = @app OR iload = @iload";

            using (var cmdSelect = new MySqlCommand(selectQuery, _conn.connection))
            {
                cmdSelect.Parameters.AddWithValue("@app", record.app_transaction_number);
                cmdSelect.Parameters.AddWithValue("@iload", record.elp_transaction_number);

                using (var dataReader = await cmdSelect.ExecuteReaderAsync())
                {
                    while (await dataReader.ReadAsync())
                    {
                        idsToUpdate.Add(dataReader.GetString(0));
                    }
                }
            }

            return idsToUpdate;
        }

        private async Task<string> GetNewStatusAsync(mydb record, string variancetype)
        {
            string newstatus = record.status;

            if (variancetype == "maya" && record.status != "ELP_SUCCESSFUL")
            {
                
                if (string.IsNullOrEmpty(record.elp_transaction_number) || record.elp_transaction_number == "NULL")
                {
                   
                    newstatus = "Failed - For Refund";
                }
                else
                {
                    if (IloadAccountStatus == true)
                    {
                        IloadElp = record.elp_transaction_number;  
                        newstatus = await IloadQueryAsync();
                    }
                    else
                    {
                        newstatus = "No Transaction Found, Need Manual Checking in iload";
                    }
                    
                }

            }
            else if (variancetype == "gcash")
            {
                if (record.status != "ELP_SUCCESSFUL")
                {
                    if (string.IsNullOrEmpty(record.elp_transaction_number) || record.elp_transaction_number == "NULL")
                    {
                        newstatus = await CheckGcashStatusAsync(record);
                    }
                    else
                    {
                        IloadElp = record.elp_transaction_number;
                        newstatus = await IloadQueryAsync();
                    }
                }
                else
                {
                    newstatus =record.status;
                }
            }

            return newstatus;
        }


        private async Task<string> CheckGcashStatusAsync(mydb record)
        {
            string newstatus = "Need more Investigation";

            string selectQuery = "select refund from tbl_variance_gcash where app_transaction = @apptrans";
            using (var cmdSelect = new MySqlCommand(selectQuery, _conn.connection))
            {
                cmdSelect.Parameters.AddWithValue("@apptrans", record.app_transaction_number);

                using (var reader = await cmdSelect.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string statusType = reader["refund"].ToString();
                        Console.WriteLine($"app : {record.app_transaction_number} Status : {statusType}");
                        if (statusType == "Not Found")
                        {
                            newstatus = "Failed - For Refund";
                        }
                        else if (statusType == "Success")
                        {
                            newstatus = "Not Subject for Refund";
                        }
                    }
                }
            }

            return newstatus;
        }

        private async Task UpdateDatabaseRecord(string id, mydb record, string newstatus)
        {
            string updateQuery = $"UPDATE {GlobalVar.tableName} SET app_transaction = @app, iload = @iload, dbStatus = @status, remarks = @remarks WHERE id = @id";

            using (var cmdUpdate = new MySqlCommand(updateQuery, _conn.connection))
            {
                cmdUpdate.Parameters.AddWithValue("@id", id);
                cmdUpdate.Parameters.AddWithValue("@app", record.app_transaction_number);
                cmdUpdate.Parameters.AddWithValue("@iload", record.elp_transaction_number);
                cmdUpdate.Parameters.AddWithValue("@status", record.status);
                cmdUpdate.Parameters.AddWithValue("@remarks", newstatus);

                await cmdUpdate.ExecuteNonQueryAsync();
            }
        }

        private async Task UpdateRemarksAsync()
        {
            string updateQuery = $"UPDATE {GlobalVar.tableName} SET remarks = 'Not Subject for Refund' WHERE app_transaction = '' OR app_transaction = 'Not Found'";
            using (var cmdUpdate = new MySqlCommand(updateQuery, _conn.connection))
            {
                await cmdUpdate.ExecuteNonQueryAsync();
            }
        }
        #region

        //public async Task iloadquery()
        //{
        //    string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.109.183.200)(PORT=1521)))(CONNECT_DATA=(SID=vloltp11)));User Id=T_HMBORJA;Password=T_Hmborja_123";

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
        //                DateTime datefrom = DatetimeModal.datefrom; // Ensure this is the correct reference.  
        //                DateTime dateto = DatetimeModal.dateto;
        //                //Format the dateTimePicker1 value
        //                if (datefrom != default(DateTime) && dateto != default(DateTime))
        //                {
        //                    string formattedDateFrom = datefrom.ToString("yyyyMMdd") + "000000.000000"; // Start of the day  
        //                    string formattedDateTo = dateto.ToString("yyyyMMdd") + "235959.999999"; // End of the day after adding 2 days  
        //                    cmd.Parameters.Add(":start_timestamp", OracleDbType.Varchar2).Value = formattedDateFrom;
        //                    cmd.Parameters.Add(":end_timestamp", OracleDbType.Varchar2).Value = formattedDateTo;

        //                }
        //                else
        //                {
        //                    System.Diagnostics.Debug.WriteLine("Date values are not initialized correctly.");
        //                }


        //                cmd.Parameters.Add(":elp", OracleDbType.Varchar2).Value = IloadElp;
        //                using (OracleDataReader Reader = cmd.ExecuteReader())
        //                {
        //                    while (Reader.Read())
        //                    {                               
        //                            if (Reader["STATUS"].ToString() == "SUCCESSFUL")
        //                            {
        //                                iloadStatus = "Not Subject for Refund";
        //                            }
        //                            else
        //                            {
        //                                iloadStatus = "Failed - For Refund";
        //                            }                                   
        //                        }

        //                }
        //               // System.Windows.MessageBox.Show("successfully." + iloadStatus + " : " + IloadElp + "\n" + datefrom + " " + dateto);
        //            }

        //            connection.Close();


        //        }
        //        catch (Exception ex)
        //        {
        //            System.Windows.MessageBox.Show("Error: " + ex.Message);
        //        }
        //    }
        //}

        //public async Task<string> iloadquery()
        //{
        //    string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.109.183.000)(PORT=1521)))(CONNECT_DATA=(SID=vloltp11)));User Id=T_HMBORJA;Password=T_Hmborja_123";
        //    string iloadStatus = "Need more Investigation"; // Default status

        //    using (OracleConnection connection = new OracleConnection(connectionString))
        //    {
        //        try
        //        {
        //            await connection.OpenAsync();
        //            using (OracleCommand cmd = new OracleCommand("SELECT DECODE(voidcode, '0000', 'SUCCESSFUL', 'FAILED') AS \"STATUS\", (SELECT de.denom_name FROM oltp_eload_user.EDB_DENOMS_v de WHERE de.plancode = lg.plancode)" +
        //                     " AS \"DENOM_NAME\", (SELECT cf.val1 FROM oltp_eload_user.EDB_APPLICATION_CONFIGS_v cf WHERE cf.application_code = 'EDB' AND cf.name = 'System Channel ID' AND cf.key = NVL(SUBSTR(NULL, 1, 3), SUBSTR(txn_rrn, 1, 3)))" +
        //                     " AS \"CHANNEL\", NVL(SUBSTR(evc_rrn, 4), txn_rrn) AS \"REFERENCE_NUMBER\" FROM oltp_eload_user.rtl_txn_logs lg WHERE txn_end BETWEEN to_timestamp(:start_timestamp, 'YYYYMMDDHH24MISS.FF6')" +
        //                     " AND to_timestamp(:end_timestamp, 'YYYYMMDDHH24MISS.FF6') AND SUBSTR(evc_rrn, 4) in (:elp)", connection))
        //            {
        //                DateTime datefrom = DatetimeModal.datefrom; // Ensure this is the correct reference.  
        //                DateTime dateto = DatetimeModal.dateto;

        //                if (datefrom != default(DateTime) && dateto != default(DateTime))
        //                {
        //                    string formattedDateFrom = datefrom.ToString("yyyyMMdd") + "000000.000000"; // Start of the day  
        //                    string formattedDateTo = dateto.ToString("yyyyMMdd") + "235959.999999"; // End of the day  
        //                    cmd.Parameters.Add(":start_timestamp", OracleDbType.Varchar2).Value = formattedDateFrom;
        //                    cmd.Parameters.Add(":end_timestamp", OracleDbType.Varchar2).Value = formattedDateTo;
        //                }
        //                else
        //                {
        //                    System.Diagnostics.Debug.WriteLine("Date values are not initialized correctly.");
        //                }

        //                cmd.Parameters.Add(":elp", OracleDbType.Varchar2).Value = IloadElp;
        //                using (DbDataReader reader = await cmd.ExecuteReaderAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        if (reader["STATUS"].ToString() == "SUCCESSFUL")
        //                        {
        //                            iloadStatus = "Not Subject for Refund";
        //                        }
        //                        else
        //                        {
        //                            iloadStatus = "Failed - For Refund";
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            System.Windows.MessageBox.Show("Error: " + ex.Message);
        //            iloadStatus = "Error occurred";
        //        }
        //    }

        //    return iloadStatus; // Return the status
        //}

        //public async Task<string> IloadQuery()
        //{
        //    string iloadStatus = "No Transaction Found, Need Manual Checking in iload";

        //    for (int i = 0; i<=iloaduser.Count; i++)
        //    {
        //        string IloadUser = iloaduser[i];
        //        string IloadPass = iloadpass[i];
        //        string connectionString = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.109.183.200)(PORT=1521)))(CONNECT_DATA=(SID=vloltp11)));User Id={IloadUser};Password={IloadPass}";
        //        bool connectionSuccessful = false;

        //        while (!connectionSuccessful)
        //        {

        //            using (OracleConnection connection = new OracleConnection(connectionString))
        //            {
        //                try
        //                {
        //                    await connection.OpenAsync();
        //                    connectionSuccessful = true; // Connection succeeded, exit the loop
        //                    using (OracleCommand cmd = new OracleCommand("SELECT DECODE(voidcode, '0000', 'SUCCESSFUL', 'FAILED') AS \"STATUS\", (SELECT de.denom_name FROM oltp_eload_user.EDB_DENOMS_v de WHERE de.plancode = lg.plancode)" +
        //                        " AS \"DENOM_NAME\", (SELECT cf.val1 FROM oltp_eload_user.EDB_APPLICATION_CONFIGS_v cf WHERE cf.application_code = 'EDB' AND cf.name = 'System Channel ID' AND cf.key = NVL(SUBSTR(NULL, 1, 3), SUBSTR(txn_rrn, 1, 3)))" +
        //                        " AS \"CHANNEL\", NVL(SUBSTR(evc_rrn, 4), txn_rrn) AS \"REFERENCE_NUMBER\" FROM oltp_eload_user.rtl_txn_logs lg WHERE txn_end BETWEEN to_timestamp(:start_timestamp, 'YYYYMMDDHH24MISS.FF6')" +
        //                        " AND to_timestamp(:end_timestamp, 'YYYYMMDDHH24MISS.FF6') AND SUBSTR(evc_rrn, 4) in (:elp)", connection))
        //                    {
        //                        DateTime datefrom = DatetimeModal.datefrom; // Ensure this is the correct reference.  
        //                        DateTime dateto = DatetimeModal.dateto;

        //                        if (datefrom != default(DateTime) && dateto != default(DateTime))
        //                        {
        //                            string formattedDateFrom = datefrom.ToString("yyyyMMdd") + "000000.000000"; // Start of the day  
        //                            string formattedDateTo = dateto.ToString("yyyyMMdd") + "235959.999999"; // End of the day  
        //                            cmd.Parameters.Add(":start_timestamp", OracleDbType.Varchar2).Value = formattedDateFrom;
        //                            cmd.Parameters.Add(":end_timestamp", OracleDbType.Varchar2).Value = formattedDateTo;
        //                        }
        //                        else
        //                        {
        //                            System.Diagnostics.Debug.WriteLine("Date values are not initialized correctly.");
        //                        }

        //                        cmd.Parameters.Add(":elp", OracleDbType.Varchar2).Value = IloadElp;
        //                        using (DbDataReader reader = await cmd.ExecuteReaderAsync())
        //                        {
        //                            while (await reader.ReadAsync())
        //                            {
        //                                if (reader["STATUS"].ToString() == "SUCCESSFUL")
        //                                {
        //                                    iloadStatus = "Not Subject for Refund";
        //                                }
        //                                else if (reader["STATUS"].ToString() == "FAILED")
        //                                {
        //                                    iloadStatus = "Failed - For Refund";
        //                                }
        //                                else
        //                                {
        //                                    iloadStatus = "No Transaction Found, Need Manual Checking in iload";
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    System.Windows.MessageBox.Show("Error: " + ex.Message);

        //                        iloadStatus = "Error occurred and user cancelled";
        //                        return iloadStatus; // User cancelled
        //                }
        //            }
        //        }
        //    }
        //    return iloadStatus; // Return the status
        //}
        #endregion

       private List<string> iloaduser = new List<string>();
       private List<string> iloadpass = new List<string>();
       private bool IloadAccountStatus = true;
        public async Task IloadAccount()
        {
            using (MySqlCommand cmdLoad = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_variance_iload_account",_conn.connection))
            {
                using (var Reader = await cmdLoad.ExecuteReaderAsync())
                {
                    while (Reader.Read())
                    {
                        iloaduser.Add(Reader.GetString(1));
                        iloadpass.Add(Reader.GetString(2));

                    }
                }
            }
        }

        private async Task<OracleConnection> GetWorkingConnectionAsync()
        {
            
            OracleConnection workingConnection = null;
            int maxAttempts = 3; // Limit attempts to 3
            int attemptCount = 0;

            for (int i = 0; i < iloaduser.Count && attemptCount < maxAttempts; i++) // Limit the loop to maxAttempts
            {
                string IloadUser = iloaduser[i];
                string IloadPass = iloadpass[i];
                string connectionString = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.109.183.200)(PORT=1521)))(CONNECT_DATA=(SID=vloltp11)));User Id={IloadUser};Password={IloadPass}";

                try
                {
                    workingConnection = new OracleConnection(connectionString);
                    await workingConnection.OpenAsync(); // Try to open the connection

                    // If connection is successful, return the working connection
                    System.Diagnostics.Debug.WriteLine($"Connection successful for {IloadUser}");
                    IloadAccountStatus = true;
                    return workingConnection;  // Exit the loop and return the successful connection
                }
                catch (Exception ex)
                {
                    attemptCount++; // Increment the attempt count
                    System.Diagnostics.Debug.WriteLine($"Connection failed for {IloadUser}: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Attempts: {attemptCount}/{maxAttempts}");

                    // Continue to the next account if connection fails
                }
            }

            // If no connection was successful, return null
            System.Diagnostics.Debug.WriteLine("Reached maximum number of attempts (3). Stopping loop.");
            IloadAccountStatus = false;
            return null;
        }


        // Main function to execute IloadQuery with a successful connection
        public async Task<string> IloadQueryAsync()
        {
            
            string iloadStatus = "No Transaction Found, Need Manual Checking in iload";

            // First, try to establish a working connection
            OracleConnection connection = await GetWorkingConnectionAsync();

            if (connection == null)
            {
                // No working connection was found, return the default status
                return iloadStatus;
            }

            // If we have a working connection, proceed with the query
            try
            {
               
                using (OracleCommand cmd = new OracleCommand("SELECT DECODE(voidcode, '0000', 'SUCCESSFUL', 'FAILED') AS \"STATUS\", (SELECT de.denom_name FROM oltp_eload_user.EDB_DENOMS_v de WHERE de.plancode = lg.plancode)" +
                             " AS \"DENOM_NAME\", (SELECT cf.val1 FROM oltp_eload_user.EDB_APPLICATION_CONFIGS_v cf WHERE cf.application_code = 'EDB' AND cf.name = 'System Channel ID' AND cf.key = NVL(SUBSTR(NULL, 1, 3), SUBSTR(txn_rrn, 1, 3)))" +
                             " AS \"CHANNEL\", NVL(SUBSTR(evc_rrn, 4), txn_rrn) AS \"REFERENCE_NUMBER\" FROM oltp_eload_user.rtl_txn_logs lg WHERE txn_end BETWEEN to_timestamp(:start_timestamp, 'YYYYMMDDHH24MISS.FF6')" +
                             " AND to_timestamp(:end_timestamp, 'YYYYMMDDHH24MISS.FF6') AND SUBSTR(evc_rrn, 4) in (:elp)", connection))
                {
                    DateTime datefrom = DatetimeModal.datefrom; // Ensure this is the correct reference.  
                    DateTime dateto = DatetimeModal.dateto;
                    //Format the dateTimePicker1 value
                    if (datefrom != default(DateTime) && dateto != default(DateTime))
                    {
                        string formattedDateFrom = datefrom.ToString("yyyyMMdd") + "000000.000000"; // Start of the day  
                        string formattedDateTo = dateto.ToString("yyyyMMdd") + "235959.999999"; // End of the day after adding 2 days  
                        cmd.Parameters.Add(":start_timestamp", OracleDbType.Varchar2).Value = formattedDateFrom;
                        cmd.Parameters.Add(":end_timestamp", OracleDbType.Varchar2).Value = formattedDateTo;

                    }
                    


                    cmd.Parameters.Add(":elp", OracleDbType.Varchar2).Value = IloadElp;
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (reader["STATUS"].ToString() == "SUCCESSFUL")
                            {
                                iloadStatus = "Not Subject for Refund";
                            }
                            else if (reader["STATUS"].ToString() == "FAILED")
                            {
                                iloadStatus = "Failed - For Refund";
                            }
                            else
                            {
                                iloadStatus = "No Transaction Found, Need Manual Checking in iload";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                iloadStatus = $"Error occurred during query execution. \n {ex.Message} \n {ex.ToString()}";
            }
            finally
            {
                // Close the connection
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close(); // Use synchronous Close() method
                    connection.Dispose(); // Dispose of the connection after closing
                }
            }


            return iloadStatus;
        }

        private Tuple<string, string> PromptForCredentials()
        {
            // Prompt user for new username and password
            // This is just an example using a simple input dialog
            var userNameInput = Microsoft.VisualBasic.Interaction.InputBox("Enter new User ID:", "User ID", "");
            var passwordInput = Microsoft.VisualBasic.Interaction.InputBox("Enter new Password:", "Password", "");

            if (string.IsNullOrEmpty(userNameInput) || string.IsNullOrEmpty(passwordInput))
            {
                return null; // User cancelled or did not provide input
            }

            return new Tuple<string, string>(userNameInput, passwordInput);
        }


        public async Task SaveToDatabaseAsync(string appTransactionData, string iloadData, string actionData, string refundData)
        {
            try
            {   

                string query = $"INSERT INTO {GlobalVar.tableName}(`file_id`,`app_transaction`,`refund`,`iload`,`action`)VALUES(@maxid, @app_transaction, @refund, @iload, @action)";
                using (MySqlCommand cmd = new MySqlCommand(query, _conn.connection))
                {

                    cmd.Parameters.AddWithValue("@maxid", GlobalVar.maxID);
                    cmd.Parameters.AddWithValue("@app_transaction", appTransactionData);
                    cmd.Parameters.AddWithValue("@refund", refundData);
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
