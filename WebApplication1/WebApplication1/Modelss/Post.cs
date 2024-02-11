using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Modelss;

public partial class Post
{
    
    public int PostId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Contents { get; set; }

    public DateTime? TimeStamp { get; set; }

    [Required]
    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
