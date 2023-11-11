using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class Category
{
    public Category() 
    {
        Books = new HashSet<Book>();
        Courses = new HashSet<Course>();
    }
    public int Id { get; set; }

    public string? CategoryName { get; set; }

    public string? Cover { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
