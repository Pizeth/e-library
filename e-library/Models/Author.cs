using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Author
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string PenName { get; set; } = null!;

    public string? Avatar { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
