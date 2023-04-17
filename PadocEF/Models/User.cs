using System;
using System.Collections.Generic;

namespace PadocEF.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Adname { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<UserRole> UserRole { get; set; } = new List<UserRole>();
}
