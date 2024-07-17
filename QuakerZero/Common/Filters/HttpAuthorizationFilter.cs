using System.Security.Claims;

namespace QuakerZero{
    public class HttpAuthorizationFilter : IActionFilter{
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        Logger logger = Logger.GetInstance();
        //public HttpAuthorizationFilter(IJwtTokenGenerator jwtTokenGenerator)
        public HttpAuthorizationFilter(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public void OnActionExecuted(ListenerContext context, object[] parameters){ }

        /// <summary>
        /// Validate JWT
        /// </summary>
        /// <param name="context"></param>
        /// <param name="parameters"></param>
        public void OnActionExecuting(ListenerContext context, object[] parameters)
        {
            string _bearerToken = _jwtTokenGenerator.GetBearerToken(context.HttpListenerContext); //Get Bearer
            ClaimsPrincipal _principal = _jwtTokenGenerator.ValidateJwtToken(_bearerToken); //Validate
        }
    }
}
