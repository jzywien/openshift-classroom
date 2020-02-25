using System.Collections.Generic;

namespace Classroom.Services.Models
{
    public class Instructor : Person
    {
        public int InstructorId { get; set; }
        public virtual IEnumerable<Course> Courses { get; set; }
    }
}
