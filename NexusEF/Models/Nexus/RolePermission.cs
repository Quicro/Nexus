using NexusEF.Attributes;

namespace NexusEF.Models.Nexus;

[Nexus]
public partial class RolePermission {
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
