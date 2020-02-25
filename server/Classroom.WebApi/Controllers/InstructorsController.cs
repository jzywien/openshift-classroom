using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Classroom.Common.Exceptions;
using Classroom.WebApi.Delegate;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Controllers
{
    public class InstructorsController : BaseController
    {
        private readonly IInstructorDelegate instructorDelegate;

        public InstructorsController(IInstructorDelegate instructorDelegate)
        {
            this.instructorDelegate = instructorDelegate;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorVm>>> GetInstructors()
        {
            var instructors = await instructorDelegate.GetInstructors();
            return Ok(instructors);
        }

        [HttpGet("{id:int}")]
        [ProducesErrorResponseType(typeof(EntityNotFoundException))]
        public async Task<ActionResult<InstructorDetailVm>> GetInstructor(int id)
        {
            var instructor = await instructorDelegate.GetInstructor(id);
            return Ok(instructor);
        }
    }
}