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
using System.Security.Cryptography;
using System.Diagnostics;

namespace L2_GLA
{
    public partial class Frm_Log_in : Form
    {

        MySqlCommand cmd;
        DBconnect conn = new DBconnect();
        MySqlDataReader reader;
        public Frm_Log_in()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
        }

        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            // Hash the entered password using the same method
            string hashedEnteredPassword = GlobalVar.HashPassword(enteredPassword);

            // Compare the two hashed passwords
            return hashedEnteredPassword == storedHashedPassword;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtID.Text;
            string enteredPassword = txtpass.Text;

            // Retrieve the hashed password for the given username from the database
            string storedHashedPassword = GetStoredHashedPassword(username);

            if (storedHashedPassword != null)
            {
                if (VerifyPassword(enteredPassword, storedHashedPassword))
                {
                    cmd = new MySqlCommand("INSERT INTO `log_in`(`user_name`,`Time_in`)VALUES('" + GlobalVar.user + "','" + DateTime.Now + "')", conn.connection);
                    cmd.ExecuteNonQuery();
                    GlobalVar.checking = "true";
                   // GlobalVar.cover = "false";
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid password. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("User not found. Please check your username.");
            }
        }

        private string GetStoredHashedPassword(string username)
        {
            string storedHashedPassword = null; // Initialize to null

            try
            {
                cmd = new MySqlCommand("SELECT * FROM `users` WHERE `user_id` = @username", conn.connection);
                cmd.Parameters.AddWithValue("@username", username);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    storedHashedPassword = reader["password"].ToString();
                    GlobalVar.user = reader["user_name"].ToString();
                    GlobalVar.role = reader["role"].ToString();
                    GlobalVar.team = reader["team"].ToString();
                    GlobalVar.access_code = reader["code"].ToString();
                }
            }
            finally
            {
                reader.Close(); // Close the reader when done
            }

            return storedHashedPassword;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Frm_user_registration objform = new Frm_user_registration();
            objform.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GlobalVar.login = false;
            this.Close();
            Frm_forgot_pass objfrm = new Frm_forgot_pass();
            objfrm.ShowDialog();
        }

        private void Frm_Log_in_Load(object sender, EventArgs e)
        {
            if (GlobalVar.checking == "true")
            {
                this.Close();
            }
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
        }

        private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string username = txtID.Text;
                string enteredPassword = txtpass.Text;

                // Retrieve the hashed password for the given username from the database
                string storedHashedPassword = GetStoredHashedPassword(username);

                if (storedHashedPassword != null)
                {
                    if (VerifyPassword(enteredPassword, storedHashedPassword))
                    {
                        cmd = new MySqlCommand("INSERT INTO `log_in`(`user_name`,`Time_in`)VALUES('" + GlobalVar.user + "','" + DateTime.Now + "')", conn.connection);
                        cmd.ExecuteNonQuery();
                        GlobalVar.checking = "true";
                        // GlobalVar.cover = "false";
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid password. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("User not found. Please check your username.");
                }
            }
        }      
    }
}
