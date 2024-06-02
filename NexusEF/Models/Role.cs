namespace NexusEF.Models;

public partial class Role {
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<RolePermission> RolePermission { get; set; } = new List<RolePermission>();

    public virtual ICollection<UserRole> UserRole { get; set; } = new List<UserRole>();
}
