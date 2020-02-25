using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Classroom.Common.Extensions;

namespace Classroom.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDelegates(this IServiceCollection services)
        {
            services.AddAutoWired(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
