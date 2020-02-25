using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Classroom.WebApi.Extensions;

namespace Classroom.WebApi.Middleware
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorLoggingMiddleware> logger;

        public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var message = $"CorrelationId: {context.Request.GetCorrelationId()}, ";
                logger.LogError(default, ex, message);
                throw;
            }
        }
    }
}
