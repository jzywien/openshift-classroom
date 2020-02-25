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
    public class InstructorService : IInstructorService
    {
        private readonly IMapper mapper;
        private readonly IClassroomContext classroomContext;
        
        public InstructorService(IMapper mapper,
            IClassroomContext classroomContext)
        {
            this.mapper = mapper;
            this.classroomContext = classroomContext;
        }
        
        public async Task<IEnumerable<Instructor>> GetInstructors()
        {
            var instructors = await classroomContext.Instructors
                .ToListAsync();
            return mapper.Map<IEnumerable<Instructor>>(instructors);
        }

        public async Task<Instructor> GetInstructor(int id)
        {
            var instructor = await classroomContext.Instructors
                .FindAsync(id);
            
            if (instructor == null) throw new EntityNotFoundException(typeof(Instructor), id);
            
            return mapper.Map<Instructor>(instructor);
        }
    }
}