using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Type
{
    public int Id { get; set; }

    public string Type1 { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
