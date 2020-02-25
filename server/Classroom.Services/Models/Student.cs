using System.Collections.Generic;

namespace Classroom.Services.Models
{
    public class Student : Person
    {
        public int StudentId { get; set; }
        public double GradePointAverage { get; set; }
        public virtual IEnumerable<Course> Courses { get; private set; } = new HashSet<Course>();
    }
}
