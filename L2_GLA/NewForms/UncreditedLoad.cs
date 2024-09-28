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
using System.Drawing;

namespace L2_GLA.NewForms
{
    internal class UncreditedLoad
    {
        private readonly DBconnect conn;

        public UncreditedLoad()
        {
            conn = new DBconnect();
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
        }

        public async void ReadExcelFile(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Get first worksheet  

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Assuming row 1 is header  
                {
                    string column1 = worksheet.Cells[row, 1].Text; // Change based on your columns  
                    string column2 = worksheet.Cells[row, 2].Text; // Change based on your columns  
                    // Continue for other columns...  

                  await InsertDataIntoDatabase(column1, column2);
                }
            }
        }

        private async Task InsertDataIntoDatabase(string column1, string column2)
        {
            string query = "INSERT INTO YourTable (Column1, Column2) VALUES (@column1, @column2)";

            using (MySqlCommand cmd = new MySqlCommand(query, conn.connection))
            {
                cmd.Parameters.AddWithValue("@column1", column1);
                cmd.Parameters.AddWithValue("@column2", column2);
                // Add other parameters as needed...  

                await cmd.ExecuteNonQueryAsync();
            }
        }

    }
}
