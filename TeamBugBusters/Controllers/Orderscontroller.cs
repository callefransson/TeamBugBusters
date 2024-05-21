using Microsoft.AspNetCore.Mvc;

namespace TeamBugBusters.Controllers
{
    public class Orderscontroller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
