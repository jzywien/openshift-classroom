using Microsoft.Extensions.Configuration;

namespace Classroom.WebApi.Logging
{
    public interface ILoggingConfig
    {
        void Register(IConfiguration config);
    }
}
