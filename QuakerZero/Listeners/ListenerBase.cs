using Microsoft.Extensions.DependencyInjection;

namespace QuakerZero
{
    public delegate Task RequestDelegate(ListenerContext context);
    /// <summary>
    /// Base class for all Listeners
    /// </summary>
    public abstract class ListenerBase
    {
        protected Logger logger = Logger.GetInstance(); //Pull Logger
        /// <summary>
        /// Build Middleware pipeline using recursive algorithm
        /// </summary>
        /// <param name="middlewareTypes"></param>
        /// <param name="requestDelegate"></param>
        /// <returns>List of IMiddleware</returns>
        public List<IMiddleware> BuildMiddlewarePipepline(IList<Type> middlewareTypes, RequestDelegate requestDelegate)
        {
            List<IMiddleware> middlewares = new List<IMiddleware>();
            PipelineBuildRecursiveIterator(middlewareTypes, requestDelegate, middlewares, 1);
            middlewares.Reverse();
            return middlewares;
        }
        /// <summary>
        /// Recursive iteration
        /// </summary>
        /// <param name="middlewareTypes"></param>
        /// <param name="requestDelegate"></param>
        /// <param name="middlewares"></param>
        /// <param name="index"></param>
        private void PipelineBuildRecursiveIterator(IList<Type> middlewareTypes,
                                                    RequestDelegate requestDelegate,
                                                    List<IMiddleware> middlewares,
                                                    int index)
        {
            if (index >= middlewareTypes.Count + 1) return;

            IMiddleware currentMiddleware = (IMiddleware)ActivatorUtilities.CreateInstance(QBuilder.ServiceProvider,
                                                                            middlewareTypes[middlewareTypes.Count - index],
                                                                            new object[] { requestDelegate });

            middlewares.Add(currentMiddleware);

            logger.Screen("ListenerBase", $"Registered Middleware to pipeline: " +
                $"{middlewareTypes[middlewareTypes.Count - index].Name} -> {requestDelegate.Method.ReflectedType!.Name}");

            PipelineBuildRecursiveIterator(middlewareTypes, (RequestDelegate)currentMiddleware.Invoke, middlewares, index + 1);
        }
    }
}
