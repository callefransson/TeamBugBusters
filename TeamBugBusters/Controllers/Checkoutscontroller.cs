using Microsoft.AspNetCore.Mvc;

namespace TeamBugBusters.Controllers
{
    public class Checkoutscontroller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
