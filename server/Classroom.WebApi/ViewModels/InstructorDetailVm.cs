using System.Collections.Generic;

namespace Classroom.WebApi.ViewModels
{
    public class InstructorDetailVm : InstructorVm
    {
        public IEnumerable<CourseVm> Courses { get; set; }
    }
}
