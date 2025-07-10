
using Microsoft.AspNetCore.Mvc;

namespace RoboIAZoho.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
