using System;
using System.Collections.Generic;

namespace PadocEF.Models;

public partial class Policy
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Number { get; set; }

    public int? ClientId { get; set; }

    public virtual ICollection<Claim> Claim { get; set; } = new List<Claim>();

    public virtual Client? Client { get; set; }
}
