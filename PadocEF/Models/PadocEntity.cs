namespace PadocEF.Models {
    public interface IPadocEntity {
        public int Id { get; set; }
    }

    public partial class Claim : IPadocEntity { }
    public partial class Client : IPadocEntity { }
    public partial class Policy : IPadocEntity { }
    public partial class User : IPadocEntity { }
    public partial class Permission : IPadocEntity { }
    public partial class RolePermission : IPadocEntity { }
    public partial class UserRole : IPadocEntity { }
    public partial class Role : IPadocEntity { }
}

