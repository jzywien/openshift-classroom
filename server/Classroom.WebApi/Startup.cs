using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Classroom.Repository;
using Classroom.Services;
using Classroom.WebApi.Logging;
using Classroom.WebApi.Middleware;

namespace Classroom.WebApi
{
    public class Startup
    {
        private const string SwaggerUrl = "/swagger/v1/swagger.json";
        private const string HealthCheckUrl = "/check/health";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggingConfig, LoggingConfig>();
            services.AddSingleton<ConfigurationLogger>();

            // Setup DI from other libraries DependencyInjection
            services.AddRepositories(Configuration);
            services.AddServices();
            services.AddDelegates();

            services.AddAutoMapper(
                typeof(DependencyInjection).Assembly,
                typeof(Services.DependencyInjection).Assembly
            );

            // Add health checks
            services.AddHealthChecks()
                .AddDbContextCheck<ClassroomContext>();

            services.AddControllers();

            // Ensure that all API responses are camelCase
            services.AddRouting(opts =>
            {
                opts.LowercaseUrls = true;
            });

            // Use Swagger to document the APIs being generated
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Classroom Api",
                    Description = "An example ASP.NET Core Web API"
                });
                c.EnableAnnotations();
                
                // TODO: Move to extension method and resolve swagger gen with Consumes & Produces, ResolveActionUsingAttribute
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.Last());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var services = app.ApplicationServices;

            var loggingConfig = services.GetService<ILoggingConfig>();
            loggingConfig.Register(Configuration);

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<ErrorLoggingMiddleware>();

            var configLogger = services.GetService<ConfigurationLogger>();
            configLogger.Log(Configuration);

            app.UseHealthChecks(HealthCheckUrl, new HealthCheckOptions()
            {
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                },
                ResponseWriter = WriteHealthCheckResponse
            });

            // Setup Swagger documentation. Can be disabled at the load balancer if needed
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerUrl, "Classroom Api v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport report)
        {
            var result = new
            {
                Status = report.Status.ToString(),
                Results = report.Entries.Select(pair => new
                {
                    Service = pair.Key,
                    Status = pair.Value.Status.ToString(),
                })
            };
            var response = JsonSerializer.Serialize(result);

            httpContext.Response.Headers.Add("Content-Type", MediaTypeNames.Application.Json);
            return httpContext.Response.WriteAsync(response);
        }
    }
}
