using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Classroom.Common.Exceptions;
using Classroom.WebApi.Attributes;
using Classroom.WebApi.Delegate;
using Classroom.WebApi.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace Classroom.WebApi.Controllers
{
    public class StudentsController : BaseController
    {
        private readonly IStudentDelegate studentDelegate;

        public StudentsController(IStudentDelegate studentDelegate)
        {
            this.studentDelegate = studentDelegate;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentVm>>> GetStudents()
        {
            var students = await studentDelegate.GetStudents();
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        [ProducesErrorResponseType(typeof(EntityNotFoundException))]
        public async Task<ActionResult<StudentDetailVm>> GetStudentDetail(int id)
        {
            var student = await studentDelegate.GetStudent(id);
            return Ok(student);
        }
        
        [HttpGet("{id:int}")]
        [ProducesMediaType("application/vnd.Classroom.student.v2+json")]
        [ProducesErrorResponseType(typeof(EntityNotFoundException))]
        public async Task<ActionResult<StudentDetailVmV2>> GetStudentDetailV2(int id)
        {
            var student = await studentDelegate.GetStudent(id);
            var response = new StudentDetailVmV2
            {
                Student = student,
                Version = 2
            };
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        [ProducesErrorResponseType(typeof(EntityNotFoundException))]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            await studentDelegate.DeleteStudent(id);
            return Ok();
        }
        
        [HttpPost]
        [SwaggerOperation("CreateStudent")]
        public async Task<ActionResult<StudentDetailVm>> CreateStudent([FromBody] CreateStudentVm request)
        {
            var result = await studentDelegate.CreateStudent(request);
            return Created($"api/classroom/student/{result.StudentId}", result);
        }
        
        [HttpPost]
        [ProducesMediaType("application/vnd.Classroom.student.v2+json")]
        [SwaggerOperation("CreateStudentV2Response")]
        public async Task<ActionResult<StudentDetailVmV2>> CreateStudentV2Response([FromBody] CreateStudentVm request)
        {
            var result = await studentDelegate.CreateStudent(request);
            
            var response = new StudentDetailVmV2
            {
                Student = result,
                Version = 2
            };
            
            return Created($"api/classroom/student/{result.StudentId}", response);
        } 

        [HttpPost]
        [ConsumesMediaType("application/vnd.Classroom.student.v2+json")]
        [SwaggerOperation("CreateStudentV2Request")]
        public async Task<ActionResult<StudentDetailVm>> CreateStudentV2Request([FromBody] CreateStudentVmV2 request)
        {
            var student = request.Student;
            var result = await studentDelegate.CreateStudent(student);
            return Created($"api/classroom/student/{result.StudentId}", result);
        }            
        
        [HttpPost]
        [ConsumesMediaType("application/vnd.Classroom.student.v2+json")]
        [ProducesMediaType("application/vnd.Classroom.student.v2+json")]
        public async Task<ActionResult<StudentDetailVmV2>> CreateStudentV2RequestV2Response([FromBody] CreateStudentVmV2 request)
        {
            var student = request.Student;
            var result = await studentDelegate.CreateStudent(student);
        
            var response = new StudentDetailVmV2
            {
                Student = result,
                Version = 2
            };
            
            return Created($"api/classroom/student/{result.StudentId}", response);
        }        
        
    }
}
