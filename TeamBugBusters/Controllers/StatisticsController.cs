using Microsoft.AspNetCore.Mvc;
using TeamBugBusters.Data;

namespace TeamBugBusters.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
