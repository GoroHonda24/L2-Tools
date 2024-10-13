using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace L2_GLA
{
    public partial class glDB : Form
    {
        private SshClient sshClient;
        private ForwardedPortLocal forwardedPort;
        private MongoClient mongoClient;

        // SSH and MongoDB connection details
        private readonly string sshHost = "10.10.144.76";
        private readonly int sshPort = 22;
        private readonly string sshUsername = "t-rcsunga";
        private readonly string sshPrivateKeyFile = @"\\10.170.249.48\c$\Users\t-rcsunga\.ssh\id_rsa"; // Replace with your private key file path
        private readonly string sshPassphrase = ""; // If your key has a passphrase

        private readonly string mongoHost = "deep-brand-service-prod.cluster-ch9pgqulupuy.ap-southeast-1.docdb.amazonaws.com";
        private readonly int mongoPort = 27017;
        private readonly string mongoUsername = "root";
        private readonly string mongoPassword = "54h6gGRHXG8MBE39"; // Replace with the actual MongoDB password
        private readonly string mongoDatabase = "54h6gGRHXG8MBE39"; // Replace with your database name
        public glDB()
        {
            InitializeComponent();
        }

        private void glDB_Load(object sender, EventArgs e)
        {
           
        }
        public static void ConnectToMySqlOverSsh()
        {
            // SSH server details
            string sshHost = "10.10.144.76"; // SSH Hostname
            string sshKeyFile = @"\\10.170.249.48\c$\Users\t-rcsunga\.ssh\id_rsa"; // SSH Key File Path

            // MySQL server details
            string mysqlHost = "gigapay-prod.ch9pgqulupuy.ap-southeast-1.rds.amazonaws.com"; // MySQL Hostname
            int mysqlPort = 3307; // MySQL Server Port
            string mysqlUser = "readGigapayDXC"; // MySQL Username
            string mysqlPassword = "5!Sd,j,yV(BC*pRW"; // MySQL Password
            string mysqlDatabase = "gigapay"; // Optional: Default schema/database

            // Create SSH client and MySQL connection objects
            SshClient sshClient = null;
            ForwardedPortLocal forwardedPort = null;
            MySqlConnection mysqlConnection = null;

            try
            {
                // Step 1: Setup SSH connection using a private key
                var privateKeyFile = new PrivateKeyFile(sshKeyFile);
                var keyFiles = new[] { privateKeyFile };
                var authMethod = new PrivateKeyAuthenticationMethod("t-rcsunga", keyFiles);
                var connectionInfo = new ConnectionInfo(sshHost, "t-rcsunga", authMethod);

                sshClient = new SshClient(connectionInfo);
                sshClient.Connect();
                MessageBox.Show("SSH connection established successfully.");

                if (sshClient.IsConnected)
                {
                    // Step 2: Forward the local port (127.0.0.1:3306) to the MySQL server over SSH
                    forwardedPort = new ForwardedPortLocal("127.0.0.1", (uint)mysqlPort, mysqlHost, (uint)mysqlPort);
                    sshClient.AddForwardedPort(forwardedPort);
                    forwardedPort.Start();
                    MessageBox.Show("Port forwarding established.");

                    // Step 3: Connect to MySQL through the forwarded port
                    string connectionString = $"Server=127.0.0.1;Port=17561;Database={mysqlDatabase};Uid={mysqlUser};Pwd={mysqlPassword};";
                    mysqlConnection = new MySqlConnection(connectionString);
                    mysqlConnection.Open();
                    MessageBox.Show("Connected to MySQL successfully.");

                    // Database operations go here...
                }
            }
            catch (SshException sshEx)
            {
                // Handle SSH connection issues
                MessageBox.Show($"SSH connection error: {sshEx.Message}");
            }
            catch (MySqlException mysqlEx)
            {
                // Handle MySQL connection issues
                MessageBox.Show($"MySQL connection error: {mysqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other generic errors
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Cleanup resources

                // Stop port forwarding if it was started
                if (forwardedPort != null && forwardedPort.IsStarted)
                {
                    forwardedPort.Stop();
                    MessageBox.Show("Port forwarding stopped.");
                }

                // Disconnect SSH client
                if (sshClient != null && sshClient.IsConnected)
                {
                    sshClient.Disconnect();
                    MessageBox.Show("SSH connection closed.");
                }

                // Close MySQL connection
                if (mysqlConnection != null && mysqlConnection.State == System.Data.ConnectionState.Open)
                {
                    mysqlConnection.Close();
                    MessageBox.Show("MySQL connection closed.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectToMySqlOverSsh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectToMongoDBOverSsh();
        }
        private void ConnectToMongoDBOverSsh()
        {
            try
            {
                // Step 1: Set up SSH tunnel using an identity file
                var keyFile = new PrivateKeyFile(sshPrivateKeyFile, sshPassphrase);
                var keyFiles = new[] { keyFile };
                var authMethod = new PrivateKeyAuthenticationMethod(sshUsername, keyFiles);
                var connectionInfo = new ConnectionInfo(sshHost, sshPort, sshUsername, authMethod);

                sshClient = new SshClient(connectionInfo);
                sshClient.Connect();
                MessageBox.Show("SSH connection established successfully.");

                // Step 2: Forward the local port to MongoDB server over SSH
                forwardedPort = new ForwardedPortLocal("127.0.0.1", (uint)mongoPort, mongoHost, (uint)mongoPort);
                sshClient.AddForwardedPort(forwardedPort);
                forwardedPort.Start();
                MessageBox.Show("Port forwarding established.");

                // Step 3: MongoDB connection string
                string mongoConnectionString = $"mongodb://{mongoUsername}:{mongoPassword}@127.0.0.1:{mongoPort}/?authSource=admin&retryWrites=false";

                // Step 4: Connect to MongoDB using the .NET Driver
                mongoClient = new MongoClient(mongoConnectionString);
                MessageBox.Show("Connected to MongoDB successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while connecting: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public IMongoDatabase GetDatabase()
        {
            if (mongoClient == null)
            {
                throw new InvalidOperationException("MongoDB client is not connected.");
            }

            return mongoClient.GetDatabase(mongoDatabase);
        }

        private void buttonCloseConnections_Click(object sender, EventArgs e)
        {
            CloseConnections();
        }

        private void CloseConnections()
        {
            // Stop port forwarding if started
            if (forwardedPort != null && forwardedPort.IsStarted)
            {
                forwardedPort.Stop();
                MessageBox.Show("Port forwarding stopped.");
            }

            // Disconnect SSH client
            if (sshClient != null && sshClient.IsConnected)
            {
                sshClient.Disconnect();
                MessageBox.Show("SSH connection closed.");
            }
        }
    }
    
}
