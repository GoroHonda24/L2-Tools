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
using Oracle.ManagedDataAccess.Client;

namespace L2_GLA
{
    public partial class frm_account : Form
    {
        MySqlCommand cmd,cmd1;
        DBconnect conn = new DBconnect();
        MySqlDataReader Reader;
        List<string> MIN = new List<string>();
        private OracleConnectionManager oracleConnectionManager;

        public frm_account()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
            oracleConnectionManager = new OracleConnectionManager();
        }

        private void frm_account_Load(object sender, EventArgs e)
        {
            load_data();
        }
        public class OracleConnectionManager
        {
            private OracleConnection connection;

            public OracleConnectionManager()
            {
                // Replace the connection details with your own
                string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.126.112.32)(PORT=1521)))(CONNECT_DATA=(SID=REPCTLG)));User Id=arbor;Password=arbor2020!;";

                connection = new OracleConnection(connectionString);
            }

            public OracleConnection GetOpenConnection()
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                return connection;
            }

            public void CloseConnection()
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                String filename = Path.GetFileName(selectedFilePath);
               // int csvcount = 0;
                    // Read and parse the CSV file using CsvHelper.
                    using (var reader = new StreamReader(selectedFilePath))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<MIN>().ToList(); // Replace YourDataClass with your custom class to map CSV columns.
                        using (OracleConnection connection = oracleConnectionManager.GetOpenConnection())
                        {
                            // Loop through the records and insert them into the database.
                            foreach (var record in records)
                            {
                                try
                                {
                                    // Adjust the SQL INSERT statement to match your table structure.
                                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO `brand_synch_2`.`account`(`min`,`app_transaction`,`created_at`)" +
                                        " VALUES (@Value1, @Value2,@Value3);", conn.connection))
                                    {
                                        // Add parameters to the command.
                                        cmd.Parameters.AddWithValue("@Value1", record.number);
                                        cmd.Parameters.AddWithValue("@Value2", record.app_transaction_number);
                                        cmd.Parameters.AddWithValue("@Value3", record.created_at);
                                        cmd.ExecuteNonQuery();
                                    }

                                    // Perform database operations here
                                    // For example, execute a query using OracleCommand
                                    using (OracleCommand oralcmd = new OracleCommand("select * from (select a.external_id as min,b.external_id,a.inactive_date,b.external_id_type from external_id_equip_map@ctlg a," +
                                        "customer_id_acct_map@cust1 b where a.account_no = b.account_no union all select a.external_id as min, b.external_id, a.inactive_date, b.external_id_type from external_id_equip_map " +
                                        "@ctlg a, customer_id_acct_map@cust2 b where a.account_no = b.account_no union all select a.external_id as min, b.external_id, a.inactive_date, b.external_id_type from " +
                                        "external_id_equip_map @ctlg a, customer_id_acct_map@cust3 b where a.account_no = b.account_no) where min in (:minParam) and inactive_date is null " +
                                        "and external_id_type = 1 ", connection))
                                    {
                                        oralcmd.Parameters.Add(":minParam", OracleDbType.Varchar2).Value = record.number;
                                        using (OracleDataReader oralreader = oralcmd.ExecuteReader())
                                        {
                                            while (oralreader.Read())
                                            {
                                            //cmd1 = new MySqlCommand("INSERT INTO `brand_synch_2`.`external`(`MIN`)values ('" + oralreader["EXTERNAL_ID"] + "')", conn.connection);
                                            cmd1 = new MySqlCommand("UPDATE account SET account ='" + oralreader["EXTERNAL_ID"] + "'  WHERE min = '" + oralreader["MIN"] + "'", conn.connection);
                                            cmd1.ExecuteNonQuery();
                                        }
                                            
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("An error occurred: " + ex.Message);
                                }
                            }
                        }
                    }               
            }
            load_data();

        }          

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                String filename = Path.GetFileName(selectedFilePath);


                try
                {
                    int csvcount = 0;


                    // Read and parse the CSV file using CsvHelper.
                    using (var reader = new StreamReader(selectedFilePath))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<account>().ToList(); // Replace YourDataClass with your custom class to map CSV columns.

                        // Loop through the records and insert them into the database.
                        foreach (var record in records)
                        {
                            // Adjust the SQL INSERT statement to match your table structure.
                            cmd = new MySqlCommand("INSERT INTO `brand_synch_2`.`external`(`MIN`,`EXTERNAL_ID`,`INACTIVE_DATE`,`EXTERNAL_ID_TYPE`)" +
                                " VALUES (@Value1, @Value2,@Value3,@Value4);", conn.connection);

                            // Add parameters to the command.
                            cmd.Parameters.AddWithValue("@Value1", record.MIN);
                            cmd.Parameters.AddWithValue("@Value2", record.EXTERNAL_ID);
                            cmd.Parameters.AddWithValue("@Value3", record.INACTIVE_DATE);
                            cmd.Parameters.AddWithValue("@Value4", record.EXTERNAL_ID_TYPE);
                            cmd.ExecuteNonQuery();



                            csvcount++;

                        }

                    }
                    MessageBox.Show("Total of " + csvcount + " Merchant Settlement record has been save");
                    csvcount = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }

            cmd = new MySqlCommand("update account inner join external ON account.min = external.min set account.account = external.EXTERNAL_ID", conn.connection);
            cmd.ExecuteNonQuery();

            load_data();

        }
        public void load_data()
        {
       

            cmd = new MySqlCommand("SELECT * FROM brand_synch_2.account", conn.connection);
            // cmd.Parameters.AddWithValue
            Reader = cmd.ExecuteReader();
            dgvlist.Rows.Clear();
            dgvlist.AllowUserToAddRows = true;
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();
                    row.Cells[0].Value = Reader["id"];
                    row.Cells[1].Value = Reader["min"];
                    row.Cells[2].Value = Reader["app_transaction"];
                    row.Cells[3].Value = Reader["account"];
                    row.Cells[4].Value = Reader["created_at"];
                    dgvlist.Rows.Add(row);


                }
                dgvlist.AllowUserToAddRows = false;

            }
            Reader.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to truncate table?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                cmd = new MySqlCommand("TRUNCATE TABLE account;", conn.connection);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("TRUNCATE TABLE external;", conn.connection);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successful Truncate table");
                load_data();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgvlist.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "SMART APP_(REJECTED 0).csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dgvlist.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[dgvlist.Rows.Count + 1];
                            for (int i = 1; i < columnCount; i++)
                            {
                                columnNames += dgvlist.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < dgvlist.Rows.Count; i++)
                            {
                                for (int j = 1; j < columnCount; j++)
                                {
                                    outputCsv[i] += dgvlist.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                }
                            }

                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }
    }
} 


        
