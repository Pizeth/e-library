using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Genre
{
    public Genre() 
    {
        Books = new HashSet<Book>();
    }
    public int Id { get; set; }

    public string GenreName { get; set; } = null!;

    public string? Cover { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
