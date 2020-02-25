using System.Threading.Tasks;
using Classroom.WebApi;
using Tests.WebApi.Common;
using Xunit;

namespace Tests.WebApi.Controllers
{
    public class HealthCheckControllerTests : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly TestWebApplicationFactory<Startup> factory;

        public HealthCheckControllerTests(TestWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task TestCanCheckHealth()
        {
            var client = factory.GetClient();

            var response = await client.GetAsync("/check/health");

            response.EnsureSuccessStatusCode();
        }
    }
}