using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return Content("Hello world");
        }
    }
}
