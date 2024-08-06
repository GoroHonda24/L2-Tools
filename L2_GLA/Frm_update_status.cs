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

namespace L2_GLA
{
    public partial class Frm_update_status : Form
    {
        MySqlCommand cmd;
        DBconnect conn = new DBconnect();
        MySqlDataReader reader;
        public Frm_update_status()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string dateTime = date + " 00:00:00";
            string today = DateTime.Now.ToString("F j, Y, g:i a");
            DateTime todayDateTime = DateTime.ParseExact(today, "F j, Y, g:i a", null);
            long todayUnixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

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
                txtmin.Text = txtmin.Text.Substring(1);
            }
            if (txtmin.Text.Length == 12)
            {
                txtmin.Text = txtmin.Text.Substring(2);
            }

            if (txtmin.Text.Length >= 11)
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

            cmd = new MySqlCommand("SELECT * FROM min_logs where min = '" + txtmin.Text + "' and incid = '" + incid.Text + "' ORDER BY id DESC LIMIT 1", conn.connection);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {

                DialogResult result = MessageBox.Show(reader["user"].ToString() + " Is already perform " + reader["action"].ToString() + " of this min with same INC ID at date: " + reader["timestamp"].ToString() + ". \r\nDo you want to Continue this process?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    txtformat.Text = ("db.min_metadata.updateOne({\"min\":\"" + txtmin.Text + "\"}," +
                   " {$set:{\"is_active\": true," +
                   " \"last_update_timestamp\": " + todayUnixTimestamp + "}});");


                    txtspiel.Text = ("Done processing the WA for this MIN " + txtmin.Text + ". Please ask the Subscriber to retry the transaction." +
                      "\r\nWe will now proceed in closing this ticket." +
                      "\r\nIf the issue is still persistent, reopen with a screenshot of the error encountered." +
                      "\r\nIssue. Brand Sync Issue. Brand is aligned in Postman (Internal, External and GL) but Mongo DB is Inactive" +
                      "\r\nWA: Brand Sync. Update is_active from Inactive to Active in Mongo DB." +
                      "\r\nError Message:" +
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
                    cmd = new MySqlCommand("INSERT INTO `brand_synch_2`.`min_logs`(`user`,`timestamp`,`min`,`action`,`incid`,`subject`)VALUES('" + GlobalVar.user + "', '" + DateTime.Now.ToString() + "', '" + txtmin.Text + "', 'Update Status','" + incid.Text + "','" + txtsubject.Text + "');", conn.connection);
                    cmd.ExecuteNonQuery();
                }

            }
            else
            {


                txtformat.Text = ("db.min_metadata.updateOne({\"min\":\"" + txtmin.Text + "\"}," +
               " {$set:{\"is_active\": true," +
               " \"last_update_timestamp\": " + todayUnixTimestamp + "}});");


                txtspiel.Text = ("Done processing the WA for this MIN " + txtmin.Text + ". Please ask the Subscriber to retry the transaction." +
                  "\r\nWe will now proceed in closing this ticket." +
                  "\r\nIf the issue is still persistent, reopen with a screenshot of the error encountered." +
                  "\r\nIssue: Brand Sync" +
                  "\r\nIssue. Brand Sync Issue. Brand is aligned in Postman (Internal, External and GL) but Mongo DB is Inactive" +
                  "\r\nWA: Brand Sync. Update is_active from Inactive to Active in Mongo DB." +
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
                cmd = new MySqlCommand("INSERT INTO `brand_synch_2`.`min_logs`(`user`,`timestamp`,`min`,`action`,`incid`,`subject`)VALUES('" + GlobalVar.user + "', '" + DateTime.Now.ToString() + "', '" + txtmin.Text + "', 'Update Status','" + incid.Text + "','" + txtsubject.Text + "');", conn.connection);
                cmd.ExecuteNonQuery();
            }
            reader.Close();
        }

        private void txtmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Suppress the key press event if the character is not a number or a control key
                e.Handled = true;
            }
        }

        private void txtmin_TextChanged(object sender, EventArgs e)
        {
            mincount.Text = txtmin.Text.Length.ToString();
        }

        private void incid_SelectedIndexChanged(object sender, EventArgs e)
        {

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
