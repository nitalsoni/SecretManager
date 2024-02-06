using Serilog;

namespace SecretManagerAPI.Startup
{
    public static class MiddlewareInitializer
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            //custom middlewares
            app.ConfigureSwagger();
            
            //microsoft middlewares
            app.UseAuthorization();
            app.MapControllers();
            app.UseSerilogRequestLogging();
            return app;
        }
    }
}
