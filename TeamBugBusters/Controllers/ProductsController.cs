using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TeamBugBusters.Data;
using TeamBugBusters.Models;

namespace TeamBugBusters.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Products
        public async Task<IActionResult> Index(int? productId)
        {
            var categories = await _context.Products
                .Include(x => x.Category)
                .ToListAsync();
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> AddToCart(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var cartItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.FkProductId == productId && p.UserId == userId);

            bool itemAdded = false;

            if (cartItem != null)
            {
                _context.Update(cartItem);
            }
            else
            {

                var newCartItem = new CartItems
                {
                    FkProductId = productId.Value,
                    FkCartId = GetOrCreateCartId(userId),
                    UserId = userId,
                    Quantity = 1
                };

                _context.CartItems.Add(newCartItem);
                itemAdded = true;
            }

            await _context.SaveChangesAsync();

            TempData["itemAdded"] = itemAdded;

            return RedirectToAction("Index");
        }

        private int GetOrCreateCartId(string userId)
        {
            var cart = _context.Carts.FirstOrDefault(c=>c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId
                };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }
            return cart.CartId;
        }

        [HttpPost]
        
        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.Category).ToList();
            return View(products);
        }
        
        public IActionResult FilterByCategory(int categoryId)
        {
            var products = _context.Products
            .Include(p => p.Category)
            .Where(p => p.Category.CategoryId == categoryId)  
            .ToList();

            return View("Index", products);
        }
        
        public IActionResult UpdateQuantity(int id, string change)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.CartItemsId == id);

            if (cartItem != null)
            {
                if (change == "increase")
                {
                    if (cartItem.Quantity == 0)
                    {
                        cartItem.Quantity = 1; 
                    }
                    else
                    {
                        cartItem.Quantity++;
                    }
                }
                else if (change == "decrease" && cartItem.Quantity > 0)
                {
                    cartItem.Quantity--;
                }

                if (cartItem.Quantity == 0)
                {
                    _context.CartItems.Remove(cartItem);
                    TempData["isRemoved"] = true; 
                }

                _context.SaveChanges();
            }

            return RedirectToAction("ShowCart");
        }

        public IActionResult ShowCart()
        {
            var userId = _userManager.GetUserId(User);
            var cart = _context.CartItems
                    .Include(c => c.Cart)
                    .Include(p => p.Product)
                    .Where(c=>c.UserId == userId)
                    .ToList();

            return View(cart);
        }

        // GET: CartItems/RemoveFromCart
        public IActionResult RemoveFromCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = _context.CartItems
                        .Include(c => c.Cart)
                        .Include(p => p.Product)
                        .FirstOrDefault(m => m.FkProductId == id);

            bool isRemoved = false;

            if (cart == null)
            {
                return NotFound();
            }

            TempData["isRemoved"] = isRemoved;

            return View(cart);
        }

        // POST: CartItems/RemoveFromCartConfirmed
        [HttpPost, ActionName("RemoveFromCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCartConfirmed(int id)
        {
            var cart = await _context.CartItems.FirstOrDefaultAsync(m => m.FkProductId == id);

            if (cart != null)
            {
                _context.CartItems.Remove(cart);
                await _context.SaveChangesAsync();
            }

            TempData["isRemoved"] = true;
            return RedirectToAction(nameof(ShowCart));
        }

        private int GetOrCreateCheckoutId()
        {
            var checkout = _context.Orders.FirstOrDefault();
            if (checkout == null)
            {
                checkout = new Order();
                _context.Orders.Add(checkout);
                _context.SaveChanges();
            }
            return checkout.OrderId;
        }

        public async Task <IActionResult> PlaceOrder(int? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }

            bool orderPlaced = false;



            TempData["orderPlaced"] = orderPlaced;

            return View();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
