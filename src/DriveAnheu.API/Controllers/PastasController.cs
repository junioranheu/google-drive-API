using Microsoft.AspNetCore.Mvc;

namespace DriveAnheu.API.Controllers
{
    public class PastasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}