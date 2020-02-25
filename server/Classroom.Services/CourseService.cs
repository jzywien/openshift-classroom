using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Classroom.Common;
using Classroom.Common.Exceptions;
using Classroom.Repository;
using Classroom.Services.Models;

namespace Classroom.Services
{
    [Autowired]
    public class CourseService : ICourseService
    {
        private readonly IMapper mapper;
        private readonly IClassroomContext classroomContext;

        public CourseService(IMapper mapper,
            IClassroomContext classroomContext)
        {
            this.mapper = mapper;
            this.classroomContext = classroomContext;
        }

        public async Task<IEnumerable<Course>> GetCourses()
        {
            var courses = await classroomContext.Courses
                .ToListAsync();

            return mapper.Map<IEnumerable<Course>>(courses);
        }

        public async Task<Course> GetCourse(int id)
        {
            var course = await classroomContext.Courses
                .FindAsync(id);

            if (course == null) throw new EntityNotFoundException(typeof(Student), id);

            return mapper.Map<Course>(course);
        }
    }
}
