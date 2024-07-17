namespace QuakerZero
{
    public class ApiController : Attribute
    {
        public ApiController() { }
    }
    /// <summary>
    /// ServiceFilter Attribute signals that the endpoint has an associated ActionFilter
    /// </summary>
    public class ServiceFilter : Attribute
    {
        public Type Type; //ActionFilter type
        public object[]? Parameters; //Input parameters for IActionFilter methods
        public ServiceFilter(Type type)
        {
            Type = type;
        }
        public ServiceFilter(Type type, object[] parameters)
        {
            Type = type;
            Parameters = parameters;
        }
    }
    public class HttpAttribute : Attribute
    {
        public string? Route { get; set; }
    }
    public class HttpGeneric : HttpAttribute
    {
        public HttpGeneric(string route)
        {
            Route = route;
        }
        public HttpGeneric()
        {
            Route = "default";
        }
    }
    public class HttpDelete : HttpAttribute
    {
        public HttpDelete(string route)
        {
            Route = route;
        }
        public HttpDelete()
        {
            Route = "default";
        }
    }
    public class HttpGet : HttpAttribute
    {
        public HttpGet(string route)
        {
            Route = route;
        }
        public HttpGet()
        {
            Route = "default";
        }
    }
    public class HttpPost : HttpAttribute
    {
        public HttpPost(string route)
        {
            Route = route;
        }
        public HttpPost()
        {
            Route = "default";
        }
    }
    public class HttpPut : HttpAttribute
    {
        public HttpPut(string route)
        {
            Route = route;
        }
        public HttpPut()
        {
            Route = "default";
        }
    }
    public class AllowAnonymous : Attribute
    {
    }
}
