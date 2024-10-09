
using System;
using System.Collections.Generic;

namespace AmazonClone.Areas.Admin.Models;

public partial class Cart
{
    public string CartId { get; set; } = null!;

    public string? DeliveryOptionId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); // Đổi CartItems thành CartItem

    public virtual DeliveryOption? DeliveryOption { get; set; }
}
