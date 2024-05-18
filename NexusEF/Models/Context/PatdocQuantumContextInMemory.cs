using Microsoft.EntityFrameworkCore;

namespace NexusEF.Models.Context;

public partial class NexusOldContextInMemory : NexusOldContext {
    public NexusOldContextInMemory() {
    }

    public NexusOldContextInMemory(DbContextOptions<NexusOldContext> options)
        : base(options) {
    }

    public virtual DbSet<Claim> Claim { get; set; }

    public virtual DbSet<Client> Client { get; set; }

    public virtual DbSet<Permission> Permission { get; set; }

    public virtual DbSet<Policy> Policy { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<RolePermission> RolePermission { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserRole> UserRole { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseInMemoryDatabase("Data Source=;Initial Catalog=PatdocQuantum;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Claim>(entity => {
            entity.ToTable("Claim", "pa");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Number).IsUnicode(false);
            entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

            entity.HasOne(d => d.Policy).WithMany(p => p.Claim)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK_Claim_Policy");
        });

        modelBuilder.Entity<Client>(entity => {
            entity.ToTable("Client", "pa");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Number).IsUnicode(false);
        });

        modelBuilder.Entity<Permission>(entity => {
            entity.ToTable("Permission", "pa");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<Policy>(entity => {
            entity.ToTable("Policy", "pa");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Number).IsUnicode(false);

            entity.HasOne(d => d.Client).WithMany(p => p.Policy)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_Policy_Client");
        });

        modelBuilder.Entity<Role>(entity => {
            entity.ToTable("Role", "pa");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<RolePermission>(entity => {
            entity.ToTable("RolePermission", "pa");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PermissionId).HasColumnName("PermissionID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermission)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermission_Permission");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermission)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermission_Role");
        });

        modelBuilder.Entity<User>(entity => {
            entity.ToTable("User", "pa");

            entity.Property(e => e
            .Id).HasColumnName("UserID");
            entity.Property(e => e.Adname)
                .IsUnicode(false)
                .HasColumnName("ADName");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.Phone).IsUnicode(false);
        });

        modelBuilder.Entity<UserRole>(entity => {
            entity.ToTable("UserRole", "pa");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRole)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRole)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Role1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
