using AutoMapper;
using Classroom.Common.Extensions;
using Classroom.Services.Models;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Mappings
{
    public class InstructorVmMappingProfile : Profile
    {
        public InstructorVmMappingProfile()
        {
            CreateMap<Instructor, InstructorVm>()
                .MapMember(dest => dest.Name, src => src.FullName)
                .ReverseMap();

            CreateMap<Instructor, InstructorDetailVm>()
                .IncludeBase<Instructor, InstructorVm>()
                .ReverseMap();
        }
    }
}
