namespace QuakerZero{
    /// <summary>
    /// Middlware to handle CancellationTokenSource Example
    /// </summary>
    public class CancellationMiddleware : IMiddleware{
        private readonly RequestDelegate _next;
        public CancellationMiddleware(RequestDelegate next) { _next = next; }
        public async Task Invoke(ListenerContext context){
            // Init CancellationToken with a determined lifespan
            context.CancellationTokenSource = new CancellationTokenSource(Global.CancellationTimeout);
            await _next(context);
        }
    }
}
