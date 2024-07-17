using System.Reflection;

namespace QuakerZero
{
    /// <summary>
    /// Extensions for MethodInfo
    /// </summary>
    public static class MethodInfoExtensions{
        /// <summary>
        /// Returns the type of IBody parameter in the given methodInfo
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns>Type</returns>
        /// <exception cref="TypeNotFound"></exception>
        public static Type GetBodyParameterType(this MethodInfo methodInfo){
            ParameterInfo[] parameters = methodInfo.GetParameters();
            for (int i = 0; i < parameters.Length; i++){
                Type[] interfaces = parameters[i].ParameterType.GetInterfaces();
                if (interfaces.Contains(typeof(IBody)))
                    return parameters[i].ParameterType;
            }
            throw new TypeNotFound();
        }
        /// <summary>
        /// Check if the return type is Task or Task of T
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static bool IsAsyncMethod(this MethodInfo methodInfo){
            return typeof(Task).IsAssignableFrom(methodInfo.ReturnType)
                || (methodInfo.ReturnType.IsGenericType
                    && methodInfo.ReturnType.GetGenericTypeDefinition() == typeof(Task<>));
        }
    }
}
