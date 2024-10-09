
using System;
using System.Collections.Generic;

namespace AmazonClone.Areas.Admin.Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string? Image { get; set; }

    public string? Name { get; set; }

    public decimal? PriceCents { get; set; }

    public string? Keywords { get; set; }

    public string? Type { get; set; }

    public string? SizeChartLink { get; set; }

    public string? InstructionsLink { get; set; }

    public string? WarrantyLink { get; set; }

    public string? Stars { get; set; }

    public string? Counting { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Tracking> Trackings { get; set; } = new List<Tracking>();
}
