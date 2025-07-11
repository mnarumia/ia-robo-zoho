
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

        public async Task<string> GetTaskDetailsAsync(string projectId, string taskId)
        {
            // O portal ID é necessário e deve ser gerenciado (ex: via appsettings)
            var portalId = "YOUR_PORTAL_ID"; // Substitua pelo seu Portal ID
            var response = await _client.GetAsync($"portal/{portalId}/projects/{projectId}/tasks/{taskId}/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetSubTasksAsync(string projectId, string taskId)
        {
            var portalId = "YOUR_PORTAL_ID"; // Substitua pelo seu Portal ID
            var response = await _client.GetAsync($"portal/{portalId}/projects/{projectId}/tasks/{taskId}/subtasks/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAttachmentsAsync(string projectId, string taskId)
        {
            var portalId = "YOUR_PORTAL_ID"; // Substitua pelo seu Portal ID
            var response = await _client.GetAsync($"portal/{portalId}/projects/{projectId}/tasks/{taskId}/attachments/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<byte[]> DownloadAttachmentAsync(string downloadUrl)
        {
            // O cliente HttpClient para download pode precisar ser configurado
            // de forma diferente se a URL for de outro domínio.
            var response = await _client.GetAsync(downloadUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
