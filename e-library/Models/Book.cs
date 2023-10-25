using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Book
{
    public int Id { get; set; }

    public string BookTitle { get; set; } = null!;

    public int AuthorId { get; set; }

    public string? Description { get; set; }

    public string? Cover { get; set; }

    public DateTime? PublishedDate { get; set; }

    public int? CategoryId { get; set; }

    public int? GenreId { get; set; }
}
