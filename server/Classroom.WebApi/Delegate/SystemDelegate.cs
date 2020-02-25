using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Classroom.Common;
using Classroom.Services;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Delegate
{
    [Autowired]
    public class SystemDelegate : ISystemDelegate
    {
        private readonly IMapper mapper;
        private readonly ISystemService systemService;

        public SystemDelegate(IMapper mapper,
            ISystemService systemService)
        {
            this.mapper = mapper;
            this.systemService = systemService;
        }

        [ExcludeFromCodeCoverage]
        public async Task MigrateAsync()
        {
            await systemService.MigrateAsync();
        }

        public async Task SeedSampleData(CancellationToken cancellationToken)
        {
            await systemService.SeedSampleData(cancellationToken);
        }

        public async Task<SystemDetailsVm> GetSystemDetails()
        {
            var systemDetails = await systemService.GetSystemDetails();
            return mapper.Map<SystemDetailsVm>(systemDetails);
        }
    }
}
