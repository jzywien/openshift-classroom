using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Classroom.Common.Helpers;
using Classroom.WebApi;
using Classroom.WebApi.ViewModels;
using Tests.WebApi.Common;
using Xunit;

namespace Tests.WebApi.Controllers
{
    public class CoursesControllerTests : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly TestWebApplicationFactory<Startup> factory;

        public CoursesControllerTests(TestWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task TestGetCoursesReturnsSuccess()
        {
            var client = factory.GetClient();

            var response = await client.GetAsync("/api/courses");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetCourseReturnsSuccess()
        {
            var client = factory.GetClient();

            var response = await client.GetAsync("/api/courses/1");
            response.EnsureSuccessStatusCode();
            
            var course = await JsonHelper.DeserializeResponseToJson<CourseDetailVm>(response);

            Assert.Equal("Peter Norvig", course.Instructor.Name);
            Assert.Equal(6, course.Students.Count());
        }
        
        [Fact]
        public async Task TestGetCourseV2ReturnsSuccess()
        {
            var client = factory.GetClient();

            var mediaTypeAccept = new MediaTypeWithQualityHeaderValue("application/vnd.Classroom.course.v2+json"); 
            client.DefaultRequestHeaders.Accept.Add(mediaTypeAccept);

            var response = await client.GetAsync("/api/courses/1");
            response.EnsureSuccessStatusCode();

            var course = await JsonHelper.DeserializeResponseToJson<CourseDetailVmV2>(response);

            Assert.Equal("Peter Norvig", course.Course.Instructor.Name);
            Assert.Equal(6, course.Course.Students.Count());
            Assert.Equal(2, course.Version);
        }        

        [Fact]
        public async Task TestGetCourseWithInvalidIdReturnsError()
        {
            var client = factory.GetClient();

            using var response = await client.GetAsync("/api/courses/99");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
