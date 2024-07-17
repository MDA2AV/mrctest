using System.Net;

namespace QuakerZero
{
    public interface IActionResult
    {
        HttpStatusCode StatusCode { get; }
        object Value { get; }
    }
    public class ObjectResult : IActionResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public object Value { get; set; }
        public ObjectResult(object value, HttpStatusCode statusCode)
        {
            Value = value;
            StatusCode = statusCode;
        }
    }
}
