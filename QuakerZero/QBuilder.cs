using Microsoft.Extensions.DependencyInjection;

namespace QuakerZero
{
    public interface IBuilderOptions{
        string[] Prefixes { get; set; }
        string wwwrootPath { get; set; }
    }
    public class BuilderOptions : IBuilderOptions
    {
        public string Id { get; set; } = "DefaultId";
        public string[] Prefixes { get; set; } = null!; // example "http://localhost:5000/"
        public string IP { get; set; } = null!; // example "127.0.0.1"
        public int Port { get; set; } // example 5000
        public string wwwrootPath { get; set; } = null!;
    }
    public sealed class QBuilder
    {
        public static ServiceProvider ServiceProvider = null!;
        public ServiceCollection Services;
        public IBuilderOptions Options;
        public QBuilder(IBuilderOptions options)
        {
            Options = options;
            Services = new ServiceCollection();
        }
    }
    public static class QBuilderExtensions
    {
        public static IApp BuildHttpListenerApp(this QBuilder builder)
        {
            builder.Services.AddListener();
            QBuilder.ServiceProvider = builder.Services.BuildServiceProvider();
            return ActivatorUtilities.CreateInstance<HttpApp>(QBuilder.ServiceProvider, [builder.Options]);
        }
    }
    public interface IMiddleware
    {
        public Task Invoke(ListenerContext context);
    }
}
