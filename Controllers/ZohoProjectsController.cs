using Microsoft.AspNetCore.Mvc;
using RoboIAZoho.classes;
using RoboIAZoho.Models; // Adicionar using para os modelos
using System.Threading.Tasks;
using System.Text.Json; // Adicionar using para o JSON
using System.Collections.Generic;

namespace RoboIAZoho.Controllers
{
    public class ZohoProjectsController : Controller
    {
        private readonly ZohoProjectsApiClient _client;
        private readonly ApplicationDbContext _context; // Adicionar DbContext

        // Atualizar construtor para injetar o DbContext
        public ZohoProjectsController(ZohoProjectsApiClient client, ApplicationDbContext context)
        {
            _client = client;
            _context = context;
        }

        public async Task<IActionResult> ListProjects()
        {
            var resultJson = await _client.ListProjectsAsync();
            ViewBag.ProjectsJson = resultJson;
            return View();
        }

        // --- NOVA ACTION ---
        public async Task<IActionResult> ImportTaskAndSubTasks(string projectId, string taskId)
        {
            // 1. Buscar os detalhes da tarefa principal da API
            var taskJson = await _client.GetTaskDetailsAsync(projectId, taskId);
            var subTasksJson = await _client.GetSubTasksAsync(projectId, taskId);
            var attachmentsJson = await _client.GetAttachmentsAsync(projectId, taskId);

            // AQUI: Você precisará desserializar o JSON para os seus modelos.
            // A estrutura exata do JSON de resposta da API do Zoho deve ser usada para criar
            // classes DTO (Data Transfer Object) ou para desserializar diretamente.
            // Por simplicidade, vamos assumir uma desserialização direta para um objeto dinâmico.
            // Recomenda-se fortemente a criação de classes DTO para robustez.

            // Exemplo de como você poderia processar (requer ajuste com base no JSON real)
            var mainTask = JsonSerializer.Deserialize<TaskItem>(taskJson); // Supondo que o JSON corresponda ao modelo
            var subTasks = JsonSerializer.Deserialize<List<SubTask>>(subTasksJson);
            var attachmentsInfo = JsonSerializer.Deserialize<List<TaskAttachment>>(attachmentsJson);

            // 2. Salvar a tarefa principal
            mainTask.ProjectId = long.Parse(projectId);
            _context.Tasks.Add(mainTask);

            // 3. Associar e salvar as subtarefas
            foreach (var subTask in subTasks)
            {
                subTask.ParentTaskId = mainTask.Id;
                _context.SubTasks.Add(subTask);
            }

            // 4. Baixar e salvar os anexos
            foreach (var attachmentInfo in attachmentsInfo)
            {
                // Baixar o conteúdo do arquivo
                var fileContent = await _client.DownloadAttachmentAsync(attachmentInfo.ZohoDownloadUrl);

                var attachment = new TaskAttachment
                {
                    Id = attachmentInfo.Id,
                    FileName = attachmentInfo.FileName,
                    ZohoDownloadUrl = attachmentInfo.ZohoDownloadUrl,
                    FileContent = fileContent, // Conteúdo do arquivo em bytes
                    TaskId = mainTask.Id
                };
                _context.TaskAttachments.Add(attachment);
            }

            // 5. Salvar todas as alterações no banco de dados
            await _context.SaveChangesAsync();

            ViewBag.ResultMessage = $"Tarefa {mainTask.Id} e seus dados foram importados com sucesso!";
            return View("ListProjects"); // Ou redirecione para uma página de sucesso
        }
    }
}