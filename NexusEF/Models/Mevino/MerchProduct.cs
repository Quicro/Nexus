namespace NexusEF.Models.Mevino {
    public partial class MerchProduct {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? Material { get; set; }
        public string? Size { get; set; }

        public virtual Product Product { get; set; }
        public string? Type { get; internal set; }
    }
}