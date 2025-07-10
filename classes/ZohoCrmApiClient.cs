
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RoboIAZoho.classes
{
    public class ZohoCrmApiClient
    {
        private readonly HttpClient _client;

        public ZohoCrmApiClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://www.zohoapis.com/crm/v2/")
            };
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_ACCESS_TOKEN");
        }

        public async Task<string> ListLeadsAsync()
        {
            var response = await _client.GetAsync("Leads");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
