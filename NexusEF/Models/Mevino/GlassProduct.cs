namespace NexusEF.Models.Mevino {
    public partial class GlassProduct {
        public int Id { get; set; }
        public string? Material { get; set; }
        public string? Size { get; set; }

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}