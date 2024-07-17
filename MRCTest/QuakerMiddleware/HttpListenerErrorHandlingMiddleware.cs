using Quaker.Common;
using Quaker.Listeners;
using Quaker.Middleware;

namespace MRCTest.QuakerMiddleware
{
    internal class HttpListenerErrorHandlingMiddleware : ErrorHandlingMiddlewareBase
    {
        public HttpListenerErrorHandlingMiddleware(RequestDelegate next) : base(next) { }
        public override async Task Invoke(ListenerContext context)
        {
            //Do something
            await base.Invoke(context);
        }
    }
}
