using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class QuizTaker
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int QuizId { get; set; }

    public int Score { get; set; }

    public string? Remark { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();

    public virtual User User { get; set; } = null!;
}
