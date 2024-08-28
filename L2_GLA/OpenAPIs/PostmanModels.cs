using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace L2_GLA.OpenAPIs
{
    internal class PostmanModels
    {
        public class PostmanEnvironmentVariable
        {
            public string key { get; set; }
            public string value { get; set; }
            public bool enabled { get; set; }
        }

        public class PostmanEnvironment
        {
            public string name { get; set; }
            public List<PostmanEnvironmentVariable> values { get; set; }
        }

        // Method to load environment variables from a JSON file
        private PostmanEnvironment LoadEnvironment(string filePath)
        {
            string environmentJson = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<PostmanEnvironment>(environmentJson);
        }
    }
}
