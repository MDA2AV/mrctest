using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MRC.Application.Services;
using OneOf;

namespace MRC.Application.RequestHandlers{
    public record LoginQuery(LoginRequest adaptedRequest) : IRequest<OneOf<bool, ValidationProblemDetails>>;
    public record LoginRequest(string customerNumber, string password);
    public class LoginHandler(IServiceScopeFactory serviceScopeFactory) 
        : IRequestHandler<LoginQuery, OneOf<bool, ValidationProblemDetails>>{
        public async Task<OneOf<bool, ValidationProblemDetails>> Handle(LoginQuery loginQuery, 
                                                                        CancellationToken cancellationToken){
            using(IServiceScope scope = serviceScopeFactory.CreateScope()){
                return await scope.ServiceProvider.GetRequiredService<ILoginService>()
                        .LoginAsync(loginQuery.adaptedRequest.customerNumber,
                               loginQuery.adaptedRequest.password);
            }
        }
    }
}
