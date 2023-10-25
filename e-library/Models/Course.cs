using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Course
{
    public int Id { get; set; }

    public string CourseName { get; set; } = null!;

    public int TypeId { get; set; }

    public string? Cover { get; set; }

    public double? Price { get; set; }

    public string ProfessorId { get; set; } = null!;
}
