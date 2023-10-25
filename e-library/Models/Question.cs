using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Question
{
    public int Id { get; set; }

    public int? CourseId { get; set; }

    public string? Question1 { get; set; }

    public string? Answer { get; set; }
}
