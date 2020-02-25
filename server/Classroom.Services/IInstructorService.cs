using System.Collections.Generic;
using System.Threading.Tasks;
using Classroom.Services.Models;

namespace Classroom.Services
{
    public interface IInstructorService
    {
        Task<IEnumerable<Instructor>> GetInstructors();
        Task<Instructor> GetInstructor(int id);
    }
}