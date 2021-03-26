using System.Linq;
using CaWorkshop.Application.Common.Interfaces;
using CaWorkshop.Infrastructure.Persistence;
using CaWorkshop.WebUI.Filters;
using CaWorkshop.WebUI.Services;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace CaWorkshop.WebUI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebUIServices(this IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>()
                .AddSmtpHealthCheck(options =>
                {
                    options.Host = "localhost";
                    options.Port = 25;
                });

            services.AddHealthChecksUI()
                .AddInMemoryStorage();

            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddControllersWithViews(options =>
                options.Filters.Add(new ApiExceptionFilterAttribute()));

            services.AddRazorPages();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "CaWorkshop API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(),
                    new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Description = "Type into the textbox: Bearer {your JWT token}."
                    });

                configure.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            return services;
        }
    }
}
