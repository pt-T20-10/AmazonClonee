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
    public class CartItemsController : Controller
    {
        private readonly Amazon3Context _context;

        public CartItemsController(Amazon3Context context)
        {
            _context = context;
        }

        // GET: Admin/CartItems
        public async Task<IActionResult> Index()
        {
            var amazon3Context = _context.CartItems.Include(c => c.Cart).Include(c => c.Product);
            return View(await amazon3Context.ToListAsync());
        }

        // GET: Admin/CartItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CartItemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: Admin/CartItems/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: Admin/CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartitemId,CartId,ProductId,ProductQuantity")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", cartItem.CartId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", cartItem.ProductId);
            return View(cartItem);
        }

        // GET: Admin/CartItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", cartItem.CartId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", cartItem.ProductId);
            return View(cartItem);
        }

        // POST: Admin/CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CartitemId,CartId,ProductId,ProductQuantity")] CartItem cartItem)
        {
            if (id != cartItem.CartItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.CartItemId))
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
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", cartItem.CartId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", cartItem.ProductId);
            return View(cartItem);
        }

        // GET: Admin/CartItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CartItemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: Admin/CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(string id)
        {
            return _context.CartItems.Any(e => e.CartItemId == id);
        }
    }
}
