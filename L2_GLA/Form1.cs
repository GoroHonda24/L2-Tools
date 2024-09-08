using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using L2_GLA.Variance;

namespace L2_GLA
{
    public partial class Form1 : Form
    {
       
        DBconnect conn = new DBconnect();
       
        public Form1()
        {            
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.109.183.200)(PORT=1521)))(CONNECT_DATA=(SID=vloltp11)));User Id=T_HMBORJA;Password=T_Hmborja_123";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string searchTerm = txtelp.Text;
                    string[] searchTerms = searchTerm.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    // Construct the SQL query dynamically with the search terms
                    string query = "SELECT DECODE(voidcode, '0000', 'SUCCESSFUL', 'FAILED') AS \"STATUS\", (SELECT de.denom_name FROM oltp_eload_user.EDB_DENOMS_v de WHERE de.plancode = lg.plancode) AS \"DENOM_NAME\", " +
                        "(SELECT cf.val1 FROM oltp_eload_user.EDB_APPLICATION_CONFIGS_v cf WHERE cf.application_code = 'EDB' AND cf.name = 'System Channel ID' AND cf.key = NVL(SUBSTR(NULL, 1, 3), SUBSTR(txn_rrn, 1, 3))) AS \"CHANNEL\", " +
                        "NVL(SUBSTR(evc_rrn, 4), txn_rrn) AS \"REFERENCE_NUMBER\" " +
                        "FROM oltp_eload_user.rtl_txn_logs lg " +
                        "WHERE txn_end BETWEEN to_timestamp(:start_timestamp, 'YYYYMMDDHH24MISS.FF6') AND to_timestamp(:end_timestamp, 'YYYYMMDDHH24MISS.FF6')  ";

                    // Add the search term filter to the SQL query
                    if (searchTerms.Length > 0)
                    {
                        query += "AND SUBSTR(evc_rrn, 4) IN (" + string.Join(", ", searchTerms.Select(t => $"'{t.Trim()}'")) + ")";
                    }

                    using (OracleCommand cmd = new OracleCommand(query, connection))
                    {
                        // Format the dateTimePicker1 value
                        string formattedDateTime = dtpto.Value.ToString("yyyyMMdd") + "000000.000000";
                        cmd.Parameters.Add(":start_timestamp", OracleDbType.Varchar2).Value = formattedDateTime;
                        formattedDateTime = dtpto.Value.AddDays(2).ToString("yyyyMMdd") + "235959.999999";
                        cmd.Parameters.Add(":end_timestamp", OracleDbType.Varchar2).Value = formattedDateTime;

                        using (OracleDataReader Reader = cmd.ExecuteReader())
                        {
                            // Clear any existing rows in the dataGridView1
                            dataGridView1.Rows.Clear();

                            // Iterate through each row in the database result
                            while (Reader.Read())
                            {
                                // Add a new row to the dataGridView1 for each result
                                dataGridView1.Rows.Add(Reader["REFERENCE_NUMBER"], Reader["STATUS"]);
                            }
                        }
                        dataGridView1.AllowUserToAddRows = false;
                    }

                    connection.Close();
                    MessageBox.Show("successfully.");

                    // Perform database operations here
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dtpto_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DatetimeModal ojbform = new DatetimeModal();
            ojbform.ShowDialog();

            // Accessing the values from the modal form  
            DateTime datefrom = DatetimeModal.datefrom; // Ensure this is the correct reference.  
            DateTime dateto = DatetimeModal.dateto;

            // Ensure the dates are correctly set before formatting.  
            if (datefrom != default && dateto != default)
            {
                string formattedDateFrom = datefrom.ToString("yyyyMMdd") + "000000.000000"; // Start of the day  
                string formattedDateTo = dateto.ToString("yyyyMMdd") + "235959.999999"; // End of the day after adding 2 days  

                // Output for debugging  
                System.Diagnostics.Debug.WriteLine($"Formatted From: {formattedDateFrom}");
                System.Diagnostics.Debug.WriteLine($"Formatted To: {formattedDateTo}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Date values are not initialized correctly.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatetimeModal ojbform = new DatetimeModal();
            ojbform.ShowDialog();

            // Accessing the values from the modal form  
            DateTime datefrom = DatetimeModal.datefrom; // Ensure this is the correct reference.  
            DateTime dateto = DatetimeModal.dateto;

            // Ensure the dates are correctly set before formatting.  
            if (datefrom != default(DateTime) && dateto != default(DateTime))
            {
                string formattedDateFrom = datefrom.ToString("yyyyMMdd") + "000000.000000"; // Start of the day  
                string formattedDateTo = dateto.ToString("yyyyMMdd") + "235959.999999"; // End of the day after adding 2 days  

                // Output for debugging  
                System.Diagnostics.Debug.WriteLine($"Formatted From: {formattedDateFrom}");
                System.Diagnostics.Debug.WriteLine($"Formatted To: {formattedDateTo}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Date values are not initialized correctly.");
            }
        }
    }
}
