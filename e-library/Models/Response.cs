using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Response
{
    public int Id { get; set; }

    public int QuizTakerId { get; set; }

    public int QuizAnswerId { get; set; }

    public string? AnswerContent { get; set; }

    public virtual QuizAnswer QuizAnswer { get; set; } = null!;

    public virtual QuizTaker QuizTaker { get; set; } = null!;
}
