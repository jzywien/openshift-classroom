using System;
namespace Classroom.Repository.Common
{
    public class AuditableEntity
    {
        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
