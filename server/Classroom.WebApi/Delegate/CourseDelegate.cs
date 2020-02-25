using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Classroom.Common;
using Classroom.Services;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Delegate
{
    [Autowired]
    public class CourseDelegate : ICourseDelegate
    {
        private readonly IMapper mapper;
        private readonly ICourseService courseService;

        public CourseDelegate(IMapper mapper,
            ICourseService courseService)
        {
            this.mapper = mapper;
            this.courseService = courseService;
        }

        public async Task<IEnumerable<CourseVm>> GetCourses()
        {
            var courses = await courseService.GetCourses();
            return mapper.Map<IEnumerable<CourseVm>>(courses);
        }

        public async Task<CourseDetailVm> GetCourseDetail(int id)
        {
            var course = await courseService.GetCourse(id);
            return mapper.Map<CourseDetailVm>(course);
        }
        
        public async Task<CourseDetailVmV2> GetCourseDetailV2(int id)
        {
            var courseModel = await courseService.GetCourse(id);
            var course = mapper.Map<CourseDetailVm>(courseModel);
            return new CourseDetailVmV2
            {
                Course = course,
                Version = 2
            };
        }        
    }
}
