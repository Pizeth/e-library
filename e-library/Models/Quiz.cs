using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Quiz
{
    public int Id { get; set; }

    public int? CourseId { get; set; }

    public int? Score { get; set; }

    public string? Summary { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();

    public virtual ICollection<QuizTaker> QuizTakers { get; set; } = new List<QuizTaker>();
}
