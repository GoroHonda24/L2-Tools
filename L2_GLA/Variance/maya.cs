using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using L2_GLA.Model;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using OfficeOpenXml;
using L2_GLA.Variance;

namespace L2_GLA.Variance
{
    public partial class maya : Form
    {
        private variance variance;
        
        public maya()
        {
            InitializeComponent();
           
            variance = new variance();
           // LoadMayaResult();
        }

    

        private async void btnImportSmartDB_Click(object sender, EventArgs e)
        {
            DatetimeModal ojbform = new DatetimeModal();
            ojbform.ShowDialog();

            if (DatetimeModal.backbutton == true)
            {
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
               // await variance.importSmartDatabase(openFileDialog.FileName, "maya");
                await variance.ImportSmartDatabaseAsync(openFileDialog.FileName, "maya");
                await LoadMayaResult();
            }
        }

        private async void btnQuery_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Pass the DataGridView instance from your form to the method
                    string file_name = Path.GetFileNameWithoutExtension(openFileDialog.SafeFileName);
                    GlobalVar.gfile_name = file_name;
                    int count = await variance.checkMayaFile(file_name);
                    if (count > 0)
                    {
                       MessageBox.Show("File name already exists.");
                    }
                    else
                    {
                        await variance.fileconfig("maya");
                        await variance.MaxId();
                        await variance.formattoquery(openFileDialog.FileName, "maya");
                        await LoadMayaResult();
                    }
                }
            }
        }

        public async Task LoadMayaResult()
        {
            try
            {
                DataTable dataTable = await variance.GetMayaResult();

                dgvTicketDetails.DataSource = dataTable;

                dgvTicketDetails.Columns["app_transaction"].HeaderText = "App Transaction Number";
                dgvTicketDetails.Columns["iload"].HeaderText = "ELP Transaction Number";
                dgvTicketDetails.Columns["dbStatus"].HeaderText = "Smart DB Status";
                dgvTicketDetails.Columns["remarks"].HeaderText = "Remarks";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void maya_Load(object sender, EventArgs e)
        {
            GlobalVar.tableName = "tbl_variance_maya";
            await LoadMayaResult();
            
        }
    }
}