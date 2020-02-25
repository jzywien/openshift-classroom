using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Classroom.Common.Exceptions;
using Classroom.WebApi.Attributes;
using Classroom.WebApi.Delegate;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Controllers
{
    public class CoursesController : BaseController
    {
        private readonly ICourseDelegate courseDelegate;

        public CoursesController(ICourseDelegate courseDelegate)
        {
            this.courseDelegate = courseDelegate;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseVm>>> GetCourses()
        {
            var courses = await courseDelegate.GetCourses();
            return Ok(courses);
        }
        
        
        [HttpGet("{id:int}")]
        [ProducesErrorResponseType(typeof(EntityNotFoundException))]
        public async Task<ActionResult<CourseDetailVm>> GetCourseDetailV1(int id)
        {
            var course = await courseDelegate.GetCourseDetail(id);
            return Ok(course);
        }

        [HttpGet("{id:int}")]
        [ProducesMediaType("application/vnd.Classroom.course.v2+json")]
        [ProducesErrorResponseType(typeof(EntityNotFoundException))]
        public async Task<ActionResult<CourseDetailVmV2>> GetCourseDetailV2(int id)
        {
            var course = await courseDelegate.GetCourseDetailV2(id);
            return Ok(course);
        }
    }
}
