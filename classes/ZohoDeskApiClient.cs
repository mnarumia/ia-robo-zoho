
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RoboIAZoho.classes
{
    public class ZohoDeskApiClient
    {
        private readonly HttpClient _client;

        public ZohoDeskApiClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://desk.zoho.com/api/v1/")
            };
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_ACCESS_TOKEN");
        }

        public async Task<string> ListTicketsAsync()
        {
            var response = await _client.GetAsync("tickets");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
