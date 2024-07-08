using System.Collections.Generic;

namespace NexusEF.Models.Mevino {
    public partial class Seller {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
