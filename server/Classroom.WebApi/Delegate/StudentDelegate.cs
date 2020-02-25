using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Classroom.Common;
using Classroom.Services;
using Classroom.Services.Models;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Delegate
{
    [Autowired]
    public class StudentDelegate : IStudentDelegate
    {
        private readonly IMapper mapper;
        private readonly IStudentService studentService;

        public StudentDelegate(IMapper mapper,
            IStudentService studentService)
        {
            this.mapper = mapper;
            this.studentService = studentService;
        }
        public async Task<IEnumerable<StudentVm>> GetStudents()
        {
            var students = await studentService.GetStudents();
            return mapper.Map<IEnumerable<StudentVm>>(students);
        }    
        
        public async Task<StudentDetailVm> GetStudent(int id)
        {
            var student = await studentService.GetStudent(id);
            return mapper.Map<StudentDetailVm>(student);
        }

        public async Task<StudentDetailVm> CreateStudent(CreateStudentVm student)
        {
            var request = mapper.Map<Student>(student);
            var response = await studentService.CreateStudent(request);

            return mapper.Map<StudentDetailVm>(response);
        }

        public async Task DeleteStudent(int id)
        {
            await studentService.DeleteStudent(id);
        }
    }
}
