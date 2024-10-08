using System;
using System.Collections.Generic;

namespace AmazonClone.Areas.Admin.Models;

public partial class Cart
{
    public string CartId { get; set; } = null!;

    public string? UserId { get; set; }

    public string? ProductId { get; set; }

    public string? DeliveryOptionId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual DeliveryOption? DeliveryOption { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
