namespace QuakerZero
{
    public static class IAppExtensions
    {
        /// <summary>
        /// Add Middleware to the Middleware Pipeline
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        public static void UseMiddleware<T>(this IApp app)
        {
            if (typeof(IMiddleware).IsAssignableFrom(typeof(T)))
            {
                // T implements IMiddleware
                Logger.GetInstance().Screen("Application", $"Adding {typeof(T).Name} to middleware types..");
                app.MiddlewareTypes.Add(typeof(T));
            }
        }
    }
}
