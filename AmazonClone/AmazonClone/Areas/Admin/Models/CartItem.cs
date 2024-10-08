using System;
using System.Collections.Generic;

namespace AmazonClone.Areas.Admin.Models;

public partial class CartItem
{
    public string CartitemId { get; set; } = null!;

    public string? CartId { get; set; }

    public string? ProductId { get; set; }

    public int? ProductQuantity { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Product? Product { get; set; }
}
