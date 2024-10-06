using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmazonClone.Areas.Admin.Data;
using AmazonClone.Areas.Admin.Models;

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
          private string GenerateCustomId()
            {
            var guid = Guid.NewGuid().ToString();

            // Tùy chỉnh: giữ lại 8 ký tự đầu và 4 ký tự cuối, thêm dấu "-" ở giữa
            return guid.Substring(0, 8) + "-" + guid.Substring(guid.Length - 4);
        }

        // Action thêm sản phẩm vào giỏ hàng
        [HttpPost]
            public ActionResult AddToCart(string productId, int quantity = 1)
            {
                var cartItem = _context.Cart.FirstOrDefault(ci => ci.ProductId == productId);
                var cartId = this.GenerateCustomId();

                if (cartItem == null)
                {
                    // Nếu sản phẩm chưa có trong giỏ, thêm mới
                    cartItem = new Cart
                    {
                        CartId = cartId,
                        ProductId = productId,
                        Quantity = quantity
                    };
                    _context.Cart.Add(cartItem);
                }
                else
                {
                    // Nếu sản phẩm đã có, cập nhật số lượng
                    cartItem.Quantity += quantity;
                }

                _context.SaveChanges(); // Lưu thay đổi vào DB

                // Trả về tổng số lượng sản phẩm trong giỏ hàng
                var totalQuantity = _context.Cart.Sum(ci => ci.Quantity);
                return RedirectToAction("Index", new { cartQuantity = totalQuantity });
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

