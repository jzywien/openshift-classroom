using System.Collections.Generic;
using System.Threading.Tasks;
using Classroom.Services.Models;

namespace Classroom.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Task<Student> CreateStudent(Student student);
        Task DeleteStudent(int id);
    }
}
