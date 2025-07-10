
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RoboIAZoho.classes
{
    public class ZohoProjectsApiClient
    {
        private readonly HttpClient _client;

        public ZohoProjectsApiClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://projectsapi.zoho.com/restapi/portal/")
            };
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_ACCESS_TOKEN");
        }

        public async Task<string> ListProjectsAsync()
        {
            var response = await _client.GetAsync("projects/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
