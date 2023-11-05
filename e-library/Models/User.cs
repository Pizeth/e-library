using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class User
{
    public User()
    {
        CourseDetails = new HashSet<CourseDetail>();
        QuizTakers = new HashSet<QuizTaker>();
        RefreshTokens = new HashSet<RefreshToken>();
    }
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public int RoleId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Avatar { get; set; }

    public string? Salt { get; set; }

    public DateTime? RegDate { get; set; }

    public virtual ICollection<CourseDetail> CourseDetails { get; set; } = new List<CourseDetail>();

    public virtual ICollection<QuizTaker> QuizTakers { get; set; } = new List<QuizTaker>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual Role Role { get; set; } = null!;
}
