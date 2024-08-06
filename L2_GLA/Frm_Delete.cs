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
    public partial class Frm_Delete : Form
    {
        MySqlCommand cmd;
        DBconnect conn = new DBconnect();
        MySqlDataReader reader;

        public Frm_Delete()
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

            cmd = new MySqlCommand("SELECT * FROM min_logs where min = '" + txtmin.Text + "' and incid = '" + incid.Text + "' ORDER BY id DESC LIMIT 1", conn.connection);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {

                DialogResult result = MessageBox.Show(reader["user"].ToString() + " Is already perform " + reader["action"].ToString() + " of this min with same INC ID at date: " + reader["timestamp"].ToString() + ". \r\nDo you want to Continue this process?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    reader.Close();
                    cmd = new MySqlCommand("INSERT INTO `brand_synch_2`.`min_logs`(`user`,`timestamp`,`min`,`action`,`incid`)VALUES('" + GlobalVar.user + "', '" + DateTime.Now.ToString() + "', '" + txtmin.Text + "', 'Delete Account','" + incid.Text + "');", conn.connection);
                    cmd.ExecuteNonQuery();

                    txtspiel.Text = ("Subs MIN:  " + txtmin.Text + "  is already deleted kindly advise subs to re-register." +
                     "\r\nWe will now proceed in closing this ticket." +
                     "\r\nIf the issue is still persistent, reopen with a screenshot of the error encountered." +
                     "\r\nThank you." +
                     "\r\n" +
                     "\r\n" +
                     "\r\nNote: Please make sure that your Smart Application is on the latest version. Thank you! ");
                }

            }
            else
            {
                reader.Close();
                cmd = new MySqlCommand("INSERT INTO `brand_synch_2`.`min_logs`(`user`,`timestamp`,`min`,`action`,`incid`)VALUES('" + GlobalVar.user + "', '" + DateTime.Now.ToString() + "', '" + txtmin.Text + "', 'Delete Account','" + incid.Text + "');", conn.connection);
                cmd.ExecuteNonQuery();

                txtspiel.Text = ("Subs MIN:  " + txtmin.Text + "  is already deleted kindly advise subs to re-register." +
                 "\r\nWe will now proceed in closing this ticket." +
                 "\r\nIf the issue is still persistent, reopen with a screenshot of the error encountered." +
                 "\r\nThank you." +
                 "\r\n" +
                 "\r\n" +
                 "\r\nNote: Please make sure that your Smart Application is on the latest version. Thank you! ");
            }
            reader.Close();
        }

        private void txtmin_TextChanged(object sender, EventArgs e)
        {
            mincount.Text = txtmin.Text.Length.ToString();
        }
    }
}
