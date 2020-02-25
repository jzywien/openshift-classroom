using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Classroom.Repository
{
    public static class DependencyInjection
    {
        // Code coverage not needed as the Tests use the In memory db configuration and won't hit this code
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(ClassroomContext));

            services.AddDbContext<ClassroomContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IClassroomContext>(provider => provider.GetService<ClassroomContext>());

            return services;
        }
    }
}
