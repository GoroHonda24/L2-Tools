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
    public partial class Frm_CMS : Form
    {
        DBconnect conn = new DBconnect();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader Reader;
        

        public Frm_CMS()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();

            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            txtEmpID.Text = txtEmpID.Text.Replace(" ", "");
            txtFname.Text = txtFname.Text.Replace(" ", "");

            if (txtEmpID.Text == "" || txtFname.Text == "" || cmbTeam.Text == "" || cmbRole.Text == "")
            {
                MessageBox.Show("Please Provide all Information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cmd = new MySqlCommand("SELECT * FROM tbl_user_whitelisted where Emp_ID = @emp_id or Fname = @fname", conn.connection);
            cmd.Parameters.AddWithValue("@emp_id", txtEmpID.Text);
            cmd.Parameters.AddWithValue("@fname", txtFname.Text);
            Reader = cmd.ExecuteReader();
            if (Reader.Read())
            {
                MessageBox.Show("User is already existing", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reader.Close();
                txtEmpID.Clear();
                txtFname.Clear();
                cmbTeam.SelectedIndex = -1;
                cmbRole.SelectedIndex = -1;
                return;
            } else
            {
                Reader.Close();
                cmd = new MySqlCommand("INSERT INTO `tbl_user_whitelisted`(`Emp_ID`,`Fname`,`Team`,`Role`,`added`,`Time_Stamp`)VALUES(@emp_id,@fname,@team,@role,@add,@time)", conn.connection);
                cmd.Parameters.AddWithValue("@emp_id", txtEmpID.Text);
                cmd.Parameters.AddWithValue("@fname", txtFname.Text);
                cmd.Parameters.AddWithValue("@team", cmbTeam.Text);
                cmd.Parameters.AddWithValue("@role", cmbRole.Text);
                cmd.Parameters.AddWithValue("@add", GlobalVar.user);
                cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successful to add in whithlisted user", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Reader.Close();

            txtEmpID.Clear();
            txtFname.Clear();
            cmbTeam.SelectedIndex = -1;
            cmbRole.SelectedIndex = -1;
        }

        public void Display_Status()
        {
            cmd = new MySqlCommand("SELECT distinct status FROM brand_synch_2.tbl_status order by status asc", conn.connection);
            Reader = cmd.ExecuteReader();
            dgvStatus.Rows.Clear();
            dgvStatus.AllowUserToAddRows = true;
            if (Reader.HasRows)
            {

                while (Reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dgvStatus.Rows[0].Clone();
                    row.Cells[0].Value = Reader["status"];
                    //row.Cells[1].Value = Reader["user"];
                    dgvStatus.Rows.Add(row);

                }
                dgvStatus.AllowUserToAddRows = false;
            }
            Reader.Close();
        }
        public void Display_user()
        {
            cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_user_whitelisted", conn.connection);
            Reader = cmd.ExecuteReader();
            dgvuser.Rows.Clear();
            dgvuser.AllowUserToAddRows = true;
            if (Reader.HasRows)
            {

                while (Reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dgvuser.Rows[0].Clone();
                    row.Cells[0].Value = Reader["Emp_ID"];
                    row.Cells[1].Value = Reader.GetString(2);
                    row.Cells[2].Value = Reader.GetString(3);
                    row.Cells[3].Value = Reader.GetString(4);
                   

                    
                    dgvuser.Rows.Add(row);

                }
                dgvuser.AllowUserToAddRows = false;
            }
            Reader.Close();
        
        }public void Filter_user()
        {
            cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_user_whitelisted where Emp_ID like @emp", conn.connection);
            cmd.Parameters.AddWithValue("@emp", txtEmpID.Text + "%");
            Reader = cmd.ExecuteReader();
            dgvuser.Rows.Clear();
            dgvuser.AllowUserToAddRows = true;
            if (Reader.HasRows)
            {

                while (Reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dgvuser.Rows[0].Clone();
                    row.Cells[0].Value = Reader["Emp_ID"];
                    row.Cells[1].Value = Reader.GetString(2);
                    row.Cells[2].Value = Reader.GetString(3);
                    row.Cells[3].Value = Reader.GetString(4);
                   

                    
                    dgvuser.Rows.Add(row);

                }
                dgvuser.AllowUserToAddRows = false;
            }
            Reader.Close();
        }

        public void Display_Reason()
        {
            cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_status order by status asc", conn.connection);
            Reader = cmd.ExecuteReader();
            dgvReason.Rows.Clear();
            dgvReason.AllowUserToAddRows = true;
            if (Reader.HasRows)
            {

                while (Reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dgvReason.Rows[0].Clone();
                    row.Cells[0].Value = Reader["reason_status"];
                    //row.Cells[1].Value = Reader["user"];
                    dgvReason.Rows.Add(row);

                }
                dgvReason.AllowUserToAddRows = false;
            }
            Reader.Close();
        }
        public void Display_system()
        {
            cmd = new MySqlCommand("SELECT distinct(System_name) as system_name FROM brand_synch_2.tbl_system_name order by System_name asc", conn.connection);
            Reader = cmd.ExecuteReader();
            dgvSystem.Rows.Clear();
            dgvSystem.AllowUserToAddRows = true;
            if (Reader.HasRows)
            {

                while (Reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dgvSystem.Rows[0].Clone();
                    row.Cells[0].Value = Reader["system_name"];
                    
                    dgvSystem.Rows.Add(row);

                }
                dgvSystem.AllowUserToAddRows = false;
            }
            Reader.Close();
        }
        public void Display_FC()
        {
            cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_system_name where func_category !='' order by func_category asc;", conn.connection);
            Reader = cmd.ExecuteReader();
            dgvFC.Rows.Clear();
            dgvFC.AllowUserToAddRows = true;
            if (Reader.HasRows)
            {

                while (Reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dgvFC.Rows[0].Clone();
                    row.Cells[0].Value = Reader["func_category"];
                    
                    dgvFC.Rows.Add(row);

                }
                dgvFC.AllowUserToAddRows = false;
            }
            Reader.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbCategory.Text))
            {
                MessageBox.Show("Please select Category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCategory.Text == "STATUS")
            {
                if (string.IsNullOrEmpty(cmbStatus.Text))
                {
                    MessageBox.Show("Please Input Details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cmd = new MySqlCommand("SELECT * FROM tbl_status where status = @status and reason_status = @reason", conn.connection);
                cmd.Parameters.AddWithValue("@status", cmbStatus.Text = cmbStatus.Text.ToUpper());
                cmd.Parameters.AddWithValue("@reason", txtDetails.Text);
                Reader = cmd.ExecuteReader();
                if (Reader.Read())
                {
                    MessageBox.Show("Data is already existing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Reader.Close();
                    return;
                }
                else
                {
                    Reader.Close();
                    cmd = new MySqlCommand("INSERT INTO `tbl_status`(`Status`,`user`,`reason_status`,`time_stamp`)VALUES(@status, @user,@reason, @date)", conn.connection);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                    cmd.Parameters.AddWithValue("@reason", txtDetails.Text);
                    cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                    cmd.ExecuteNonQuery();

                }
                Reader.Close();
            }
            else if (cmbCategory.Text == "SYSTEM NAME")
            {
                if (cmbStatus.Text == "")
                {
                    MessageBox.Show("Please Input Name or Details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cmd = new MySqlCommand("SELECT * FROM tbl_system_name where system_name = @system and func_category = @fc", conn.connection);
                cmd.Parameters.AddWithValue("@system", cmbStatus.Text = cmbStatus.Text.ToUpper());
                cmd.Parameters.AddWithValue("@fc", txtDetails.Text);
                Reader = cmd.ExecuteReader();
                if (Reader.Read())
                {
                    MessageBox.Show("Data is already existing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Reader.Close();
                    return;
                }
                else
                {
                    Reader.Close();
                    cmd = new MySqlCommand("INSERT INTO `tbl_system_name`(`System_name`,`func_category`,`user`,`Time_Stamp`)VALUES(@system,@fc, @user, @date)", conn.connection);
                    cmd.Parameters.AddWithValue("@system", cmbStatus.Text = cmbStatus.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@fc", txtDetails.Text);
                    cmd.Parameters.AddWithValue("@user", GlobalVar.user);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                    cmd.ExecuteNonQuery();

                }
                Reader.Close();
            }

            cmbStatus.SelectedIndex = -1;
            cmbStatus.Items.Clear();
            cmbStatus.Items.Clear(); 
            cmbCategory.SelectedIndex = -1;
            txtDetails.Clear();
            Display_Status();
            Display_system();
            Display_Reason();
            Display_FC();
        }
        
        private void Frm_CMS_Load(object sender, EventArgs e)
        {
            Display_Status();
            Display_system();
            Display_Reason();
            Display_FC();
            Display_user();
            load_smart_app_cms();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategory.Text == "STATUS")
            {
                using (MySqlCommand sqlcmd = new MySqlCommand("SELECT distinct(Status) AS status FROM brand_synch_2.tbl_status order by status asc", conn.connection))
                {

                    cmbStatus.Items.Clear();
                    using (MySqlDataReader reader = sqlcmd.ExecuteReader())
                        while (reader.Read())
                        {
                            cmbStatus.Items.Add(reader["status"].ToString());
                        }
                }
            }
            else if (cmbCategory.Text=="SYSTEM NAME")
            {
                using(MySqlCommand sqlcmd = new MySqlCommand("SELECT distinct(System_name) as system_name FROM brand_synch_2.tbl_system_name order by System_name asc;", conn.connection))
                {                    
                    cmbStatus.Items.Clear();
                    using (MySqlDataReader reader = sqlcmd.ExecuteReader())
                        while (reader.Read())
                        {
                            cmbStatus.Items.Add(reader["System_name"].ToString());
                        }
                  

                }
                
            }

        }
       
        private void cmbStatus_TextChanged(object sender, EventArgs e)
        {
           
            //cmbStatus.Items.Clear();
            //cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_status  where status like @status order by status asc", conn.connection);
            //cmd.Parameters.AddWithValue("@status", cmbStatus.Text + "%");
            //Reader = cmd.ExecuteReader();
            //if (Reader.Read())
            //{
            //    cmbStatus.Items.Add(Reader["status"].ToString());
            //}
            //Reader.Close();

            //cmbStatus.DroppedDown = true;
            //cmbStatus.SelectionStart = cmbStatus.Text.Length;

        }
    


        public void removeStatus(object sender, EventArgs e)
        {
            string status = dgvStatus.Rows[dgvStatus.CurrentCell.RowIndex].Cells[0].Value.ToString();
            cmd = new MySqlCommand("delete from tbl_status where status =@status", conn.connection);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.ExecuteNonQuery();

            MessageBox.Show($"Successfuly Deleted all Status & Reason: {status}");
            Display_Reason();
            Display_Status(); 
           

        }
        public void removeReason(object sender, EventArgs e)
        {
            string reason = dgvReason.Rows[dgvReason.CurrentCell.RowIndex].Cells[0].Value.ToString();
            cmd = new MySqlCommand("delete from tbl_status where reason_status =@reason", conn.connection);
            cmd.Parameters.AddWithValue("@reason", reason);
            cmd.ExecuteNonQuery();

            MessageBox.Show($"Successfuly Deleted Reason: {reason}");
            Display_Reason();
            Display_Status(); 
            

        }
        public void removerSystem(object sender, EventArgs e)
        {
            string system = dgvSystem.Rows[dgvSystem.CurrentCell.RowIndex].Cells[0].Value.ToString();
            cmd = new MySqlCommand("delete from tbl_system_name where system_name =@system", conn.connection);
            cmd.Parameters.AddWithValue("@system", system);
            cmd.ExecuteNonQuery();

            MessageBox.Show($"Successfuly Deleted System Name: {system}");
            Display_system();
            Display_FC();

        }
        public void removeFC(object sender, EventArgs e)
        {
            string fc = dgvFC.Rows[dgvFC.CurrentCell.RowIndex].Cells[0].Value.ToString();
            cmd = new MySqlCommand("delete from tbl_system_name where func_category = @FC", conn.connection);
            cmd.Parameters.AddWithValue("@FC", fc);
            cmd.ExecuteNonQuery();

            MessageBox.Show($"Successfuly Deleted Functional Category: {fc}");
            Display_FC();
            Display_system();

        }
        private void dgvStatus_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            if (dgvStatus.Rows.Count != 0)
            {
                if(e.Button == MouseButtons.Right)
                {
                    if (e.RowIndex == -1) return;
                    if (e.ColumnIndex == -1) return;

                    dgvStatus.CurrentCell = dgvStatus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvStatus.Rows[e.RowIndex].Selected = true;
                    dgvStatus.Focus();
                    ToolStripMenuItem mnuremove = new ToolStripMenuItem("Remove");
                    mnuremove.Click += new EventHandler(removeStatus);
                    mnu.Items.AddRange(new ToolStripItem[] { mnuremove });
                    dgvStatus.ContextMenuStrip = mnu;
                }
            }
        }

        private void dgvReason_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            if (dgvReason.Rows.Count != 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (e.RowIndex == -1) return;
                    if (e.ColumnIndex == -1) return;

                    dgvReason.CurrentCell = dgvReason.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvReason.Rows[e.RowIndex].Selected = true;
                    dgvReason.Focus();
                    ToolStripMenuItem mnuremove = new ToolStripMenuItem("Remove");
                    mnuremove.Click += new EventHandler(removeReason);
                    mnu.Items.AddRange(new ToolStripItem[] { mnuremove });
                    dgvReason.ContextMenuStrip = mnu;
                }
            }
        }

        private void dgvSystem_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            if (dgvSystem.Rows.Count != 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (e.RowIndex == -1) return;
                    if (e.ColumnIndex == -1) return;

                    dgvSystem.CurrentCell = dgvSystem.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvSystem.Rows[e.RowIndex].Selected = true;
                    dgvSystem.Focus();
                    ToolStripMenuItem mnuremove = new ToolStripMenuItem("Remove");
                    mnuremove.Click += new EventHandler(removerSystem);
                    mnu.Items.AddRange(new ToolStripItem[] { mnuremove });
                    dgvSystem.ContextMenuStrip = mnu;
                }
            }
        }
        private void dgvFC_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            if (dgvFC.Rows.Count != 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (e.RowIndex == -1) return;
                    if (e.ColumnIndex == -1) return;

                    dgvFC.CurrentCell = dgvFC.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvFC.Rows[e.RowIndex].Selected = true;
                    dgvFC.Focus();
                    ToolStripMenuItem mnuremove = new ToolStripMenuItem("Remove");
                    mnuremove.Click += new EventHandler(removeFC);
                    mnu.Items.AddRange(new ToolStripItem[] { mnuremove });
                    dgvFC.ContextMenuStrip = mnu;
                }
            }
        }
        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
          
        }
        
        private void txtEmpID_TextChanged(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("SELECT * FROM brand_synch_2.tbl_user_whitelisted where Emp_ID like @emp", conn.connection);
            cmd.Parameters.AddWithValue("@emp", txtEmpID.Text + "%");
            Reader = cmd.ExecuteReader();
            if (Reader.Read())
            {
                txtFname.Text = Reader["fname"].ToString();
            }
            else
            {
                txtFname.Clear();
            }
            Reader.Close();

            Filter_user();
        }

        private void cmbCategory_Click(object sender, EventArgs e)
        {
            
        }

        private void cmbStatus_Click(object sender, EventArgs e)
        {
            cmbStatus.DroppedDown = true;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        public void load_smart_app_cms()
        {

            using (MySqlCommand cmd = new MySqlCommand("SELECT fc FROM tbl_cms_smart WHERE fc IS NOT NULL", conn.connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dgv_FC.Rows.Clear();
                    dgv_FC.AllowUserToAddRows = true;
                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dgv_FC.Rows[0].Clone();
                        row.Cells[0].Value = reader["fc"];

                        dgv_FC.Rows.Add(row);
                    }

                    dgv_FC.AllowUserToAddRows = false;
                }
            }

            using (MySqlCommand cmd = new MySqlCommand("SELECT owner FROM tbl_cms_smart WHERE owner IS NOT NULL", conn.connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dgvOwner.Rows.Clear();
                    dgvOwner.AllowUserToAddRows = true;
                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvOwner.Rows[0].Clone();
                        row.Cells[0].Value = reader["owner"];

                        dgvOwner.Rows.Add(row);
                    }

                    dgvOwner.AllowUserToAddRows = false;
                }
            }
            using (MySqlCommand cmd = new MySqlCommand("SELECT status FROM tbl_cms_smart WHERE status IS NOT NULL", conn.connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dgv_Status.Rows.Clear();
                    dgv_Status.AllowUserToAddRows = true;
                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dgv_Status.Rows[0].Clone();
                        row.Cells[0].Value = reader["status"];

                        dgv_Status.Rows.Add(row);
                    }

                    dgv_Status.AllowUserToAddRows = false;
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string category = cmbmune.SelectedItem?.ToString();
            string details = textDetails.Text.Trim();

            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(details))
            {
                MessageBox.Show("Please select a category and enter details.");
                return;
            }

            if (IsExisting(category, details))
            {
                MessageBox.Show($"{category} '{details}' already exists.");
            }
            else
            {
                InsertRecord(category, details);
                MessageBox.Show($"{category} '{details}' has been added.");
            }
        }
        private bool IsExisting(string category, string details)
        {
            string columnName = GetColumnName(category);

            using (MySqlCommand cmd = new MySqlCommand($"SELECT COUNT(*) FROM tbl_cms_smart WHERE { columnName } = @Details",conn.connection))
            {
               
                cmd.Parameters.AddWithValue("@Details", details);
                var result = cmd.ExecuteScalar();
                int count = Convert.ToInt32(result);
                return count > 0;
            }
        }

        private void InsertRecord(string category, string details)
        {
            string columnName = GetColumnName(category);
 

            using (MySqlCommand cmd = new MySqlCommand($"INSERT INTO tbl_cms_smart ({columnName}) VALUES (@Details)",conn.connection))
            {
              
                cmd.Parameters.AddWithValue("@Details", details);

                cmd.ExecuteNonQuery();
            }
        }

        private string GetColumnName(string category)
        {
            switch (category)
            {
                case "Functional Category":
                    return "fc";
                case "Owner":
                    return "owner";
                case "Status":
                    return "status";
                default:
                    throw new ArgumentException("Invalid category");
            }
        }
    }
}
