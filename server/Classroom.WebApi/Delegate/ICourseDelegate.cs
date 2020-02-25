using System.Collections.Generic;
using System.Threading.Tasks;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Delegate
{
    public interface ICourseDelegate
    {
        Task<IEnumerable<CourseVm>> GetCourses();
        Task<CourseDetailVm> GetCourseDetail(int id);
        Task<CourseDetailVmV2> GetCourseDetailV2(int id);
    }
}
