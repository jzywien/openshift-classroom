using System.Collections.Generic;

namespace Classroom.WebApi.ViewModels
{
    public class CourseDetailVm : CourseVm
    {
        public IEnumerable<StudentVm> Students { get; set; }
    }
}
