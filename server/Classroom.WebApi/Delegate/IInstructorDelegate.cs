using System.Collections.Generic;
using System.Threading.Tasks;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Delegate
{
    public interface IInstructorDelegate
    {
        Task<IEnumerable<InstructorVm>> GetInstructors();
        Task<InstructorDetailVm> GetInstructor(int id);
    }
}