using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;

namespace Classroom.WebApi.Logging
{
    public class LoggingConfig : ILoggingConfig
    {
        public void Register(IConfiguration config)
        {
            var nLogSection = config.GetSection("NLog");
            var loggingConfig = new NLogLoggingConfiguration(nLogSection);
            LogManager.Configuration = loggingConfig;
        }
    }
}