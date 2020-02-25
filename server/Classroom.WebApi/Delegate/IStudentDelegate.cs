using System.Collections.Generic;
using System.Threading.Tasks;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Delegate
{
    public interface IStudentDelegate
    {
        Task<IEnumerable<StudentVm>> GetStudents();
        Task<StudentDetailVm> GetStudent(int id);
        Task<StudentDetailVm> CreateStudent(CreateStudentVm student);
        Task DeleteStudent(int id);
    }
}
