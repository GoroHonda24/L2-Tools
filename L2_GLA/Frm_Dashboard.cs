using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using LiveCharts.Defaults;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;

namespace L2_GLA
{
    public partial class Frm_Dashboard : Form
    {
        DBconnect conn = new DBconnect();

        string user = "";
        public Frm_Dashboard()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();

        }
        public void CartesianChart()
        {


            try
            {
                DateTime minDate = DateTime.MaxValue;
                DateTime maxDate = DateTime.MinValue;

                cartesianChart1.Series.Clear();
                cartesianChart1.AxisX.Clear();
                cartesianChart1.AxisY.Clear();
                SeriesCollection series = new SeriesCollection();
                

                using (MySqlCommand cmd = new MySqlCommand("SELECT DATE_FORMAT(STR_TO_DATE(time_stamp, '%m-%d-%Y'), '%m-%d-%Y') AS date, status,COUNT(uts_id) AS uts_count FROM tbl_triage WHERE STR_TO_DATE(time_stamp, '%m-%d-%Y') BETWEEN DATE_SUB(CURDATE(), INTERVAL 7 DAY) AND CURDATE() GROUP BY DATE_FORMAT(STR_TO_DATE(time_stamp, '%m-%d-%Y'), '%m-%d-%Y'), status ORDER BY date ", conn.connection))
                {
                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string dateString = reader["date"].ToString();
                            DateTime date;

                            if(DateTime.TryParseExact(dateString, "MM-dd-yyyy",CultureInfo.InvariantCulture ,DateTimeStyles.None, out date))
                            {
                                string status = reader["status"].ToString();
                                int count = Convert.ToInt32(reader["uts_count"]);
                              //  DateTime filterdate = Convert.ToDateTime(reader["date"]);
                                // Update minDate and maxDate based on the current date
                                if (date < minDate)
                                    minDate = date;
                                if (date > maxDate)
                                    maxDate = date;
                                // Check if there's already a series for the current status
                                var existingSeries = series.FirstOrDefault(s => s.Title == status);

                                if (existingSeries == null)
                                {
                                    // If no series exists for the current status, create a new one
                                    existingSeries = new LineSeries
                                    {
                                        Title = status,
                                        Values = new ChartValues<ObservablePoint>(),
                                        DataLabels = false, // Optionally set to true if you want to display labels on the data points
                                        ScalesYAt = 0 // Use the primary Y axis
                                    };
                                    series.Add(existingSeries);
                                }
                                existingSeries.Values.Add(new ObservablePoint(date.ToOADate(), count));
                            }                        
                         
                        }
                    }
                }

                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "Date",
                    LabelFormatter = value => DateTime.FromOADate(value).ToString("MM-dd-yyyy"), // Format the X-axis labels as desired
                    MinValue = minDate.ToOADate(), // Set the minimum value of the X-axis based on the minDate
                    MaxValue = maxDate.ToOADate() // Set the maximum value of the X-axis based on the maxDate
                });

                // Set the chart's series, Y-axis, and legend location
                cartesianChart1.Series = series;
                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Count",
                    LabelFormatter = value => value.ToString()
                });
                cartesianChart1.LegendLocation = LegendLocation.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void piechart()
        {
            int seriesCount = 0;

            try
            {
                pieChart1.Series.Clear();
                using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(uts_id) as uts_count, user FROM tbl_triage GROUP BY user", conn.connection))
                {

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string user = reader["user"].ToString();
                            int count = Convert.ToInt32(reader["uts_count"]);

                            string seriesName = "USER" + seriesCount;

                            // Check if seriesName already exists
                            var existingSeries = pieChart1.Series.FirstOrDefault(s => s.Title == seriesName);

                            if (existingSeries == null)
                            {
                                // If it doesn't exist, create a new series
                                var newSeries = new LiveCharts.Wpf.PieSeries
                                {

                                    Title = "",
                                    Values = new LiveCharts.ChartValues<int> { count },
                                    DataLabels = true,
                                    LabelPoint = point => $"{user}: {count}"

                                };

                                pieChart1.Series.Add(newSeries);
                                seriesCount++;
                            }
                            else
                            {
                                // If it exists, add data to the existing series
                                existingSeries.Values.Add(count);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        public void piechatDateOnly()
        {
            int seriesCount = 0;

            try
            {
                pieChart1.Series.Clear();
                using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(uts_id) as uts_count, user FROM tbl_triage where time_stamp >= @from and time_stamp <= @to GROUP BY user;", conn.connection))
                {
                    cmd.Parameters.AddWithValue("@from", dtpfrom.Value.ToString("MM-dd-yyyy 00:00:00"));
                    cmd.Parameters.AddWithValue("@to", dtpto.Value.ToString("MM-dd-yyyy 23:59:59"));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string user = reader["user"].ToString();
                            int count = Convert.ToInt32(reader["uts_count"]);

                            string seriesName = "USER" + seriesCount;

                            // Check if seriesName already exists
                            var existingSeries = pieChart1.Series.FirstOrDefault(s => s.Title == seriesName);

                            if (existingSeries == null)
                            {
                                // If it doesn't exist, create a new series
                                var newSeries = new LiveCharts.Wpf.PieSeries
                                {

                                    Title = "",
                                    Values = new LiveCharts.ChartValues<int> { count },
                                    DataLabels = true,
                                    LabelPoint = point => $"{user}: {count}"

                                };

                                pieChart1.Series.Add(newSeries);
                                seriesCount++;
                            }
                            else
                            {
                                // If it exists, add data to the existing series
                                existingSeries.Values.Add(count);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        public void CartDateOnly()
        {
            try
            {
                DateTime minDate = DateTime.MaxValue;
                DateTime maxDate = DateTime.MinValue;

                cartesianChart1.Series.Clear();
                cartesianChart1.AxisX.Clear();
                cartesianChart1.AxisY.Clear();
                SeriesCollection series = new SeriesCollection();


                using (MySqlCommand cmd = new MySqlCommand("SELECT DATE_FORMAT(STR_TO_DATE(time_stamp, '%m-%d-%Y %H:%i:%s'),'%m-%d-%Y') AS date,status,COUNT(uts_id) AS uts_count FROM tbl_triage " +
                "INNER JOIN users ON users.user_name = tbl_triage.user WHERE DATE(STR_TO_DATE(time_stamp, '%m-%d-%Y %H:%i:%s')) BETWEEN @from AND @to " +
                "GROUP BY DATE_FORMAT(STR_TO_DATE(time_stamp, '%m-%d-%Y %H:%i:%s'),'%m-%d-%Y'), status ORDER BY date", conn.connection))
                {
                    cmd.Parameters.AddWithValue("@from", dtpfrom.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@to", dtpto.Value.ToString("yyyy-MM-dd"));
                    Console.WriteLine("Dates from: " + dtpfrom.Value.ToString("yyyy-MM-dd") + ", to: " + dtpto.Value.ToString("yyyy-MM-dd"));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string dateString = reader["date"].ToString();
                            DateTime date;

                            if (DateTime.TryParseExact(dateString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                            {
                                string status = reader["status"].ToString();
                                int count = Convert.ToInt32(reader["uts_count"]);
                                //  DateTime filterdate = Convert.ToDateTime(reader["date"]);
                                // Update minDate and maxDate based on the current date
                                if (date < minDate)
                                    minDate = date;
                                if (date > maxDate)
                                    maxDate = date;
                                // Check if there's already a series for the current status
                                var existingSeries = series.FirstOrDefault(s => s.Title == status);

                                if (existingSeries == null)
                                {
                                    // If no series exists for the current status, create a new one
                                    existingSeries = new LineSeries
                                    {
                                        Title = status,
                                        Values = new ChartValues<ObservablePoint>(),
                                        DataLabels = false, // Optionally set to true if you want to display labels on the data points
                                        ScalesYAt = 0 // Use the primary Y axis
                                    };
                                    series.Add(existingSeries);
                                }
                                existingSeries.Values.Add(new ObservablePoint(date.ToOADate(), count));
                            }
                            //DateTime date = Convert.ToDateTime(reader["date"]);
                            //string status = reader["status"].ToString();
                            //int count = Convert.ToInt32(reader["uts_count"]);
                            //DateTime filterdate = Convert.ToDateTime(reader["date"]);
                            //// Update minDate and maxDate based on the current date
                            //if (filterdate < minDate)
                            //    minDate = filterdate;
                            //if (filterdate > maxDate)
                            //    maxDate = filterdate;
                            //// Check if there's already a series for the current status
                            //var existingSeries = series.FirstOrDefault(s => s.Title == status);

                            //if (existingSeries == null)
                            //{
                            //    // If no series exists for the current status, create a new one
                            //    existingSeries = new LineSeries
                            //    {
                            //        Title = status,
                            //        Values = new ChartValues<ObservablePoint>(),
                            //        DataLabels = false, // Optionally set to true if you want to display labels on the data points
                            //        ScalesYAt = 0 // Use the primary Y axis
                            //    };
                            //    series.Add(existingSeries);
                            //}

                            //// Add the data point to the series
                            //existingSeries.Values.Add(new ObservablePoint(date.ToOADate(), count));
                        }
                    }
                }
                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "Date",
                    LabelFormatter = value => DateTime.FromOADate(value).ToString("MM-dd-yyyy"),
                    MinValue = minDate.ToOADate(),
                    MaxValue = maxDate.ToOADate()
                });

                // Set the chart's series, Y-axis, and legend location
                cartesianChart1.Series = series;
                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Count",
                    LabelFormatter = value => value.ToString()
                });
                cartesianChart1.LegendLocation = LegendLocation.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void Frm_Dashboard_Load(object sender, EventArgs e)
        {
            //selected_user();
            //piechart();
           
            cbbuserlist();
            dtpto.Value = DateTime.Today;
            dtpfrom.Value = DateTime.Today;
            //dtpfrom.MaxDate = DateTime.Today;
            filterDateUser();

        }
        public void cbbuserlist()
        {

            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM brand_synch_2.users where selected = 0 order by user_name asc", conn.connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    cbbUser.Items.Clear();
                    while (reader.Read())
                    {
                        cbbUser.Items.Add(reader["user_name"]).ToString();
                    }
                    reader.Close();
                }
            }


        }
        public void selected_user()
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT user_name FROM brand_synch_2.users where selected = 1", conn.connection))
            {
                using (MySqlDataReader Reader = cmd.ExecuteReader())
                {
                    dgvSelected_user.Rows.Clear();
                    dgvSelected_user.AllowUserToAddRows = true;
                    if (Reader.HasRows)
                    {

                        while (Reader.Read())
                        {
                            DataGridViewRow row = (DataGridViewRow)dgvSelected_user.Rows[0].Clone();
                            row.Cells[0].Value = Reader["user_name"];


                            dgvSelected_user.Rows.Add(row);
                        }
                        dgvSelected_user.AllowUserToAddRows = false;
                    }
                    Reader.Close();
                }
            }
        }
        public void filterbydate()
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT uts_id,call_id,system_name,fc,status,reason,concern,error,work_around,findings,bulk,user FROM tbl_triage" +
                " inner JOIN users ON users.user_name = tbl_triage.user WHERE users.selected = 1 and tbl_triage.time_stamp >= @from and tbl_triage.time_stamp <= @to order by user asc", conn.connection))
            {
                cmd.Parameters.AddWithValue("@from", dtpfrom.Value.ToString("MM-dd-yyyy 00:00:00"));
                cmd.Parameters.AddWithValue("@to", dtpto.Value.ToString("MM-dd-yyyy 23:59:59"));
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dgvlist.Rows.Clear();
                    dgvlist.AllowUserToAddRows = true;
                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();

                        row.Cells[0].Value = reader["uts_id"];
                        row.Cells[1].Value = reader["call_id"];
                        row.Cells[2].Value = reader["system_name"];
                        row.Cells[3].Value = reader["fc"];
                        row.Cells[4].Value = reader["status"];
                        row.Cells[5].Value = reader["reason"];
                        row.Cells[6].Value = reader["concern"];
                        row.Cells[7].Value = reader["error"];
                        row.Cells[8].Value = reader["work_around"];
                        row.Cells[9].Value = reader["findings"];
                        row.Cells[10].Value = reader["bulk"];
                        row.Cells[11].Value = reader["user"];
                        dgvlist.Rows.Add(row);
                    }

                    dgvlist.AllowUserToAddRows = false;

                }
            }
        }
        public void filterUserOnly()
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT uts_id,call_id,system_name,fc,status,reason,concern,error,work_around,findings,bulk,user FROM tbl_triage" +
                 " inner JOIN users ON users.user_name = tbl_triage.user WHERE users.selected = 1 order by user asc", conn.connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dgvlist.Rows.Clear();
                    dgvlist.AllowUserToAddRows = true;
                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();

                        row.Cells[0].Value = reader["uts_id"];
                        row.Cells[1].Value = reader["call_id"];
                        row.Cells[2].Value = reader["system_name"];
                        row.Cells[3].Value = reader["fc"];
                        row.Cells[4].Value = reader["status"];
                        row.Cells[5].Value = reader["reason"];
                        row.Cells[6].Value = reader["concern"];
                        row.Cells[7].Value = reader["error"];
                        row.Cells[8].Value = reader["work_around"];
                        row.Cells[9].Value = reader["findings"];
                        row.Cells[10].Value = reader["bulk"];
                        row.Cells[11].Value = reader["user"];
                        dgvlist.Rows.Add(row);
                    }

                    dgvlist.AllowUserToAddRows = false;

                }
            }
        }
        public void filterDateUser()
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT user_name FROM brand_synch_2.users where selected = 1", conn.connection))
            {
                using (MySqlDataReader Reader = cmd.ExecuteReader())
                {
                    if (Reader.HasRows)
                    {
                        Reader.Close();
                        if (cbDate.Checked == true)
                        {
                            DisplayPie_UserDate();
                            if(dtpfrom.Value != DateTime.Today)
                            {
                                DisplayCart_UserDate();
                            }
                            

                            filterbydate();
                        }
                        else
                        {
                            filterUserOnly();
                            DisplayPie_User();
                            DisplayCart_User();
                        }
                    }
                    else
                    {
                        Reader.Close();
                        if (cbDate.Checked == true)
                        {
                            filterbyDateonly();
                            piechatDateOnly();
                            if (dtpfrom.Value != DateTime.Today)
                            {
                                CartDateOnly();
                            }
                        }
                        else
                        {
                            piechart();
                            CartesianChart();
                            dgvlist.Rows.Clear();
                        }
                    }
                }
            }
            selected_user();
            cbbuserlist();
        }
        public void filterbyDateonly()
        {

            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbl_triage WHERE time_stamp >= @from and time_stamp <= @to order by user asc", conn.connection))
            {
                cmd.Parameters.AddWithValue("@from", dtpfrom.Value.ToString("MM-dd-yyyy 00:00:00"));
                cmd.Parameters.AddWithValue("@to", dtpto.Value.ToString("MM-dd-yyyy 23:59:59"));
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dgvlist.Rows.Clear();
                    dgvlist.AllowUserToAddRows = true;
                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvlist.Rows[0].Clone();

                        row.Cells[0].Value = reader["uts_id"];
                        row.Cells[1].Value = reader["call_id"];
                        row.Cells[2].Value = reader["system_name"];
                        row.Cells[3].Value = reader["fc"];
                        row.Cells[4].Value = reader["status"];
                        row.Cells[5].Value = reader["reason"];
                        row.Cells[6].Value = reader["concern"];
                        row.Cells[7].Value = reader["error"];
                        row.Cells[8].Value = reader["work_around"];
                        row.Cells[9].Value = reader["findings"];
                        row.Cells[10].Value = reader["bulk"];
                        row.Cells[11].Value = reader["user"];
                        dgvlist.Rows.Add(row);
                    }

                    dgvlist.AllowUserToAddRows = false;

                }
            }
        }

        public void DisplayPie_UserDate()
        {
            pieChart1.Series.Clear();

            using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(uts_id) as uts_count, user FROM tbl_triage inner JOIN users ON users.user_name = tbl_triage.user WHERE users.selected = 1 and time_stamp >=@from and time_stamp <= @to group by user order by user asc", conn.connection))
            {
                cmd.Parameters.AddWithValue("@from", dtpfrom.Value.ToString("MM-dd-yyyy 00:00:00"));
                cmd.Parameters.AddWithValue("@to", dtpto.Value.ToString("MM-dd-yyyy 23:59:59"));

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string user = reader["user"].ToString();
                        int count = Convert.ToInt32(reader["uts_count"]);

                        var newSeries = new LiveCharts.Wpf.PieSeries
                        {
                            Title = "",
                            Values = new LiveCharts.ChartValues<int> { count },
                            DataLabels = true,
                            LabelPoint = point => $"{user}: {count}"
                        };

                        pieChart1.Series.Add(newSeries); 
                    }
                }
            }        
        
        }
        public void DisplayCart_UserDate()
        {
           

            //try
            //{
                DateTime minDate = DateTime.MaxValue;
                DateTime maxDate = DateTime.MinValue;

                cartesianChart1.Series.Clear();
                cartesianChart1.AxisX.Clear();
                cartesianChart1.AxisY.Clear();
                SeriesCollection series = new SeriesCollection();


                using (MySqlCommand cmd = new MySqlCommand("SELECT DATE_FORMAT(STR_TO_DATE(time_stamp, '%m-%d-%Y %H:%i:%s'),'%m-%d-%Y') AS date,status,COUNT(uts_id) AS uts_count FROM tbl_triage " +
                "INNER JOIN users ON users.user_name = tbl_triage.user WHERE users.selected = 1 AND DATE(STR_TO_DATE(time_stamp, '%m-%d-%Y %H:%i:%s')) BETWEEN @from AND @to " +
                "GROUP BY DATE_FORMAT(STR_TO_DATE(time_stamp, '%m-%d-%Y %H:%i:%s'),'%m-%d-%Y'), status ORDER BY date", conn.connection))
                {
                    cmd.Parameters.AddWithValue("@from", dtpfrom.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@to", dtpto.Value.ToString("yyyy-MM-dd"));
                   // Console.WriteLine("Dates from: " + dtpfrom.Value.ToString("yyyy-MM-dd") + ", to: " + dtpto.Value.ToString("yyyy-MM-dd"));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string dateString = reader["date"].ToString();
                            DateTime date;

                            if (DateTime.TryParseExact(dateString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                            {
                                string status = reader["status"].ToString();
                                int count = Convert.ToInt32(reader["uts_count"]);
                                //  DateTime filterdate = Convert.ToDateTime(reader["date"]);
                                // Update minDate and maxDate based on the current date
                                if (date < minDate)
                                    minDate = date;
                                if (date > maxDate)
                                    maxDate = date;
                                // Check if there's already a series for the current status
                                var existingSeries = series.FirstOrDefault(s => s.Title == status);

                                if (existingSeries == null)
                                {
                                    // If no series exists for the current status, create a new one
                                    existingSeries = new LineSeries
                                    {
                                        Title = status,
                                        Values = new ChartValues<ObservablePoint>(),
                                        DataLabels = false, // Optionally set to true if you want to display labels on the data points
                                        ScalesYAt = 0 // Use the primary Y axis
                                    };
                                    series.Add(existingSeries);
                                }
                                existingSeries.Values.Add(new ObservablePoint(date.ToOADate(), count));
                            }
                        }
                    }
                }
                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "Date",
                    LabelFormatter = value => DateTime.FromOADate(value).ToString("MM-dd-yyyy"), // Format the X-axis labels as desired
                    MinValue = minDate.ToOADate(), // Set the minimum value of the X-axis based on the minDate
                    MaxValue = maxDate.ToOADate()
                });

                // Set the chart's series, Y-axis, and legend location
                cartesianChart1.Series = series;
                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Count",
                    LabelFormatter = value => value.ToString()
                });
                cartesianChart1.LegendLocation = LegendLocation.Right;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}

        }
        public void DisplayPie_User()
        {
            pieChart1.Series.Clear(); // Clear existing series outside the loop
            using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(uts_id) as uts_count, user FROM tbl_triage inner JOIN users ON users.user_name = tbl_triage.user WHERE users.selected = 1 group by user order by user asc", conn.connection))
            {

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string user = reader["user"].ToString();
                        int count = Convert.ToInt32(reader["uts_count"]);

                        var newSeries = new LiveCharts.Wpf.PieSeries
                        {
                            Title = "",
                            Values = new LiveCharts.ChartValues<int> { count },
                            DataLabels = true,
                            LabelPoint = point => $"{user}: {count}"
                        };

                        pieChart1.Series.Add(newSeries); // Add series directly to the PieChart
                    }
                }
            }
            if (pieChart1.Series.Count == 0)
            {
                piechart(); // You may need to define the behavior of this method
            }
        }
        public void DisplayCart_User()
        {
            //try
            //{
                DateTime minDate = DateTime.MaxValue;
                DateTime maxDate = DateTime.MinValue;

                cartesianChart1.Series.Clear();
                cartesianChart1.AxisX.Clear();
                cartesianChart1.AxisY.Clear();
                SeriesCollection series = new SeriesCollection();

                using (MySqlCommand cmd = new MySqlCommand("SELECT DATE_FORMAT(STR_TO_DATE(time_stamp, '%m-%d-%Y %H:%i:%s'),'%m-%d-%Y') AS date,status,COUNT(uts_id) AS uts_count FROM tbl_triage " +
                "INNER JOIN users ON users.user_name = tbl_triage.user WHERE users.selected = 1 " +
                "GROUP BY DATE_FORMAT(STR_TO_DATE(time_stamp, '%m-%d-%Y %H:%i:%s'),'%m-%d-%Y'), status ORDER BY date", conn.connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string dateString = reader["date"].ToString();
                            DateTime date;

                            if (DateTime.TryParseExact(dateString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                            {
                                string status = reader["status"].ToString();
                                int count = Convert.ToInt32(reader["uts_count"]);
                                //  DateTime filterdate = Convert.ToDateTime(reader["date"]);
                                // Update minDate and maxDate based on the current date
                                if (date < minDate)
                                    minDate = date;
                                if (date > maxDate)
                                    maxDate = date;
                                // Check if there's already a series for the current status
                                var existingSeries = series.FirstOrDefault(s => s.Title == status);

                                if (existingSeries == null)
                                {
                                    // If no series exists for the current status, create a new one
                                    existingSeries = new LineSeries
                                    {
                                        Title = status,
                                        Values = new ChartValues<ObservablePoint>(),
                                        DataLabels = false, // Optionally set to true if you want to display labels on the data points
                                        ScalesYAt = 0 // Use the primary Y axis
                                    };
                                    series.Add(existingSeries);
                                }
                                existingSeries.Values.Add(new ObservablePoint(date.ToOADate(), count));
                            }
                        }
                    }
                }

                // Check if both minDate and maxDate are valid
                if (minDate != DateTime.MaxValue && maxDate != DateTime.MinValue)
                {
                    cartesianChart1.AxisX.Add(new Axis
                    {
                        Title = "Date",
                        LabelFormatter = value => DateTime.FromOADate(value).ToString("MM-dd-yyyy"), // Format the X-axis labels as desired
                        MinValue = minDate.ToOADate(), // Set the minimum value of the X-axis based on the minDate
                        MaxValue = maxDate.ToOADate()
                    });
                }
                else
                {
                    // Handle the case where the range is invalid
                    MessageBox.Show("Invalid date range. Please ensure your data contains valid dates.");
                }

                // Set the chart's series, Y-axis, and legend location
                cartesianChart1.Series = series;
                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Count",
                    LabelFormatter = value => value.ToString()
                });
                cartesianChart1.LegendLocation = LegendLocation.Right;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}
        }

        private void cbbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            user = cbbUser.Text;
            using (MySqlCommand updateCmd = new MySqlCommand("UPDATE `brand_synch_2`.`users` SET `selected` = '1' WHERE `user_name` = @user", conn.connection))
            {
                updateCmd.Parameters.AddWithValue("@user", cbbUser.Text);
                updateCmd.ExecuteNonQuery();
            }

            selected_user();
            cbbuserlist();
            filterDateUser();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            using (MySqlCommand cmd = new MySqlCommand("UPDATE `brand_synch_2`.`users` SET `selected` = 0", conn.connection))
            {
                cmd.ExecuteNonQuery();
            }
            dgvlist.Rows.Clear();
            dgvSelected_user.Rows.Clear();
            cbbuserlist();
            pieChart1.Series.Clear();
            cbDate.Checked = false;
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();
            piechart();
            CartesianChart();

        }

        private void dtpto_ValueChanged(object sender, EventArgs e)
        {
            if (cbDate.Checked == true)
            {
                filterDateUser();
            }

        }



        private void cbDate_CheckedChanged(object sender, EventArgs e)
        {
            filterDateUser();

        }

        private void dtpfrom_ValueChanged(object sender, EventArgs e)
        {
            if (cbDate.Checked == true)
            {
                
                    filterDateUser();
                
                

            }

        }
        public void removeuser(object sender, EventArgs e)
        {
            using (MySqlCommand updateCmd = new MySqlCommand("UPDATE `brand_synch_2`.`users` SET `selected` = '0' WHERE `user_name` = @user", conn.connection))
            {
                string system = dgvSelected_user.Rows[dgvSelected_user.CurrentCell.RowIndex].Cells[0].Value.ToString();
                updateCmd.Parameters.AddWithValue("@user", system);
                updateCmd.ExecuteNonQuery();
            }

            filterDateUser();

        }
        private void dgvSelected_user_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            if (dgvSelected_user.Rows.Count != 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (e.RowIndex == -1) return;
                    if (e.ColumnIndex == -1) return;

                    dgvSelected_user.CurrentCell = dgvSelected_user.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvSelected_user.Rows[e.RowIndex].Selected = true;
                    dgvSelected_user.Focus();
                    ToolStripMenuItem mnuremove = new ToolStripMenuItem("Remove");
                    mnuremove.Click += new EventHandler(removeuser);
                    mnu.Items.AddRange(new ToolStripItem[] { mnuremove });
                    dgvSelected_user.ContextMenuStrip = mnu;
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                    saveFileDialog.Title = "Save CSV File";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;

                        using (MySqlCommand cmd = new MySqlCommand("SELECT uts_id,call_id,system_name,fc,status,reason,concern,error,work_around,findings,bulk,user FROM tbl_triage INNER JOIN users ON users.user_name = tbl_triage.user WHERE users.selected = 1 order by user asc;", conn.connection))
                        {
                            using (MySqlDataAdapter reader = new MySqlDataAdapter(cmd))
                            {
                                DataTable traige = new DataTable();
                                reader.Fill(traige);

                                string csvheader = "INCIDENT ID, CALL ID,SYSTEM NAME, Functional Category, STATUS, REASON, CONCERN, ERROR, WORK AROUND, FINDING, BULK , USER";
                                SaveToCsv(traige, filePath, csvheader);
                            }
                        }
                    }

                    MessageBox.Show("Export successful!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }
        private void SaveToCsv(DataTable dataTable, string filePath, string csvheader)
        {

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(csvheader);
                foreach (DataRow row in dataTable.Rows)
                {
                    writer.WriteLine(string.Join(",", row.ItemArray));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvlist.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "L2 Triage Daily Report ";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dgvlist.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[dgvlist.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += dgvlist.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < dgvlist.Rows.Count; i++)
                            {
                                var rowData = dgvlist.Rows[i - 1].Cells.Cast<DataGridViewCell>().Select(cell => cell.Value.ToString().Replace("\n", " ").Replace("\r", " ").Replace(",", " "));
                                outputCsv[i] = string.Join(",", rowData);
                            }

                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }

        }

        private void dgvlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

   
