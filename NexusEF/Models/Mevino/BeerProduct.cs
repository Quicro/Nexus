namespace NexusEF.Models.Mevino {
    public partial class BeerProduct {
        public int Id { get; set; }
        public string? Brewery { get; set; }
        public decimal? AlcoholVolume { get; set; }

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string? Type { get; internal set; }
    }
}