using MRC.Application.Services;
using SimpleW;

namespace MRCTest.Controllers{
    internal sealed class AuthController : Controller{
        /*
        private readonly IMediator _mediator;
        public AuthController() =>
            _mediator = ActivatorUtilities
                          .GetServiceOrCreateInstance<IMediator>(Application.Current!.Handler.MauiContext!.Services);

        [SimpleW.Route("GET", "/login")]
        public object Login() {
            //Debug.WriteLine(Request.Body);
            OneOf<bool, ValidationProblemDetails> result = 
                 _mediator.Send(new LoginQuery(new LoginRequest("id12", "password"))).Result;

            return result.Match(OkResult => handleResult(result.AsT0),
                                ErrorResult => handleProblemDetails(result.AsT1));
        }*/

        /* SimpleW issues:
         *  - No async controllers
         *  - No middleware support
         *  - There is sort of a filters support but only to execute before request and not after
         *  - No support for a mediator, have to manually implement parameter validation login on controllers?
         *  - How to implement global error handling?
         *  - Return types are annoying to deal with
         *  - No inbuilt logger?
         *  
         *  Nevertheless, static page serving works fine
         */

        //http://localhost:8001/api/login?username=John&password=a$$word
        [Route("GET", "/login")] public object Login(string username, string password) => ActivatorUtilities
            .GetServiceOrCreateInstance<ILoginService>(Application.Current!.Handler.MauiContext!.Services)
            .Login(username, password);
    }
}
