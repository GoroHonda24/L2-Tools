﻿using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;
using static L2_GLA.OpenAPIs.PostmanModels;
using System.Net.Http;
using System.Net.Http.Headers;

namespace L2_GLA.OpenAPIs
{
    public partial class GL : Form
    {
        private PostmanEnvironment environment;
        

        private HttpClient client;
        public GL()
        {
            InitializeComponent();
        }
        private PostmanEnvironment LoadEnvironment(string filePath)
        {
            string environmentJson = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<PostmanEnvironment>(environmentJson);
        }
        // Method to retrieve a token from the environment
        private string GetEnvironmentToken(string key, string environmentFilePath)
        {
            var environment = LoadEnvironment(environmentFilePath);

            // Find the token using the key
            var tokenVariable = environment.values.Find(v => v.key == key);

            return tokenVariable?.value ?? string.Empty; // Return the token value or an empty string if not found
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            // Set up the API base URL and endpoint
            string baseUrl = "https://app1.smart.com.ph";
            string endpoint = "/api/v2/get-brand";
            string phoneNumber = textBox1.Text.Trim(); // This should be dynamic or user input

            // Set up tokens from your environment file
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiIsImp0aSI6IjIwOTUyYzYwLWZmMmUtNDJjNC1iZWExLWJiMWJkZjA0MzQyNSJ9.eyJqdGkiOiIyMDk1MmM2MC1mZjJlLTQyYzQtYmVhMS1iYjFiZGYwNDM0MjUiLCJpYXQiOjE2MjQwMjE1NzEsIm5iZiI6MTYyNDAyMTU3MSwiZXhwIjoxNjI0MTA3OTcxfQ.CyxD8pO9PggVpOgXTwR9Eio6dWvWZYkZHyHoPfpI9OE"; // Replace with actual token
            string appToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJhcHBfaWQiOiJpb3MiLCJpYXQiOjE1OTg1ODY1MjYsIm5iZiI6MTU5ODU4NjUyNiwiZXhwIjoxOTEzOTQ2NTI2fQ.xVM2s_Owt4zNWLOlllhPXcRQ4b23x6KQpqs_2NGu9zPlQ9hjOsSS6pr9Qams7jfsyMPXtik2MFvv8V_nT8oG5Q"; // Replace with actual app token

            // Create a RestClient
            var client = new RestClient(baseUrl);

            // Create a RestRequest with the endpoint
            var request = new RestRequest(endpoint, Method.Get);
            request.AddParameter("number", phoneNumber);

            // Add headers for authentication
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("X-Application-Token", appToken);

            try
            {
                // Execute the request
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    // Deserialize the response if needed
                    var responseData = JsonConvert.DeserializeObject(response.Content);
                    txtgl.Text =  "API call successful. Response data: " + responseData;
                }
                else
                {
                    MessageBox.Show("API call failed. Status: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            try
            {
                // Check if data is expired or null, and run GetData if needed
                if (string.IsNullOrEmpty(GlobalVar.rawHookData) || DateTime.Now > GlobalVar.dataExpirationTime)
                {
                    await RunGetDataAsync();
                }

                // Check if token is expired or null, and run GetToken if needed
                if (string.IsNullOrEmpty(GlobalVar.rawHookToken) || DateTime.Now > GlobalVar.tokenExpirationTime)
                {
                    await RunGetTokenAsync();
                }

                // Proceed to run GetBrandInfo if token is available and valid
                if (!string.IsNullOrEmpty(GlobalVar.rawHookToken))
                {
                    await RunGetBrandInfoAsync();
                }
                Console.WriteLine(GlobalVar.rawHookData.ToString());
                Console.WriteLine(GlobalVar.rawHookToken.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            #region
            //// Step 1: Run GetData
            //string baseUrl2 = "https://smart-consumer-ws-eb.multidemos.com";
            //string getDataEndpoint = "/api/v2/hook/generate-data";

            //var client2 = new RestClient(baseUrl2);
            //var getDataRequest = new RestRequest(getDataEndpoint, Method.Get);

            //try
            //{
            //    // Execute GetData
            //    var getDataResponse = await client2.ExecuteAsync(getDataRequest);

            //    if (!getDataResponse.IsSuccessful)
            //    {
            //        MessageBox.Show("GetData call failed: " + getDataResponse.StatusCode);
            //        return;
            //    }

            //    // Save response to a variable
            //    string rawHookData = getDataResponse.Content.Trim('\"'); // Trim quotes if present

            //    // Step 2: Run GetToken
            //    string hookUrl = "https://api01.smart.com.ph/HookAPI_Multisys"; // Ensure this is the correct base URL for your API
            //    string getTokenEndpoint = "/auth.svc/json/GetToken";
            //    var getTokenRequest = new RestRequest(hookUrl + getTokenEndpoint, Method.Post);
            //    getTokenRequest.AddHeader("Content-Type", "application/json");

            //    // Construct the JSON body manually to avoid serialization issues
            //    string jsonBody = $"{{\"ConsumerKey\":\"multisys.prod\",\"Data\":\"{rawHookData}\"}}";
            //    //  MessageBox.Show("GetToken Request Body: " + jsonBody); // Log for debugging
            //    getTokenRequest.AddJsonBody(jsonBody);

            //    var getTokenResponse = await client2.ExecuteAsync(getTokenRequest);

            //    // Log the raw response
            //    // MessageBox.Show("GetToken Raw Response: " + getTokenResponse.Content);

            //    if (!getTokenResponse.IsSuccessful)
            //    {
            //        MessageBox.Show("GetToken call failed: " + getTokenResponse.StatusCode);
            //        return;
            //    }

            //    // Parse token from response
            //    dynamic tokenResponse = JsonConvert.DeserializeObject(getTokenResponse.Content);
            //    string rawHookToken = tokenResponse?.Token?.ToString(); // Safely accessing Token

            //    // Check if the token is empty or null
            //    if (string.IsNullOrEmpty(rawHookToken))
            //    {
            //        MessageBox.Show("Token extraction failed.");
            //        return;
            //    }

            //    // Log the extracted token
            //    // MessageBox.Show("Extracted Token: " + rawHookToken);

            //    // Step 3: Run GetBrandInfo
            //    string getBrandInfoEndpoint = "/external.svc/json/GetBrandInfo";
            //    var getBrandInfoRequest = new RestRequest(hookUrl + getBrandInfoEndpoint, Method.Post);
            //    getBrandInfoRequest.AddHeader("Content-Type", "application/json");

            //    // Validate AccountNumber
            //    string accountNumber = textBox1.Text.Trim(); // Assume you get this from a TextBox for user input
            //    if (string.IsNullOrWhiteSpace(accountNumber))
            //    {
            //        MessageBox.Show("Account number is required.");
            //        return;
            //    }

            //    // Add body with AccountNumber and Token
            //    var getBrandInfoRequestBody = new
            //    {
            //        AccountNumber = accountNumber, // Ensure this is correctly set
            //        Token = rawHookToken // Use the token obtained from GetToken call
            //    };

            //    // Serialize the request body to JSON and log
            //    string brandInfoJsonBody = JsonConvert.SerializeObject(getBrandInfoRequestBody);
            //    //MessageBox.Show("GetBrandInfo Request Body: " + brandInfoJsonBody); // Log for debugging
            //    getBrandInfoRequest.AddJsonBody(brandInfoJsonBody);

            //    var getBrandInfoResponse = await client2.ExecuteAsync(getBrandInfoRequest);

            //    if (getBrandInfoResponse.IsSuccessful)
            //    {
            //        // Display GetBrandInfo response
            //        string formattedJson = JsonConvert.SerializeObject(
            //            JsonConvert.DeserializeObject(getBrandInfoResponse.Content),
            //            Formatting.Indented);

            //        txthook.Text = formattedJson; // Display formatted JSON in TextBox
            //    }
            //    else
            //    {
            //        MessageBox.Show("GetBrandInfo call failed: " + getBrandInfoResponse.StatusCode);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("An error occurred: " + ex.Message);
            //}
            #endregion
        }

        private void GL_Load(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {

            
        }

        // Method to run GetData
        private async Task RunGetDataAsync()
        {
            string baseUrl = "https://smart-consumer-ws-eb.multidemos.com";
            string getDataEndpoint = "/api/v2/hook/generate-data";
            var client = new RestClient(baseUrl);
            var getDataRequest = new RestRequest(getDataEndpoint, Method.Get);

            var getDataResponse = await client.ExecuteAsync(getDataRequest);

            if (!getDataResponse.IsSuccessful)
            {
                MessageBox.Show("GetData call failed: " + getDataResponse.StatusCode);
                return;
            }

            GlobalVar.rawHookData = getDataResponse.Content.Trim('\"'); // Save and trim quotes if present
            GlobalVar.dataExpirationTime = DateTime.Now.AddMinutes(GlobalVar.dataValidityMinutes); // Set data expiration time
        }

        // Method to run GetToken
        private async Task RunGetTokenAsync()
        {
            string hookUrl = "https://api01.smart.com.ph/HookAPI_Multisys";
            string getTokenEndpoint = "/auth.svc/json/GetToken";
            var client = new RestClient(hookUrl);
            var getTokenRequest = new RestRequest(getTokenEndpoint, Method.Post);
            getTokenRequest.AddHeader("Content-Type", "application/json");

            string jsonBody = $"{{\"ConsumerKey\":\"multisys.prod\",\"Data\":\"{GlobalVar.rawHookData}\"}}";
            getTokenRequest.AddJsonBody(jsonBody);

            var getTokenResponse = await client.ExecuteAsync(getTokenRequest);

            if (!getTokenResponse.IsSuccessful)
            {
                MessageBox.Show("GetToken call failed: " + getTokenResponse.StatusCode);
                return;
            }

            dynamic tokenResponse = JsonConvert.DeserializeObject(getTokenResponse.Content);
            GlobalVar.rawHookToken = tokenResponse?.Token?.ToString(); // Save token
            GlobalVar.tokenExpirationTime = DateTime.Now.AddMinutes(GlobalVar.tokenValidityMinutes); // Set token expiration time

            if (string.IsNullOrEmpty(GlobalVar.rawHookToken))
            {
                MessageBox.Show("Token extraction failed. Retrying with fresh data.");
                await RunGetDataAsync(); // Retry with fresh data if token extraction failed
                await RunGetTokenAsync(); // Retry getting the token
            }
        }

        // Method to run GetBrandInfo
        private async Task RunGetBrandInfoAsync()
        {
            string hookUrl = "https://api01.smart.com.ph/HookAPI_Multisys";
            string getBrandInfoEndpoint = "/external.svc/json/GetBrandInfo";
            var client = new RestClient(hookUrl);
            var getBrandInfoRequest = new RestRequest(getBrandInfoEndpoint, Method.Post);
            getBrandInfoRequest.AddHeader("Content-Type", "application/json");

            // Validate AccountNumber
            string accountNumber = textBox1.Text.Trim(); // Assume you get this from a TextBox for user input
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                MessageBox.Show("Account number is required.");
                return;
            }

            var getBrandInfoRequestBody = new
            {
                AccountNumber = accountNumber,
                Token = GlobalVar.rawHookToken
            };

            string brandInfoJsonBody = JsonConvert.SerializeObject(getBrandInfoRequestBody);
            getBrandInfoRequest.AddJsonBody(brandInfoJsonBody);

            var getBrandInfoResponse = await client.ExecuteAsync(getBrandInfoRequest);

            if (getBrandInfoResponse.IsSuccessful)
            {
                // Deserialize the response content into a dynamic object or a defined class
                var responseObject = JsonConvert.DeserializeObject<dynamic>(getBrandInfoResponse.Content);

                // Assuming the data to be displayed is in a list/array format within the response
                // For example, responseObject.BrandInfoOutput could be a list of objects
                // Convert it to a List that can be bound to the DataGridView
                var brandInfoList = new List<dynamic> { responseObject.BrandInfoOutput };

                // Bind the list to the DataGridView
                dataGridView1.DataSource = brandInfoList;

                // Alternatively, you can convert the object to a DataTable and bind it to DataGridView
                // This method is more common when you need more control over the data
                //var dataTable = ConvertToDataTable(brandInfoList);
                //dataGridView1.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("GetBrandInfo call failed: " + getBrandInfoResponse.StatusCode);
            }
        }

        // Utility method to convert a list of objects to a DataTable (optional)
        private DataTable ConvertToDataTable<T>(List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);

            // Get all properties of the type
            var props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var prop in props)
            {
                // Define each property as a column in the datatable
                dataTable.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

    }
}
