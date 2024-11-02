using Microsoft.AspNetCore.Mvc;

namespace Tabaarak.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
