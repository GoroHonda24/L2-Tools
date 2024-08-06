using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;


namespace L2_GLA
{
    public partial class Frm_Optima : Form
    {
        private OracleConnection conn;
        private OracleConnection conn2;

        public Frm_Optima()
        {
            InitializeComponent();
          //  InitializeOracleConnection();
          //InitializeOracleConnection2();
         
        }
        private void InitializeOracleConnection()
        {
            // Replace the connection details with your own
            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.126.112.32)(PORT=1521)))(CONNECT_DATA=(SID=REPCTLG)));User Id=arbor;Password=arbor2020!;";

            conn = new OracleConnection(connectionString);
        }
        private void InitializeOracleConnection2()
        {
            // Replace the connection details with your own
            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.126.112.23)(PORT=1521)))(CONNECT_DATA=(SID=CTLG)));User Id=arbor;Password=arbor2020!;";

            conn2 = new OracleConnection(connectionString);
        }

        private void txtmin_TextChanged(object sender, EventArgs e)
        {

            if (txtmin.Text == "")
            {
                dgvlist.Rows.Clear();
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtmin.Text == "")
            {
                dgvlist.Rows.Clear();
                return;
            }

            //string external_id = "";
            using (OracleCommand cmd = new OracleCommand("with p_data as (select external_id, account_no, subscr_no, server_id, active_date, inactive_date from external_Id_equip_map @ctlg a where external_id in" +
                   " (:external))select p.external_id, p.account_no, p.subscr_no,p.server_id, p.active_date, p.inactive_date, sed.param_value from service @cust1  s join p_data p on s.subscr_no = p.subscr_no" +
                   " join service_ext_data@cust1 sed on s.view_id = sed.view_id and sed.param_id = 400003 and s.parent_account_no = p.account_no union all select p2.external_id, p2.account_no, p2.subscr_no," +
                   " p2.server_id,p2.active_date, p2.inactive_date, sed2.param_value from service @cust2  s2 join p_data p2 on s2.subscr_no = p2.subscr_no join service_ext_data@cust2 sed2 on s2.view_id = " +
                   "sed2.view_id and sed2.param_id = 400003 and s2.parent_account_no = p2.account_no union all select p.external_id, p.account_no, p.subscr_no,p.server_id, p.active_date,p.inactive_date," +
                   " sed3.param_value from service @cust3  s3 join p_data p on s3.subscr_no = p.subscr_no join service_ext_data@cust3 sed3 on s3.view_id = sed3.view_id and sed3.param_id = 400003 and s3.parent_account_no = p.account_no", conn))
            {
                cmd.Parameters.Add(":external", OracleDbType.Varchar2).Value = txtmin.Text;
                conn.Open();

                using (OracleDataReader Reader = cmd.ExecuteReader())
                {
                    dgvlist.Rows.Clear();
                    dgvlist.AllowUserToAddRows = true;
                    while (Reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();
                        row.Cells[0].Value = Reader["EXTERNAL_ID"].ToString();
                        row.Cells[1].Value = Reader["ACCOUNT_NO"].ToString();
                        row.Cells[2].Value = Reader["SUBSCR_NO"].ToString();
                        row.Cells[3].Value = Reader["SERVER_ID"].ToString();
                        row.Cells[4].Value = Reader["ACTIVE_DATE"].ToString();
                        row.Cells[5].Value = Reader["INACTIVE_DATE"].ToString();
                        row.Cells[6].Value = Reader["PARAM_VALUE"].ToString();
                        dgvlist.Rows.Add(row);
                    }
                    dgvlist.AllowUserToAddRows = false;
                }
                MessageBox.Show("successfully.");
            }
            conn.Close();

            //Optima 2
            string account2 = "";
            //string min2 = "";
            using (OracleCommand cmd = new OracleCommand("select account_no from external_id_equip_map_view@ctlg where external_id IN (:account2)", conn2))
            {
                cmd.Parameters.Add(":account2", OracleDbType.Varchar2).Value = txtmin.Text;
                conn2.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        account2 = reader["ACCOUNT_NO"].ToString();
                    }
                }

            }
            using (OracleCommand cmd = new OracleCommand("with ord_order as (select * from ord_order @cust1 union select * from ord_order @cust2 union select * from ord_order @cust3)" +
              "select cart_id, order_id, order_status_id, account_no, create_dt, complete_dt, order_type_id, comments from ord_order where account_no in (:min2)" +
              " and order_type_id IN(191, 190, 193, 196)", conn2))
            {
                cmd.Parameters.Add(":min2", OracleDbType.Varchar2).Value = account2;

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    DgvConn2.Rows.Clear();
                    DgvConn2.AllowUserToAddRows = true;
                    while (reader.Read())
                    {

                        DataGridViewRow row = (DataGridViewRow)DgvConn2.Rows[0].Clone();
                        row.Cells[0].Value = reader["cart_id"].ToString();
                        row.Cells[1].Value = reader["order_id"].ToString();
                        row.Cells[2].Value = reader["order_status_id"].ToString();
                        row.Cells[3].Value = reader["account_no"].ToString();
                        row.Cells[4].Value = reader["create_dt"].ToString();
                        row.Cells[5].Value = reader["complete_dt"].ToString();
                        row.Cells[6].Value = reader["order_type_id"].ToString();
                        row.Cells[7].Value = reader["comments"].ToString();

                        if (reader["order_type_id"].ToString() == "191")
                        {
                            row.Cells[8].Value = "Change Brand – Disconnection";

                        }
                        else if (reader["order_type_id"].ToString() == "193")
                        {
                            row.Cells[8].Value = " Port In - New Connection";
                        }
                        else if (reader["order_type_id"].ToString() == "190")
                        {
                            row.Cells[8].Value = "Change Brand - New Connection";
                        }
                        else if (reader["order_type_id"].ToString() == "195")
                        {
                            row.Cells[8].Value = " Port Out - Service disconnection";
                        }
                        else if (reader["order_type_id"].ToString() == "196")
                        {
                            row.Cells[8].Value = "Port In - Change MSISDN";
                        }
                        else
                        {

                        }
                        DgvConn2.Rows.Add(row);
                    }
                    DgvConn2.AllowUserToAddRows = false;
                }
            }
            conn2.Close();           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtmin.Text == "")
            {
                dgvlist.Rows.Clear();
                return;
            }

            string connString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.126.112.32)(PORT=1521)))(CONNECT_DATA=(SID=REPCTLG)));User Id=arbor;Password=arbor2020!";

            using (OracleConnection opticonn = new OracleConnection(connString))
            {
                try
                {
                    opticonn.Open();
                    using (OracleCommand cmd = new OracleCommand("with p_data as (select external_id, account_no, subscr_no, server_id, active_date, inactive_date from external_Id_equip_map @ctlg a where external_id in" +
                        " (:external))select p.external_id, p.account_no, p.subscr_no,p.server_id, p.active_date, p.inactive_date, sed.param_value from service @cust1  s join p_data p on s.subscr_no = p.subscr_no" +
                        " join service_ext_data@cust1 sed on s.view_id = sed.view_id and sed.param_id = 400003 and s.parent_account_no = p.account_no union all select p2.external_id, p2.account_no, p2.subscr_no," +
                        " p2.server_id,p2.active_date, p2.inactive_date, sed2.param_value from service @cust2  s2 join p_data p2 on s2.subscr_no = p2.subscr_no join service_ext_data@cust2 sed2 on s2.view_id = " +
                        "sed2.view_id and sed2.param_id = 400003 and s2.parent_account_no = p2.account_no union all select p.external_id, p.account_no, p.subscr_no,p.server_id, p.active_date,p.inactive_date," +
                        " sed3.param_value from service @cust3  s3 join p_data p on s3.subscr_no = p.subscr_no join service_ext_data@cust3 sed3 on s3.view_id = sed3.view_id and sed3.param_id = 400003 and s3.parent_account_no = p.account_no", opticonn))
                    {
                        cmd.Parameters.Add(":external", OracleDbType.Varchar2).Value = txtmin.Text;

                        using (OracleDataReader Reader = cmd.ExecuteReader())
                        {
                            dgvlist.Rows.Clear();
                            dgvlist.AllowUserToAddRows = true;
                            while (Reader.Read())
                            {
                                DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();
                                row.Cells[0].Value = Reader["EXTERNAL_ID"].ToString();
                                row.Cells[1].Value = Reader["ACCOUNT_NO"].ToString();
                                row.Cells[2].Value = Reader["SUBSCR_NO"].ToString();
                                row.Cells[3].Value = Reader["SERVER_ID"].ToString();
                                row.Cells[4].Value = Reader["ACTIVE_DATE"].ToString();
                                row.Cells[5].Value = Reader["INACTIVE_DATE"].ToString();
                                row.Cells[6].Value = Reader["PARAM_VALUE"].ToString();
                                dgvlist.Rows.Add(row);
                            }
                            dgvlist.AllowUserToAddRows = false;
                        }
                    }
                    opticonn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.126.112.23)(PORT=1521)))(CONNECT_DATA=(SID=CTLG)));User Id=arbor;Password=arbor2020!";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string account2 = "";
                    //string min2 = "";
                    using (OracleCommand cmd = new OracleCommand("select account_no from external_id_equip_map_view@ctlg where external_id IN (:account2)", connection))
                    {
                        cmd.Parameters.Add(":account2", OracleDbType.Varchar2).Value = txtmin.Text;
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                account2 = reader["ACCOUNT_NO"].ToString();
                            }
                        }
                    }
                    using (OracleCommand cmd = new OracleCommand("with ord_order as (select * from ord_order @cust1 union select * from ord_order @cust2 union select * from ord_order @cust3)" +
                                "select cart_id, order_id, order_status_id, account_no, create_dt, complete_dt, order_type_id, comments from ord_order where account_no in (:min2)" +
                                " and order_type_id IN(191, 190, 193, 196)", connection))
                    {
                        cmd.Parameters.Add(":min2", OracleDbType.Varchar2).Value = account2;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            DgvConn2.Rows.Clear();
                            DgvConn2.AllowUserToAddRows = true;
                            while (reader.Read())
                            {

                                DataGridViewRow row = (DataGridViewRow)DgvConn2.Rows[0].Clone();
                                row.Cells[0].Value = reader["cart_id"].ToString();
                                row.Cells[1].Value = reader["order_id"].ToString();
                                row.Cells[2].Value = reader["order_status_id"].ToString();
                                row.Cells[3].Value = reader["account_no"].ToString();
                                row.Cells[4].Value = reader["create_dt"].ToString();
                                row.Cells[5].Value = reader["complete_dt"].ToString();
                                row.Cells[6].Value = reader["order_type_id"].ToString();
                                row.Cells[7].Value = reader["comments"].ToString();

                                if (reader["order_type_id"].ToString() == "191")
                                {
                                    row.Cells[8].Value = "Change Brand – Disconnection";

                                }
                                else if (reader["order_type_id"].ToString() == "193")
                                {
                                    row.Cells[8].Value = " Port In - New Connection";
                                }
                                else if (reader["order_type_id"].ToString() == "190")
                                {
                                    row.Cells[8].Value = "Change Brand - New Connection";
                                }
                                else if (reader["order_type_id"].ToString() == "195")
                                {
                                    row.Cells[8].Value = " Port Out - Service disconnection";
                                }
                                else if (reader["order_type_id"].ToString() == "196")
                                {
                                    row.Cells[8].Value = "Port In - Change MSISDN";
                                }
                                else
                                {

                                }
                                DgvConn2.Rows.Add(row);
                            }
                            DgvConn2.AllowUserToAddRows = false;
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
