using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace L2_GLA.OpenAPIs
{
    public partial class API : Form
    {
        public API()
        {
            InitializeComponent();
        }

        private void API_Load(object sender, EventArgs e)
        {

        }
        // Step 1: Search MINs (GET request)
        private async Task<string> SearchMinsAsync(string baseUrl, string mobileNumber, string token, string appToken)
        {
            using (HttpClient client = new HttpClient())
            {
                // Add Authorization and App Token headers
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("X-Application-Token", appToken);

                // Construct the full URL with the mobile number as a query parameter
                string url = $"{baseUrl}/api/v2/get-brand?number={mobileNumber}";

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }

        // Step 2: Get Data (GET request)
        private async Task<string> GetDataAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://smart-consumer-ws-eb.multidemos.com/api/v2/hook/generate-data";
                    Console.WriteLine($"Request URL: {url}"); // Log the URL
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    

                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException httpEx)
                {
                    MessageBox.Show($"Request error: {httpEx.Message}");
                    return null;
                }
            }
        }


        // Step 3: Get Token using the data retrieved from GetData (POST request)
        private async Task<string> GetTokenAsync(string hookUrl, string rawHookData, string consumerKey)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"{hookUrl}/auth.svc/json/GetToken";
                    var body = new
                    {
                        ConsumerKey = consumerKey,
                        Data = rawHookData  // Ensure this is in the correct format expected by the API
                    };

                    string jsonBody = JsonConvert.SerializeObject(body);
                    Console.WriteLine($"Request body: {jsonBody}");  // Log the request body

                    var request = new HttpRequestMessage(HttpMethod.Post, url)
                    {
                        Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                    };

                    HttpResponseMessage response = await client.SendAsync(request);
                    var responseContent = await response.Content.ReadAsStringAsync();  // Capture the full response

                    // Check if response is not successful
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Error: {response.StatusCode}, Content: {responseContent}");
                        return null;
                    }

                    return responseContent;  // Token should be returned here
                }
                catch (HttpRequestException httpEx)
                {
                    MessageBox.Show($"Request error: {httpEx.Message}");
                    return null;
                }
            }
        }


        // Step 4: Get Brand Info using the token and mobile number (POST request)
        private async Task<string> GetBrandInfoAsync(string hookUrl, string mobileNumber, string rawHookToken)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"{hookUrl}/external.svc/json/GetBrandInfo";
                var body = new
                {
                    AccountNumber = mobileNumber,
                    Token = rawHookToken
                };
                string jsonBody = JsonConvert.SerializeObject(body);

                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                Console.WriteLine($"Request URL: {url}");

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            string hookUrl = "https://api01.smart.com.ph/HookAPI_Multisys"; // Hook API base URL
            string consumerKey = "multisys.prod"; // Replace with your actual consumer key
            string mobileNumber = textBox1.Text.Trim(); // Get the mobile number from a text box

            // Base URL for the GL Checker API (search MINs)
            string baseUrl = "https://app1.smart.com.ph";
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiIsImp0aSI6IjIwOTUyYzYwLWZmMmUtNDJjNC1iZWExLWJiMWJkZjA0MzQyNSJ9.eyJqdGkiOiIyMDk1MmM2MC1mZjJlLTQyYzQtYmVhMS1iYjFiZGYwNDM0MjUiLCJpYXQiOjE2MjQwMjE1NzEsIm5iZiI6MTYyNDAyMTU3MSwiZXhwIjoxNjI0MTA3OTcxfQ.CyxD8pO9PggVpOgXTwR9Eio6dWvWZYkZHyHoPfpI9OE"; // Replace with your actual token
            string appToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJhcHBfaWQiOiJpb3MiLCJpYXQiOjE1OTg1ODY1MjYsIm5iZiI6MTU5ODU4NjUyNiwiZXhwIjoxOTEzOTQ2NTI2fQ.xVM2s_Owt4zNWLOlllhPXcRQ4b23x6KQpqs_2NGu9zPlQ9hjOsSS6pr9Qams7jfsyMPXtik2MFvv8V_nT8oG5Q"; // Replace with your actual appToken

            // Validate and format mobile number
            if (!mobileNumber.StartsWith("63"))
            {
                if (mobileNumber.StartsWith("0"))
                {
                    mobileNumber = "63" + mobileNumber.Substring(1);
                }
                else
                {
                    MessageBox.Show("Phone number is invalid.");
                    return;
                }
            }

            try
            {

                // Step 1: Search MINs and get the brand information
                string minsResult = await SearchMinsAsync(baseUrl, mobileNumber, token, appToken);
                textBoxAccountDetails.Text = minsResult;

                // Step 1: Retrieve the data
                string rawHookData = await GetDataAsync();
                MessageBox.Show($"Data retrieved: {rawHookData}");

                // Check if rawHookData is null or empty before proceeding
                if (string.IsNullOrEmpty(rawHookData))
                {
                    MessageBox.Show("Failed to retrieve data. Cannot proceed.");
                    return; // Stop further execution if rawHookData is invalid
                }

                // Step 2: Get the Token using the retrieved data
                string rawHookToken = await GetTokenAsync(hookUrl, rawHookData, consumerKey);
                MessageBox.Show($"Token retrieved: {rawHookToken}");

                // Check if rawHookToken is null or empty before proceeding
                if (string.IsNullOrEmpty(rawHookToken))
                {
                    MessageBox.Show("Failed to retrieve token. Cannot proceed.");
                    return; // Stop further execution if rawHookToken is invalid
                }

                // Step 3: Get the Brand Info using the token and mobile number
                string brandInfo = await GetBrandInfoAsync(hookUrl, mobileNumber, rawHookToken);

                // Display the brand info in a TextBox or similar UI element
                textBoxBrandInfo.Text = brandInfo;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            

        }

    }
}
