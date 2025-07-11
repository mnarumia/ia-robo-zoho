using Microsoft.Extensions.Options;
using RoboIAZoho.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RoboIAZoho.classes
{
    public class ZohoProjectsApiClient : BaseApiClient
    {
        private readonly HttpClient _client;

        public ZohoProjectsApiClient(HttpClient client, IOptions<ZohoApiSettings> apiSettings)
            : base(client, apiSettings, apiSettings.Value.ProjectsApiBaseUrl)
        {
        }

        /// <summary>
        /// Lista todos os projetos.x   
        /// </summary>
        public async Task<string> ListProjectsAsync()
        {
            var response = await _client.GetAsync("portal/YOUR_PORTAL_ID/projects/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Lista todas as tarefas de um projeto espec�fico.
        /// </summary>
        public async Task<string> ListTasksAsync(string projectId)
        {
            var portalId = "YOUR_PORTAL_ID"; // Substitua pelo seu Portal ID
            var response = await _client.GetAsync($"portal/{portalId}/projects/{projectId}/tasks/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Obt�m os detalhes de uma tarefa espec�fica.
        /// </summary>
        public async Task<string> GetTaskDetailsAsync(string projectId, string taskId)
        {
            var portalId = "YOUR_PORTAL_ID"; // Substitua pelo seu Portal ID
            var response = await _client.GetAsync($"portal/{portalId}/projects/{projectId}/tasks/{taskId}/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Obt�m as subtarefas de uma tarefa espec�fica.
        /// </summary>
        public async Task<string> GetSubTasksAsync(string projectId, string taskId)
        {
            var portalId = "YOUR_PORTAL_ID"; // Substitua pelo seu Portal ID
            var response = await _client.GetAsync($"portal/{portalId}/projects/{projectId}/tasks/{taskId}/subtasks/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Obt�m a lista de anexos de uma tarefa.
        /// </summary>
        public async Task<string> GetAttachmentsAsync(string projectId, string taskId)
        {
            var portalId = "YOUR_PORTAL_ID"; // Substitua pelo seu Portal ID
            var response = await _client.GetAsync($"portal/{portalId}/projects/{projectId}/tasks/{taskId}/attachments/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Baixa o conte�do de um anexo a partir de uma URL.
        /// </summary>
        public async Task<byte[]> DownloadAttachmentAsync(string downloadUrl)
        {
            // Para URLs de download, � melhor usar um novo HttpClient, pois a BaseAddress pode ser diferente.
            using (var downloadClient = new HttpClient())
            {
                var response = await downloadClient.GetAsync(downloadUrl);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
        }
    }
}