using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Classroom.Common;
using Classroom.Repository;
using Classroom.Services.Models;

namespace Classroom.Services
{
    [Autowired]
    public class SystemService : ISystemService
    {
        private readonly IClassroomContext classroomContext;
        private readonly IHostEnvironment environment;

        public SystemService(IHostEnvironment environment, 
            IClassroomContext classroomContext)
        {
            this.environment = environment;
            this.classroomContext = classroomContext;
        }

        [ExcludeFromCodeCoverage]
        public async Task MigrateAsync()
        {
            await classroomContext.MigrateAsync();
        }

        public async Task SeedSampleData(CancellationToken cancellationToken)
        {
            var seeder = new ClassroomSeeder(classroomContext);
            await seeder.SeedAllAsync(cancellationToken);
        }

        public Task<SystemDetails> GetSystemDetails()
        {
            var serverInfo = new SystemDetails
            {
                EnvironmentName = environment.EnvironmentName,
                MachineName = Environment.MachineName,
                OperatingSystem = new OperatingSystemDetails
                {
                    Version = Environment.OSVersion.VersionString,
                    Platform = Environment.OSVersion.Platform.ToString()
                }
            };
            return Task.FromResult(serverInfo);
        }
    }
}
