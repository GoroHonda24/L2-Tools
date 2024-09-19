using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;

namespace L2_GLA.BrandSynch
{
    public partial class InsertBrand : Form
    {
        private readonly MySqlCommand cmd;
        private readonly DBconnect conn = new DBconnect();
        private MySqlDataReader reader;
        private readonly List<string> min = new List<string>();
        private string brand_id, brand_name, brand_desc;
        public InsertBrand()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
        }

        private void CmbBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (brandData.ContainsKey(CmbBrand.Text))
            {
                var brandInfo = brandData[CmbBrand.Text];
                brand_id = brandInfo.id;
                brand_name = brandInfo.name;
                brand_desc = brandInfo.desc;
            }
        }

        private void txtmin_TextChanged(object sender, EventArgs e)
        {
          //  mincount.Text = txtmin.Text.Length.ToString();
            string searchTerm = txtmin.Text;
            string[] searchTerms = searchTerm.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            btnGen.Text = searchTerms.Length > 1 ? "Export" : "Generate";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            incid.SelectedIndex = -1;
            incid.Text = "";
            txtsubject.Clear();
            CmbBrand.SelectedIndex = -1;
            txtmin.Clear();
            btnGen.Enabled = true;
            txtformat.Clear();
            txtspiel.Clear();
            min.Clear();
        }

        private async void btnGen_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string[] searchTerms = txtmin.Text.Replace("\n", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            long todayUnixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            if (btnGen.Text == "Generate")
            {
                await GenerateLogEntry(todayUnixTimestamp);
            }
            else
            {
                await ExportBulkLogs(searchTerms, todayUnixTimestamp);
            }

            btnGen.Enabled = false;
        }

        private void InsertBrand_Load(object sender, EventArgs e)
        {

        }
        #region Class
        private readonly Dictionary<string, (string id, string name, string desc)> brandData = new Dictionary<string, (string, string, string)>()
        {
            {"TNT PREPAID", ("TNT", "TNT Prepaid", "TNT")},
            {"SMART PREPAID", ("BUDDY", "Smart Prepaid", "Smart Prepaid")},
            {"SMART POSTPAID", ("POSTPD", "Smart Postpaid", "Smart Postpaid")},
            {"SMART BRO PREPAID", ("BROPRE", "SmartBro Prepaid", "Smart Bro Prepaid")},
            {"SMART BRO POSTPAID", ("BROPOS", "SmartBro Postpaid", "Smart Bro Postpaid")},
            {"HOME WIFI PREPAID", ("PHPW", "Home Wifi Prepaid", "Home Wifi Prepaid")}
        };

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(incid.Text))
            {
                MessageBox.Show("Please Input INC", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtsubject.Enabled && string.IsNullOrWhiteSpace(txtsubject.Text))
            {
                MessageBox.Show("Please Input Subject from email", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtmin.Text.Length <= 9)
            {
                MessageBox.Show("Please Input Proper MIN size", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(CmbBrand.Text))
            {
                MessageBox.Show("Please select BRAND", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private async Task GenerateLogEntry(long todayUnixTimestamp)
        {
            try
            {
                string query = "SELECT * FROM min_logs WHERE min = @min AND incid = @incid ORDER BY id DESC LIMIT 1";
                using (var cmd = new MySqlCommand(query, conn.connection))
                {
                    cmd.Parameters.AddWithValue("@min", txtmin.Text);
                    cmd.Parameters.AddWithValue("@incid", incid.Text);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            string user = reader["user"].ToString();
                            string action = reader["action"].ToString();
                            string timestamp = reader["timestamp"].ToString();

                            DialogResult result = MessageBox.Show($"{user} has already performed {action} on this MIN with the same INC ID at {timestamp}. Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result != DialogResult.Yes)
                            {
                                return;
                            }
                        }
                    }
                    await InsertLogEntry(todayUnixTimestamp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async Task InsertLogEntry(long todayUnixTimestamp)
        {
            string format = $",\r\n\"min\": \"{txtmin.Text}\",\r\n\"brand_id\": \"{brand_id}\",\r\n\"brand_name\": \"{brand_name}\",\r\n\"brand_description\": \"{brand_desc}\",\r\n\"is_active\": true,\r\n\"creation_timestamp\": {todayUnixTimestamp},\r\n\"last_update_timestamp\": {todayUnixTimestamp}";
            txtformat.Text = format;

            //txtspiel.Text = $"Done processing the WA for this MIN {txtmin.Text}. Please ask the Subscriber to retry the transaction.\r\nWe will now proceed in closing this ticket.";
            txtspiel.Text = ("Done processing the WA for this MIN " + txtmin.Text + ". Please ask the Subscriber to retry the transaction." +
                            "\r\nWe will now proceed in closing this ticket." +
                            "\r\nIf the issue is still persistent, reopen with a screenshot of the error encountered." +
                            "\r\n" +
                            "\r\nIssue: Brand Sync Issue. Brand is aligned in Postman (Internal, External and GL) but Mongo DB is not searchable." +
                            "\r\nBrand Sync using Insert Document. " +
                            "\r\nError:" +
                            "\r\nInvestigation Note:" +
                            "\r\n" +
                            "\r\nMIN: " + txtmin.Text + "" +
                            "\r\nExpected MIN: " + CmbBrand.Text + "" +
                            "\r\nInternal: " + CmbBrand.Text + "" +
                            "\r\nExternal: " + CmbBrand.Text + "" +
                            "\r\nGL: " + CmbBrand.Text + "" +
                            "\r\n" +
                            "\r\n" +
                            "\r\nNote: Please make sure that your Smart Application is on the latest version. Thank you! ");
            // Insert log into database
            string insertQuery = "INSERT INTO `brand_synch_2`.`min_logs` (`user`,`timestamp`,`min`,`new_brand`,`action`,`incid`,`subject`) VALUES (@user, @timestamp, @min, @newBrand, 'insert min', @incid, @subject)";
            using (var cmd = new MySqlCommand(insertQuery, conn.connection))
            {
                cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                cmd.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@min", txtmin.Text);
                cmd.Parameters.AddWithValue("@newBrand", CmbBrand.Text);
                cmd.Parameters.AddWithValue("@incid", incid.Text);
                cmd.Parameters.AddWithValue("@subject", txtsubject.Text);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        private async Task ExportBulkLogs(string[] searchTerms, long todayUnixTimestamp)
        {
            try
            {
                foreach (string term in searchTerms)
                {
                    min.Add(term);
                }

                dataGridView1.Rows.Clear();
                for (int i = 0; i < min.Count; i++)
                {
                    dataGridView1.Rows.Add(min[i], brand_id, brand_name, brand_desc, "true", todayUnixTimestamp, todayUnixTimestamp);
                    string insertQuery = "INSERT INTO `brand_synch_2`.`min_logs` (`user`,`timestamp`,`min`,`new_brand`,`action`,`incid`,`subject`) VALUES (@user, @timestamp, @min, @newBrand, 'insert min', @incid, @subject)";
                    using (var cmd = new MySqlCommand(insertQuery, conn.connection))
                    {
                        cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                        cmd.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@min", dataGridView1.Rows[i].Cells[0].Value.ToString());
                        cmd.Parameters.AddWithValue("@newBrand", CmbBrand.Text);
                        cmd.Parameters.AddWithValue("@incid", incid.Text);
                        cmd.Parameters.AddWithValue("@subject", txtsubject.Text);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                ExportToCsv();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void incid_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtsubject.Enabled = incid.Text == "EMAIL";
            if (!txtsubject.Enabled) txtsubject.Clear();
        }

        private void ExportToCsv()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog { Filter = "CSV (*.csv)|*.csv", FileName = "Bulk_Insert.csv" };

                if (sfd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(sfd.FileName))
                {
                    try
                    {
                        using (var sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                        {
                            // Write CSV header
                            var header = string.Join(",", dataGridView1.Columns.Cast<DataGridViewColumn>().Select(column => column.HeaderText));
                            sw.WriteLine(header);

                            // Write rows
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                var rowData = string.Join(",", row.Cells.Cast<DataGridViewCell>().Select(cell =>
                                {
                                    string cellValue = cell.Value?.ToString() ?? "";
                                    // Replace newlines with a space and remove any leading/trailing whitespace
                                    return cellValue.Replace("\r\n", " ").Replace("\n", " ").Trim();
                                }));
                                sw.WriteLine(rowData);
                            }
                        }
                        MessageBox.Show("Data Exported Successfully !!!", "Info");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }
        #endregion
    }
}

