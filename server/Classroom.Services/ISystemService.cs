using System.Threading;
using System.Threading.Tasks;
using Classroom.Services.Models;

namespace Classroom.Services
{
    public interface ISystemService
    {
        Task MigrateAsync();
        Task SeedSampleData(CancellationToken cancellationToken);
        Task<SystemDetails> GetSystemDetails();
    }
}
