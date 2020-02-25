using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Classroom.Common.Extensions;

namespace Classroom.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddAutoWired(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
