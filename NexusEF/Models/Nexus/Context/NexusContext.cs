using Microsoft.EntityFrameworkCore;


namespace NexusEF.Models.Nexus.Context;

public partial class NexusContext : DbContext {
    public NexusContext() {
    }

    public NexusContext(DbContextOptions<NexusContext> options)
        : base(options) {
    }

    public virtual DbSet<Permission> Permission { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<RolePermission> RolePermission { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserRole> UserRole { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        _ = optionsBuilder.UseInMemoryDatabase("Data Source=;Initial Catalog=Nexus;Integrated Security=True;Encrypt=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        _ = modelBuilder.Entity<Permission>(entity => {
            _ = entity.ToTable("Permission", "pa");

            _ = entity.Property(e => e.Id).HasColumnName("ID");
            _ = entity.Property(e => e.Name).IsUnicode(false);
        });

        _ = modelBuilder.Entity<Role>(entity => {
            _ = entity.ToTable("Role", "pa");

            _ = entity.Property(e => e.Id).HasColumnName("ID");
            _ = entity.Property(e => e.Name).IsUnicode(false);
        });

        _ = modelBuilder.Entity<RolePermission>(entity => {
            _ = entity.ToTable("RolePermission", "pa");

            _ = entity.Property(e => e.Id).HasColumnName("ID");
            _ = entity.Property(e => e.PermissionId).HasColumnName("PermissionID");
            _ = entity.Property(e => e.RoleId).HasColumnName("RoleID");

            _ = entity.HasOne(d => d.Permission).WithMany(p => p.RolePermission)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermission_Permission");

            _ = entity.HasOne(d => d.Role).WithMany(p => p.RolePermission)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermission_Role");
        });

        _ = modelBuilder.Entity<User>(entity => {
            _ = entity.ToTable("User", "pa");

            _ = entity.Property(e => e
            .Id).HasColumnName("UserID");
            _ = entity.Property(e => e.Adname)
                .IsUnicode(false)
                .HasColumnName("ADName");
            _ = entity.Property(e => e.Email).IsUnicode(false);
            _ = entity.Property(e => e.Name).IsUnicode(false);
            _ = entity.Property(e => e.Password).IsUnicode(false);
            _ = entity.Property(e => e.Phone).IsUnicode(false);
        });

        _ = modelBuilder.Entity<UserRole>(entity => {
            _ = entity.ToTable("UserRole", "pa");

            _ = entity.Property(e => e.Id).HasColumnName("ID");
            _ = entity.Property(e => e.RoleId).HasColumnName("RoleID");
            _ = entity.Property(e => e.UserId).HasColumnName("UserID");

            _ = entity.HasOne(d => d.Role).WithMany(p => p.UserRole)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Role");

            _ = entity.HasOne(d => d.User).WithMany(p => p.UserRole)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Role1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
