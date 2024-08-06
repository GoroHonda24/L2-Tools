using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using System.Windows.Forms;
    using MySql.Data.MySqlClient;
namespace L2_GLA
{
    public partial class Frm_insert : Form
    {
        MySqlCommand cmd;
        DBconnect conn = new DBconnect();
        MySqlDataReader reader;
        List<string> min = new List<string>();
        string brand_id, brand_name, brand_desc;

        private void cmbBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBrand.Text == "TNT PREPAID")
            {
                brand_id = "TNT";
                brand_name = "TNT Prepaid";
                brand_desc = "TNT";

            }else if (cmbBrand.Text == "SMART PREPAID")
            {
                brand_id = "BUDDY";
                brand_name = "Smart Prepaid";
                brand_desc = "Smart Prepaid";
            }else if (cmbBrand.Text == "SMART POSTPAID")
            {
                brand_id = "POSTPD";
                brand_name = "Smart Postpaid";
                brand_desc = "Smart Postpaid";
            }else if (cmbBrand.Text == "SMART BRO PREPAID")
            {
                brand_id = "BROPRE";
                brand_name = "SmartBro Prepaid";
                brand_desc = "Smart Bro Prepaid";
            }else if (cmbBrand.Text == "SMART BRO POSTPAID")
            {
                brand_id = "BROPOS";
                brand_name = "SmartBro Postpaid";
                brand_desc = "Smart Bro Postpaid";
            }else if (cmbBrand.Text == "HOME WIFI PREPAID")
            {
                brand_id = "PHPW";
                brand_name = "Home Wifi Prepaid";
                brand_desc = "Home Wifi Prepaid";
            }
        }

        public Frm_insert()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
        }

        private void txtmin_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtmin_TextChanged(object sender, EventArgs e)
        {
            mincount.Text = txtmin.Text.Length.ToString();
            string searchTerm = txtmin.Text;
            string[] searchTerms = searchTerm.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (searchTerms.Length > 1)
            {
                btnGen.Text = "Export";
            }
            else if(searchTerms.Length > 0)
            {
                btnGen.Text = "Generate";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            incid.SelectedIndex = -1;
            incid.Text = "";
            txtsubject.Clear();
            cmbBrand.SelectedIndex = -1;
            txtmin.Clear();
            //  dataGridView1.Rows.Clear();
            btnGen.Enabled = true;
            txtformat.Clear();
            txtspiel.Clear();
            min.Clear();
        }

        private void Frm_insert_Load(object sender, EventArgs e)
        {

        }


        private void btnGen_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string dateTime = date + " 00:00:00";
            string today = DateTime.Now.ToString("F j, Y, g:i a");
            DateTime todayDateTime = DateTime.ParseExact(today, "F j, Y, g:i a", null);
            long todayUnixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string searchTerm = txtmin.Text.Replace("\n", "");
            string[] searchTerms = searchTerm.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (incid.Text == "")
            {
                MessageBox.Show("Please Input INC", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtsubject.Enabled == true && txtsubject.Text == "")
            {
                MessageBox.Show("Please Input Subject from email", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtmin.Text.Length <= 9)
            {
                MessageBox.Show("Please Input Proper MIN size", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtmin.Text.Length == 11)
            {
                MessageBox.Show("Please Check MIN count", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtmin.Text.Length == 12)
            {
                txtmin.Text = txtmin.Text.Substring(2);
            }

            if (txtmin.Text == "")
            {
                MessageBox.Show("Please Input MIN", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cmbBrand.Text == "")
            {
                MessageBox.Show("Please select BRAND", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (btnGen.Text == "Generate")
            {
                cmd = new MySqlCommand("SELECT * FROM min_logs where min = '" + txtmin.Text + "' and incid = '" + incid.Text + "' ORDER BY id DESC LIMIT 1", conn.connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    DialogResult result = MessageBox.Show(reader["user"].ToString() + " Is already perform " + reader["action"].ToString() + " of this min with same INC ID at date: " + reader["timestamp"].ToString() + ". \r\nDo you want to Continue this process?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (cmbBrand.Text == "SMART POSTPAID")
                        {
                            txtformat.Text = ("," +
                                "\r\n\"" + "min\": \"" + txtmin.Text + "\"," +
                                " \r\n\"brand_id\": \"POSTPD\"," +
                                " \r\n\"brand_name\": \"Smart Postpaid\"," +
                                " \r\n\"brand_description\": \"Smart Postpaid\"," +
                                " \r\n\"is_active\": true," +
                                " \r\n\"creation_timestamp\": " + todayUnixTimestamp + "," +
                                " \r\n\"last_update_timestamp\": " + todayUnixTimestamp + "");
                        }
                        else if (cmbBrand.Text == "SMART PREPAID")
                        {
                            txtformat.Text = ("," +
                                "\r\n\"" + "min\": \"" + txtmin.Text + "\"," +
                                " \r\n\"brand_id\": \"BUDDY\"," +
                                " \r\n\"brand_name\": \"Smart Prepaid\"," +
                                " \r\n\"brand_description\": \"Smart Prepaid\"," +
                                " \r\n\"is_active\": true," +
                                " \r\n\"creation_timestamp\": " + todayUnixTimestamp + "," +
                                " \r\n\"last_update_timestamp\": " + todayUnixTimestamp + "");
                        }
                        else if (cmbBrand.Text == "TNT PREPAID")
                        {
                            txtformat.Text = ("," +
                                "\r\n\"" + "min\": \"" + txtmin.Text + "\"," +
                                " \r\n\"brand_id\": \"TNT\"," +
                                " \r\n\"brand_name\": \"TNT Prepaid\"," +
                                " \r\n\"brand_description\": \"TNT Prepaid\"," +
                                " \r\n\"is_active\": true," +
                                " \r\n\"creation_timestamp\": " + todayUnixTimestamp + "," +
                                " \r\n\"last_update_timestamp\": " + todayUnixTimestamp + "");
                        }
                        else if (cmbBrand.Text == "SMART BRO POSTPAID")
                        {
                            txtformat.Text = ("," +
                                "\r\n\"" + "min\": \"" + txtmin.Text + "\"," +
                                " \r\n\"brand_id\": \"BROPOS\"," +
                                " \r\n\"brand_name\": \"SmartBro Postpaid\"," +
                                " \r\n\"brand_description\": \"Smart Bro Postpaid\"," +
                                " \r\n\"is_active\": true," +
                                " \r\n\"creation_timestamp\": " + todayUnixTimestamp + "," +
                                " \r\n\"last_update_timestamp\": " + todayUnixTimestamp + "");

                        }
                        else if (cmbBrand.Text == "SMART BRO PREPAID")
                        {
                            txtformat.Text = ("," +
                               "\r\n\"" + "min\": \"" + txtmin.Text + "\"," +
                               " \r\n\"brand_id\": \"BROPRE\"," +
                               " \r\n\"brand_name\": \"BRO Prepaid\"," +
                               " \r\n\"brand_description\": \"Smart Bro Prepaid\"," +
                               " \r\n\"is_active\": true," +
                               " \r\n\"creation_timestamp\": " + todayUnixTimestamp + "," +
                               " \r\n\"last_update_timestamp\": " + todayUnixTimestamp + "");
                        }
                        else if (cmbBrand.Text == "HOME WIFI PREPAID")
                        {
                            txtformat.Text = ("," +
                                "\r\n\"" + "min\": \"" + txtmin.Text + "\"," +
                                " \r\n\"brand_id\": \"PHPW\"," +
                                " \r\n\"brand_name\": \"Home Wifi Prepaid\"," +
                                " \r\n\"brand_description\": \"Home Wifi Prepaid\"," +
                                " \r\n\"is_active\": true," +
                                " \r\n\"creation_timestamp\": " + todayUnixTimestamp + "," +
                                " \r\n\"last_update_timestamp\": " + todayUnixTimestamp + "");

                        }


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
                            "\r\nExpected MIN: " + cmbBrand.Text + "" +
                            "\r\nInternal: " + cmbBrand.Text + "" +
                            "\r\nExternal: " + cmbBrand.Text + "" +
                            "\r\nGL: " + cmbBrand.Text + "" +
                            "\r\n" +
                            "\r\n" +
                            "\r\nNote: Please make sure that your Smart Application is on the latest version. Thank you! ");


                        reader.Close();
                        cmd = new MySqlCommand("INSERT INTO `brand_synch_2`.`min_logs`(`user`,`timestamp`,`min`,`new_brand`,`action`,`incid`,`subject`)VALUES('" + GlobalVar.user + "', '" + DateTime.Now.ToString() + "', '" + txtmin.Text + "','" + cmbBrand.Text + "', 'insert min', '" + incid.Text + "','" + txtsubject.Text + "');", conn.connection);
                        cmd.ExecuteNonQuery();
                    }

                }
                else
                {
                    if (cmbBrand.Text == "SMART POSTPAID")
                    {
                        txtformat.Text = ("," +
                            "\r\n\"" + "min\": \"" + txtmin.Text + "\"," +
                            " \r\n\"brand_id\": \"POSTPD\"," +
                            " \r\n\"brand_name\": \"Smart Postpaid\"," +
                            " \r\n\"brand_description\": \"Smart Postpaid\"," +
                            " \r\n\"is_active\": true," +
                            " \r\n\"creation_timestamp\": " + todayUnixTimestamp + "," +
                            " \r\n\"last_update_timestamp\": " + todayUnixTimestamp + "");
                    }
                    else if (cmbBrand.Text == "SMART PREPAID")
                    {
                        txtformat.Text = ("," +
                            "\r\n\"" + "min\": \"" + txtmin.Text + "\"," +
                            " \r\n\"brand_id\": \"BUDDY\"," +
                            " \r\n\"brand_name\": \"Smart Prepaid\"," +
                            " \r\n\"brand_description\": \"Smart Prepaid\"," +
                            " \r\n\"is_active\": true," +
                            " \r\n\"creation_timestamp\": " + todayUnixTimestamp + "," +
                            " \r\n\"last_update_timestamp\": " + todayUnixTimestamp + "");
                    }
                    else if (cmbBrand.Text == "TNT PREPAID")
                    {
                        txtformat.Text = ("," +
                            "\r\n\"" + "min\": \"" + txtmin.Text + "\"," +
                            " \r\n\"brand_id\": \"TNT\"," +
                            " \r\n\"brand_name\": \"TNT Prepaid\"," +
                            " \r\n\"brand_description\": \"TNT Prepaid\"," +
                            " \r\n\"is_active\": true," +
                            " \r\n\"creation_timestamp\": " + todayUnixTimestamp + "," +
                            " \r\n\"last_update_timestamp\": " + todayUnixTimestamp + "");
                    }
                    else if (cmbBrand.Text == "SMART BRO POSTPAID")
                    {
                        txtformat.Text = ("," +
                            "\r\n\"" + "min\": \"" + txtmin.Text + "\"," +
                            " \r\n\"brand_id\": \"BROPOS\"," +
                            " \r\n\"brand_name\": \"SmartBro Postpaid\"," +
                            " \r\n\"brand_description\": \"Smart Bro Postpaid\"," +
                            " \r\n\"is_active\": true," +
                            " \r\n\"creation_timestamp\": " + todayUnixTimestamp + "," +
                            " \r\n\"last_update_timestamp\": " + todayUnixTimestamp + "");

                    }
                    else if (cmbBrand.Text == "SMART BRO PREPAID")
                    {
                        txtformat.Text = ("," +
                           "\r\n\"" + "min\": \"" + txtmin.Text + "\"," +
                           " \r\n\"brand_id\": \"BROPRE\"," +
                           " \r\n\"brand_name\": \"BRO Prepaid\"," +
                           " \r\n\"brand_description\": \"Smart Bro Prepaid\"," +
                           " \r\n\"is_active\": true," +
                           " \r\n\"creation_timestamp\": " + todayUnixTimestamp + "," +
                           " \r\n\"last_update_timestamp\": " + todayUnixTimestamp + "");
                    }
                    else if (cmbBrand.Text == "HOME WIFI PREPAID")
                    {
                        txtformat.Text = ("," +
                            "\r\n\"" + "min\": \"" + txtmin.Text + "\"," +
                            " \r\n\"brand_id\": \"PHPW\"," +
                            " \r\n\"brand_name\": \"Home Wifi Prepaid\"," +
                            " \r\n\"brand_description\": \"Home Wifi Prepaid\"," +
                            " \r\n\"is_active\": true," +
                            " \r\n\"creation_timestamp\": " + todayUnixTimestamp + "," +
                            " \r\n\"last_update_timestamp\": " + todayUnixTimestamp + "");

                    }


                    txtspiel.Text = ("Done processing the WA for this MIN " + txtmin.Text + ". Please ask the Subscriber to retry the transaction." +
                        "\r\nWe will now proceed in closing this ticket." +
                        "\r\nIf the issue is still persistent, reopen with a screenshot of the error encountered." +
                        "\r\n" +
                        "\r\nIssue: Brand Sync Issue. Brand is aligned in Postman (Internal, External and GL) but Mongo DB is not searchable." +
                        "\r\nBrand Sync using Insert Document. " +
                        "\r\nConcern:" +
                        "\r\nInvestigation Note:" +
                        "\r\n" +
                        "\r\nMIN: " + txtmin.Text + "" +
                        "\r\nExpected MIN: " + cmbBrand.Text + "" +
                        "\r\nInternal: " + cmbBrand.Text + "" +
                        "\r\nExternal: " + cmbBrand.Text + "" +
                        "\r\nGL: " + cmbBrand.Text + "" +
                        "\r\n" +
                        "\r\n" +
                        "\r\nNote: Please make sure that your Smart Application is on the latest version. Thank you! ");


                    reader.Close();
                    cmd = new MySqlCommand("INSERT INTO `brand_synch_2`.`min_logs`(`user`,`timestamp`,`min`,`new_brand`,`action`,`incid`,`subject`)VALUES('" + GlobalVar.user + "', '" + DateTime.Now.ToString() + "', '" + txtmin.Text + "','" + cmbBrand.Text + "', 'insert min', '" + incid.Text + "','" + txtsubject.Text + "');", conn.connection);
                    cmd.ExecuteNonQuery();
                }
                reader.Close();
            }
            else
            {
                dataGridView1.Rows.Clear();

                for (int i = 0; i < searchTerms.Length; i++)
                {
                    min.Add(searchTerms[i]);

                }

                // Ensure dataGridView1 has enough rows
                while (dataGridView1.Rows.Count < min.Count)
                {
                    dataGridView1.Rows.Add();

                }

                for (int i = 0; i < min.Count; i++)
                {
                    if (dataGridView1.Rows.Count > i)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = min[i];
                        dataGridView1.Rows[i].Cells[1].Value = brand_id;
                        dataGridView1.Rows[i].Cells[2].Value = brand_name;
                        dataGridView1.Rows[i].Cells[3].Value = brand_desc;
                        dataGridView1.Rows[i].Cells[4].Value = "true";
                        dataGridView1.Rows[i].Cells[5].Value = todayUnixTimestamp;
                        dataGridView1.Rows[i].Cells[6].Value = todayUnixTimestamp;
                    }
                    cmd = new MySqlCommand("INSERT INTO `brand_synch_2`.`min_logs`(`user`,`timestamp`,`min`,`new_brand`,`action`,`incid`,`subject`)VALUES('" + GlobalVar.user + "', '" + DateTime.Now.ToString() + "', '" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "','" + cmbBrand.Text + "', 'insert min', '" + incid.Text + "','" + txtsubject.Text + "');", conn.connection);
                    cmd.ExecuteNonQuery();
                }

                if (dataGridView1.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "CSV (*.csv)|*.csv";
                    sfd.FileName = "Bulk_Insert.csv";
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
                                int columnCount = dataGridView1.Columns.Count;
                                string columnNames = "";
                                string[] outputCsv = new string[dataGridView1.Rows.Count + 1];
                                for (int i = 0; i < columnCount; i++)
                                {
                                    columnNames += dataGridView1.Columns[i].HeaderText.ToString() + ",";
                                }
                                outputCsv[0] += columnNames;

                                for (int i = 1; (i - 1) < dataGridView1.Rows.Count; i++)
                                {
                                    var rowData = dataGridView1.Rows[i - 1].Cells.Cast<DataGridViewCell>().Select(cell => cell.Value.ToString().Replace("\n", "").Replace("\r", ""));
                                    outputCsv[i] = string.Join(",", rowData);
                                }

                                File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                                MessageBox.Show("Data Exported Successfully !!!", "Info");
                                dataGridView1.Rows.Clear();

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

            btnGen.Enabled = false;
        }

        private void incid_TextChanged(object sender, EventArgs e)
        {
            if (incid.Text == "EMAIL")
            {
                txtsubject.Enabled = true;
                txtsubject.Clear();
            }
            else
            {
                txtsubject.Enabled = false;
                txtsubject.Clear();
            }
        }
    }
}
