using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MRC.Application.Behaviors;
using System.Reflection;
using FluentValidation;

namespace MRC.Application.Extensions{
    public static class DependencyInjectionExtension{
        public static IServiceCollection AddApplication(this IServiceCollection services){
            // Registering MediatR for this assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjectionExtension).Assembly));
            // Registering validation pipeline
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            // Adding all validators..
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}