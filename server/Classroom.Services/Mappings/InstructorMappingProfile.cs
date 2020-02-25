using AutoMapper;
using Classroom.Services.Models;
using Entities = Classroom.Repository.Entities;

namespace Classroom.Services.Mappings
{
    public class InstructorMappingProfile : Profile
    {
        public InstructorMappingProfile()
        {
            CreateMap<Entities.Instructor, Instructor>();
        }
    }
}
