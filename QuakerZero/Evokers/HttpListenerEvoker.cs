using Newtonsoft.Json.Linq;
using System.Net;


namespace QuakerZero
{
    public class HttpListenerEvoker : HttpEvokerBase, IEvoker
    {
        public HttpListenerEvoker(IEnumerable<ControllerBase> controllers) : base(controllers) { }
        /// <summary>
        /// Evoke endpoint handle!
        /// </summary>
        /// <param name="context"></param>
        /// <returns>awaitable IActionResult</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IActionResult> Evoke(ListenerContext context)
        {

            string[] urlParameters; //Split Url parameters using '&' separator, if fails, returns an empy string[]
            JObject bodyObject; //Get request body as a JObject

            HttpListenerRequest request = context.HttpListenerContext.Request; //API request
            MethodNode methodNode = context.MethodNode; //Pre init
            string[] requestData = request.RawUrl.Split('?'); //Split URL route from parameters; //Pre init

            #region HTTP Endpoint Call
            switch (request.HttpMethod) //For each Http call, existing Filters are searched and executed
            {
                case "GET":
                    urlParameters = requestData.SplitUrlParameters();
                    OnActionExecutingFilters(context);
                    IActionResult GETResult = await GET(methodNode,
                                                        GetParameters(urlParameters),
                                                        context.CancellationTokenSource);
                    OnActionExecutedFilters(context);
                    return GETResult;
                case "POST":
                    bodyObject = request.GetBody();
                    OnActionExecutingFilters(context);
                    IActionResult POSTResult = await POST(methodNode,
                                                          bodyObject,
                                                          context.CancellationTokenSource);
                    OnActionExecutedFilters(context);
                    return POSTResult;
                case "PUT":
                    bodyObject = request.GetBody();
                    OnActionExecutingFilters(context);
                    IActionResult PUTResult = await PUT(methodNode,
                                                        bodyObject,
                                                        context.CancellationTokenSource);
                    OnActionExecutedFilters(context);
                    return PUTResult;
                case "DELETE":
                    urlParameters = requestData.SplitUrlParameters();
                    OnActionExecutingFilters(context);
                    IActionResult DELETEResult = await DELETE(methodNode,
                                                              GetParameters(urlParameters),
                                                              context.CancellationTokenSource);
                    OnActionExecutedFilters(context);
                    return DELETEResult;
                default: throw new NotImplementedException();
            }
            #endregion
        }
    }
}
