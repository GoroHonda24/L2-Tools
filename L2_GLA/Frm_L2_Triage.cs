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
    public partial class Frm_L2_Triage : Form
    {
        MySqlCommand cmd = new MySqlCommand();
        DBconnect conn = new DBconnect();
        MySqlDataReader reader;
        public Frm_L2_Triage()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
        }

        public void load_status()
        {
            cmd = new MySqlCommand("SELECT distinct status FROM tbl_status order by status asc", conn.connection);
            reader = cmd.ExecuteReader();
            cmbStatus.Items.Clear();
            while (reader.Read())
            {
                cmbStatus.Items.Add(reader["status"].ToString());
            }
            reader.Close();
        }
        
        public void load_reason()
        {
            cmd = new MySqlCommand("SELECT distinct reason_status as reason FROM tbl_status where Status=@status order by status asc", conn.connection);
            cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
            reader = cmd.ExecuteReader();
            cmbReason.Items.Clear();
            while (reader.Read())
            {
                cmbReason.Items.Add(reader["reason"].ToString());
            }
            reader.Close();
        }
        public void load_system()
        {
            if (GlobalVar.team == "L2 Smart App")
            {
                cmbsystem_name.Items.Add("SMART APP");
                cmbsystem_name.Items.Add("ELP");
            }
            else
            {
                cmd = new MySqlCommand("SELECT distinct System_name as sname FROM brand_synch_2.tbl_system_name order by System_name asc;", conn.connection);
                reader = cmd.ExecuteReader();
                cmbsystem_name.Items.Clear();
                while (reader.Read())
                {
                    cmbsystem_name.Items.Add(reader["sname"].ToString());
                }
                reader.Close();
            }          

           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string no, yes;
            txtUts_id.Text = txtUts_id.Text.Replace(" ", "");
            txtCall_id.Text = txtCall_id.Text.Replace(" ", "");
            if (txtCall_id.Text == "" || txtUts_id.Text == "" || cmbsystem_name.Text == "" || cmbStatus.Text == "" || rdbNo.Checked == false && rdbYes.Checked == false)
            {
                
                    MessageBox.Show("Please fill all required field", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbStatus.Text != "ASSIGNED")
            {
                if (cmbReason.Text == "")
                {
                    MessageBox.Show("Please fill all required field", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }           

            cmd = new MySqlCommand("SELECT * FROM tbl_triage where uts_id = @uts", conn.connection);
            cmd.Parameters.AddWithValue("@uts", txtUts_id.Text);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                DialogResult result = MessageBox.Show(reader["uts_id"].ToString() + " Is already Process by " + reader["user"].ToString() + " date of : " + reader["time_stamp"].ToString() + ". \r\nDo you want to Continue this process?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    reader.Close();
                    cmd = new MySqlCommand("INSERT INTO `tbl_triage`(`uts_id`,`call_id`,`system_name`,`status`,`fc`,`reason`,`concern`,`error`,`work_around`,`findings`,`bulk`,`user`,`time_stamp`)VALUES(@uts, @call, @system, @status,@fc," +
                "@reason, @concern, @error, @work_around,@findings, @bulk, @user, @date)", conn.connection);
                    cmd.Parameters.AddWithValue("@uts", txtUts_id.Text);
                    cmd.Parameters.AddWithValue("@call", txtCall_id.Text);
                    cmd.Parameters.AddWithValue("@system", cmbsystem_name.Text);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                    cmd.Parameters.AddWithValue("@fc", cmbFc.Text);
                    cmd.Parameters.AddWithValue("@reason", cmbReason.Text);

                    cmd.Parameters.AddWithValue("@concern", txtConcern.Text);
                    cmd.Parameters.AddWithValue("@error", txtError.Text);
                    cmd.Parameters.AddWithValue("@work_around", txtWork_around.Text);
                    cmd.Parameters.AddWithValue("@findings", txtfindings.Text);
                    if (rdbNo.Checked == true)
                    {
                        no = "NO";
                        cmd.Parameters.AddWithValue("@bulk", no);
                    }
                    else if (rdbYes.Checked == true)
                    {
                        yes = "YES";
                        cmd.Parameters.AddWithValue("@bulk", yes);
                    }
                    cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfuly save UTS_ID " + txtUts_id.Text + "", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
                reader.Close();
                clear();
            }
            else
            {
                reader.Close();
                cmd = new MySqlCommand("INSERT INTO `tbl_triage`(`uts_id`,`call_id`,`system_name`,`status`,`fc`,`reason`,`concern`,`error`,`work_around`,`findings`,`bulk`,`user`,`time_stamp`)VALUES(@uts, @call, @system, @status,@fc," +
               "@reason, @concern, @error, @work_around,@findings, @bulk, @user, @date)", conn.connection);
                cmd.Parameters.AddWithValue("@uts", txtUts_id.Text);
                cmd.Parameters.AddWithValue("@call", txtCall_id.Text);
                cmd.Parameters.AddWithValue("@system", cmbsystem_name.Text);
                cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                cmd.Parameters.AddWithValue("@fc", cmbFc.Text);
                cmd.Parameters.AddWithValue("@reason", cmbReason.Text);
                cmd.Parameters.AddWithValue("@concern", txtConcern.Text);
                cmd.Parameters.AddWithValue("@error", txtError.Text);
                cmd.Parameters.AddWithValue("@work_around", txtWork_around.Text);
                cmd.Parameters.AddWithValue("@findings", txtfindings.Text);
                if (rdbNo.Checked == true)
                {
                    no = "NO";
                    cmd.Parameters.AddWithValue("@bulk", no);
                }
                else if (rdbYes.Checked == true)
                {
                    yes = "YES";
                    cmd.Parameters.AddWithValue("@bulk", yes);
                }
                cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfuly save UTS_ID " + txtUts_id.Text + "", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();

            }
            reader.Close();
        }
        public void clear()
        {
            txtCall_id.Clear();
            txtConcern.Clear();
            txtError.Clear();
            txtUts_id.Clear();
            txtWork_around.Clear();
            txtfindings.Clear();
            cmbReason.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            cmbsystem_name.SelectedIndex = -1;
            rdbNo.Checked = false;
            rdbYes.Checked = false;

        }

        private void Frm_L2_Triage_Load(object sender, EventArgs e)
        {
            load_status();
            load_system();
        }
        public void Display_FC()
        {
            cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_system_name where System_name = @system order by func_category asc", conn.connection);
            cmd.Parameters.AddWithValue("@system", cmbsystem_name.Text);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            { 
                cmbFc.Items.Clear();
                while (reader.Read())
                {
                    cmbFc.Items.Add(reader["func_category"]);
                }                
            }

        }
        private void cmbsystem_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            Display_FC();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbStatus.Text == "ASSIGNED")
            {
                label13.Visible = false;
            }else
            {
                label13.Visible = true;
            }
            load_reason();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
