using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace L2_GLA
{
    class DBconnect
    {
        public MySqlConnection connection;

        private string server;
        private string database;
        private string uid;
        private string password;
        private System.Timers.Timer connectionCheckTimer;
        private int failureCount = 0;
        private const int maxRetries = 3;
        public DBconnect()
        {
            Initialize();
            StartConnectionCheckTimer();
        }

        private void Initialize()
        {
            server = "127.0.0.1";
            database = "brand_synch_2";
            uid = "root";
            password = "";

            string connString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            connection = new MySqlConnection(connString);

            TryOpenConnection();
        }

        private void TryOpenConnection()
        {
            try
            {
                using (var tempConnection = new MySqlConnection(connection.ConnectionString))
                {
                    tempConnection.Open();
                    Console.WriteLine("Initial connection opened successfully.");
                    tempConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Initial connection failed: {ex.Message}");
                StartXamppAndRetry();
            }
        }

        private void StartXamppAndRetry()
        {
            if (failureCount >= maxRetries)
            {
                Console.WriteLine("Max retries reached. Stopping further reconnection attempts.");
                return;
            }

            failureCount++;
            try
            {
                Console.WriteLine("Attempting to start XAMPP...");
                Thread xamppThread = new Thread(() =>
                {
                    try
                    {
                        var process = Process.Start("C:\\Applications\\XAMPP_8.2.4\\mysql_start.bat");
                        process.WaitForExit();
                        Console.WriteLine("XAMPP start script executed.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to start XAMPP: {ex.Message}");
                    }
                });

                xamppThread.Start();
                xamppThread.Join();

                Thread.Sleep(5000);
                CheckAndOpenConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to reconnect: {ex.Message}");
            }
        }


        private void StartConnectionCheckTimer()
        {
            connectionCheckTimer = new System.Timers.Timer(60000);
            connectionCheckTimer.Elapsed += OnConnectionCheck;
            connectionCheckTimer.AutoReset = true;
            connectionCheckTimer.Enabled = true;
        }

        private void OnConnectionCheck(object source, ElapsedEventArgs e)
        {
            CheckAndOpenConnection();
        }

        private void CheckAndOpenConnection()
        {
            try
            {
                using (var tempConnection = new MySqlConnection(connection.ConnectionString))
                {
                    if (tempConnection.State != System.Data.ConnectionState.Open)
                    {
                        tempConnection.Open();
                        Console.WriteLine("Connection opened successfully.");
                        tempConnection.Close();
                        failureCount = 0; // Reset the failure count on successful connection
                    }
                    else
                    {
                        Console.WriteLine("Connection is already open.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection check failed: {ex.Message}");
                StartXamppAndRetry();
            }
        }
    }
}



