using System;
using System.Collections.Generic;

namespace AmazonClone.Areas.Admin.Models;

public partial class Tracking
{
    public string TrackingId { get; set; } = null!;

    public string? OrderId { get; set; }

    public string? ProductId { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
