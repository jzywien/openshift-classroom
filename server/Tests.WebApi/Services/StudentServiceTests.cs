using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using FluentAssertions;
using Classroom.Common.Exceptions;
using Classroom.Repository;
using Classroom.Services;
using Classroom.Services.Models;
using Moq;
using Tests.WebApi.Extensions;
using Xunit;
using Entities = Classroom.Repository.Entities;

namespace Tests.WebApi.Services
{
    public class StudentServiceTests
    {
        private static IFixture CreateFixture()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization
            {
                ConfigureMembers = true
            });
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return fixture;
        }
        
        [Fact]
        public async Task CanGetStudentById()
        {
            // Arrange
            var fixture = CreateFixture();
            var context = fixture.FreezeMoq<IClassroomContext>();
            var mapper = fixture.FreezeMoq<IMapper>();
            var service = fixture.Create<StudentService>();
            var id = fixture.Create<int>();
            
            // Act
            var student = await service.GetStudent(id);

            // Assert
            student.Should().NotBeNull();
            
            context.Verify(x => x.Students.FindAsync(id), Times.Once);
            mapper.Verify(x => x.Map<Student>(It.IsAny<Entities.Student>()), Times.Once);
        }
        
        [Fact]
        public void WillThrowNotFoundExceptionWhenNotFound()
        {
            // Arrange
            var fixture = CreateFixture();
            var context = fixture.FreezeMoq<IClassroomContext>();
            var mapper = fixture.FreezeMoq<IMapper>();
            var id = fixture.Create<int>();

            context.Setup(x => x.Students.FindAsync(id)).ReturnsAsync((Entities.Student)null);
            var service = fixture.Create<StudentService>();

            // Act
            Func<Task> getStudent = async () => await service.GetStudent(id);

            // Assert
            getStudent.Should().Throw<EntityNotFoundException>();
        }
    }
}