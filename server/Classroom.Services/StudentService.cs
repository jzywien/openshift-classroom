using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Classroom.Common;
using Classroom.Common.Exceptions;
using Classroom.Repository;
using Classroom.Services.Models;
using Entities = Classroom.Repository.Entities;

namespace Classroom.Services
{
    [Autowired]
    public class StudentService : IStudentService
    {
        private readonly IMapper mapper;
        private readonly IClassroomContext classroomContext;

        public StudentService(IMapper mapper,
            IClassroomContext classroomContext)
        {
            this.mapper = mapper;
            this.classroomContext = classroomContext;
        }

        public async Task<Student> CreateStudent(Student student)
        {
            var entity = mapper.Map<Entities.Student>(student);

            var result = await classroomContext.Students.AddAsync(entity);
            await classroomContext.SaveChangesAsync(CancellationToken.None);
           
            var updatedStudent = await classroomContext.Students
                .Where(s => s.StudentId == result.Entity.StudentId)
                .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Course)
                .FirstOrDefaultAsync();

            return mapper.Map<Student>(updatedStudent);
        }

        public async Task DeleteStudent(int id)
        {
            var student = await classroomContext.Students
                .FindAsync(id);

            if (student == null) throw new EntityNotFoundException(typeof(Student), id);

            classroomContext.Students.Remove(student);
            await classroomContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<Student> GetStudent(int id)
        {
            var student = await classroomContext.Students
                .FindAsync(id);

            if (student == null) throw new EntityNotFoundException(typeof(Student), id);

            return mapper.Map<Student>(student);
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            var students = await classroomContext.Students
                .ToListAsync();

            return mapper.Map<IEnumerable<Student>>(students);
        }
    }
}
