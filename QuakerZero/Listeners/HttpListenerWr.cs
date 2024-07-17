using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace QuakerZero
{
    public sealed class HttpListenerWr : ListenerBase, IListener
    {
        protected readonly IEvoker _evoker;
        public HttpListenerWr(IEvoker evoker)
        {
            listener = new HttpListener();
            _evoker = evoker;
            _evoker.PopulateEndpointsMetadata();
        }
        public HttpListener listener; //HttpListener instanced wrapped on

        public async Task Listen(IList<Type> middlewareTypes, IBuilderOptions options)
        {


            InitializeListener(options.Prefixes);
            List<IMiddleware> middlewares = InitializeMiddlewarePipeline(middlewareTypes);

            TaskFactory requestTaskFactory = new TaskFactory();
            while (listener.IsListening)
            {

                HttpListenerContext httpListenerContext = listener.GetContext();//Wait for request

                /*
                if (httpListenerContext.Request.Url.AbsolutePath.Contains('.'))
                {
                    ExecuteStaticFileRequest(httpListenerContext, options.wwwrootPath);
                    continue;
                }*/

                MethodNode? methodNode = null;
                try { methodNode = getMethodNode(httpListenerContext); }
                catch (Exception ex) { logger.Screen("HttpListenerWr/Listen", ex.Message); continue; }
                ListenerContext context = new ListenerContext
                {
                    HttpListenerContext = httpListenerContext,
                    CancellationTokenSource = new CancellationTokenSource(),
                    MethodNode = methodNode
                };
                //A new request arrived!
                //Task request = ExecuteRequestPipeline(middlewares, context); //Execute request
                requestTaskFactory.StartNew(async () => await middlewares[0].Invoke(context));
            }
        }

        /// <summary>
        /// Start the Listener
        /// </summary>
        public void StartListener() { listener.Start(); }
        /// <summary>
        /// Add prefixes to the Listener
        /// </summary>
        /// <param name="prefixes"></param>
        public void AddPrefixes(string[] prefixes)
        {
            foreach (string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
                logger.Screen("HttpListenerBase", $"added {prefix}");
            }
        }
        /// <summary>
        /// Add prefixes and start the listener
        /// </summary>
        /// <param name="prefixes"></param>
        public void InitializeListener(string[] prefixes)
        {
            AddPrefixes(prefixes);
            StartListener();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="middlewareTypes"></param>
        /// <returns>List of IMiddleware</returns>
        public List<IMiddleware> InitializeMiddlewarePipeline(IList<Type> middlewareTypes) =>
            BuildMiddlewarePipepline(middlewareTypes, Handle);
        /// <summary>
        /// Http request Handle
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Task</returns>
        public async Task Handle(ListenerContext context)
        {
            IActionResult result = await _evoker.Evoke(context); //Execute request
            HttpListenerResponse response = context.HttpListenerContext.Response; //Get request response context
            response.StatusCode = (int)result.StatusCode; //Get result status code

            //Flush result object after serializing it
            response.FlushBuffer(Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(result.Value)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpListenerContext"></param>
        /// <returns></returns>
        protected MethodNode getMethodNode(HttpListenerContext httpListenerContext)
        {
            HttpListenerRequest request = httpListenerContext.Request;
            string[] requestData = request.RawUrl.Split('?'); //Split URL route from parameters
            string[] route = requestData[0].Split('/').Skip(1).ToArray(); //Split routes
            string controllerRoute = route[0]; //Which controller
            string endpointRoute = route[1]; //Which endpoint
            return _evoker.GetMethodNode(controllerRoute, endpointRoute, request.HttpMethod); //Get MethodNode
        }
    }
}
