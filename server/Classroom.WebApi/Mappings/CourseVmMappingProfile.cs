using System.Linq;
using AutoMapper;
using Classroom.Common.Extensions;
using Classroom.Services.Models;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Mappings
{
    public class CourseVmMappingProfile : Profile
    {
        public CourseVmMappingProfile()
        {
            CreateMap<Course, CourseVm>()
                .MapMember(dest => dest.Instructor, src => src.Instructor);

            CreateMap<Course, CourseDetailVm>()
                .IncludeBase<Course, CourseVm>()
                .MapMember(dest => dest.Students, src => src.Students.Select(s => s));
        }
    }
}
