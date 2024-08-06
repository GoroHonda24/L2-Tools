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
    public partial class queryFormat : Form
    {
        public queryFormat()
        {
            InitializeComponent();
        }

        private void queryFormat_Load(object sender, EventArgs e)
        {
            txtQuery.Text = GlobalVar.formatedQuery;
        }
    }
}
