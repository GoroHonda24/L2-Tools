using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace L2_GLA
{
    public class dashboardRepository
    {
        private readonly DBconnect _conn;
        private int count = 0;
        public dashboardRepository()
        {
            _conn = new DBconnect();
            if (_conn.connection.State != ConnectionState.Open) _conn.connection.Open();
        }

        public bool CheckSelectedValue()
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT count(user_name) as count FROM brand_synch_2.users where team = 'L2 Smart App' and selected = '1' order by user_name asc", _conn.connection))
            {
                 count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }          
        }
        public DataTable GetAllData(string dtpfrom, string dtpto)
        {
            DataTable dataTable = new DataTable();
            bool isSelected = CheckSelectedValue();
            string query = "SELECT inc, min, fc, owner, status, notes, created_at, user " +
                   "FROM brand_synch_2.tbl_inc_logs " +
                   "WHERE created_at >= @startDate AND created_at <= @endDate " +
                   "ORDER BY created_at DESC";

            if (isSelected)
            {
                query = "SELECT inc, min, fc, owner, status, notes, created_at, user " +
                        "FROM brand_synch_2.tbl_inc_logs INNER JOIN users ON users.user_name = tbl_inc_logs.user " +
                        "WHERE selected = 1 AND created_at >= @startDate AND created_at <= @endDate " +
                        "ORDER BY created_at DESC";
            }

            // Execute the query and fill the DataTable
            using (MySqlCommand cmd = new MySqlCommand(query, _conn.connection))
            {
                cmd.Parameters.AddWithValue("@startDate", dtpfrom);
                cmd.Parameters.AddWithValue("@endDate", dtpto);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;

        }

        public DataTable GetStatusAsGroup(string startDate, string endDate)
        {
            DataTable dataTable = new DataTable();
            bool isSelected = CheckSelectedValue();
            string query = "SELECT user, COUNT(status) AS count, status " +
                   "FROM brand_synch_2.tbl_inc_logs " +
                   "WHERE created_at >= @startDate AND created_at <= @endDate " +
                   "GROUP BY status, user " +
                   "ORDER BY created_at DESC, user";

            if (isSelected)
            {
                query = "SELECT COUNT(status) AS count, user, status " +
                        "FROM brand_synch_2.tbl_inc_logs INNER JOIN users ON users.user_name = tbl_inc_logs.user " +
                        "WHERE selected = 1 AND created_at >= @startDate AND created_at <= @endDate " +
                        "GROUP BY status, user " +
                        "ORDER BY created_at DESC, user";
            }

            // Execute the query and fill the DataTable
            using (MySqlCommand cmd = new MySqlCommand(query, _conn.connection))
            {
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }

        public DataTable GetUser()
        {
            DataTable dataTable = new DataTable();

            using (MySqlCommand cmd = new MySqlCommand("SELECT user_name FROM brand_synch_2.users where team = 'L2 Smart App' and selected = '0' order by user_name asc", _conn.connection))
            {

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }

            }

            return dataTable;
        }

        public void SaveSelectedStatus(string selectedUser, string seletedValue)
        {
            using (MySqlCommand cmd = new MySqlCommand("UPDATE `brand_synch_2`.`users` SET `selected` = @value WHERE `user_name` = @username",_conn.connection))
            {
                cmd.Parameters.AddWithValue("@username", selectedUser);
                cmd.Parameters.AddWithValue("@value", seletedValue);
                cmd.ExecuteNonQuery();
            }
        }


        public DataTable GetSelectedUser()
        {
            DataTable dataTable = new DataTable();

            using (MySqlCommand cmd = new MySqlCommand("SELECT user_name FROM brand_synch_2.users where team = 'L2 Smart App' and selected = '1' order by user_name asc", _conn.connection))
            {

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }

            }

            return dataTable;
        }
        public DataTable GetCount(string type, string startDate, string endDate)
        {
            DataTable dataTable = new DataTable();
            string query = string.Empty;
            bool isSelected = CheckSelectedValue();

            switch (type)
            {
                case "SmartApp":
                    query = "SELECT COUNT(inc) AS count FROM brand_synch_2.tbl_inc_logs " +
                            "WHERE owner = 'Smart App' AND created_at >= @startDate AND created_at <= @endDate";
                    if (isSelected)
                    {
                        query = "SELECT COUNT(inc) AS count FROM brand_synch_2.tbl_inc_logs INNER JOIN users ON users.user_name = tbl_inc_logs.user " +
                                "WHERE selected = 1 AND owner = 'Smart App' AND created_at >= @startDate AND created_at <= @endDate";
                    }
                    break;

                case "TotalTickets":
                    query = "SELECT COUNT(DISTINCT inc) AS count FROM brand_synch_2.tbl_inc_logs";                    
                    break;

                case "TotalTicketsToday":
                    query = "SELECT COUNT(inc) AS count FROM brand_synch_2.tbl_inc_logs " +
                            "WHERE created_at >= @startDate AND created_at <= @endDate";
                    if (isSelected)
                    {
                        query = "SELECT COUNT(inc) AS count FROM brand_synch_2.tbl_inc_logs INNER JOIN users ON users.user_name = tbl_inc_logs.user " +
                                "WHERE selected = 1 AND created_at >= @startDate AND created_at <= @endDate";
                    }
                    break;

                default:
                    throw new ArgumentException("Invalid type specified");
            }

            using (MySqlCommand cmd = new MySqlCommand(query, _conn.connection))
            {
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }

        public DataTable GetStatusCounts(string startDate, string endDate)
        {
            DataTable dataTable = new DataTable();
            bool isSelected = CheckSelectedValue();
            string query = "SELECT COUNT(inc) AS count, status " +
                           "FROM brand_synch_2.tbl_inc_logs INNER JOIN users ON users.user_name = tbl_inc_logs.user " +
                           "WHERE created_at >= @startDate AND created_at <= @endDate " +
                           "GROUP BY status;";
            if (isSelected)
            {
               query = "SELECT COUNT(inc) AS count, status " +
                   "FROM brand_synch_2.tbl_inc_logs INNER JOIN users ON users.user_name = tbl_inc_logs.user " +
                   "WHERE selected = 1 AND created_at >= @startDate AND created_at <= @endDate " +
                   "GROUP BY status";
            }

            using (MySqlCommand cmd = new MySqlCommand(query, _conn.connection))
            {
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }

        public DataTable GetUserCount(string startDate, string endDate)
        {
            DataTable userCounts = new DataTable();
            bool isSelected = CheckSelectedValue();

            string query = "SELECT COUNT(inc) AS count, user FROM brand_synch_2.tbl_inc_logs WHERE created_at >= @startDate AND created_at <= @endDate GROUP BY user";
            if (isSelected)
            {
                query = "SELECT COUNT(inc) AS count, user FROM brand_synch_2.tbl_inc_logs INNER JOIN users ON users.user_name = tbl_inc_logs.user WHERE selected = 1 AND" +
                    " created_at >= @startDate AND created_at <= @endDate GROUP BY user";
            }

            using (MySqlCommand cmd = new MySqlCommand(query, _conn.connection))
            {
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    userCounts.Load(reader);
                }
            }
            return userCounts;
        }



    }
}
