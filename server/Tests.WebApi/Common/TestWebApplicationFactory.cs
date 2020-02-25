using System;
using System.Net.Http;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Classroom.Repository;
using Classroom.Services;
using Classroom.WebApi.Delegate;
using DependencyInjection = Classroom.WebApi.DependencyInjection;

namespace Tests.WebApi.Common
{
    public class TestWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddEntityFrameworkProxies()
                    .BuildServiceProvider();

                // Setup in memory database context
                services.AddDbContext<ClassroomContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTestingDb");
                    options.UseInternalServiceProvider(serviceProvider);
                    options.UseLazyLoadingProxies();
                });

                services.AddScoped<IClassroomContext>(provider => provider.GetService<ClassroomContext>());
                services.AddScoped<ISystemService, SystemService>();

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ClassroomContext>();
                var logger = scopedServices.GetRequiredService<ILogger<TestWebApplicationFactory<TStartup>>>();
                var systemService = scopedServices.GetRequiredService<ISystemService>();

                context.Database.EnsureCreated();
                try
                {
                    systemService.SeedSampleData(CancellationToken.None).Wait();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred seeding the db: ${ex.Message}");
                }
            })
            .UseEnvironment("Test");
        }

        public HttpClient GetClient()
        {
            var client = CreateClient();
            return client;
        }
    }
}
