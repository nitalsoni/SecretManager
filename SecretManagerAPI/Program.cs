using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SecretManagerAPI.Models;
using SecretManagerAPI.Startup;
using SecretManagerModels.Models;
using Serilog;

namespace SecretManagerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.ConfigureAppSettings();

            //Read config file
            var apiSettings = builder.Configuration.GetRequiredSection("AppConfig").Get<APISettings>();
            builder.Services.AddSingleton<IAPISettings>(apiSettings);

            //var dbConnections = apiSettings?.DBConnections;
            //Register Services.
            builder.Services.RegisterServices().
            AddDbContext<SecretManagerDBContext>(options => options.UseSqlServer(apiSettings.SandboxDB));

            //Configure Serilog
            Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
            builder.Host.UseSerilog(((ctx, lc) => lc
            .ReadFrom.Configuration(ctx.Configuration)));

            var app = builder.Build();

            //Configure middleware
            app.ConfigureMiddleware();

            //Run the app
            app.Run();
        }

    }
}