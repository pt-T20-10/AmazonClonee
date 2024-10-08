using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmazonClone.Areas.Admin.Data;
using AmazonClone.Areas.Admin.Models;
using System.Security.Claims;

namespace AmazonClone.Controllers
{
        public class ProductController : Controller
        {
            private readonly Amazon3Context _context;

            public ProductController(Amazon3Context context)
            {
                _context = context;
            }

        // Action hiển thị danh sách sản phẩm
        public ActionResult Index()
        {
            var products = _context.Products.ToList(); // Lấy danh sách sản phẩm từ DB
            return View(products);
        }

        // Action thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public ActionResult AddToCart(string productId, int quantity = 1)
        {
            // Lấy hoặc tạo giỏ hàng cho người dùng
            var cartId = GenerateCustomId(); // Thực hiện lấy giỏ hàng hiện tại của người dùng
            var cart = _context.Cart.Include(c => c.CartItems).FirstOrDefault(c => c.CartId == cartId);

            if (cart == null)
            {
                cart = new Cart
                {
                    CartId = GenerateCustomId(),
                    UserId = GetCurrentUserId() // Thực hiện lấy ID của người dùng đã đăng nhập
                };
                _context.Cart.Add(cart);
            }

            // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem == null)
            {
                // Nếu sản phẩm chưa có trong giỏ hàng, thêm mới một CartItem
                cartItem = new CartItem
                {
                    CartitemId = GenerateCustomId(),
                    CartId = cart.CartId,
                    ProductId = productId,
                    ProductQuantity = cart.CartItems.Count()
                };
                cart.CartItems.Add(cartItem); // Thêm CartItem vào giỏ hàng
            }
            else
            {
                // Nếu sản phẩm đã có trong giỏ hàng, cập nhật số lượng
                cartItem.ProductQuantity += quantity;
            }

            // Lưu các thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            // Trả về tổng số lượng sản phẩm trong giỏ hàng
            var totalQuantity = cart.CartItems.Sum(ci => ci.ProductQuantity);
            return RedirectToAction("Index", new { cartQuantity = totalQuantity });
        }
        private string GetCurrentUserId()
        {
            return HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Thay đổi ClaimTypes nếu cần thiết
        }

        private string GenerateCustomId()
        {
            var guid = Guid.NewGuid().ToString();
            return guid.Substring(0, 8) + "-" + guid.Substring(guid.Length - 4);
        }

        // Action tìm kiếm sản phẩm
        public ActionResult Search(string query)
            {
                var products = _context.Products
                    .Where(p => p.Name.Contains(query) || p.Keywords.Contains(query) || p.Type.Contains(query))
                    .ToList();

                return View("Index", products); // Trả về View với kết quả tìm kiếm
            }
        }


    }

