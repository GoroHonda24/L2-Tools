using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace L2_GLA
{
    public partial class Frm_Dashboard_smart : Form
    {
        private readonly dashboardRepository _repository;
        public Frm_Dashboard_smart()
        {
            InitializeComponent();
            _repository = new dashboardRepository();
            LoadUser();
            LoadSeleteduser();
            LoadAllData();
            LoadTotalTickets();
        }

        private void DisplayTicketsCount()
        {
            lblOthers.Text = (Convert.ToInt32(lbltotaltoday.Text) - Convert.ToInt32(lblSmartappTickets.Text)).ToString();
        }
        private void LoadAllData()
        {
            bool isSelected = _repository.CheckSelectedValue();
            string todayStart = "0";
            if (DateTime.Now >= DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd") + " 18:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))
            {
                 todayStart = DateTime.Now.ToString("yyyy-MM-dd") + " 09:00:00";
            }
            else
            {
                DateTime startDate = DateTime.Today.AddDays(-1);
                 todayStart = startDate.ToString("yyyy-MM-dd 18:00:00");
            }

            
          //  string todayStart = startDate.ToString("yyyy-MM-dd 18:00:00");
         //   string todayStart = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            string todayEnd = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

            LoadData(todayStart, todayEnd);
            LoadStatusAsGroup(todayStart, todayEnd);
            LoadSmartTickets(todayStart, todayEnd);
            LoadTicketToday(todayStart, todayEnd);
            DisplayTicketsCount();
            LoadChart(todayStart, todayEnd);
            LoadDoughnutChart(todayStart, todayEnd);
        }


        private void LoadData(string dtpfrom, string dtpto)
        {
            DataTable dataTable = _repository.GetAllData(dtpfrom,dtpto);
            dgvTicketDetails.DataSource = dataTable;

            dgvTicketDetails.Columns["inc"].HeaderText = "INC #";
            dgvTicketDetails.Columns["min"].HeaderText = "MIN";
            dgvTicketDetails.Columns["fc"].HeaderText = "Functional Category";
            dgvTicketDetails.Columns["owner"].HeaderText = "Owner";
            dgvTicketDetails.Columns["status"].HeaderText = "Status";
            dgvTicketDetails.Columns["notes"].HeaderText = "Notes";
            dgvTicketDetails.Columns["created_at"].HeaderText = "Created At";
            dgvTicketDetails.Columns["user"].HeaderText = "Process by";
        }

        private void LoadStatusAsGroup(string dtpfrom, string dtpto)
        {
            DataTable dataTable = _repository.GetStatusAsGroup(dtpfrom, dtpto);
            dgvAccomplishCount.DataSource = dataTable;

            dgvAccomplishCount.Columns["user"].HeaderText = "Process by";
            dgvAccomplishCount.Columns["status"].HeaderText = "Status";
            dgvAccomplishCount.Columns["count"].HeaderText = "Total";       
        }
        private void LoadSeleteduser()
        {
            DataTable dataTable = _repository.GetSelectedUser();
            dgvSelectedUser.DataSource = dataTable;

            dgvSelectedUser.Columns["user_name"].HeaderText = "Selected User";
        }

        private void LoadUser()
        {
            DataTable dataTable = _repository.GetUser();
            cmbUser.Items.Clear();
            cmbUser.Items.Add("All");

            foreach (DataRow row in dataTable.Rows)
            {
                cmbUser.Items.Add(row["user_name"].ToString());
            }

        }

        private void Frm_Dashboard_smart_Load(object sender, EventArgs e)
        {
            dtpStartDate.MaxDate = DateTime.Now.AddDays(-1);
            dtpEndDate.MaxDate = DateTime.Now;


        }
        private void LoadTotalTickets()
        {
            DataTable dataTable = _repository.GetCount("TotalTickets", null, null);
            if (dataTable.Rows.Count > 0)
            {
                int totalTickets = Convert.ToInt32(dataTable.Rows[0]["count"]);
                lblTotalTickets.Text = totalTickets.ToString();
            }
            else
            {
                lblTotalTickets.Text = "0";
            }
        }
        private void LoadSmartTickets(string startDate, string endDate)
        {
            DataTable dataTable = _repository.GetCount("SmartApp", startDate, endDate);
            if (dataTable.Rows.Count > 0)
            {
                int totalTickets = Convert.ToInt32(dataTable.Rows[0]["count"]);
                lblSmartappTickets.Text = totalTickets.ToString();
            }
            else
            {
                lblSmartappTickets.Text = "0";
            }
        }

        private void LoadTicketToday(string startDate, string endDate)
        {
            DataTable dataTable = _repository.GetCount("TotalTicketsToday", startDate, endDate);
            if (dataTable.Rows.Count > 0)
            {
                int totalTickets = Convert.ToInt32(dataTable.Rows[0]["count"]);
                lbltotaltoday.Text = totalTickets.ToString();
            }
            else
            {
                lbltotaltoday.Text = "0";
            }
        }
        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedUser = cmbUser.SelectedItem.ToString();
            string seletedValue = "1";
            _repository.SaveSelectedStatus(selectedUser, seletedValue);
            LoadUser();
            LoadSeleteduser();
            LoadAllData();
        }

        private void dgvSelectedUser_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            if (dgvSelectedUser.Rows.Count != 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (e.RowIndex == -1) return;
                    if (e.ColumnIndex == -1) return;

                    dgvSelectedUser.CurrentCell = dgvSelectedUser.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvSelectedUser.Rows[e.RowIndex].Selected = true;
                    dgvSelectedUser.Focus();
                    ToolStripMenuItem mnuextract = new ToolStripMenuItem("Remove");
                    mnuextract.Click += new EventHandler(Extract);

                    mnu.Items.AddRange(new ToolStripItem[] { mnuextract });
                    dgvSelectedUser.ContextMenuStrip = mnu;
                }
            }
        }

        public void Extract(object sender, EventArgs e)
        {
            string selectedUser = dgvSelectedUser.Rows[dgvSelectedUser.CurrentCell.RowIndex].Cells[0].Value.ToString();
            string seletedValue = "0";
            _repository.SaveSelectedStatus(selectedUser, seletedValue);
            LoadUser();
            LoadSeleteduser();
            LoadAllData();
        }

        private void btnLast7Days_Click(object sender, EventArgs e)
        {

            DateTime startDate = DateTime.Today.AddDays(-6);
            string startDateStr = startDate.ToString("yyyy-MM-dd 00:00:00");
            string endDateStr = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

            LoadData(startDateStr, endDateStr);
            LoadStatusAsGroup(startDateStr, endDateStr);
            LoadSmartTickets(startDateStr, endDateStr);
            LoadTicketToday(startDateStr, endDateStr);
            LoadChart(startDateStr, endDateStr);
            LoadDoughnutChart(startDateStr, endDateStr);
            LoadTotalTickets();
            DisplayTicketsCount();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            string todayStart = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            string todayEnd = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

            LoadData(todayStart, todayEnd);
            LoadStatusAsGroup(todayStart, todayEnd);
            LoadSmartTickets(todayStart, todayEnd);
            LoadTicketToday(todayStart, todayEnd);
            LoadChart(todayStart, todayEnd);
            LoadDoughnutChart(todayStart, todayEnd);
            LoadTotalTickets();
            DisplayTicketsCount();
        }

        private void btnOkCustomDate_Click(object sender, EventArgs e)
        {

            string customStartDate = dtpStartDate.Value.ToString("yyyy-MM-dd 00:00:00");
            string customEndDate = dtpEndDate.Value.ToString("yyyy-MM-dd 23:59:59");

            LoadData(customStartDate, customEndDate);
            LoadStatusAsGroup(customStartDate, customEndDate);
            LoadSmartTickets(customStartDate, customEndDate);
            LoadTicketToday(customStartDate, customEndDate);
            LoadChart(customStartDate, customEndDate);
            LoadDoughnutChart(customStartDate, customEndDate);
            LoadTotalTickets();
            DisplayTicketsCount();
        }

        private void btnLast30Days_Click(object sender, EventArgs e)
        {

        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            string startDateStr = startDate.ToString("yyyy-MM-dd 00:00:00");
            string endDateStr = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

            LoadData(startDateStr, endDateStr);
            LoadStatusAsGroup(startDateStr, endDateStr);
            LoadSmartTickets(startDateStr, endDateStr);
            LoadTicketToday(startDateStr, endDateStr);
            LoadChart(startDateStr, endDateStr);
            LoadDoughnutChart(startDateStr, endDateStr);
            LoadTotalTickets();
            DisplayTicketsCount();
        }

        private void LoadChart(string startDate, string endDate)
        {
            DataTable statusCounts = _repository.GetStatusCounts(startDate, endDate);

            // Clear existing series and titles
            chartStatus.Series.Clear();
            chartStatus.Titles.Clear();

            // Create a new series
            Series series = new Series("StatusCounts");
            series.ChartType = SeriesChartType.Column; // You can choose other types like Pie, Line, etc.
            chartStatus.Series.Add(series);

            // Add data points to the series
            foreach (DataRow row in statusCounts.Rows)
            {
                string status = row["status"].ToString();
                int count = Convert.ToInt32(row["count"]);
                series.Points.AddXY(status, count);
            }

            // Add a title to the chart
            Title chartTitle = new Title("Ticket Status Count");
            chartTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            chartStatus.Titles.Add(chartTitle);
            // Adjust the axis scale
            chartStatus.ChartAreas[0].RecalculateAxesScale();
        }
        private void LoadDoughnutChart(string startDate, string endDate)
        {
            // Retrieve user counts from the database
            DataTable userCounts = _repository.GetUserCount(startDate, endDate);

            // Clear existing series and titles
            chartDoughnut.Series.Clear();
            chartDoughnut.Titles.Clear();

            chartDoughnut.BackColor = Color.FromArgb(93, 193, 108);
            Series series = new Series("UserCounts");
            series.ChartType = SeriesChartType.Doughnut;
            chartDoughnut.Series.Add(series);

            // Add data points to the series
            foreach (DataRow row in userCounts.Rows)
            {
                string user = row["user"].ToString();
                int count = Convert.ToInt32(row["count"]);

                // Add data point to the series
                DataPoint dataPoint = series.Points.Add(count);
                dataPoint.Label = count.ToString(); // Set the label to the count value
                dataPoint.LegendText = user; // Set the legend text to the user name
            }

            // Add a title to the chart
            Title chartTitle = new Title("User Counts");
            chartTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            chartDoughnut.Titles.Add(chartTitle);

            // Check if a legend with the name "Legend" exists, and modify it if it does
            Legend existingLegend = chartDoughnut.Legends.FindByName("Legend");
            if (existingLegend != null)
            {
                existingLegend.Enabled = true;
                existingLegend.Docking = Docking.Bottom;
            }
            else
            {
                // If the legend doesn't exist, add it
                Legend newLegend = new Legend("Legend");
                newLegend.Enabled = true;
                newLegend.Docking = Docking.Bottom;
                chartDoughnut.Legends.Add(newLegend);
            }

            // Adjust the axis scale
            chartDoughnut.ChartAreas[0].RecalculateAxesScale();
        }

    }
}
