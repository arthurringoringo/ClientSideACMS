using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using ACMS.DAL.Models;

namespace ClientSideACMS.Data
{
    public class APIServiceExtension : IAPIServiceExtension
    {
        private HttpClient _client = new HttpClient();

        public APIServiceExtension(HttpClient Client)
        {
            _client = Client;
            ////client.BaseAddress = new Uri("https://localhost:5001/");
        }


        public async Task<string> SendEmail(EmailDto emailDto)
        {
            var emailInJson = JsonConvert.SerializeObject(emailDto);

            var result = await _client.PostAsync("/ServiceExtension/email/send", new StringContent(emailInJson, Encoding.UTF8, "application/json"));

            result.EnsureSuccessStatusCode();

            var httpResponseMessage = await result.Content.ReadAsStringAsync();

            return httpResponseMessage;
        }
    }
}
