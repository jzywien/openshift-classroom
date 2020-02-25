using System;
using Microsoft.AspNetCore.Http;

namespace Classroom.WebApi.Extensions
{
    public static class HttpRequestExtensions
    {
        public const string CORRELATION_ID_HEADER = "correlationId";

        public static string GetCorrelationId(this HttpRequest httpRequest)
        {
            if (string.IsNullOrEmpty(httpRequest.Headers[CORRELATION_ID_HEADER]))
                httpRequest.Headers[CORRELATION_ID_HEADER] = Guid.NewGuid().ToString();


            return httpRequest.Headers[CORRELATION_ID_HEADER];
        }
    }
}
