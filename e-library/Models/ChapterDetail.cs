using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class ChapterDetail
{
    public int Id { get; set; }

    public int ChapterId { get; set; }

    public string? Image { get; set; }

    public string? Body { get; set; }

    public virtual BookChapter Chapter { get; set; } = null!;
}
