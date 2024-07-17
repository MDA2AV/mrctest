using Microsoft.Extensions.DependencyInjection;
using MRC.Application.Services;
using MRC.Infrastructure.Services;

namespace MRC.Infrastructure.Extensions{
    public static class DependencyInjectionExtension{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services){
            services.AddScoped<ILoginService, LoginService>();
            return services;
        }
    }
}
