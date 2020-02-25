using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Classroom.Common.Helpers;
using Classroom.WebApi;
using Classroom.WebApi.ViewModels;
using Tests.WebApi.Common;
using Xunit;

namespace Tests.WebApi.Controllers
{
    public class StudentsControllerTests : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly TestWebApplicationFactory<Startup> factory;
        
        private static IFixture CreateFixture()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization
            {
                ConfigureMembers = true
            });
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return fixture;
        }

        public StudentsControllerTests(TestWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task TestGetStudentsReturnsSuccess()
        {
            var client = factory.GetClient();

            var response = await client.GetAsync("/api/students");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetStudentReturnsSuccess()
        {
            var client = factory.GetClient();

            var response = await client.GetAsync("/api/students/1");

            response.EnsureSuccessStatusCode();
            
            var student = await JsonHelper.DeserializeResponseToJson<StudentDetailVm>(response);
            Assert.Equal("Peter Griffin", student.Name);
        }
        
        [Fact]
        public async Task TestGetStudentReturnsSuccessV2()
        {
            var client = factory.GetClient();
            
            var mediaTypeAccept = new MediaTypeWithQualityHeaderValue("application/vnd.Classroom.student.v2+json"); 
            client.DefaultRequestHeaders.Accept.Add(mediaTypeAccept);

            var response = await client.GetAsync("/api/students/1");

            response.EnsureSuccessStatusCode();
            var student = await JsonHelper.DeserializeResponseToJson<StudentDetailVmV2>(response);
            Assert.Equal("Peter Griffin", student.Student.Name);
        }        

        [Fact]
        public async Task TestGetStudentWithInvalidIdReturnsError()
        {
            var client = factory.GetClient();

            var response = await client.GetAsync("/api/students/99");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task TestCanDeleteStudent()
        {
            var client = factory.GetClient();

            var response = await client.DeleteAsync("/api/students/2");

            response.EnsureSuccessStatusCode();
        }
        
        [Fact]
        public async Task TestDeletedStudentReturnsNotFound()
        {
            var client = factory.GetClient();

            var response = await client.GetAsync("/api/students/2");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [Fact]
        public async Task TestCanCreateStudent()
        {
            var fixture = CreateFixture();
            var studentRequest = fixture.Create<CreateStudentVm>();
            var request = JsonHelper.SerializeToJson(studentRequest);
            
            var client = factory.GetClient();

            var response = await client.PostAsync("/api/students", request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
            var student = await JsonHelper.DeserializeResponseToJson<StudentDetailVm>(response);
            Assert.Equal(studentRequest.Name, student.Name);
        }
        
        [Fact]
        public async Task TestCanCreateStudentV2Request()
        {
            var fixture = CreateFixture();
            var studentRequest = fixture.Create<CreateStudentVmV2>();
            var request = JsonHelper.SerializeToJson(studentRequest);
            request.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.Classroom.student.v2+json");
            
            var client = factory.GetClient();
            var response = await client.PostAsync("/api/students", request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
            var student = await JsonHelper.DeserializeResponseToJson<StudentDetailVm>(response);
            Assert.Equal(studentRequest.Student.Name, student.Name);
        }    
        
        [Fact]
        public async Task TestCanCreateStudentV2Response()
        {
            var fixture = CreateFixture();
            var studentRequest = fixture.Create<CreateStudentVm>();
            var request = JsonHelper.SerializeToJson(studentRequest);
            
            var client = factory.GetClient();
            var mediaTypeAccept = new MediaTypeWithQualityHeaderValue("application/vnd.Classroom.student.v2+json"); 
            client.DefaultRequestHeaders.Accept.Add(mediaTypeAccept);
            
            var response = await client.PostAsync("/api/students", request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
            var student = await JsonHelper.DeserializeResponseToJson<StudentDetailVmV2>(response);
            Assert.Equal(studentRequest.Name, student.Student.Name);
        }     
        
        [Fact]
        public async Task TestCanCreateStudentV2RequestV2Response()
        {
            var fixture = CreateFixture();
            var studentRequest = fixture.Create<CreateStudentVmV2>();
            var request = JsonHelper.SerializeToJson(studentRequest);
            request.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.Classroom.student.v2+json");

            var client = factory.GetClient();
            var mediaTypeAccept = new MediaTypeWithQualityHeaderValue("application/vnd.Classroom.student.v2+json"); 
            client.DefaultRequestHeaders.Accept.Add(mediaTypeAccept);
            
            var response = await client.PostAsync("/api/students", request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
            var student = await JsonHelper.DeserializeResponseToJson<StudentDetailVmV2>(response);
            Assert.Equal(studentRequest.Student.Name, student.Student.Name);
        }            
    }
}
