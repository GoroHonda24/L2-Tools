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


namespace L2_GLA
{
    public partial class Frm_user_registration : Form
    {
        MySqlCommand cmd;
        DBconnect conn = new DBconnect();
        MySqlDataReader reader;

        public Frm_user_registration()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

  


        //private void InsertUser(string user_id, string username, string hashedPassword, string role)
        //{
        //    txtID.Text = txtID.Text.Replace(" ", "");
        //    txtname.Text = txtname.Text.Replace(" ", "");

        //    cmd = new MySqlCommand("SELECT * FROM tbl_user_whitelisted where emp_id = @emp_id and Fname = @fname", conn.connection);
        //    cmd.Parameters.AddWithValue("@emp_id", txtID.Text);
        //    cmd.Parameters.AddWithValue("@fname", txtname.Text);
        //    reader = cmd.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        reader.Close();
        //        cmd = new MySqlCommand("INSERT INTO `users`(`user_id`,`user_name`,`password`,`role`) VALUES (@user_id, @Username, @Password, @role)", conn.connection);
        //        cmd.Parameters.AddWithValue("@user_id", user_id);
        //        cmd.Parameters.AddWithValue("@Username", username);
        //        cmd.Parameters.AddWithValue("@Password", hashedPassword);
        //        cmd.Parameters.AddWithValue("@role", role);
        //        cmd.ExecuteNonQuery();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Your ID & Name is not allowed to register\r\nPlease Contact sir Abdel / Joeff", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }
                
                
          
        //}

        private void btnsave_Click(object sender, EventArgs e)
        {
            if(txtpass.Text != txtconpass.Text)
            {
                MessageBox.Show("Password not match");
            }else
            {

                string username = txtname.Text;
                string password = txtpass.Text;
                string user_id = txtID.Text;
                

                // Hash the password
                string hashedPassword =GlobalVar.HashPassword(password);

               // InsertUser(user_id, username, hashedPassword,role);
                txtID.Text = txtID.Text.Replace(" ", "");
                txtname.Text = txtname.Text.Replace(" ", "");

                cmd = new MySqlCommand("SELECT * FROM users where user_id = @id", conn.connection);
                cmd.Parameters.AddWithValue("@id", txtID.Text);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("Your ID & Name is already register\r\nPlease Contact sir Abdel / Joeff", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    reader.Close();
                    return;
                }
                reader.Close();

                cmd = new MySqlCommand("SELECT * FROM tbl_user_whitelisted where emp_id = @emp_id and Fname = @fname", conn.connection);
                cmd.Parameters.AddWithValue("@emp_id", txtID.Text);
                cmd.Parameters.AddWithValue("@fname", txtname.Text);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string role = reader["role"].ToString();
                    string team = reader["team"].ToString();
                    reader.Close();
                    cmd = new MySqlCommand("INSERT INTO `users`(`user_id`,`user_name`,`password`,`team`,`role`) VALUES (@user_id, @Username, @Password,@team, @role)", conn.connection);
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@team", team);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.ExecuteNonQuery();

                }
                else
                {
                    MessageBox.Show("Your ID & Name is not allowed to register\r\nPlease Contact sir Abdel / Joeff", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    reader.Close();
                    return;
                }
                reader.Close();
                MessageBox.Show("Registration successful!");

                this.Close();
            }
        }
    }
}
