using Microsoft.EntityFrameworkCore;

namespace NexusEF.Models.Insurance.Context;

public partial class InsuranceContext : DbContext {
    public InsuranceContext() {
    }

    public InsuranceContext(DbContextOptions<InsuranceContext> options)
        : base(options) {
    }


    public virtual DbSet<Claim> Claim { get; set; }
    public virtual DbSet<Policy> Policy { get; set; }
    public virtual DbSet<Client> Client { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        _ = optionsBuilder.UseSqlServer("Data Source=;Initial Catalog=Insurance;Integrated Security=True;Encrypt=False", options => {
            _ = options.CommandTimeout(1);
            _ = optionsBuilder.EnableSensitiveDataLogging();
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        _ = modelBuilder.Entity<Claim>(entity => {
            _ = entity.ToTable("Claim", "pa");

            _ = entity.Property(e => e.Id).HasColumnName("ID");
            _ = entity.Property(e => e.Name).IsUnicode(false);
            _ = entity.Property(e => e.Number).IsUnicode(false);
            _ = entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

            _ = entity.HasOne(d => d.Policy).WithMany(p => p.Claim)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK_Claim_Policy");
        });

        _ = modelBuilder.Entity<Client>(entity => {
            _ = entity.ToTable("Client", "pa");

            _ = entity.Property(e => e.Id).HasColumnName("ID");
            _ = entity.Property(e => e.Name).IsUnicode(false);
            _ = entity.Property(e => e.Number).IsUnicode(false);
        });

        _ = modelBuilder.Entity<Policy>(entity => {
            _ = entity.ToTable("Policy", "pa");

            _ = entity.Property(e => e.Id).HasColumnName("ID");
            _ = entity.Property(e => e.ClientId).HasColumnName("ClientID");
            _ = entity.Property(e => e.Name).IsUnicode(false);
            _ = entity.Property(e => e.Number).IsUnicode(false);

            _ = entity.HasOne(d => d.Client).WithMany(p => p.Policy)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_Policy_Client");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
