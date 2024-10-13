using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Transactions;
using Org.BouncyCastle.Bcpg;
using System.Security.Cryptography;
using static L2_GLA.OpenAPIs.PostmanModels;

namespace L2_GLA
{
    class GlobalVar
    {
        public static string user, role, checking, team, gfile_name, gfile_id,varName,vartype,access_code, formatedQuery, filepath, tableName, t_account, rsa_id;
        public static bool admin = false;
        public static bool login = true;
        public static int maxID = 0, mayarowZ;
        
       // public PostmanEnvironment environment;
        public static string rawHookData;
        public static string rawHookToken;
        public static DateTime dataExpirationTime;
        public static DateTime tokenExpirationTime;
        public const int dataValidityMinutes = 1440;  // Set appropriate expiration time
        public const int tokenValidityMinutes = 20; // Set appropriate expiration time

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }

    

    public class merchant
    {
        public string SETTLEMENT_TXN_ID { get; set; }
        public string MERCHANT_ID { get; set; }
        public string MERCHANT_NAME { get; set; }
        public string SETTLE_DATE { get; set; }
        public string MERCHANT_TRANS_ID { get; set; }
        public string ACQUIREMENT_ID { get; set; }
        public string TRANSACTION_TYPE { get; set; }
        public string TRANSACTION_DATETIME { get; set; }
        public string MERCHANT_REFUND_REQUEST_ID { get; set; }
        public string REFUND_ID { get; set; }
        public string TRANSACTION_AMOUNT { get; set; }
        public string NET_MDR { get; set; }
        public string SETTLE_AMOUNT { get; set; }
    } 
    
    public class smart
    {
        public string settlement_txn_id { get; set; }
        public string merchant_id { get; set; }
        public string merchant_name { get; set; }
        public string settle_date { get; set; }
        public string merchant_trans_id { get; set; }
        public string acquirement_id { get; set; }
        public string transaction_type { get; set; }
        public string transaction_datetime { get; set; }
        public string merchant_request_id { get; set; }
        public string refund_id { get; set; }
        public string transaction_amount { get; set; }
        public string net_mdr { get; set; }
        public string settle_amount { get; set; }
    }

    public class splunk
    {
        public string _time { get; set; }
        public string id { get; set; }
        public string processor_ref_no { get; set; }
        public string app_transaction_number { get; set; }
        public string state { get; set; }
    }
    public class maya
    {
        public string Transaction { get; set; }
        public string MERCHANT_TRANS_ID { get; set;}
        public string ELP_Reference_Number { get; set; }
        //public string STATUS_A { get; set;}
        //public string Amount_A { get; set; }
        //public string STATUS_B { get; set; }
        //public string Amount_B { get; set; }
        //public string STATUS_C { get; set; }
        //public string Amount_C { get; set; }
        //public string L2_Investigation_Remarks { get; set;}

                       

    }
    public class mydb
    {
        public string status { get; set; }
        public string app_transaction_number { get; set; }
        public string elp_transaction_number { get; set; }
        public string created_at { get; set; }
        //public string STATUS_A { get; set;}
        //public string Amount_A { get; set; }
        //public string STATUS_B { get; set; }
        //public string Amount_B { get; set; }
        //public string STATUS_C { get; set; }
        //public string Amount_C { get; set; }
        //public string L2_Investigation_Remarks { get; set;}



    }

    public class MIN
    {
        public string number { get; set; }
        public string app_transaction_number  { get; set; }
        public string created_at { get; set; }
    }

    public class account
    {
        public string MIN { get; set; }
        public string EXTERNAL_ID { get; set; }
        public string INACTIVE_DATE  { get; set; }
        public string EXTERNAL_ID_TYPE { get; set; }
    }

}


