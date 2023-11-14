using Microsoft.AspNetCore.Mvc;

namespace DriveAnheu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PastasController : BaseController<PastasController>
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}