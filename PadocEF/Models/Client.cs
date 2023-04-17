using System;
using System.Collections.Generic;

namespace PadocEF.Models;

public partial class Client
{
    public int Id { get; set; }

    public string? Number { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Policy> Policy { get; set; } = new List<Policy>();
}
