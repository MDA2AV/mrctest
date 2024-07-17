using System.Security.Claims;

namespace QuakerZero
{
    /// <summary>
    /// Base class for client JWT authorization middleware, only usable with HttpListener Quaker
    /// For Socket Quaker, implmentation must be custom
    /// </summary>
    public abstract class JwtAuthorizationMiddlewareBase : IMiddleware
    {
        protected readonly RequestDelegate _next;
        protected readonly IJwtTokenGenerator _jwtTokenGenerator;
        protected string _bearerToken;
        protected ClaimsPrincipal _principal;
        public JwtAuthorizationMiddlewareBase(IJwtTokenGenerator jwtTokenGenerator, RequestDelegate next){
            _jwtTokenGenerator = jwtTokenGenerator;
            _next = next;
        }
        /// <summary>
        /// Validate JWT for non anonymous methods
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task Invoke(ListenerContext context){

            if (context.MethodNode.AllowAnonymous) { Logger.GetInstance().Screen("@JwtAuthorizationMiddlewareBase", "Anonymous, skipping authorization."); }
            else{

                _bearerToken = _jwtTokenGenerator.GetBearerToken(context.HttpListenerContext); //Get Bearer

                //If validation fails, exception is thrown, should be caught using global exception handler.
                //Otherwise, handle the exception on the child authorization middleware
                ClaimsPrincipal _principal = _jwtTokenGenerator.ValidateJwtToken(_bearerToken); //Validate
            }
            /* How to print all claims in the bearer token
             * foreach (ClaimsIdentity c in _principal.Identities){
                foreach (Claim claim in c.Claims){ await Console.Out.WriteLineAsync($"Claim: {claim}"); }
            }*/
            await _next(context);
        }
    }
}
