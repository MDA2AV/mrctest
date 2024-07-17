using MediatR;
using MRC.Application.RequestHandlers;
using OneOf;
using Quaker.Common;

namespace MRCTest.QuakerControllers{
    [ApiController]
    internal class QuakerAuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public QuakerAuthController(IMediator mediator){
            _mediator = mediator;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(string username, string password, CancellationToken cancellationToken)
        {
            OneOf<bool, Microsoft.AspNetCore.Mvc.ValidationProblemDetails> result =
                 await _mediator.Send(new LoginQuery(new LoginRequest("id12", "password")));

            return result.Match(OkResult => Ok(result.AsT0),
                                ErrorResult => BadRequest(extractProblemDetails(result.AsT1)));
        }

        protected string extractProblemDetails(Microsoft.AspNetCore.Mvc.ValidationProblemDetails details)
        {
            string ret = string.Empty;
            foreach (var error in details.Errors)
            {
                string[] messages = error.Value;
                foreach (var message in messages) { ret += $"{error.Key} -> {message}\n"; }
            }
            return ret;
        }
    }
}
