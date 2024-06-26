namespace NexusEF.Models;

public partial class Permission {
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<RolePermission> RolePermission { get; set; } = new List<RolePermission>();
}
