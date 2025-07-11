using RoboIAZoho.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoboIAZoho.Services
{
    public interface IZohoProjectService
    {
        // Adicionamos um método para listar os projetos
        Task<List<Project>> GetProjectsFromApiAsync();

        Task<List<TaskItem>> GetTasksFromProjectApiAsync(string projectId);

        // Este método irá conter toda a lógica de importação
        Task ImportTaskWithDetailsAsync(string projectId, string taskId);
    }
}