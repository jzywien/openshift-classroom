using System.Threading;
using System.Threading.Tasks;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Delegate
{
    public interface ISystemDelegate
    {
        Task MigrateAsync();
        Task SeedSampleData(CancellationToken cancellationToken);
        Task<SystemDetailsVm> GetSystemDetails();
    }
}
