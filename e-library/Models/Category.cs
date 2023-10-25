using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? CategoryName { get; set; }

    public string? Cover { get; set; }
}
