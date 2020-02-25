using System.Collections.Generic;

namespace Classroom.WebApi.ViewModels
{
    public class CreateStudentVm
    {
        public string Name { get; set; }
        public float Gpa { get; set; }
        public IEnumerable<int> CourseIds { get; set; }
    }
}
