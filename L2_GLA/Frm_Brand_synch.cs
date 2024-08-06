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
    public partial class Frm_Brand_synch : Form
    {
      
        DBconnect conn = new DBconnect();
        public Frm_Brand_synch()
        {
            if (conn.connection.State != ConnectionState.Open) conn.connection.Open();
            InitializeComponent();
        }



        private void brand_series_Click(object sender, EventArgs e)
        {

        }
    }
}
