using System.Net;

namespace QuakerZero
{
    public abstract class ControllerBase
    {

        /// <summary>
        /// Collection of defined ObjectResults
        /// </summary>
        #region ObjectResults
        protected ObjectResult Ok(object value) => 
            new ObjectResult(value, HttpStatusCode.OK);
        protected ObjectResult Ok() => 
            new ObjectResult("", HttpStatusCode.OK); 
        protected ObjectResult NotFound(object value) => 
            new ObjectResult(value, HttpStatusCode.NotFound); 
        protected ObjectResult NotFound() => 
            new ObjectResult("", HttpStatusCode.NotFound);
        protected ObjectResult BadRequest(object value) => 
            new ObjectResult(value, HttpStatusCode.BadRequest);
        protected ObjectResult BadRequest() => 
            new ObjectResult("", HttpStatusCode.BadRequest);
        protected ObjectResult FailedDependency(object value) => 
            new ObjectResult(value, HttpStatusCode.FailedDependency);
        protected ObjectResult FailedDependency() => 
            new ObjectResult("", HttpStatusCode.FailedDependency);
        protected ObjectResult Forbidden(object value) => 
            new ObjectResult(value, HttpStatusCode.Forbidden);
        protected ObjectResult Forbidden() =>
            new ObjectResult("", HttpStatusCode.Forbidden);
        protected ObjectResult Unauthorized(object value) => 
            new ObjectResult(value, HttpStatusCode.Unauthorized);
        protected ObjectResult Unauthorized() => 
            new ObjectResult("", HttpStatusCode.Unauthorized);
        #endregion
    }
}
