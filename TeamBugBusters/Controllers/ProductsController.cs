using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using TeamBugBusters.Data;
using TeamBugBusters.Models;

namespace TeamBugBusters.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
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

            var cartItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.FkProductId == productId);

            if (cartItem != null)
            {
                _context.Update(cartItem);
            }
            else
            {

                var newCartItem = new CartItems
                {
                    FkProductId = productId.Value,
                    FkCartId = GetOrCreateCartId()
                };

                _context.CartItems.Add(newCartItem);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("ShowCart");
        }

        private int GetOrCreateCartId()
        {
            var cart = _context.Carts.FirstOrDefault();
            if (cart == null)
            {
                cart = new Cart();
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }
            return cart.CartId;
        }

        [HttpPost]
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

                _context.SaveChanges();
            }

            return RedirectToAction("ShowCart");
        }

        public IActionResult ShowCart()
        {
            var cart = _context.CartItems
                .Include(c => c.Cart)
                .Include(p => p.Product)
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

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: CartItems/RemoveFromCartConfirmed
        [HttpPost, ActionName("RemoveFromCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCartConfirmed(int id)
        {
            var cart = await _context.CartItems.FindAsync(id);

            if (cart != null)
            {
                _context.CartItems.Remove(cart);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ShowCart));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? categoryId, Product product)
        {
            if(categoryId == null)
            {
                return NotFound();
            }
            product.FkCategoryId = categoryId.Value;
            if (product != null)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int? categoryId, Product product)
        {
            if (id != product.ProductId || categoryId == null)
            {
                return NotFound();
            }
            product.FkCategoryId = categoryId.Value;
            if (product != null)
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
    }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
