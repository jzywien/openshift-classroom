using System.Collections.Generic;

namespace Classroom.Repository.Entities
{
    public class Student : Person
    {
        public int StudentId { get; set; }
        public double GradePointAverage { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; private set; } = new HashSet<StudentCourse>();
    }
}
