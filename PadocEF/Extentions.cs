using Microsoft.EntityFrameworkCore;
using PadocEF.Models;

namespace PadocEF.Extentions {
    public class Extentions<T> where T : class {
        protected Extentions<T> extentions = new();

        public T get(int ID) => DatabaseManager.context.Set<T>().Single(obj => (int)typeof(T).GetProperty("Id").GetValue(obj) == ID);
        public List<T> get(string name) => DatabaseManager.context.Set<T>().Where(obj => (string)typeof(T).GetProperty("Name").GetValue(obj) == name).ToList();
    }

    public sealed class ClaimExtention : Extentions<Claim> {
        public static IQueryable<Policy?> getPolicy(Claim claim) {
            IQueryable<Policy> queryable = DatabaseManager.context.Claim
                .Include(c => c.Policy)
                .Select(c => c.Policy);

            return queryable;
        }
    }

    public sealed class ClientExtention : Extentions<Client> {
        public static IQueryable<Policy> getPolicy(Client client) {
            IQueryable<Policy> queryable = DatabaseManager.context.Policy
                .Where(p => p.Client == client);
            return queryable;
        }

        public static IQueryable<Permission> getPermissions(User user) {
            IQueryable<Permission> queryable = DatabaseManager.context.UserRole
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .ThenInclude(r => r.RolePermission)
                .ThenInclude(rp => rp.Permission)
                .Where(ur => ur.User == user)
                .SelectMany(ur => ur.Role.RolePermission.Select(rp => rp.Permission));
            return queryable;
        }


    }

    public sealed class PermissionExtention : Extentions<Permission> {
        public static IQueryable<Role> getRoles(Permission permission) {
            IQueryable<Role> queryable = DatabaseManager.context.Role
                .Include(r => r.RolePermission)
                .ThenInclude(rp => rp.Permission)
                .Where(r => r.RolePermission.Any(rp => rp.Permission == permission));

            return queryable;
        }

        public static IQueryable<User> getUsers(Permission permission) {
            IQueryable<User> queryable = DatabaseManager.context.RolePermission
                .Include(rp => rp.Permission)
                .Include(rp => rp.Role)
                .ThenInclude(r => r.UserRole)
                .ThenInclude(ur => ur.User)
                .Where(rp => rp.Permission == permission)
                .SelectMany(rp => rp.Role.UserRole.Select(ur => ur.User));
            return queryable;
        }
    }

    public sealed class RoleExtention : Extentions<Role> {
        public static IQueryable<User> getUsers(Role role) {
            IQueryable<User> queryable = DatabaseManager.context.User
                .Include(u => u.UserRole)
                .Where(rp => rp.UserRole.Any(ur => ur.Role == role));

            return queryable;
        }

        public static IQueryable<Permission> getPermissions(Role role) {
            IQueryable<Permission> queryable = DatabaseManager.context.Permission
                .Include(r => r.RolePermission)
                .Where(rp => rp.RolePermission.Any(ur => ur.Role == role));

            return queryable;
        }
    }

    public sealed class UserExtention : Extentions<User> {
        public static IQueryable<Permission> getPermissions(User user) {
            IQueryable<Permission> queryable = DatabaseManager.context.UserRole
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .ThenInclude(r => r.RolePermission)
                .ThenInclude(rp => rp.Permission)
                .Where(ur => ur.User == user)
                .SelectMany(ur => ur.Role.RolePermission.Select(rp => rp.Permission));
            return queryable;
        }

        public static IQueryable<Role> getRoles(User user) {
            IQueryable<Role> queryable = DatabaseManager.context.Role
                .Include(r => r.UserRole)
                .ThenInclude(ur => ur.User)
                .Where(r => r.UserRole.Any(ur => ur.User == user));

            return queryable;
        }



        public static bool hasPermissions(User user, params string[] permissionNames) {
            var permissionsQueryable = getPermissions(user);

            var foundPermissions = permissionsQueryable
                .Where(rp => permissionNames.Contains(rp.Name));
            return foundPermissions.Any();
        }

        public static User? validate(string username, string password) {
            User? user = DatabaseManager.context.User.FirstOrDefault(u =>
                u.Name == username &&
                u.Password == password
            );
            return user;
        }
    }

    public sealed class PolicyExtention : Extentions<Policy> {
        public static IQueryable<Client> getClient(Policy policy) {
            IQueryable<Client> queryable = DatabaseManager.context.Policy
                .Include(policy => policy.Client)
                .Select(policy => policy.Client);

            return queryable;
        }

        public static IQueryable<Claim> getClaims(Policy policy) {
            IQueryable<Claim> queryable = DatabaseManager.context.Policy
                .Include(policy => policy.Claim)
                .SelectMany(policy => policy.Claim);

            return queryable;
        }
    }
}
