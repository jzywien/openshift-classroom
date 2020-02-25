using System.Threading.Tasks;
using Classroom.WebApi;
using Tests.WebApi.Common;
using Xunit;

namespace Tests.WebApi.Controllers
{
    public class SystemDetailsControllerTests : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly TestWebApplicationFactory<Startup> factory;

        public SystemDetailsControllerTests(TestWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }
        
        [Fact]
        public async Task TestCanGetSystemDetails()
        {
            var client = factory.GetClient();

            var response = await client.GetAsync("/api/systemdetails");

            response.EnsureSuccessStatusCode();
        }        
    }
}