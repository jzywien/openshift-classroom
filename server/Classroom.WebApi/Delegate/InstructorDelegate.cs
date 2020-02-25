using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Classroom.Common;
using Classroom.Services;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Delegate
{
    [Autowired]
    public class InstructorDelegate : IInstructorDelegate
    {
        private readonly IMapper mapper;
        private readonly IInstructorService instructorService;
        
        public InstructorDelegate(IMapper mapper,
            IInstructorService instructorService)
        {
            this.mapper = mapper;
            this.instructorService = instructorService;
        }
        
        public async Task<IEnumerable<InstructorVm>> GetInstructors()
        {
            var instructors = await instructorService.GetInstructors();
            return mapper.Map<IEnumerable<InstructorVm>>(instructors);
        }

        public async Task<InstructorDetailVm> GetInstructor(int id)
        {
            var instructor = await instructorService.GetInstructor(id);
            return mapper.Map<InstructorDetailVm>(instructor);
        }
    }
}