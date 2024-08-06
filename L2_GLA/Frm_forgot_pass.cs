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
    public partial class Frm_forgot_pass : Form
    {
        MySqlCommand cmd;
        DBconnect conn = new DBconnect();
        MySqlDataReader reader;
        public Frm_forgot_pass()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string password = txtpass.Text;
            string hashedPassword = GlobalVar.HashPassword(password);

            if (txtconpass.Text != txtpass.Text)
            {
                MessageBox.Show("Password not Match","Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cmd = new MySqlCommand("Select * from users where user_id = @id", conn.connection);
            cmd.Parameters.AddWithValue("@id", id);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();

                cmd = new MySqlCommand("UPDATE `users` SET `password` = @password  WHERE `user_id` = @id", conn.connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.ExecuteNonQuery();
                MessageBox.Show("save");
                GlobalVar.login = true;
            }
            else
            {
                MessageBox.Show("Employee id not found!");
            }

            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frm_Log_in objfrm = new Frm_Log_in();
            objfrm.ShowDialog();
            this.Close();
        }
    }
}
