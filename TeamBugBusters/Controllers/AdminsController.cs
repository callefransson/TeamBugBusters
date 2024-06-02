using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamBugBusters.Data;
using TeamBugBusters.Models;

namespace TeamBugBusters.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Products
                .Include(x => x.Category)
                .ToListAsync();
            return View(await _context.Products.ToListAsync());
        }
        //GET: Products/AddDiscount
        [HttpGet]
        public IActionResult AddDiscount()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }
        //POST: Products/AddDiscount/5
        [HttpPost]
        public async Task<IActionResult> AddDiscount(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product != null)
            {
                return RedirectToAction("AddDiscountPrice", new { productId = productId });
            }
            else
            {
                return RedirectToAction("AddDiscount");
            }
        }

        //GET: Products/AddDiscountPrice/5
        [HttpGet]
        public async Task<IActionResult> AddDiscountPrice(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductDiscountViewModel
            {
                ProductName = product.ProductName,
                CurrentPrice = product.ProductPrice
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddDiscountPrice(ProductDiscountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == model.ProductId);
                if (product == null)
                {
                    return NotFound();
                }

                product.DiscountPrice = product.ProductPrice - model.Discount;
                product.DiscountStartDate = model.StartDate;
                product.DiscountEndDate = model.EndDate;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
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
        [HttpGet]
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
            if (categoryId == null)
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
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.ProductId == id);
            
            if (product == null)
            {
                return NotFound();
            }
            var viewModel = new EditProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.FkCategoryId,
                ProductDescription = product.ProductDescription,
                ProductStock = product.ProductStock,
                ProductPrice = product.ProductPrice
            };
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");

            return View(viewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProductViewModel viewModel)
        {
            if (id != viewModel.ProductId)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.ProductName = viewModel.ProductName;
            product.FkCategoryId = viewModel.CategoryId;
            product.ProductDescription = viewModel.ProductDescription;
            product.ProductStock = viewModel.ProductStock;
            product.ProductPrice = viewModel.ProductPrice;

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(viewModel.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
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
    }
}
