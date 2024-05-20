using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamBugBusters.Data;

namespace TeamBugBusters.Controllers
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cart = _context.CartItems
                .Include(c=>c.Cart)
                .ToList();
            return View(cart);
        }
    }
}
