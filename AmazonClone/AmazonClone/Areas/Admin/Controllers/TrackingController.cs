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
    public class TrackingController : Controller
    {
        private readonly Amazon3Context _context;

        public TrackingController(Amazon3Context context)
        {
            _context = context;
        }

        // GET: Admin/Tracking
        public async Task<IActionResult> Index()
        {
            var amazon3Context = _context.Trackings.Include(t => t.Order).Include(t => t.Product);
            return View(await amazon3Context.ToListAsync());
        }

        // GET: Admin/Tracking/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracking = await _context.Trackings
                .Include(t => t.Order)
                .Include(t => t.Product)
                .FirstOrDefaultAsync(m => m.TrackingId == id);
            if (tracking == null)
            {
                return NotFound();
            }

            return View(tracking);
        }

        // GET: Admin/Tracking/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: Admin/Tracking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackingId,OrderId,ProductId,Status,UpdateTime")] Tracking tracking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tracking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", tracking.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", tracking.ProductId);
            return View(tracking);
        }

        // GET: Admin/Tracking/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracking = await _context.Trackings.FindAsync(id);
            if (tracking == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", tracking.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", tracking.ProductId);
            return View(tracking);
        }

        // POST: Admin/Tracking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TrackingId,OrderId,ProductId,Status,UpdateTime")] Tracking tracking)
        {
            if (id != tracking.TrackingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tracking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackingExists(tracking.TrackingId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", tracking.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", tracking.ProductId);
            return View(tracking);
        }

        // GET: Admin/Tracking/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracking = await _context.Trackings
                .Include(t => t.Order)
                .Include(t => t.Product)
                .FirstOrDefaultAsync(m => m.TrackingId == id);
            if (tracking == null)
            {
                return NotFound();
            }

            return View(tracking);
        }

        // POST: Admin/Tracking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tracking = await _context.Trackings.FindAsync(id);
            if (tracking != null)
            {
                _context.Trackings.Remove(tracking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackingExists(string id)
        {
            return _context.Trackings.Any(e => e.TrackingId == id);
        }
    }
}
