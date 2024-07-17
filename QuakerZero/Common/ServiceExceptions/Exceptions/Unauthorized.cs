using System.Net;

namespace QuakerZero
{
    public class Unauthorized : ServiceException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
        public override object Value => "";
    }
}
