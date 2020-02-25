using System.Collections.Generic;

namespace Classroom.Services.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Title { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual IEnumerable<Student> Students { get; private set; } = new HashSet<Student>();
    }
}
