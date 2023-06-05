using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
