using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class QuizQuestion
{
    public int Id { get; set; }

    public int QuizId { get; set; }

    public int? Score { get; set; }

    public string? Question { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual ICollection<QuizAnswer> QuizAnswers { get; set; } = new List<QuizAnswer>();
}
