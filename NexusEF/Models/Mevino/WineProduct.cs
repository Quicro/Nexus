namespace NexusEF.Models.Mevino {
    public partial class WineProduct {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? Category { get; set; }
        public string? Region { get; set; }
        public string? Variety { get; set; }
        public int? Vintage { get; set; }
        public string? Grape { get; internal set; }

        public virtual Product Product { get; set; }
    }
}