using System.Collections.Generic;

namespace Classroom.WebApi.ViewModels
{
    public class StudentDetailVm : StudentVm
    {
        public double GPA { get; set; }
        public IEnumerable<CourseVm> Courses { get; set; }
    }
}
