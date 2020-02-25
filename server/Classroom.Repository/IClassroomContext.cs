using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Classroom.Repository.Entities;

namespace Classroom.Repository
{
    public interface IClassroomContext
    {
        DbSet<Course> Courses { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Instructor> Instructors { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task MigrateAsync();
    }
}
