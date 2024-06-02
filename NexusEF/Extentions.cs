using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NexusEF.Models;
using System.Reflection;

namespace NexusEF {

    /// <summary>
    /// The Extentions class provides a set of static methods for querying entities in the database.
    /// These methods are used to retrieve related entities based on the type of the entity and the method name.
    /// </summary>
    public class Extentions {
        private static readonly Dictionary<Tuple<Type, Type>, Type> mappingMoreMoreRelations = new();

        static Extentions() {
            mappingMoreMoreRelations.Add(new(typeof(User), typeof(UserRole)), typeof(Role));
            mappingMoreMoreRelations.Add(new(typeof(Role), typeof(UserRole)), typeof(User));
            mappingMoreMoreRelations.Add(new(typeof(Role), typeof(RolePermission)), typeof(Permission));
            mappingMoreMoreRelations.Add(new(typeof(Permission), typeof(RolePermission)), typeof(Role));
        }

        public static Type getPossibleMoreMoreRelationType(Type entityType, Type referenceType) {
            return mappingMoreMoreRelations
                .Where(tuple => tuple.Key.Item1 == entityType && tuple.Key.Item2 == referenceType)
                .Select(tuple => tuple.Value)
                .FirstOrDefault();
        }

        private static IQueryable<INexusEntity> ConstructAndInvokeGenericQueryable(INexusEntity entity, Type referenceType, string? methodName = null) {
            Type entityType = entity.GetType();

            // Construct the generic getQueryable<T, R> method based on the entity and reference types
            MethodInfo genericMethod = typeof(Extentions).GetMethod(nameof(getQueryable))
                .MakeGenericMethod(entityType, referenceType);

            // Invoke the constructed generic method with the entity and methodName parameters
            IQueryable<INexusEntity> result = (IQueryable<INexusEntity>)genericMethod.Invoke(null, new object[] { entity, methodName });

            return result;
        }

        private static IQueryable<INexusEntity> ConstructAndInvokeRelatedGenericQueryable(INexusEntity entity, Type referenceType, string? methodName = null) {
            Type entityType = entity.GetType();

            // Construct the generic getQueryable<T, R> method based on the entity and reference types
            MethodInfo genericMethod = typeof(Extentions).GetMethod(nameof(getRelatedQueryable))
                .MakeGenericMethod(entityType, referenceType);

            // Invoke the constructed generic method with the entity and methodName parameters
            IQueryable<INexusEntity> result = (IQueryable<INexusEntity>)genericMethod.Invoke(null, new object[] { entity, methodName });

            return result;
        }

        public static IQueryable<R> getQueryableByID<R, T>(INexusEntity entity, int ID)

            where R : class, INexusEntity
            where T : class, INexusEntity {

            DbSet<T> set = DatabaseManager.context.Set<T>();
            IQueryable<R> a = ConstructAndInvokeGenericQueryable(entity, typeof(R)).Where(e => e.Id == ID).Cast<R>();
            return a;

        }


        public static IQueryable<T> getRelatedQueryableByID<R, T>(INexusEntity entity)

            where R : class, INexusEntity
            where T : class, INexusEntity {
            DatabaseManager.context.Set<T>();
            IQueryable<T> a = ConstructAndInvokeRelatedGenericQueryable(entity, typeof(T)).Cast<T>();
            a.ToQueryString();
            return a;


        }

        public static IQueryable<R> getQueryable<T, R>(T entity, string? methodName = null)
            where T : class, INexusEntity
            where R : class, INexusEntity {
            Type entityType = typeof(T);
            Type extensionType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.IsSubclassOf(typeof(Extentions<>).MakeGenericType(entityType)));

            if (extensionType == null) {
                throw new InvalidOperationException($"No extension class found for type '{entityType.Name}'.");
            }

            // Find the method in the extension class
            MethodInfo method = null;
            MethodInfo[] methods = extensionType.GetMethods();

            method = methodName.IsNullOrEmpty()
                ? methods
                    .FirstOrDefault(m => m.GetParameters().Length == 1 && m.GetParameters()[ 0 ].ParameterType == typeof(T) && m.ReturnType == typeof(IQueryable<R>))
                : methods
                    .FirstOrDefault(m => m.Name == methodName && m.GetParameters().Length == 1 && m.GetParameters()[ 0 ].ParameterType == typeof(T) && m.ReturnType == typeof(IQueryable<R>));

            if (method == null) {
                throw new InvalidOperationException($"No matching method found in the extension class '{extensionType.Name}' for method name '{methodName}' and parameter type '{entityType.Name}'.");
            }

            // Invoke the method if found, otherwise return null
            if (method != null) {
                IQueryable<R>? queryable = (IQueryable<R>)method.Invoke(null, new object[] { entity });
                return queryable;
            }

            return null;
        }

        public static IQueryable<R> getRelatedQueryable<T, R>(T entity, string? methodName = null)
            where T : class, INexusEntity
            where R : class, INexusEntity {
            Type entityType = typeof(T);
            Type extensionType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.IsSubclassOf(typeof(Extentions<>).MakeGenericType(entityType)));

            if (extensionType == null) {
                throw new InvalidOperationException($"No extension class found for type '{entityType.Name}'.");
            }

            // Find the method in the extension class
            MethodInfo method = null;
            IEnumerable<MethodInfo> methods = extensionType.GetMethods().Where(m => !( m.Name.Contains("ToString") | m.Name.Contains("ToString") || m.Name.Contains("Equals") || m.Name.Contains("GetHashCode") || m.Name.Contains("GetType") ));

            method = methodName.IsNullOrEmpty()
                ? methods
                    .FirstOrDefault(m => m.GetParameters().Length == 1 && m.GetParameters()[ 0 ].ParameterType == typeof(T) && m.ReturnType == typeof(IQueryable<R>))
                : methods
                    .FirstOrDefault(m => m.Name == methodName && m.GetParameters().Length == 1 && m.GetParameters()[ 0 ].ParameterType == typeof(T) && m.ReturnType == typeof(IQueryable<R>));

            // Invoke the method if found, otherwise return null
            if (method != null) {
                IQueryable<R>? queryable = (IQueryable<R>)method.Invoke(null, new object[] { entity });
                return queryable;
            }

            return null;
        }
    }

    /// <summary>
    /// The Extentions class is a generic class that provides extension methods for any class that implements the INexusEntity interface.
    /// It is used to generate the IQueryable&lt;T&gt; needed for fetching the related entities from the database.
    /// </summary>
    /// <example>
    /// This sample shows how to use the Extentions class to get an IQueryable of related entities.
    /// <code>
    /// Extentions&lt;Client&gt; clientExtentions = new Extentions&lt;Client&gt;();
    /// IQueryable&lt;Policy&gt; relatedPolicies = clientExtentions.getQueryable(clientID);
    /// </code>
    /// </example>
    /// <typeparam name="T">The type of the class that implements the INexusEntity interface.</typeparam>
    public class Extentions<T> where T : class, INexusEntity {
        protected Extentions<T> extentions = new();

        public static IQueryable<T> getQueryable(int ID) {
            return DatabaseManager.context.Set<T>().Where(x => x.Id == ID);
        }

        public static T get(int ID) {
            return DatabaseManager.context.Set<T>().Single(x => x.Id == ID);
        }
    }

    public sealed class ClaimExtention : Extentions<Claim> {
        public static IQueryable<Policy> getPolicyQueryable(Claim claim) {
            IQueryable<Policy> queryable = DatabaseManager.context.Claim
                .Where(c => c == claim)
                .Include(c => c.Policy)
                .Select(c => c.Policy);

            return queryable;
        }

        public static IQueryable<Policy> getPolicyQueryable(int claimID) {
            IQueryable<Policy> queryable = DatabaseManager.context.Claim
                .Where(c => c.Id == claimID)
                .Include(c => c.Policy)
                .Select(c => c.Policy);

            return queryable;
        }
    }

    public sealed class ClientExtention : Extentions<Client> {
        public static IQueryable<Policy> getPolicyQueryable(Client client) {
            IQueryable<Policy> queryable = DatabaseManager.context.Policy
                .Where(p => p.Client == client);
            return queryable;
        }


    }

    public sealed class PermissionExtention : Extentions<Permission> {
        public static IQueryable<Role> getRolesQueryable(Permission permission) {
            IQueryable<Role> queryable = DatabaseManager.context.Role
                .Include(r => r.RolePermission)
                .ThenInclude(rp => rp.Permission)
                .Where(r => r.RolePermission.Any(rp => rp.Permission == permission));

            return queryable;
        }

        public static IQueryable<User> getUsersQueryable(Permission permission) {
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
        public static IQueryable<User> getUsersQueryable(Role role) {
            IQueryable<User> queryable = DatabaseManager.context.User
                .Include(u => u.UserRole)
                .Where(rp => rp.UserRole.Any(ur => ur.Role == role));

            return queryable;
        }

        public static IQueryable<Permission> getPermissionsQueryable(Role role) {
            IQueryable<Permission> queryable = DatabaseManager.context.Permission
                .Include(r => r.RolePermission)
                .Where(rp => rp.RolePermission.Any(ur => ur.Role == role));

            return queryable;
        }
    }

    public sealed class UserExtention : Extentions<User> {
        public static IQueryable<Permission> getPermissionsQueryable(User user) {
            IQueryable<Permission> queryable = DatabaseManager.context.UserRole
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
            IQueryable<Role> queryable = DatabaseManager.context.Role
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
            User? user = DatabaseManager.context.User.FirstOrDefault(u =>
                u.Name == username &&
                u.Password == password
            );
            return user;
        }
    }

    public sealed class PolicyExtention : Extentions<Policy> {
        public static IQueryable<Client> getClientQueryable(Policy policy) {
            IQueryable<Client> queryable = DatabaseManager.context.Policy
                .Include(policy => policy.Client)
                .Where(p => p == policy)
                .Select(policy => policy.Client);

            return queryable;
        }

        public static IQueryable<Claim> getClaimsQueryable(Policy policy) {
            IQueryable<Claim> queryable = DatabaseManager.context.Claim
                .Where(c => c.Policy == policy);

            return queryable;
        }
    }
}
