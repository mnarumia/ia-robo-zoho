
using Microsoft.AspNetCore.Mvc;
using RoboIAZoho.classes;
using System.Threading.Tasks;

namespace RoboIAZoho.Controllers
{
    public class ZohoDeskController : Controller
    {
        private readonly ZohoDeskApiClient _client;

        public ZohoDeskController(ZohoDeskApiClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> ListTickets()
        {
            var resultJson = await _client.ListTicketsAsync();
            ViewBag.TicketsJson = resultJson;
            return View();
        }
    }
}
