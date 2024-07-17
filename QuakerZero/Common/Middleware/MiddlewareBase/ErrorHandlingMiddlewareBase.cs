using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace QuakerZero
{
    /// <summary>
    /// Global error handling middleware base
    /// </summary>
    public abstract class ErrorHandlingMiddlewareBase : IMiddleware{
        protected readonly RequestDelegate _next;
        protected Logger logger;
        public ErrorHandlingMiddlewareBase(RequestDelegate next){ 
            _next = next;
            logger = Logger.GetInstance();
        }
        /// <summary>
        /// Invoke Middleware delegate
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task Invoke(ListenerContext context){
            try { await _next(context); }
            catch (Exception exception) 
                when (exception is IServiceException) {
                logger.Screen($"SocketErrorHandlingMiddleware", $"Service Exception: {exception.Message}");
                await HandleExpectedExceptionAsync(context, (IServiceException)exception); 
            }catch (Exception exception) {
                logger.Screen($"SocketErrorHandlingMiddleware", $"Service Exception: {exception.Message}");
                await HandleUnexpectedExceptionAsync(context, exception); }
        }
        /// <summary>
        /// Handle Endpoint ServiceException
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected Task HandleExpectedExceptionAsync(ListenerContext context, IServiceException exception){
            context.HttpListenerContext.Response.StatusCode = (int)exception.StatusCode;
            return context.HttpListenerContext.Response.FlushBufferAsync(Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(new StandardError{ Error = exception.Value.ToString() })));
        }
        /// <summary>
        /// Handle Endpoint unexpected exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected Task HandleUnexpectedExceptionAsync(ListenerContext context, Exception exception){
            HttpStatusCode code = HttpStatusCode.InternalServerError; //500
            context.HttpListenerContext.Response.StatusCode = (int)code;
            return context.HttpListenerContext.Response.FlushBufferAsync(Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(new { Error = exception.Message })));
        }
    }
}
