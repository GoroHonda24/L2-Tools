using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;

namespace L2_GLA.connections
{
    public class MongoDBConnector
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

        public MongoClient MongoClient => mongoClient;

        public MongoDBConnector()
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

        public void CloseConnections()
        {
            // Stop port forwarding if started
            if (forwardedPort != null && forwardedPort.IsStarted)
            {
                forwardedPort.Stop();
              //  MessageBox.Show("Port forwarding stopped.");
            }

            // Disconnect SSH client
            if (sshClient != null && sshClient.IsConnected)
            {
                sshClient.Disconnect();
              //  MessageBox.Show("SSH connection closed.");
            }
        }
    }
}
