using System;
using System.Collections.Generic;

namespace AmazonClone.Areas.Admin.Models;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public string? ProductId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ICollection<Tracking> Trackings { get; set; } = new List<Tracking>();
}
