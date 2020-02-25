using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Classroom.Repository.Entities;

namespace Classroom.Repository
{
    public class ClassroomSeeder
    {
        private readonly IClassroomContext context;

        private readonly ICollection<Student> Students = new List<Student>();
        private readonly ICollection<Instructor> Instructors = new List<Instructor>();

        public ClassroomSeeder(IClassroomContext context)
        {
            this.context = context;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            // If there is any data, don't seed
            if (context.Students.Any()) return;

            await SeedStudentsAsync(cancellationToken);
            await SeedInstructorsAsync(cancellationToken);
            await SeedCoursesAsync(cancellationToken);
        }

        private async Task SeedStudentsAsync(CancellationToken cancellationToken)
        {
            Students.Add(new Student { FirstName = "Peter", LastName = "Griffin", GradePointAverage = 1.2 });
            Students.Add(new Student { FirstName = "Stewie", LastName = "Griffin", GradePointAverage = 4.0 });
            Students.Add(new Student { FirstName = "Brian", LastName = "Griffin", GradePointAverage = 3.5 });
            Students.Add(new Student { FirstName = "Homer", LastName = "Simpson", GradePointAverage = 1.5 });
            Students.Add(new Student { FirstName = "Lisa", LastName = "Simpson", GradePointAverage = 4.0 });
            Students.Add(new Student { FirstName = "Bart", LastName = "Simpson", GradePointAverage = 2.0 });

            context.Students.AddRange(Students);
            await context.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedInstructorsAsync(CancellationToken cancellationToken)
        {
            Instructors.Add(new Instructor { FirstName = "Peter", LastName = "Norvig" });
            Instructors.Add(new Instructor { FirstName = "Barbara", LastName = "Liskov" });

            context.Instructors.AddRange(Instructors);
            await context.SaveChangesAsync(cancellationToken);

        }

        private async Task SeedCoursesAsync(CancellationToken cancellationToken)
        {
            var course1 = new Course
            {
                Title = "Intro to Computer Science",
                Instructor = Instructors.ElementAt(0),
                StudentCourses = Students.Select(s => new StudentCourse
                {
                    Student = s
                }).ToList()
            };

            context.Courses.Add(course1);


            var course2 = new Course
            {
                Title = "Design Patterns",
                Instructor = Instructors.ElementAt(1),
                StudentCourses = Students.Select(s => new StudentCourse
                {
                    Student = s
                }).ToList()
            };


            context.Courses.Add(course2);
            await context.SaveChangesAsync(cancellationToken);
        }

    }
}
