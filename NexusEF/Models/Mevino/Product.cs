namespace NexusEF.Models.Mevino {
    public partial class Product {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }

        // Navigation property for the many OrderLines related to this Product
        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();


        // Navigation properties to specific product details
        public virtual WineProduct WineProduct { get; set; }
        public virtual MerchProduct MerchProduct { get; set; }
        public virtual BeerProduct BeerProduct { get; set; }
        public virtual GlassProduct GlassProduct { get; set; }
    }
}
