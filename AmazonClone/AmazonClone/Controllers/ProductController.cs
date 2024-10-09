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


    }
}

