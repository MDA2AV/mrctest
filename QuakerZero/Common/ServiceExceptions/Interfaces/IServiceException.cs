using System.Net;

namespace QuakerZero
{
    public interface IServiceException{
        HttpStatusCode StatusCode { get; }
        object Value { get; }
    }
}
