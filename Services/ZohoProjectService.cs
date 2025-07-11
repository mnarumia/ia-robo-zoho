using RoboIAZoho.classes;
using RoboIAZoho.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoboIAZoho.Services
{
    public class ZohoProjectService : IZohoProjectService
    {
        private readonly ZohoProjectsApiClient _apiClient;
        private readonly ApplicationDbContext _context;

        public ZohoProjectService(ZohoProjectsApiClient apiClient, ApplicationDbContext context)
        {
            _apiClient = apiClient;
            _context = context;
        }

        public async Task<List<TaskItem>> GetTasksFromProjectApiAsync(string projectId)
        {
            // A lógica de negócio está aqui
           // return await _apiClient.ListTasksAsync(projectId);
           return null;
        }

        public async Task ImportTaskDetailsAsync(string projectId, string taskId)
        {
            // A lógica de orquestração fica no serviço, não no controller.
            // Aqui você chamaria os métodos do _apiClient para buscar detalhes,
            // subtarefas, anexos, e depois usaria o _context para salvar no banco.
            // Exemplo:
            // var taskDetails = await _apiClient.GetTaskDetailsAsync(projectId, taskId);
            // ... processar e salvar com _context.Tasks.Add(...);
            await _context.SaveChangesAsync();
        }

        public Task<string> ListTasksAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }
    }
}