using Microsoft.AspNetCore.Mvc;

namespace Classroom.WebApi.Attributes
{
    // ConsumesAttribute already implements IActionConstraint. This class is used mainly for consistency
    // So that the base Produces and Consumes attributes are not used
    public class ConsumesMediaTypeAttribute : ConsumesAttribute
    {
        public ConsumesMediaTypeAttribute(string contentType, params string[] otherContentTypes) 
            : base(contentType, otherContentTypes)
        {
        }
    }
}