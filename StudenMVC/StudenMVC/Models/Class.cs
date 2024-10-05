using System;
using System.Collections.Generic;

namespace StudenMVC.Models;

public partial class Class
{
    public string ClassId { get; set; } = null!;

    public string ClassName { get; set; } = null!;

    public string? Slot { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
