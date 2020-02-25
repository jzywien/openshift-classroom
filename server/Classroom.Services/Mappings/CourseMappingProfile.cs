using System.Linq;
using AutoMapper;
using Classroom.Common.Extensions;
using Classroom.Services.Models;
using Entities = Classroom.Repository.Entities;

namespace Classroom.Services.Mappings
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<Entities.Course, Course>()
                .MapMember(dest => dest.Students, src => src.StudentCourses.Select(sc => sc.Student));
        }
    }
}
