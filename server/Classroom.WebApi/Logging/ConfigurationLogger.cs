using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Classroom.Repository;

namespace Classroom.WebApi.Logging
{
    public class ConfigurationLogger
    {
        private readonly ILogger<ConfigurationLogger> logger;

        public ConfigurationLogger(ILogger<ConfigurationLogger> logger)
        {
            this.logger = logger;
        }

        public void Log(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(ClassroomContext));
            logger.LogInformation($"Connection String: {connectionString}");
        }
    }
}