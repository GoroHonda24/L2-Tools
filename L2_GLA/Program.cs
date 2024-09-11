using L2_GLA.OpenAPIs;
using L2_GLA.Variance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace L2_GLA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Frm_Gcash());

           //Application.Run(new L2_GLA.Variance.maya());
           Application.Run(new GL());
        }
    }
}
