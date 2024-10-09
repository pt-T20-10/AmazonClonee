using System;
using Microsoft.AspNetCore.Mvc;
using AmazonClone.Areas.Admin.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AmazonClone.Models;
using AmazonClone.Areas.Admin.Data;
using System.Security.Claims;

public class CartController : Controller
{
    private readonly Amazon3Context _context;

    public CartController(Amazon3Context context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        string cartId = GetCartId();
        var cartItems = _context.CartItems
            .Where(ci => ci.CartId == cartId)
            .Include(ci => ci.Product)
            .ToList();

        var deliveryOptions = _context.DeliveryOptions.ToList(); // Lấy DeliveryOptions từ CSDL
        decimal? productTotal = cartItems.Sum(ci => ci.Quantity * (ci.Product.PriceCents ?? 0)); // Tính tổng sản phẩm
        decimal shippingCost = 5.00m; // Giá trị mặc định cho phí vận chuyển
        decimal estimatedTax = (productTotal ?? 0) * 0.1m; // Tính thuế 10%
        decimal orderTotal = (productTotal ?? 0) + shippingCost + estimatedTax; // Tính tổng đơn hàng

        var viewModel = new CartViewModel
        {
            CartItems = cartItems.Select(ci => new CartItemViewModel
            {
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                Quantity = ci.Quantity,
                Price = ci.Product.PriceCents,
                ImageUrl = ci.Product.Image
            }).ToList(),
            DeliveryOptions = deliveryOptions,
            TotalPrice = productTotal ?? 0,
            ShippingCost = shippingCost,
            EstimatedTax = estimatedTax,
            OrderTotal = orderTotal
        };

        return View(viewModel);
    }

    // Action thêm sản phẩm vào giỏ hàng
    [HttpPost]
    public ActionResult AddToCart(string productId, int quantity)
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
        if (product == null)
        {
            return NotFound("Product not found.");
        }

        // Lấy hoặc tạo giỏ hàng cho người dùng
        var cartId = GetCartId();
        var cart = _context.Cart.Include(c => c.CartItems).FirstOrDefault(c => c.CartId == cartId);

        if (cart == null)
        {
            cart = new Cart
            {
                CartId = GenerateCustomId()
                // Thực hiện lấy ID của người dùng đã đăng nhập
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
                CartItemId = GenerateCustomId(),
                CartId = cart.CartId,
                ProductId = productId,
                Quantity = quantity // Sửa lại để sử dụng giá trị quantity
            };
            cart.CartItems.Add(cartItem); // Thêm CartItem vào giỏ hàng
        }
        else
        {
            // Nếu sản phẩm đã có trong giỏ hàng, cập nhật số lượng
            cartItem.Quantity += quantity;
        }

        // Lưu các thay đổi vào cơ sở dữ liệu
        _context.SaveChanges();

        // Trả về tổng số lượng sản phẩm trong giỏ hàng
        var totalQuantity = cart.CartItems.Sum(ci => ci.Quantity);
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
            .Where(p => (p.Name.Contains(query) || p.Keywords.Contains(query) || p.Type.Contains(query)))
            .ToList();

        return View("Index", products); // Trả về View với kết quả tìm kiếm
    }

    [HttpPost]
    public IActionResult UpdateQuantity(string productId, int quantity)
    {
        string cartId = GetCartId();
        var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartId == cartId && ci.ProductId == productId);

        if (cartItem != null)
        {
            if (quantity <= 0)
            {
                return Delete(productId); // Xoá sản phẩm nếu số lượng <= 0
            }
            cartItem.Quantity = quantity;
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(string productId)
    {
        string cartId = GetCartId();
        var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartId == cartId && ci.ProductId == productId);

        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
        }

        TempData["Message"] = cartItem != null ? "Product removed successfully" : "Product not found";
        return RedirectToAction("Index");
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
