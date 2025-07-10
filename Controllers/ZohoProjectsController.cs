
using Microsoft.AspNetCore.Mvc;
using RoboIAZoho.classes;
using System.Threading.Tasks;

namespace RoboIAZoho.Controllers
{
    public class ZohoProjectsController : Controller
    {
        private readonly ZohoProjectsApiClient _client;

        public ZohoProjectsController(ZohoProjectsApiClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> ListProjects()
        {
            var resultJson = await _client.ListProjectsAsync();
            ViewBag.ProjectsJson = resultJson;
            return View();
        }
    }
}
