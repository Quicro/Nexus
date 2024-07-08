using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NexusEF.DatabaseManager;
using NexusEF.Models.Nexus;
using NexusEF.Models.Nexus.Extention;
using System.Reflection;

namespace NexusEF.Extentions {

    /// <summary>
    /// The Extentions class provides a set of static methods for querying entities in the database.
    /// These methods are used to retrieve related entities based on the type of the entity and the method name.
    /// </summary>
    public class DatabaseManagerExtention<T> where T : IDatabaseManager {
        protected static readonly Dictionary<Tuple<Type, Type>, Type> mappingMoreMoreRelations = new();
        public DbContext context { get; private set; }

        public DatabaseManagerExtention() {
            Type DatabaseManagerContextGenericType = typeof(T).GetTypeInfo().GenericTypeArguments[ 0 ];


            Type DatabaseManagerContextType = typeof(ContextExtention<>).MakeGenericType(DatabaseManagerContextGenericType);

            ContextExtention<T> contextExtention = (ContextExtention<T>)Activator.CreateInstance(DatabaseManagerContextType);
            context = contextExtention.databasemanager.context;
        }

    }

    public class ContextExtention<T> where T : IDatabaseManager {

        public T databasemanager { get; set; }
        protected static readonly Dictionary<Tuple<Type, Type>, Type> mappingMoreMoreRelations = new();

        public static Type getPossibleMoreMoreRelationType(Type entityType, Type referenceType) {
            return mappingMoreMoreRelations
                .Where(tuple => tuple.Key.Item1 == entityType && tuple.Key.Item2 == referenceType)
                .Select(tuple => tuple.Value)
                .FirstOrDefault();
        }

        protected static IQueryable<INexusEntity> ConstructAndInvokeGenericQueryable(INexusEntity entity, Type referenceType, string? methodName = null) {
            Type entityType = entity.GetType();

            // Construct the generic getQueryable<T, R> method based on the entity and reference types
            MethodInfo genericMethod = typeof(NexusExtentions).GetMethod(nameof(getQueryable))
                .MakeGenericMethod(entityType, referenceType);

            // Invoke the constructed generic method with the entity and methodName parameters
            IQueryable<INexusEntity> result = (IQueryable<INexusEntity>)genericMethod.Invoke(null, new object[] { entity, methodName });

            return result;
        }

        protected static IQueryable<INexusEntity> ConstructAndInvokeRelatedGenericQueryable(INexusEntity entity, Type referenceType, string? methodName = null) {
            Type entityType = entity.GetType();

            // Construct the generic getQueryable<T, R> method based on the entity and reference types
            MethodInfo genericMethod = typeof(NexusExtentions).GetMethod(nameof(getRelatedQueryable))
                .MakeGenericMethod(entityType, referenceType);

            // Invoke the constructed generic method with the entity and methodName parameters
            IQueryable<INexusEntity> result = (IQueryable<INexusEntity>)genericMethod.Invoke(null, new object[] { entity, methodName });

            return result;
        }

        [Obsolete]
        public static IQueryable<R> getQueryableByID<R, T>(DbContext context, INexusEntity entity, int ID)

            where R : class, INexusEntity
            where T : class, INexusEntity {

            DbSet<T> set = context.Set<T>();
            IQueryable<R> a = ConstructAndInvokeGenericQueryable(entity, typeof(R)).Where(e => e.Id == ID).Cast<R>();
            return a;

        }

        [Obsolete]
        public static IQueryable<T> getRelatedQueryableByID<R, T>(DbContext context, INexusEntity entity)

            where R : class, INexusEntity
            where T : class, INexusEntity {
            context.Set<T>();
            IQueryable<T> a = ConstructAndInvokeRelatedGenericQueryable(entity, typeof(T)).Cast<T>();
            _ = a.ToQueryString();
            return a;


        }

        public static IQueryable<R> getQueryable<T, R>(DbContext context,T entity, string? methodName = null)
            where T : class, INexusEntity
            where R : class, INexusEntity {
            Type entityType = typeof(T);
            Type extensionType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.IsSubclassOf(typeof(ClassExtentions<,>).MakeGenericType(context.GetType() ,entityType)));

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

        public static IQueryable<R> getRelatedQueryable<T, R>(DbContext context,T entity, string? methodName = null)
            where T : class, INexusEntity
            where R : class, INexusEntity {
            Type entityType = typeof(T);
            Type extensionType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.IsSubclassOf(typeof(ClassExtentions<,>).MakeGenericType(context.GetType(),entityType)));

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
}
