using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AmazonClone.Areas.Admin.Models;

public class CartItem
{
    [Required]
    public string CartItemId { get; set; }
    [Required]
    public string CartId { get; set; }
    [Required]
    public string ProductId { get; set; } // Kiểm tra đây
    public int Quantity { get; set; }

    public virtual Cart Cart { get; set; }
    public virtual Product Product { get; set; }
}
