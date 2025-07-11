using RoboIAZoho.classes;
using RoboIAZoho.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq; // Necessário para .Any()

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

        public async Task<List<Project>> GetProjectsFromApiAsync()
        {
            var projectsJson = await _apiClient.ListProjectsAsync();
            // Supondo que a API retorna um objeto com uma propriedade "projects"
            var apiResponse = JsonSerializer.Deserialize<ProjectsApiResponse>(projectsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return apiResponse?.Projects ?? new List<Project>();
        }

        public Task<List<TaskItem>> GetTasksFromProjectApiAsync(string projectId)
        {
            // Correção: Remova o 'await' aqui.
            // Você está simplesmente passando a "promessa" (Task) adiante.
            return _apiClient.ListTasksAsync(projectId);
        }

        public async Task ImportTaskWithDetailsAsync(string projectId, string taskId)
        {
            // 1. Buscar todos os dados da API em paralelo para mais eficiência
            var taskDetailsJsonTask = _apiClient.GetTaskDetailsAsync(projectId, taskId);
            var subTasksJsonTask = _apiClient.GetSubTasksAsync(projectId, taskId);
            var attachmentsJsonTask = _apiClient.GetAttachmentsAsync(projectId, taskId);

            await Task.WhenAll(taskDetailsJsonTask, subTasksJsonTask, attachmentsJsonTask);

            // 2. Desserializar os resultados
            // NOTA: Crie classes DTO (como TasksApiResponse) para todas as respostas da API
            var mainTask = JsonSerializer.Deserialize<TaskItem>(await taskDetailsJsonTask);
            var subTasks = JsonSerializer.Deserialize<List<SubTask>>(await subTasksJsonTask);
            var attachmentsInfo = JsonSerializer.Deserialize<List<TaskAttachment>>(await attachmentsJsonTask);

            // 3. Salvar a tarefa principal (verificando se já não existe)
            if (!_context.Tasks.Any(t => t.Id == mainTask.Id))
            {
                mainTask.ProjectId = long.Parse(projectId);
                _context.Tasks.Add(mainTask);
            }

            // 4. Salvar as subtarefas
            foreach (var subTask in subTasks)
            {
                if (!_context.SubTasks.Any(s => s.Id == subTask.Id))
                {
                    subTask.ParentTaskId = mainTask.Id;
                    _context.SubTasks.Add(subTask);
                }
            }

            // 5. Baixar e salvar os anexos
            foreach (var attachmentInfo in attachmentsInfo)
            {
                if (!_context.TaskAttachments.Any(a => a.Id == attachmentInfo.Id))
                {
                    var fileContent = await _apiClient.DownloadAttachmentAsync(attachmentInfo.ZohoDownloadUrl);
                    var attachment = new TaskAttachment
                    {
                        Id = attachmentInfo.Id,
                        FileName = attachmentInfo.FileName,
                        FileContent = fileContent,
                        TaskId = mainTask.Id
                    };
                    _context.TaskAttachments.Add(attachment);
                }
            }

            // 6. Salvar tudo no banco de dados em uma única transação
            await _context.SaveChangesAsync();
        }
    }

    // Classe auxiliar para desserializar a lista de projetos
    public class ProjectsApiResponse
    {
        public List<Project> Projects { get; set; }
    }
}