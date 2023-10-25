using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class BookChapter
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public string? ChapterTitle { get; set; }

    public string? Cover { get; set; }
}
