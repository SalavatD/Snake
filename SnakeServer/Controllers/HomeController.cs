using Microsoft.AspNetCore.Mvc;

namespace SnakeServer.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult HomePage()
        {
            ViewData["Message"] = "Game start page";
            return View("HomePage");
        }
    }
}
