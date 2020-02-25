namespace Classroom.WebApi.ViewModels
{
    public class CourseVm
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public InstructorVm Instructor { get; set; }
    }
}
