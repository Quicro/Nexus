using Microsoft.EntityFrameworkCore;
using NexusEF.Attributes;
using NexusEF.DatabaseManager;
using NexusEF.Extentions;
using NexusEF.Models.Nexus;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace NexusEF {
    public class Helper {
        private readonly DbContext context;
        public Helper(IDatabaseManager databaseManager) {
            context = databaseManager.context;
        }

        /// <summary> obj is IList </summary>
        public bool isList(object obj) {
            return obj is IList;
        }

        public bool isCollection(Type obj) {
            return obj.Name.ToString().StartsWith("ICollection");
        }

        public static object? callMethod(object obj, string methodName, params object[] parameters) {
            MethodInfo method = obj.GetType().GetMethod(methodName);
            _ = method.GetType();
            object? result = method.Invoke(obj, parameters);
            return result;
        }

        public static object? callGenericMethod(object obj, string methodName, Type[] types, params object[] parameters) {
            MethodInfo method = obj.GetType().GetMethod(methodName);
            MethodInfo genericMethod = method.MakeGenericMethod(types);
            object? result = genericMethod.Invoke(obj, parameters);
            return result;
        }
        public object callStaticGenericMethod(Type callType, string methodName, Type[] types, params object[] parameters) {
            MethodInfo v = callType.GetMethod(methodName);
            MethodInfo genericMethod = v.MakeGenericMethod(types);
            object? aa = genericMethod.Invoke(null, parameters);
            _ = aa.GetType();
            return aa;
        }

        /// <summary> typeof(T).IsAssignableFrom(obj.GetType()); </summary>
        public IQueryable<INexusEntity>? getQuery(Type type) {
            _ = type.GetCustomAttribute<NexusAttribute>() is not null;
            /*DbContext context = isNexusType ? NexusDatabaseManager.context : AppDatabaseManager.context;
            MethodInfo methodInfo = typeof(DbContext)
                 .GetMethods()
                 .Where(m => m.Name == "Set" && m.GetParameters().Length == 0)
                 .First()
                 .MakeGenericMethod(type);
            IQueryable<INexusEntity>? query = methodInfo.Invoke(context, null) as IQueryable<INexusEntity>;
            return query;*/

            return null;



        }

        /// <summary> typeof(T).IsAssignableFrom(obj.GetType()); </summary>
        public static bool isSubOf<T>(object obj) {
            bool returnValue = typeof(T).IsAssignableFrom(obj.GetType());
            return returnValue;
        }

        /// <summary> typeof(T).IsAssignableFrom(type); </summary>
        public static bool isSubTypeOfType<T>(Type type) {
            bool returnValue = typeof(T).IsAssignableFrom(type);
            return returnValue;
        }

        /// <summary>
        /// Checks if the specified type is a subtype of INexusEntity or represents a nullable ID (int or string).
        /// </summary>
        /// <param name="obj">The type to check.</param>
        /// <returns>
        /// <c>true</c> if the type is a subtype of INexusEntity or represents a nullable ID (int or string);
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool isSubTypeOfEntity(Type obj) {
            bool assignableFromINexusEntity = typeof(INexusEntity).IsAssignableFrom(obj);
            bool isNullable = obj.IsGenericType &&
                                obj.GetGenericTypeDefinition() == typeof(Nullable<>);
            bool isGenericStringOrInt = isNullable && new[] { typeof(string), typeof(int) }.Contains(obj.GetGenericArguments()[ 0 ]);

            return assignableFromINexusEntity || isGenericStringOrInt;
        }

        /// <summary> if (isList(obj)) obj.GetType().GetGenericArguments()[0]; </summary>
        public Type getListType(object obj) {
            Type returnValue = null;

            if (isList(obj)) {
                returnValue = obj.GetType().GetGenericArguments()[ 0 ];
            }

            return returnValue;
        }

        /// <summary> isSubTypeOfType<T>(getListType(obj)); </summary>
        public bool isListOf<T>(object obj) {
            bool returnValue = isSubTypeOfType<T>(getListType(obj));
            return returnValue;
        }

        /// <summary> ((IList)obj).Cast<T>().ToList(); </summary>
        public static List<T> asListOf<T>(object obj) {
            List<T> returnValue = ( (IList)obj ).Cast<T>().ToList();
            return returnValue;
        }

        /// <summary> ((IList)obj).Cast<T>().ToArray(); </summary>
        public static T[] asArrayOf<T>(object obj) {
            T[] returnValue = ( (IList)obj ).Cast<T>().ToArray();
            return returnValue;
        }

        public static void outputList<T>(List<T> list, bool newLine = false, Func<T, object>? function = null) {
            outputList(list.ToArray(), newLine, function);
        }

        public static void outputList<T>(T[] array, bool newLine = false, Func<T, object>? function = null) {
            function ??= x => x.ToString();

            string text = "[";
            if (newLine) {
                text = "\n[";
            }

            if (newLine) {
                text += "\n";
            }

            for (int i = 0; i < array.Length; i++) {
                if (array[ i ].GetType() == typeof(string)) {
                    text += "\"";
                }

                text += $"{function(array[ i ])}";

                if (array[ i ].GetType() == typeof(string)) {
                    text += "\"";
                }

                if (i < array.Length - 1) {
                    text += ", ";
                }

                if (newLine) {
                    text += "\n";
                }
            }
            text += "]";
        }

        public static IList makeListOfVariableType(IEnumerable values, Type type) {
            IEnumerable<object> bbb = values.Cast<object>().Select(entity => Convert.ChangeType(entity, type));
            Type listType = typeof(List<>).MakeGenericType(type);
            IList list = (IList)Activator.CreateInstance(listType);

            foreach (object? item in bbb) {
                _ = list.Add(item);
            }

            return list;
        }

        public PacketRelationshipType getPacketRelationshipType(bool list, bool subTypeOfEntity, bool listOf) {
            if (list) {
                if (listOf) {
                    return PacketRelationshipType.Array;
                } else {
                    Debugger.Break();
                    throw new NotImplementedException("not possible in EF");
                }
            } else {
                return subTypeOfEntity ? PacketRelationshipType.Single : PacketRelationshipType.Dummy;
            }
        }

        public Type? getPossibleMoreMoreRelationType(Type type, Type referenceType) {
            throw new NotImplementedException();
        }
    }

    public class GenericHelper : Helper {

        public GenericHelper(Type type) {
            Helper<> genericHelper = (Helper)Activator.CreateInstance(type);
        }
    }

    public class Helper<T> : Helper where T : IDatabaseManager {
        private readonly T databaseManager;

        public Helper(T databaseManager) : base(databaseManager) {
            this.databaseManager = databaseManager;
        }
    }
}
