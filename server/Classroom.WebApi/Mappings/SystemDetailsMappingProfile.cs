using AutoMapper;
using Classroom.Common.Extensions;
using Classroom.Services.Models;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Mappings
{
    public class SystemDetailsMappingProfile : Profile
    {
        public SystemDetailsMappingProfile()
        {
            CreateMap<SystemDetails, SystemDetailsVm>()
                .MapMember(dest => dest.OsPlatform, src => src.OperatingSystem.Platform)
                .MapMember(dest => dest.OsVersion, src => src.OperatingSystem.Version);
        }
    }
}