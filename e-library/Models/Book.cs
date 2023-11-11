using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Book
{
    public Book()
    {
        BookChapters = new HashSet<BookChapter>();
    }
    public int Id { get; set; }

    public string BookTitle { get; set; } = null!;

    public int AuthorId { get; set; }

    public string? Description { get; set; }

    public string? Cover { get; set; }

    public DateTime? PublishedDate { get; set; }

    public int? CategoryId { get; set; }

    public int? GenreId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<BookChapter> BookChapters { get; set; } = new List<BookChapter>();

    public virtual Category? Category { get; set; }

    public virtual Genre? Genre { get; set; }
}
