using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Professor
{
    public int Id { get; set; }

    public string? ProfessorName { get; set; }

    public string? Institute { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
