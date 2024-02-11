using System;
using System.Collections.Generic;

namespace WebApplication1.Modelss;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
