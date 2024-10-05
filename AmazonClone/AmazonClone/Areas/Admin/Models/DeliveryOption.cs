using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AmazonClone.Areas.Admin.Models;

public partial class DeliveryOption
{
    [Required]
    public string DeliveryOptionId { get; set; } 

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
