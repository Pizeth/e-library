using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string Genre1 { get; set; } = null!;

    public string? Cover { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
