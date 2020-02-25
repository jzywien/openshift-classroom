using System;
using System.Linq;
using AutoMapper;
using Classroom.Common.Extensions;
using Classroom.Services.Models;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Mappings
{
    public class StudentVmMappingProfile : Profile
    {
        public StudentVmMappingProfile()
        {
            CreateMap<Student, StudentVm>()
                .MapMember(dest => dest.Name, src => src.FullName.Trim());

            CreateMap<Student, StudentDetailVm>()
                .IncludeBase<Student, StudentVm>()
                .MapMember(dest => dest.GPA, src => src.GradePointAverage);

            CreateMap<CreateStudentVm, Student>()
                .MapMember(dest => dest.FirstName, src => src.Name.Split(" ", StringSplitOptions.None).FirstOrDefault())
                .MapMember(dest => dest.LastName, src => src.Name.Split(" ", StringSplitOptions.None).ElementAtOrDefault(1))
                .MapMember(dest => dest.GradePointAverage, src => src.Gpa)
                .MapMember(dest => dest.Courses, src => src.CourseIds.Select(id => new Course() { CourseId = id }));
        }
    }
}
