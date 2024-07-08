using Microsoft.EntityFrameworkCore;


namespace NexusEF.Models.Mevino.Context;

public partial class MevinoContextInMemory : DbContext {
    public MevinoContextInMemory() {
    }

    public MevinoContextInMemory(DbContextOptions<MevinoContextInMemory> options)
        : base(options) {
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderLine> OrderLines { get; set; }
    public virtual DbSet<Seller> Sellers { get; set; }
    public virtual DbSet<WineProduct> WineProducts { get; set; }
    public virtual DbSet<BeerProduct> BeerProducts { get; set; }
    public virtual DbSet<MerchProduct> MerchProducts { get; set; }
    public virtual DbSet<GlassProduct> GlassProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        _ = optionsBuilder.UseInMemoryDatabase("Data Source=;Initial Catalog=Mevino;Integrated Security=True;Encrypt=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        _ = modelBuilder.Entity<Product>(entity => {
            _ = entity.ToTable("Products");
            _ = entity.HasKey(e => e.Id);
            _ = entity.Property(e => e.Name).IsRequired(false).IsUnicode(false);
            _ = entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
        });

        _ = modelBuilder.Entity<Order>(entity => {
            _ = entity.ToTable("Orders");
            _ = entity.HasKey(e => e.Id);
            _ = entity.HasMany(e => e.OrderLines).WithOne(ol => ol.Order).HasForeignKey(ol => ol.OrderId);
            _ = entity.HasMany(e => e.Sellers).WithMany(s => s.Orders).UsingEntity(j => j.ToTable("OrderSellers"));
        });

        _ = modelBuilder.Entity<OrderLine>(entity => {
            _ = entity.ToTable("OrderLines");
            _ = entity.HasKey(e => e.Id);
            _ = entity.HasOne(ol => ol.Order).WithMany(o => o.OrderLines).HasForeignKey(ol => ol.OrderId);
            _ = entity.HasOne(ol => ol.Product).WithMany(p => p.OrderLines).HasForeignKey(ol => ol.ProductId);
            _ = entity.Property(e => e.Quantity).IsRequired(false); // Assuming Quantity can be nullable
        });

        _ = modelBuilder.Entity<Seller>(entity => {
            _ = entity.ToTable("Sellers");
            _ = entity.HasKey(e => e.Id);
            _ = entity.Property(e => e.Name).IsRequired().IsUnicode(false);
            _ = entity.HasMany(s => s.Orders).WithMany(o => o.Sellers).UsingEntity(j => j.ToTable("OrderSellers"));
        });

        _ = modelBuilder.Entity<Product>(entity => {
            _ = entity.ToTable("Products");
            _ = entity.HasKey(e => e.Id);
            _ = entity.Property(e => e.Name).IsRequired().IsUnicode(false);
            _ = entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
        });

        _ = modelBuilder.Entity<Order>(entity => {
            _ = entity.ToTable("Orders");
            _ = entity.HasKey(e => e.Id);
            _ = entity.HasMany(e => e.OrderLines).WithOne(ol => ol.Order).HasForeignKey(ol => ol.OrderId);
            _ = entity.HasMany(e => e.Sellers).WithMany(s => s.Orders).UsingEntity(j => j.ToTable("OrderSellers"));
        });

        _ = modelBuilder.Entity<OrderLine>(entity => {
            _ = entity.ToTable("OrderLines");
            _ = entity.HasKey(e => e.Id);
            _ = entity.HasOne(ol => ol.Order).WithMany(o => o.OrderLines).HasForeignKey(ol => ol.OrderId);
            _ = entity.HasOne(ol => ol.Product).WithMany(p => p.OrderLines).HasForeignKey(ol => ol.ProductId);
            _ = entity.Property(e => e.Quantity).IsRequired(false); // Assuming Quantity can be nullable
        });

        _ = modelBuilder.Entity<Seller>(entity => {
            _ = entity.ToTable("Sellers");
            _ = entity.HasKey(e => e.Id);
            _ = entity.Property(e => e.Name).IsRequired(false).IsUnicode(false);
            _ = entity.HasMany(s => s.Orders).WithMany(o => o.Sellers).UsingEntity(j => j.ToTable("OrderSellers"));
        });

        _ = modelBuilder.Entity<BeerProduct>(entity => {
            _ = entity.ToTable("BeerProducts");
            _ = entity.HasKey(e => e.Id);
            _ = entity.Property(e => e.Brewery).IsRequired(false).IsUnicode(false);
            _ = entity.Property(e => e.AlcoholVolume).IsRequired(false).HasColumnType("decimal(5,2)");
        });

        _ = modelBuilder.Entity<GlassProduct>(entity => {
            _ = entity.ToTable("GlassProducts");
            _ = entity.HasKey(e => e.Id);
            _ = entity.Property(e => e.Material).IsRequired(false).IsUnicode(false);
            _ = entity.Property(e => e.Size).IsRequired(false).IsUnicode(false);
        });

        _ = modelBuilder.Entity<MerchProduct>(entity => {
            _ = entity.ToTable("MerchProducts");
            _ = entity.HasKey(e => e.Id);
            _ = entity.Property(e => e.Material).IsRequired(false).IsUnicode(false);
            _ = entity.Property(e => e.Size).IsRequired(false).IsUnicode(false);
        });

        _ = modelBuilder.Entity<WineProduct>(entity => {
            _ = entity.ToTable("WineProducts");
            _ = entity.HasKey(e => e.Id);
            _ = entity.Property(e => e.Category).IsRequired(false).IsUnicode(false);
            _ = entity.Property(e => e.Region).IsRequired(false).IsUnicode(false);
            _ = entity.Property(e => e.Variety).IsRequired(false).IsUnicode(false);
            _ = entity.Property(e => e.Vintage).IsRequired(false);
        });

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
