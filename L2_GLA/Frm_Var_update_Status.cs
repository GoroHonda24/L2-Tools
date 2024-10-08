﻿using System;
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
    public partial class Frm_Var_update_Status : Form
    {
        DBconnect conn = new DBconnect();
        public Frm_Var_update_Status(Frm_Gcash frmUpdate, Frm_maya frmmayaupdate)
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
            frm = frmUpdate;
            frmmaya = frmmayaupdate;
        }

        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);
        public event UpdateDelegate UpdateEventHandler;
        Frm_Gcash frm;
        Frm_maya frmmaya;


        public class UpdateEventArgs : EventArgs
        {
           public string Data { get; set; }
        }

        protected void raiseUpdate()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler?.Invoke(this, args);
        }

        private void Frm_Var_update_Status_Load(object sender, EventArgs e)
        {
            label1.Text = GlobalVar.gfile_name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cbbStatus.Text == "Done")
            {
                using (MySqlCommand sql = new MySqlCommand("UPDATE `brand_synch_2`.`tbl_variance_file` SET `Status` = @status, `Done_by` = @doneby,`end_at` = @enddt WHERE `id` = @id", conn.connection))
                {

                    sql.Parameters.AddWithValue("@status", cbbStatus.Text);
                    sql.Parameters.AddWithValue("@doneby", GlobalVar.user);
                    sql.Parameters.AddWithValue("@enddt", DateTime.Now);
                    sql.Parameters.AddWithValue("@id", GlobalVar.gfile_id);
                    sql.ExecuteNonQuery();

                    MessageBox.Show("Successful Updating Status", "Information");
                    raiseUpdate();
                    //  Console.WriteLine("2" + file_name);
                }
            }
            else
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE `brand_synch_2`.`tbl_variance_file` SET `Status` = @status WHERE `id` = @fileID", conn.connection))
                {
                    cmd.Parameters.AddWithValue("@fileID", GlobalVar.gfile_id);
                    cmd.Parameters.AddWithValue("@status", cbbStatus.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successful Updating Status", "Information");
                    raiseUpdate();
                }

            }
            this.Close();
        }
    }
}
