using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace SecretManagerLogger
{
    public static class SMLogger
    {
        public static void ConfigureLogger(this IHostBuilder host, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();

            host.UseSerilog(((ctx, lc) => lc
            .ReadFrom.Configuration(ctx.Configuration)));
        }
    }
}