using Microsoft.AspNetCore.Mvc;
using RoboIAZoho.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic; // Para List<Project>
using RoboIAZoho.Models; // Para os modelos

namespace RoboIAZoho.Controllers
{
    public class ZohoProjectsController : Controller
    {
        private readonly IZohoProjectService _projectService;

        public ZohoProjectsController(IZohoProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IActionResult> ListProjects()
        {
            try
            {
                List<Project> projects = await _projectService.GetProjectsFromApiAsync();
                return View(projects); // Passe o modelo diretamente para a View
            }
            catch (Exception ex)
            {
                // Tratar o erro de forma amigável
                ViewBag.ErrorMessage = $"Não foi possível carregar os projetos: {ex.Message}";
                return View(new List<Project>()); // Retorna uma lista vazia em caso de erro
            }
        }

        public async Task<IActionResult> ListTasks(string projectId)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                return RedirectToAction("ListProjects");
            }

            try
            {
                var tasks = await _projectService.GetTasksFromProjectApiAsync(projectId);
                ViewBag.ProjectId = projectId;
                return View(tasks);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Erro ao buscar tarefas: {ex.Message}";
                return View(new List<TaskItem>());
            }
        }

        // Action para o formulário ou link que inicia a importação
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportTask(string projectId, string taskId)
        {
            if (string.IsNullOrEmpty(projectId) || string.IsNullOrEmpty(taskId))
            {
                return BadRequest("Project ID e Task ID são obrigatórios.");
            }

            try
            {
                await _projectService.ImportTaskWithDetailsAsync(projectId, taskId);
                TempData["SuccessMessage"] = $"Tarefa {taskId} e seus dados foram importados com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Falha ao importar a tarefa {taskId}: {ex.Message}";
            }

            // Redireciona de volta para a lista de tarefas do projeto
            return RedirectToAction("ListTasks", new { projectId = projectId });
        }
    }
}