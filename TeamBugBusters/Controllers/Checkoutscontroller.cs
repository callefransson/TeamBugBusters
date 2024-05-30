using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Runtime.InteropServices.Marshalling;
using TeamBugBusters.Data;
using TeamBugBusters.Models;
using TeamBugBusters.Services;

namespace TeamBugBusters.Controllers
{
    public class Checkoutscontroller : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly WeatherService _weatherService;

        public Checkoutscontroller(ApplicationDbContext context, UserManager<IdentityUser> userManager, WeatherService weatherService)
        {
            _context = context;
            _userManager = userManager;
            _weatherService = weatherService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            var userId = _userManager.GetUserId(User);
            var checkoutItems = (
                from c in _context.Carts
                join ci in _context.CartItems on c.CartId equals ci.FkCartId
                join p in _context.Products on ci.FkProductId equals p.ProductId
                where ci.UserId == userId
                select new CheckoutViewModel
                {
                    OrderStatus = OrderStatus.Unconfirmed,
                    ProductName = p.ProductName,
                    Quantity = ci.Quantity,
                    ProductPrice = p.ProductPrice,
                    DiscountPrice = p.DiscountPrice,
                    TotalPrice = p.ProductPrice * ci.Quantity,
                }
            ).ToList();
            var checkoutModel = new CheckoutPageViewModel
            {
                CheckoutItems = checkoutItems,
                Address = new AddressViewModel()
            };
            return View(checkoutModel);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitOrder(CheckoutPageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Checkout", model);
            }

            var userId = _userManager.GetUserId(User);

            var cart = _context.Carts.FirstOrDefault(c => c.UserId == userId);
            if (cart == null)
            {
                return NotFound("No cart found for the current user.");
            }

            var cartItems = _context.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.FkCartId == cart.CartId)
            .ToList();

            if (cartItems.Count == 0)
            {
                return RedirectToAction("Index", "Products");
            }
            var weatherInfo = await _weatherService.GetWeatherAsync(model.Address.City);

            var newOrder = new Order
            {
                UserId = userId,
                FkCartId = cart.CartId,
                OrderDate = DateTime.Now,
                ShippingAdress = model.Address.ShippingAddress,
                City = model.Address.City,
                ZipCode = model.Address.ZipCode,
                OrderStatus = OrderStatus.Unconfirmed,
                TrackingNumber = Guid.NewGuid(),
                OrderNumber = new Random().Next(100000, 999999),
                WeatherDescription = weatherInfo.Weather[0].Description,
                Temperature = weatherInfo.Main.Temp,
                WeatherIcon = weatherInfo.Weather[0].Icon,
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            foreach (var cartItem in cartItems)
            {
                var product = cartItem.Product;
                if (product != null)
                {
                    product.ProductStock -= cartItem.Quantity;
                    if (product.ProductStock < 0)
                    {
                        product.ProductStock = 0;
                    }
                    _context.Products.Update(product);
                }
            }

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            

            var orderViewModel = new OrderViewModel
            {
                OrderNumber = newOrder.OrderNumber,
                TrackingNumber = newOrder.TrackingNumber,
                OrderDate = newOrder.OrderDate,
                ShippingAddress = newOrder.ShippingAdress,
                OrderStatus = newOrder.OrderStatus,
            };

            return View(new List<OrderViewModel> { orderViewModel });
        }
       
    }
    
}

