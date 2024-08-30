using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L2_GLA.Variance
{
    public partial class DatetimeModal : Form
    {
        public static bool backbutton = false;
        public static DateTime datefrom, dateto;
        public DatetimeModal()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            datefrom = dtpFrom.Value;
            dateto = dtpTo.Value;
            this.Close();
        }

        private void DatetimeModal_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            backbutton = true;
            this.Close();
        }
    }
}
