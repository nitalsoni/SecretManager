using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.OpenApi.Models;
using SecretManagerModels.Models;
using Serilog;
using System.Runtime.Intrinsics;

namespace SecretManagerAPI.Startup
{
    public static class ServiceInitializers
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer(); 
            services.AddApiVersioning((options) => {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer((options => {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            }));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { 
                    Version = "v1.0",
                    Title = "SecrentManagement",
                    Description = "SecrentManagement API for any UI",
                    Contact = new OpenApiContact { 
                        Name = "SecretManagementTeam",
                        Url = new Uri("http://www.google.com")
                    }
                });
            });

            return services;
        }

    }
}
