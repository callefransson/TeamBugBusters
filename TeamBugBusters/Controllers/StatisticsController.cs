using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamBugBusters.Data;
using TeamBugBusters.Models;

namespace TeamBugBusters.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                              .Include(o => o.User)
                              .ToListAsync();

            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p=>p.ProductStock < 5)
                .ToListAsync();

            var viewModel = new OrdersAndLowStockProductsViewModel
            {
                Orders = orders,
                LowStockProducts = products
            };
            return View(viewModel);
        }
    }
}
