using L2_GLA.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L2_GLA.Variance
{
    public partial class gcash : Form
    {
        private variance variance;
        public gcash()
        {
            InitializeComponent();
            variance = new variance();
            
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
                        await variance.fileconfig("gcash");
                        await variance.MaxId();
                        await variance.formattoquery(openFileDialog.FileName, "gcash");
                        await LoadGcashResult();
                    }
                }
            }
        }
        public async Task LoadGcashResult()
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
        private async void gcash_Load(object sender, EventArgs e)
        {
            GlobalVar.tableName = "tbl_variance_gcash";
            await LoadGcashResult();
        }

        private async void btnImportSmartDB_Click(object sender, EventArgs e)
        {
            DatetimeModal frm = new DatetimeModal();
            frm.ShowDialog();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // await variance.importSmartDatabase(openFileDialog.FileName, "gcash");
                await variance.IloadAccount();
                await variance.ImportSmartDatabaseAsync(openFileDialog.FileName, "gcash");
                
                await LoadGcashResult();
            }
        }
    }
}
