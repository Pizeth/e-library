using System;
using System.Collections.Generic;

namespace e_library.Models;

public partial class RefreshToken
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; } = string.Empty;

    public DateTime? ExpiryDate { get; set; }

    public virtual User User { get; set; } = null!;
}
