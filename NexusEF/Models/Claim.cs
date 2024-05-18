using System;
using System.Collections.Generic;

namespace NexusEF.Models;

public partial class Claim
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Number { get; set; }

    public int? PolicyId { get; set; }

    public virtual Policy? Policy { get; set; }
}
