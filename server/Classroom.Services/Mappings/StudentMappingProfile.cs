using System.Linq;
using AutoMapper;
using Classroom.Common.Extensions;
using Classroom.Services.Models;
using Entities = Classroom.Repository.Entities;

namespace Classroom.Services.Mappings
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<Entities.Student, Student>()
                .MapMember(
                    dest => dest.Courses,
                    src => src.StudentCourses.Select(sc => sc.Course))
                .ReverseMap()
                .MapPath(
                    dest => dest.StudentCourses,
                    src => src.Courses.Select(c => new Entities.StudentCourse()
                    {
                        CourseId = c.CourseId
                    }));
        }
    }
}
