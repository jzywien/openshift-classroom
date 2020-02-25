using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Classroom.WebApi.Attributes
{
    public class ProducesMediaTypeAttribute : ProducesAttribute, IActionConstraint
    {
        // The ConsumesContentTypeAttribute Order is set to 200 by the framework
        // Setting this one higher will ensure that if both Consumes and Produces are used
        // together, that they will be inclusive
        private const int ProducesActionConstraintOrder = 201;
        public ProducesMediaTypeAttribute(string contentType, params string[] additionalContentTypes) 
            : base(contentType, additionalContentTypes)
        {
            Order = ProducesActionConstraintOrder;
        }

        public bool Accept(ActionConstraintContext context)
        {
            var routeContext = context.RouteContext;
            var httpContext = routeContext.HttpContext;

            httpContext.Request.Headers.TryGetValue("Accept", out var contentType);

            return ContentTypes.Contains(contentType);
        }
    }
}