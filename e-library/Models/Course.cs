using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Course
{
    public int Id { get; set; }

    public string CourseName { get; set; } = null!;

    public int CategoryId { get; set; }

    public int TypeId { get; set; }

    public string? Cover { get; set; }

    public double? Price { get; set; }

    public int ProfessorId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<CourseDetail> CourseDetails { get; set; } = new List<CourseDetail>();

    public virtual Professor Professor { get; set; } = null!;

    public virtual ICollection<Quiz> Quizs { get; set; } = new List<Quiz>();

    public virtual Type Type { get; set; } = null!;
}
