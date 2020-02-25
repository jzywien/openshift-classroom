using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Classroom.WebApi;
using Classroom.WebApi.ViewModels;
using Tests.WebApi.Common;
using Xunit;

namespace Tests.WebApi.Controllers
{
    public class InstructorControllerTests : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly TestWebApplicationFactory<Startup> factory;

        public InstructorControllerTests(TestWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }
        
        [Fact]
        public async Task TestGetInstructorsReturnsSuccess()
        {
            var client = factory.GetClient();

            var response = await client.GetAsync("/api/instructors");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetInstructorReturnsSuccess()
        {
            var client = factory.GetClient();

            var response = await client.GetAsync("/api/instructors/1");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var instructor = JsonSerializer.Deserialize<InstructorDetailVm>(content, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            Assert.Equal("Peter Norvig", instructor.Name);
            Assert.Single(instructor.Courses);
        }      
        
        [Fact]
        public async Task TestGetUnknownInstructorReturnsNotFound()
        {
            var client = factory.GetClient();

            var response = await client.GetAsync("/api/instructors/99");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }          
    }
}