using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ACMS.DAL.Models;
using System.Text;

namespace ClientSideACMS.Data
{
    public class StudentApiServices
    {
        public HttpClient client;

        public StudentApiServices(HttpClient Client)
        {
            client = Client;
            client.BaseAddress = new Uri("https://localhost:5001/");
        }

        public async Task<string> CreateAccount(Student model) 
        {
            var modelInJson = JsonConvert.SerializeObject(model);

            var result = await client.PostAsync("/Student/account/create", new StringContent(modelInJson, Encoding.UTF8, "application/json"));

            result.EnsureSuccessStatusCode();

            var httpResponseMessage = await result.Content.ReadAsStringAsync();

            return httpResponseMessage;
        }
    }
}
