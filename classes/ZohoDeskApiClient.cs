
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
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "1000.dd3cc5a61dc36aa4b23db964e00dd9b3.f13cd9311c2fb60ce7a7378a747716ed");
        }

        public async Task<string> ListTicketsAsync()
        {
            var response = await _client.GetAsync("tickets");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
