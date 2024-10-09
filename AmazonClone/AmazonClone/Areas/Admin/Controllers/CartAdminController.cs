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
    public class CartAdminController : Controller
    {
        private readonly Amazon3Context _context;

        public CartAdminController(Amazon3Context context)
        {
            _context = context;
        }

        // GET: Admin/Cart
        public async Task<IActionResult> Index()
        {
            // Include cả CartItems và DeliveryOption để hiển thị
            var carts = await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product) // Include sản phẩm trong CartItem
                .Include(c => c.DeliveryOption) // Include DeliveryOption
                .ToListAsync();

            return View(carts);
        }


        // GET: Admin/Cart/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product) // Include sản phẩm trong CartItem
                .Include(c => c.DeliveryOption) // Include tùy chọn giao hàng
                .FirstOrDefaultAsync(m => m.CartId == id);

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }


        // GET: Admin/Cart/Create
        public IActionResult Create()
        {
            // Lấy danh sách sản phẩm và tùy chọn giao hàng để hiển thị trong view
            ViewData["DeliveryOptionId"] = new SelectList(_context.DeliveryOptions, "DeliveryOptionId", "DeliveryOptionName");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return View();
        }


        // POST: Admin/Cart/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId, DeliveryOptionId")] Cart cart, string[] selectedProducts, int[] quantities)
        {
            if (ModelState.IsValid)
            {
                // Thêm giỏ hàng mới
                _context.Add(cart);
                await _context.SaveChangesAsync();

                // Thêm các sản phẩm vào giỏ hàng
                for (int i = 0; i < selectedProducts.Length; i++)
                {
                    var cartItem = new CartItem
                    {
                        CartId = cart.CartId,
                        ProductId = selectedProducts[i],
                        Quantity = quantities[i]
                    };
                    _context.CartItems.Add(cartItem);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Nếu có lỗi, hiển thị lại form với dữ liệu cũ
            ViewData["DeliveryOptionId"] = new SelectList(_context.DeliveryOptions, "DeliveryOptionId", "DeliveryOptionName", cart.DeliveryOptionId);
            return View(cart);
        }


        // GET: Admin/Cart/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Lấy giỏ hàng và các sản phẩm trong giỏ
            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.CartId == id);

            if (cart == null)
            {
                return NotFound();
            }

            ViewData["DeliveryOptionId"] = new SelectList(_context.DeliveryOptions, "DeliveryOptionId", "DeliveryOptionName", cart.DeliveryOptionId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return View(cart);
        }


        // POST: Admin/Cart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CartId,DeliveryOptionId")] Cart cart, string[] selectedProducts, int[] quantities)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Cập nhật giỏ hàng
                    _context.Update(cart);

                    // Cập nhật các sản phẩm trong giỏ
                    var existingCartItems = _context.CartItems.Where(ci => ci.CartId == cart.CartId).ToList();
                    _context.CartItems.RemoveRange(existingCartItems); // Xóa các sản phẩm cũ

                    // Thêm các sản phẩm mới
                    for (int i = 0; i < selectedProducts.Length; i++)
                    {
                        var cartItem = new CartItem
                        {
                            CartId = cart.CartId,
                            ProductId = selectedProducts[i],
                            Quantity = quantities[i]
                        };
                        _context.Add(cartItem);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartId))
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

            ViewData["DeliveryOptionId"] = new SelectList(_context.DeliveryOptions, "DeliveryOptionId", "DeliveryOptionName", cart.DeliveryOptionId);
            return View(cart);
        }

        // GET: Admin/Cart/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product) // Bao gồm các sản phẩm trong giỏ
                .Include(c => c.DeliveryOption)
                .FirstOrDefaultAsync(m => m.CartId == id);

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Admin/Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cart = await _context.Cart
                .Include(c => c.CartItems) // Đảm bảo rằng đây là ICollection<CartItem>
                .FirstOrDefaultAsync(c => c.CartId == id);
                
            if (cart != null)
            {
                // Xóa các sản phẩm trong giỏ trước
                _context.CartItems.RemoveRange(cart.CartItems);
                _context.Cart.Remove(cart);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public string GetCartId()
        {
            if (HttpContext.Session.TryGetValue("CartId", out var cartIdBytes))
            {
                return System.Text.Encoding.UTF8.GetString(cartIdBytes);
            }
            else
            {
                var newCartId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("CartId", newCartId);
                return newCartId;
            }
        }

        public bool CartExists(string cartId)
        {
            return _context.CartItems.Any(ci => ci.CartId == cartId);
        }
    }
}
