using System.Collections.Generic;
using System.Threading.Tasks;
using Classroom.Services.Models;

namespace Classroom.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCourses();
        Task<Course> GetCourse(int id);
    }
}
