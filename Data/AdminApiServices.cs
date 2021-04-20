using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientSideACMS.Data
{
    public class AdminApiServices
    {
        private HttpClient client = new HttpClient();

        public AdminApiServices(HttpClient Client)
        {
            client = Client;
        }
    }
}
