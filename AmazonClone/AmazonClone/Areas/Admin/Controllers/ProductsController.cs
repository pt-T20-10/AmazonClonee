using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmazonClone.Areas.Admin.Data;
using AmazonClone.Areas.Admin.Models;

namespace AmazonClone.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly Amazon3Context _context;

        public ProductsController(Amazon3Context context)
        {
            _context = context;
        }

        // GET: Admin/Products
        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var products = _context.Products; // Không cần Include 'Type' ở đây
            ViewBag.Type = await GetTypeSelectList();
            return View(await products.ToListAsync());
        }

        // POST: Admin/Products (Lọc theo Type)
        [HttpPost]
        public async Task<IActionResult> Index(IFormCollection form)
        {
            string selectedType = form["Type"].ToString();
            IQueryable<Product> products;

            if (!string.IsNullOrEmpty(selectedType))
            {
                products = _context.Products.Where(p => p.Type == selectedType); // Lọc theo Type
            }
            else
            {
                products = _context.Products; // Không cần Include 'Type' ở đây
            }

            ViewBag.Type = await GetTypeSelectList(selectedType);  // Cập nhật giá trị được chọn trong dropdown
            return View(await products.ToListAsync());
        }

        private async Task<SelectList> GetTypeSelectList(string selectedType = null)
        {
            var types = await _context.Products.Select(p => p.Type).Distinct().ToListAsync();
            return new SelectList(types, selectedType);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(string id)
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

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Image,Name,PriceCents,Keywords,Type,SizeChartLink,InstructionsLink,WarrantyLink,Stars,Counting")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
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

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductId,Image,Name,PriceCents,Keywords,Type,SizeChartLink,InstructionsLink,WarrantyLink,Stars,Counting")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(string id)
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

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
