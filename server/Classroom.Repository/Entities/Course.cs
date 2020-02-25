using System.Collections.Generic;
using Classroom.Repository.Common;

namespace Classroom.Repository.Entities
{
    public class Course : AuditableEntity
    {
        public int CourseId { get; set; }
        public string Title { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
    }
}
