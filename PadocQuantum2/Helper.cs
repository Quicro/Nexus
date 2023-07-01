using Microsoft.EntityFrameworkCore;
using PadocEF;
using PadocEF.Models;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace PadocQuantum2 {
    internal static class Helper {

        /// <summary> obj is IList </summary>
        public static bool isList(object obj) {
            return obj is IList;
        }

        public static bool isCollection(Type obj) {
            return obj.Name.ToString().StartsWith("ICollection");
        }

        public static object callMethod(object obj, string methodName) {
            object? v = obj.GetType().GetMethod(methodName).Invoke(obj, null);
            Type type = v.GetType();
            return v;
        }

        public static object callGenericMethod(object obj, string methodName, Type[] types, params object[] parameters) {
            MethodInfo v = obj.GetType().GetMethod(methodName);
            v.MakeGenericMethod(types);
            var aa = v.Invoke(obj, parameters);
            Type type = aa.GetType();
            return v;
        }
        public static object callStaticGenericMethod(Type callType, string methodName, Type[] types, params object[] parameters) {
            MethodInfo v = callType.GetMethod(methodName);
            v.MakeGenericMethod(types);
            var aa = v.Invoke(null, parameters);
            Type type = aa.GetType();
            return v;
        }

        /// <summary> typeof(T).IsAssignableFrom(obj.GetType()); </summary>
        public static IQueryable<IPadocEntity> getQuery(Type type) {
            System.Reflection.MethodInfo methodInfo = typeof(DbContext)
                                            .GetMethods()
                                            .Where(m => m.Name == "Set" && m.GetParameters().Length == 0)
                                            .First()
                                            .MakeGenericMethod(type);
            var query = methodInfo.Invoke(DatabaseManager.context, null) as IQueryable<IPadocEntity>;
            return query;
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
        /// Checks if the specified type is a subtype of IPadocEntity or represents a nullable ID (int or string).
        /// </summary>
        /// <param name="obj">The type to check.</param>
        /// <returns>
        /// <c>true</c> if the type is a subtype of IPadocEntity or represents a nullable ID (int or string);
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool isSubTypeOfEntity(Type obj) {
            bool assignableFromIPadocEntity = typeof(IPadocEntity).IsAssignableFrom(obj);
            bool isNullable = obj.IsGenericType &&
                                obj.GetGenericTypeDefinition() == typeof(Nullable<>);
            bool isGenericStringOrInt = isNullable && new[] { typeof(string), typeof(int) }.Contains(obj.GetGenericArguments()[0]);

            return assignableFromIPadocEntity || isGenericStringOrInt;
        }

        /// <summary> if (isList(obj)) obj.GetType().GetGenericArguments()[0]; </summary>
        public static Type getListType(object obj) {
            Type returnValue = null;

            if (isList(obj))
                returnValue = obj.GetType().GetGenericArguments()[0];

            return returnValue;
        }

        /// <summary> isSubTypeOfType<T>(getListType(obj)); </summary>
        public static bool isListOf<T>(object obj) {
            var returnValue = isSubTypeOfType<T>(getListType(obj));
            return returnValue;
        }

        /// <summary> ((IList)obj).Cast<T>().ToList(); </summary>
        public static List<T> asListOf<T>(object obj) {
            var returnValue = ((IList)obj).Cast<T>().ToList();
            return returnValue;
        }

        /// <summary> ((IList)obj).Cast<T>().ToArray(); </summary>
        public static T[] asArrayOf<T>(object obj) {
            var returnValue = ((IList)obj).Cast<T>().ToArray();
            return returnValue;
        }

        public static void outputList<T>(List<T> list, bool newLine = false, Func<T, object> function = null) {
            outputList(list.ToArray(), newLine, function);
        }

        public static void outputList<T>(T[] array, bool newLine = false, Func<T, object> function = null) {
            if (function == null)
                function = x => x.ToString();

            string text = "[";
            if (newLine)
                text = "\n[";
            if (newLine)
                text += "\n";
            for (int i = 0; i < array.Length; i++) {
                if (array[i].GetType() == typeof(string))
                    text += "\"";

                text += $"{function(array[i])}";

                if (array[i].GetType() == typeof(string))
                    text += "\"";
                if (i < array.Length - 1)
                    text += ", ";
                if (newLine)
                    text += "\n";
            }
            text += "]";
        }

        public static IList makeListOfVariableType(IEnumerable values, Type type) {
            var bbb = values.Cast<object>().Select(entity => Convert.ChangeType(entity, type));
            Type listType = typeof(List<>).MakeGenericType(type);
            IList list = (IList)Activator.CreateInstance(listType);

            foreach (var item in bbb) {
                list.Add(item);
            }

            return list;
        }

        public static string toDisplayString(object value) {
            if (value is IList)
                return getListType(value).Name + "[" + (value as IList).Count + "]";
            if (value is IPadocEntity)
                return (value as IPadocEntity).ToString();
            throw new Exception();
        }
    }
}
