using System;
using System.Data;
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
            if (conn.connection.State != ConnectionState.Open)
                conn.connection.Open();
            InitializeComponent();
        }

        // Helper method to verify user credentials
        private bool ValidateUser(string username, string enteredPassword)
        {
            try
            {
                string storedHashedPassword = GetStoredHashedPassword(username);

                if (storedHashedPassword == null)
                {
                    MessageBox.Show("User not found. Please check your username.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (VerifyPassword(enteredPassword, storedHashedPassword))
                {
                    LogUserLogin();
                    GlobalVar.checking = "true";
                    return true;
                }
                else
                {
                    MessageBox.Show("Invalid password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Improved password verification with hashing
        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            string hashedEnteredPassword = GlobalVar.HashPassword(enteredPassword);
            return hashedEnteredPassword == storedHashedPassword;
        }

        // Retrieves the stored hashed password and other user details
        private string GetStoredHashedPassword(string username)
        {
            string storedHashedPassword = null;

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
                    GlobalVar.t_account = reader["taccount"].ToString() ;
                    GlobalVar.rsa_id = reader["rsa_id"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving user data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                reader?.Close(); // Use null conditional to ensure reader is not null
            }

            return storedHashedPassword;
        }

        // Logs user login time
        private void LogUserLogin()
        {
            try
            {
                cmd = new MySqlCommand("INSERT INTO `log_in`(`user_name`, `Time_in`) VALUES (@username, @timeIn)", conn.connection);
                cmd.Parameters.AddWithValue("@username", GlobalVar.user);
                cmd.Parameters.AddWithValue("@timeIn", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while logging user login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Button click handler for login
        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtID.Text;
            string enteredPassword = txtpass.Text;

            if (ValidateUser(username, enteredPassword))
            {
                this.Close();
            }
        }

        // Handles the Enter key event for password textbox
        private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string username = txtID.Text;
                string enteredPassword = txtpass.Text;

                if (ValidateUser(username, enteredPassword))
                {
                    this.Close();
                }
            }
        }

        // Link click handler to open user registration form
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Frm_user_registration objform = new Frm_user_registration();
            objform.ShowDialog();
        }

        // Button click handler to exit the application
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Link click handler to open forgot password form
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GlobalVar.login = false;
            this.Close();
            Frm_forgot_pass objfrm = new Frm_forgot_pass();
            objfrm.ShowDialog();
        }

        // Form load event handler
        private void Frm_Log_in_Load(object sender, EventArgs e)
        {
            if (GlobalVar.checking == "true")
            {
                this.Close();
            }
        }
    }
}
