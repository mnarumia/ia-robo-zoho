using RoboIAZoho.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoboIAZoho.Services
{
    public interface IZohoProjectService
    {
        Task<List<TaskItem>> GetTasksFromProjectApiAsync(string projectId);
        Task ImportTaskDetailsAsync(string projectId, string taskId);

        Task<string> ListTasksAsync(string projectId);
    }
}