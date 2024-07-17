using System.Reflection;

namespace QuakerZero
{
    /// <summary>
    /// Extensions for Type
    /// </summary>
    public static class TypeExtensions{
        /// <summary>
        /// Get all public methods in this Type which have T attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns>MethodInfo[]</returns>
        public static MethodInfo[] GetPublicMethods<T>(this Type type){
            //Get all methods in controller which match the search criteria:
            //  -   Public instance member
            //  -   Has T Attribute
            MethodInfo[] publicMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                             .Where(method => Attribute.IsDefined(method, typeof(T)))
                                             .ToArray();
            return publicMethods;
        }
    }
}
