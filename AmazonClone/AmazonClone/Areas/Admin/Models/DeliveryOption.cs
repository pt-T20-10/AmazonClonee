using System;
using System.Collections.Generic;

namespace AmazonClone.Areas.Admin.Models;

public partial class DeliveryOption
{
    public string DeliveryOptionId { get; set; } = null!;

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
