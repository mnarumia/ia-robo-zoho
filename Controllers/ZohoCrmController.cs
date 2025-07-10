
using Microsoft.AspNetCore.Mvc;
using RoboIAZoho.classes;
using System.Threading.Tasks;

namespace RoboIAZoho.Controllers
{
    public class ZohoCrmController : Controller
    {
        private readonly ZohoCrmApiClient _client;

        public ZohoCrmController(ZohoCrmApiClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> ListLeads()
        {
            var resultJson = await _client.ListLeadsAsync();
            ViewBag.LeadsJson = resultJson;
            return View();
        }
    }
}
