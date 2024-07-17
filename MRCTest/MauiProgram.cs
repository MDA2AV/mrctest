using Microsoft.Extensions.Logging;
using MRC.Application.Extensions;
using MRC.Infrastructure.Extensions;
using MRCTest.QuakerMiddleware;
using MRCTest.ViewModels;
using NetCoreServer;
using Quaker.Applications;
using Quaker.Authorization;
using Quaker.Builders;
using Quaker.Common;
using Quaker.Middleware;
using SimpleW;
using System.Net;
using System.Reflection;

namespace MRCTest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            //Register MRC.Application services
            builder.Services.AddApplication();
            //Register MRC.Infrastructure services
            builder.Services.AddInfrastructure();

            //Initialize SimpleW server
            initSimpleWServer();
            initHttpListenerQuaker();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }

        private static void initSimpleWServer(){
            SimpleWServer server = new SimpleWServer(IPAddress.Any, 8001);
            server.AddStaticContent(@"/data/user/0/com.companyname.mrctest/files/", "/");
            server.AddDynamicContent("/api");
            server.AutoIndex = true;
            server.Start();
        }

        private static void initHttpListenerQuaker()
        {
            //Builder
            QuakerBuilder builder = new QuakerBuilder(new BuilderOptions
            {
                Id = "QuakerHttpListener",
                BuildType = BuildType.HttpQuaker,
                Prefixes = new string[] { "http://localhost:8002/" },
                //wwwrootPath = AppContext.BaseDirectory + "/"
                //wwwrootPath = "/data/data/com.companyname.quakermauiexample/files/"
                wwwrootPath = "C:\\Users\\MDA2AV\\source\\Quaker\\src\\QuakerMAUIExample\\QuakerMAUIExample\\Resources\\Raw\\"
            });

            //Logger Type, using console (console application), other options are Debug and custom
            builder.SetLogger(ScreenOption.Debug);
            //Adding this assembly controllers
            builder.Services.AddControllers(Assembly.GetExecutingAssembly());
            //Adding Core service implementations
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();
            //Adding authorization service with default settings
            builder.Services.AddAuth(new JwtSettings
            {
                //default values
            });

            // Build
            IQuakerApp app = builder.Build();
            {
                app.UseMiddleware<HttpListenerErrorHandlingMiddleware>(); //Use global error handler
                //Run
                app.Run();
            }
        }
    }
}
