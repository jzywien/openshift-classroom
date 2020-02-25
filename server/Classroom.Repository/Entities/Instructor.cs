using System.Collections.Generic;

namespace Classroom.Repository.Entities
{
    public class Instructor : Person
    {
        public int InstructorId { get; set; }
        public virtual ICollection<Course> Courses { get; private set; } = new HashSet<Course>();
    }
}
