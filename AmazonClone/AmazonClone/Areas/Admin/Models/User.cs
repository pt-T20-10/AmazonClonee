using System;
using System.Collections.Generic;

namespace AmazonClone.Areas.Admin.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();
}
