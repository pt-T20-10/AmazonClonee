using System;
using System.Collections.Generic;

namespace AmazonClone.Areas.Admin.Models;

public partial class Login
{
    public string LoginId { get; set; } = null!;

    public string? UserId { get; set; }

    public DateTime? LoginTime { get; set; }

    public DateTime? LogoutTime { get; set; }

    public virtual User? User { get; set; }
}
