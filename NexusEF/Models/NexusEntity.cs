namespace NexusEF.Models {
    public interface INexusEntity {
        public int Id { get; set; }
    }

    public partial class Claim : INexusEntity { }
    public partial class Client : INexusEntity { }
    public partial class Policy : INexusEntity { }
    public partial class User : INexusEntity { }
    public partial class Permission : INexusEntity { }
    public partial class RolePermission : INexusEntity { }
    public partial class UserRole : INexusEntity { }
    public partial class Role : INexusEntity { }
}

