using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace L2_GLA.connections
{
    internal class BashConnection
    {
        public class BashConn
        {
            private string sshHost = "10.10.144.76";
            private int sshPort = 22;
            private string sshUser = GlobalVar.t_account;
            private string sshKeyFile = $@"{GlobalVar.rsa_id}";

            private string mysqlHost = "gigapay-prod.ch9pgqulupuy.ap-southeast-1.rds.amazonaws.com";
            private int mysqlPort = 3307;
            private string mysqlUser = "readGigapayDXC";
            private string mysqlPassword = "5!Sd,j,yV(BC*pRW";
            private string mysqlDatabase = "gigapay";

            private SshClient sshClient;
            private ForwardedPortLocal forwardedPort;
            public MySqlConnection connection { get; private set; }

            public BashConn()
            {
                sshClient = null;
                forwardedPort = null;
                connection = null;
                ConnectToMySqlOverSsh();
            }

            private void ConnectToMySqlOverSsh()
            {
                try
                {
                    // Setup SSH connection using a private key
                    var privateKeyFile = new PrivateKeyFile(sshKeyFile);
                    var keyFiles = new[] { privateKeyFile };
                    var authMethod = new PrivateKeyAuthenticationMethod(sshUser, keyFiles);
                    var connectionInfo = new ConnectionInfo(sshHost, sshUser, authMethod);

                    sshClient = new SshClient(connectionInfo);
                    sshClient.Connect();

                    if (sshClient.IsConnected)
                    {
                        // Forward the local port to the MySQL server over SSH
                        forwardedPort = new ForwardedPortLocal("127.0.0.1", (uint)mysqlPort, mysqlHost, (uint)mysqlPort);
                        sshClient.AddForwardedPort(forwardedPort);
                        forwardedPort.Start();

                        // Connect to MySQL through the forwarded port
                        string connectionString = $"Server=127.0.0.1;Port={mysqlPort};Database={mysqlDatabase};Uid={mysqlUser};Pwd={mysqlPassword};";
                        connection = new MySqlConnection(connectionString);
                        connection.Open();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while connecting: {ex.Message}");
                }
            }

            public void CloseConnections()
            {
                // Stop port forwarding if started
                if (forwardedPort != null && forwardedPort.IsStarted)
                {
                    forwardedPort.Stop();
                }

                // Disconnect SSH client
                if (sshClient != null && sshClient.IsConnected)
                {
                    sshClient.Disconnect();
                }

                // Close MySQL connection
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
