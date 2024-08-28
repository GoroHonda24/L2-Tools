using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using L2_GLA.Variance;
using L2_GLA.OpenAPIs;

namespace L2_GLA
{
    public partial class Frm_main : Form
    {
        public Frm_main()
        {
            InitializeComponent();
            panelControl();
        }
        public void CloseActiveForm()
        {
            List<Form> openForm = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForm.Add(f);

            foreach (Form f in openForm)
                if (f.Name != "Frm_main")
                {
                    f.Close();

                }
        }

        public void panelControl()
        {
            panelBrandsync.Visible = false;
            panelPayment.Visible = false;
            panelVariance.Visible = false;
        }

        public void hideSubmenu()
        {
            if (panelBrandsync.Visible == true)
                panelBrandsync.Visible = false;

            if (panelPayment.Visible == true)
                panelPayment.Visible = false;
            if (panelVariance.Visible == true)
                panelVariance.Visible = false;
        }
        public void showSubmenu(Panel subMenu)
        {
            if(subMenu.Visible == false)
            {
                hideSubmenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }


        private string[] accesscode;
        public void load_access()
        {
            lbluser.Text = "Hi " + GlobalVar.user + " !";
            accesscode = GlobalVar.access_code.Split(',');
            
            if (accesscode.Contains("1"))
            {
                btnDashboard.Visible = true;
            }
            if (accesscode.Contains("2"))
            {
                btnAccount.Visible = true;
            }
            if (accesscode.Contains("3"))
            {
                btnBrandsync.Visible = true;
            }
            if (accesscode.Contains("4"))
            {
                btnDelete.Visible = true;
            }
            if (accesscode.Contains("5"))
            {
                btnPayment.Visible = true;
            }
            if (accesscode.Contains("6"))
            {
                btnVariance.Visible = true;
            }
            if (accesscode.Contains("7"))
            {
                btnMNP.Visible = true;
            }
            if (accesscode.Contains("8"))
            {
                btnINC.Visible = true;
            }
            if (accesscode.Contains("9"))
            {
                btnL2.Visible = true;
            }
            if (accesscode.Contains("10"))
            {
                btnCMS.Visible = true;
            }
            if (accesscode.Contains("11"))
            {
                btnLogout.Visible = true;
            }
            if (accesscode.Contains("12"))
            {
                btnIload.Visible = true;
            }

        }
        
        private void Frm_main_Load(object sender, EventArgs e)
        {
            btnDashboard.Visible = false;
            btnAccount.Visible = false;
            btnBrandsync.Visible = false;
            btnDelete.Visible = false;
            btnPayment.Visible = false;
            btnVariance.Visible = false;
            btnMNP.Visible = false;
            btnINC.Visible = false;
            btnL2.Visible = false;
            btnCMS.Visible = false;
            btnLogout.Visible = false;

            if (GlobalVar.checking !="true")
            {
                    Frm_Log_in frmobj = new Frm_Log_in();
                    frmobj.ShowDialog();
                    

            }
        }


        private void Frm_main_Activated(object sender, EventArgs e)
        {
            if (GlobalVar.checking == "true")
            { 
                load_access();
            }
        }

        private void Frm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //String process = Process.GetCurrentProcess().ProcessName;
                //Process.Start("cmd.exe", "/c taskkill /F /IM " + process + ".exe /T");

               Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void Pmain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            Frm_Inc_logs frmobj = new Frm_Inc_logs();
            frmobj.Show();
        }

        private void btnBrandsync_Click(object sender, EventArgs e)
        {
            showSubmenu(panelBrandsync);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "INSERT MIN")
            {
                CloseActiveForm();
                lbltitle.Text = "INSERT MIN";
                Frm_insert frmobj = new Frm_insert();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();
            }
            hideSubmenu();
        }

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            if (GlobalVar.team == "L2 Smart App")
            {
                if (lbltitle.Text != "DASHBOARD")
                {
                    CloseActiveForm();
                    lbltitle.Text = "DASHBOARD";
                    Frm_Dashboard_smart frmobj = new Frm_Dashboard_smart();
                    frmobj.TopLevel = false;
                    frmobj.FormBorderStyle = FormBorderStyle.None;
                    frmobj.Dock = DockStyle.Fill;
                    Pmain.AutoScroll = true;
                    Pmain.Controls.Add(frmobj);
                    frmobj.Show();

                }
            }
            else
            {
                if (lbltitle.Text != "DASHBOARD")
                {
                    CloseActiveForm();
                    lbltitle.Text = "DASHBOARD";
                    Frm_Dashboard frmobj = new Frm_Dashboard();
                    frmobj.TopLevel = false;
                    frmobj.FormBorderStyle = FormBorderStyle.None;
                    frmobj.Dock = DockStyle.Fill;
                    Pmain.AutoScroll = true;
                    Pmain.Controls.Add(frmobj);
                    frmobj.Show();

                }
            }

            
        }

        private void btnupdatebrand_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "UPDATE BRAND")
            {
                CloseActiveForm();
                lbltitle.Text = "UPDATE BRAND";
                Frm_Update_Brand frmobj = new Frm_Update_Brand();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();
            }
            hideSubmenu();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "ACCOUNT O")
            {
                CloseActiveForm();
                lbltitle.Text = "ACCOUNT O";
                frm_account frmobj = new frm_account();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.AutoScroll = true;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();
            }
            hideSubmenu();
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "UPDATE STATUS")
            {
                CloseActiveForm();
                lbltitle.Text = "UPDATE STATUS";
                Frm_update_status frmobj = new Frm_update_status();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();
            }
            hideSubmenu();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "DELETE MIN")
            {
                CloseActiveForm();
                lbltitle.Text = "DELETE MIN";
                Frm_Delete frmobj = new Frm_Delete();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();
            }
            hideSubmenu();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            showSubmenu(panelPayment);
        }

        private void btnPaymentGcash_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "MERCHANT SETTLEMENT")
            {
                CloseActiveForm();
                lbltitle.Text = "MERCHANT SETTLEMENT";
                Frm_variance frmobj = new Frm_variance();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();
            }
            hideSubmenu();
        }

        private void btnPaymentMaya_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "SPLUNK FILE")
            {
                CloseActiveForm();
                lbltitle.Text = "SPLUNK FILE";
                Frm_Splunk frmobj = new Frm_Splunk();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();
            }
            hideSubmenu();
        }

        private void btnVariance_Click(object sender, EventArgs e)
        {
            showSubmenu(panelVariance);
        }

        private void btnVarianceGcash_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "GCASH VARIANCE")
            {
                CloseActiveForm();
                lbltitle.Text = "GCASH VARIANCE";
                L2_GLA.Variance.gcash frmobj = new L2_GLA.Variance.gcash();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();
            }
            hideSubmenu();
        }

        private void btnVarianceMaya_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "MAYA VARIANCE")
            {
                CloseActiveForm();
                lbltitle.Text = "MAYA VARIANCE";
                L2_GLA.Variance.maya frmobj = new L2_GLA.Variance.maya();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();


            }
            hideSubmenu();
        }

        private void btnVarianceReport_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "VARIANCE REPORT")
            {
                CloseActiveForm();
                lbltitle.Text = "VARIANCE REPORT";
                Frm_Var_Report frmobj = new Frm_Var_Report();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();

            }
            hideSubmenu();
        }

        private void btnMNP_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "MNP CHECKING")
            {
                CloseActiveForm();
                lbltitle.Text = "MNP CHECKING";
                Frm_Optima frmobj = new Frm_Optima();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();
            }
            hideSubmenu();
        }

        private void btnL2_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "l2 TRIAGE")
            {
                CloseActiveForm();
                lbltitle.Text = "L2 TRIAGE";
                Frm_L2_Triage frmobj = new Frm_L2_Triage();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();

            }
            hideSubmenu();
        }

        private void btnCMS_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "CMS")
            {
                CloseActiveForm();
                lbltitle.Text = "CMS";
                Frm_CMS frmobj = new Frm_CMS();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();

            }
            hideSubmenu();
        }
        private void btnINC_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "SMART APP INC LOGS")
            {
                CloseActiveForm();
                lbltitle.Text = "SMART APP INC LOGS";
                Frm_INC frmobj = new Frm_INC();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();

            }
            hideSubmenu();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Log out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                CloseActiveForm();
                lbltitle.Text = "";
                lbluser.Text = "HI !";
                GlobalVar.checking = "false";
                btnDashboard.Visible = false;
                btnAccount.Visible = false;
                btnBrandsync.Visible = false;
                btnDelete.Visible = false;
                btnPayment.Visible = false;
                btnVariance.Visible = false;
                btnMNP.Visible = false;
                btnINC.Visible = false;
                btnL2.Visible = false;
                btnCMS.Visible = false;
                btnLogout.Visible = false;
                Frm_Log_in frmobj = new Frm_Log_in();
                frmobj.ShowDialog();
            }
        }

        private void btnIload_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "iLoad")
            {
                CloseActiveForm();
                lbltitle.Text = "iLoad";
                Form1 frmobj = new Form1();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();

            }
            hideSubmenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text != "Postman")
            {
                CloseActiveForm();
                lbltitle.Text = "Postman";
                GL frmobj = new GL();
                frmobj.TopLevel = false;
                frmobj.FormBorderStyle = FormBorderStyle.None;
                frmobj.Dock = DockStyle.Fill;
                Pmain.Controls.Add(frmobj);
                frmobj.Show();
            }
            hideSubmenu();
        }
    }
}
