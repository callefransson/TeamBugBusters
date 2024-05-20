using Microsoft.AspNetCore.Mvc;

namespace TeamBugBusters.Controllers
{
    public class Order : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
