using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace L2_GLA.Model
{
    internal class apiclient
    {
        private string bearerToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiI3MWE3MjJjMy1mODA2LTQyY2EtODlhMC1lZjQ1MDNmM2JkZmYiLCJ1c2VyX2lkIjoiNzFhNzIyYzMtZjgwNi00MmNhLTg5YTAtZWY0NTAzZjNiZGZmIiwiaWF0IjoiMTYyMzg1NTA5Ni42MDU2NzIiLCJuYmYiOiIxNjIzODU1MDk2LjYwNTY3MiIsImV4cCI6IjE2MjM4NTU5OTYuNjA1NjcyIn0.ucJIhMl0PLsK1U_pEr65oucE1Xb9l9m7pE4a-T3BXB8";
        private string appToken = "abf1fb92-fb70-4fa6-9ec0-2d567ceb6ab9";

        public async Task CallAPI()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);
                client.DefaultRequestHeaders.Add("X-Application-Token", appToken);

                string apiUrl = "https://app1.smart.com.ph/api/v2/get-brand?number=9338172836";

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    // Parse and process the API response data as needed
                    Console.WriteLine(apiResponse);
                }
                else
                {
                    Console.WriteLine("Error calling API. Status code: " + response.StatusCode);
                }
            }
        }
    }
}
