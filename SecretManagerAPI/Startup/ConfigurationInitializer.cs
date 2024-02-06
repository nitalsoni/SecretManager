namespace SecretManagerAPI.Startup
{
    public static class ConfigurationInitializer
    {
        public static IHostBuilder ConfigureAppSettings(this IHostBuilder host)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var machine = Environment.MachineName;

            host.ConfigureAppConfiguration((ctx, builder) =>
            {
                builder.AddJsonFile("appsettings.json", false, true);
                builder.AddJsonFile($"appsettings.{env}.json", true, true);
                builder.AddJsonFile($"appsettings.{machine}.json", true, true);
                builder.AddEnvironmentVariables();
            });

            return host;
        }
    }
}
