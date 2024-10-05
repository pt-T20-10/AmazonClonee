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
    public class DeliveryOptionsController : Controller
    {
        private readonly Amazon3Context _context;

        public DeliveryOptionsController(Amazon3Context context)
        {
            _context = context;
        }

        // GET: Admin/DeliveryOptions
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeliveryOptions.ToListAsync());
        }

        // GET: Admin/DeliveryOptions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryOption = await _context.DeliveryOptions
                .FirstOrDefaultAsync(m => m.DeliveryOptionId == id);
            if (deliveryOption == null)
            {
                return NotFound();
            }

            return View(deliveryOption);
        }

        // GET: Admin/DeliveryOptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/DeliveryOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryOptionId,Name,Price")] DeliveryOption deliveryOption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryOption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryOption);
        }

        // GET: Admin/DeliveryOptions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryOption = await _context.DeliveryOptions.FindAsync(id);
            if (deliveryOption == null)
            {
                return NotFound();
            }
            return View(deliveryOption);
        }

        // POST: Admin/DeliveryOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DeliveryOptionId,Name,Price")] DeliveryOption deliveryOption)
        {
            if (id != deliveryOption.DeliveryOptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryOption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryOptionExists(deliveryOption.DeliveryOptionId))
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
            return View(deliveryOption);
        }

        // GET: Admin/DeliveryOptions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryOption = await _context.DeliveryOptions
                .FirstOrDefaultAsync(m => m.DeliveryOptionId == id);
            if (deliveryOption == null)
            {
                return NotFound();
            }

            return View(deliveryOption);
        }

        // POST: Admin/DeliveryOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var deliveryOption = await _context.DeliveryOptions.FindAsync(id);
            if (deliveryOption != null)
            {
                _context.DeliveryOptions.Remove(deliveryOption);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryOptionExists(string id)
        {
            return _context.DeliveryOptions.Any(e => e.DeliveryOptionId == id);
        }
    }
}
