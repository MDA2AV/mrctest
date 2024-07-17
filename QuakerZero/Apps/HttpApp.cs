namespace QuakerZero
{
    /// <summary>
    /// Contaner for middleware registration
    /// </summary>
    public sealed class HttpApp : IApp
    {
        public IList<Type> MiddlewareTypes;
        private readonly IListener _listener;
        private readonly IBuilderOptions _options;
        public HttpApp(IListener listener, IBuilderOptions options)
        {
            _listener = listener;
            _options = options;
            MiddlewareTypes = new List<Type>();
        }
        public async Task Run() => await _listener.Listen(MiddlewareTypes, _options);
    }
}
