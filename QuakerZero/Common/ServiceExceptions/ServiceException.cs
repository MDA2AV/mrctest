using System.Net;

namespace QuakerZero
{
    /// <summary>
    /// SericeException, must implement IServiceException to be handled by global error handler middleware properly.
    /// Also implements IAction result so that it can be returned easily by endpoint calls to be flushed.
    /// </summary>
    public abstract class ServiceException : Exception, IActionResult, IServiceException{
        public virtual HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
        public virtual object Value => "Unexpected error";
    }
}
