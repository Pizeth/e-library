using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class QuizAnswer
{
    public int Id { get; set; }

    public int QuizQuestionId { get; set; }

    public bool IsCorrect { get; set; }

    public string? Content { get; set; }

    public virtual QuizQuestion QuizQuestion { get; set; } = null!;

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();
}
