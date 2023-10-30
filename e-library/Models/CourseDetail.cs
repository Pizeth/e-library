﻿using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class CourseDetail
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int UserId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
