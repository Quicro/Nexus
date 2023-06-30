using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PadocEF.Models;
using System.Reflection;

namespace PadocEF.Extentions {

    public class Extentions {
        public static IQueryable<IPadocEntity> getQueryable2(IPadocEntity entity, Type referenceType, string methodName = null) {
            Type entityType = entity.GetType();

            // Construct the generic getQueryable<T, R> method based on the entity and reference types
            MethodInfo genericMethod = typeof(Extentions).GetMethod("getQueryable")
                .MakeGenericMethod(entityType, referenceType);

            // Invoke the constructed generic method with the entity and methodName parameters
            IQueryable<IPadocEntity> result = (IQueryable<IPadocEntity>)genericMethod.Invoke(null, new object[] { entity, methodName });

            return result;
        }

        public static IQueryable<R> getQueryable<T, R>(T entity, string methodName = null)
            where T : class, IPadocEntity
            where R : class, IPadocEntity {
            Type entityType = typeof(T);
            Type extensionType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.IsSubclassOf(typeof(Extentions<>).MakeGenericType(entityType)));

            if (extensionType == null) {
                throw new InvalidOperationException($"No extension class found for type '{entityType.Name}'.");
            }

            // Find the method in the extension class
            MethodInfo method = null;
            var methods = extensionType.GetMethods();

            if (methodName.IsNullOrEmpty()) {
                method = methods
                    .FirstOrDefault(m => m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(T) && m.ReturnType == typeof(IQueryable<R>));
            } else {
                method = methods
                    .FirstOrDefault(m => m.Name == methodName && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(T) && m.ReturnType == typeof(IQueryable<R>));
            }

            if (method == null) {
                throw new InvalidOperationException($"No matching method found in the extension class '{extensionType.Name}' for method name '{methodName}' and parameter type '{entityType.Name}'.");
            }

            // Invoke the method if found, otherwise return null
            if (method != null) {
                var queryable = (IQueryable<R>)method.Invoke(null, new object[] { entity });
                return queryable;
            }

            return null;
        }
    }

    public class Extentions<T> where T : class, IPadocEntity {
        protected Extentions<T> extentions = new();

        public static IQueryable<T> getQueryable(int ID) => DatabaseManager.context.Set<T>().Where(x => x.Id == ID);
        public static T get(int ID) => DatabaseManager.context.Set<T>().Single(x => x.Id == ID);


    }

    public sealed class ClaimExtention : Extentions<Claim> {
        public static IQueryable<Policy> getPolicy(Claim claim) {
            IQueryable<Policy> queryable = DatabaseManager.context.Claim
                .Include(c => c.Policy)
                .Select(c => c.Policy);

            return queryable;
        }

        public static IQueryable<Policy> getPolicy(int claimID) {
            IQueryable<Policy> queryable = DatabaseManager.context.Claim
                .Where(c => c.Id == claimID)
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
