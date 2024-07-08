using NexusEF.DatabaseManager;
using NexusEF.Extentions;

namespace NexusEF.Models.Nexus.Extention {

    /// <summary>
    /// The Extentions class provides a set of static methods for querying entities in the database.
    /// These methods are used to retrieve related entities based on the type of the entity and the method name.
    /// </summary>
    public class NexusExtentions : DatabaseManagerExtention<NexusDatabaseManager> {
        static NexusExtentions() {
            mappingMoreMoreRelations.Add(new(typeof(User), typeof(UserRole)), typeof(Role));
            mappingMoreMoreRelations.Add(new(typeof(Role), typeof(UserRole)), typeof(User));
            mappingMoreMoreRelations.Add(new(typeof(Role), typeof(RolePermission)), typeof(Permission));
            mappingMoreMoreRelations.Add(new(typeof(Permission), typeof(RolePermission)), typeof(Role));
        }

        /*public sealed class PermissionExtention : ClassExtentions<Permission> {
            public static IQueryable<Role> getRolesQueryable(Permission permission) {
                IQueryable<Role> queryable = NexusDatabaseManager.context.Role
                    .Include(r => r.RolePermission)
                    .ThenInclude(rp => rp.Permission)
                    .Where(r => r.RolePermission.Any(rp => rp.Permission == permission));

                return queryable;
            }

            public static IQueryable<User> getUsersQueryable(Permission permission) {
                IQueryable<User> queryable = NexusDatabaseManager.context.RolePermission
                    .Include(rp => rp.Permission)
                    .Include(rp => rp.Role)
                    .ThenInclude(r => r.UserRole)
                    .ThenInclude(ur => ur.User)
                    .Where(rp => rp.Permission == permission)
                    .SelectMany(rp => rp.Role.UserRole.Select(ur => ur.User));
                return queryable;
            }
        }

        public sealed class RoleExtention : ClassExtentions<Role> {
            public static IQueryable<User> getUsersQueryable(Role role) {
                IQueryable<User> queryable = NexusDatabaseManager.context.User
                    .Include(u => u.UserRole)
                    .Where(rp => rp.UserRole.Any(ur => ur.Role == role));

                return queryable;
            }

            public static IQueryable<Permission> getPermissionsQueryable(Role role) {
                IQueryable<Permission> queryable = NexusDatabaseManager.context.Permission
                    .Include(r => r.RolePermission)
                    .Where(rp => rp.RolePermission.Any(ur => ur.Role == role));

                return queryable;
            }
        }

        public sealed class UserExtention : ClassExtentions<User> {
            public static IQueryable<Permission> getPermissionsQueryable(User user) {
                IQueryable<Permission> queryable = .context.UserRole
                    .Include(ur => ur.User)
                    .Include(ur => ur.Role)
                    .ThenInclude(r => r.RolePermission)
                    .ThenInclude(rp => rp.Permission)
                    .Where(ur => ur.User == user)
                    .SelectMany(ur => ur.Role.RolePermission.Select(rp => rp.Permission));
                return queryable;
            }

            public static List<Permission>? getPermissions(User user) {
                List<Permission>? permissions = user?.UserRole
                    ?.SelectMany(ur => ur.Role.RolePermission.Select(rp => rp.Permission))
                    ?.ToList();

                return permissions;
            }


            public static IQueryable<Role> getRolesQueryable(User user) {
                IQueryable<Role> queryable = NexusDatabaseManager.context.Role
                    .Include(r => r.UserRole)
                    .ThenInclude(ur => ur.User)
                    .Where(r => r.UserRole.Any(ur => ur.User == user));

                return queryable;
            }



            public static bool hasPermissions(User user, params string[] permissionNames) {
                IQueryable<Permission> permissionsQueryable = getPermissionsQueryable(user);

                IQueryable<Permission> foundPermissions = permissionsQueryable
                    .Where(rp => permissionNames.Contains(rp.Name));
                return foundPermissions.Any();
            }

            public static User? validate(string username, string password) {
                User? user = NexusDatabaseManager.context.User.FirstOrDefault(u =>
                    u.Name == username &&
                    u.Password == password
                );
                return user;
            }
        }*/
    }
}