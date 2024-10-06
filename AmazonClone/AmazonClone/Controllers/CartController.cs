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
    public class CartController : Controller
    {
        private readonly Amazon3Context _context;

        public CartController(Amazon3Context context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var cartItems = _context.Cart.Include(ci => ci.Product).ToList(); // Include để lấy thông tin sản phẩm từ quan hệ
            return View(cartItems);
        }

        [HttpPost]
        public ActionResult UpdateQuantity(string productId, int quantity)
        {
            var cartItem = _context.Cart.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }


}
