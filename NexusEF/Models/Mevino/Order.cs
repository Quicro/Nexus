using System.Collections.Generic;

namespace NexusEF.Models.Mevino {
    public partial class Order {
        public int Id { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();
    }
}
