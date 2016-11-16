namespace StudentSystem.Client
{
    using System;
    using System.Globalization;
    using System.Linq;
    using StudentSystem.Models;
    using StudentSystem.Data;
    using System.Data.Entity.SqlServer;
    public class StudentSystemMain
    {
        static void Main()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InstalledUICulture;
            var context = new StudentSystemContext();

            //context.Database.Initialize(true);

            //ListAllStudentsAndTheirHomeworks(context);
            //ListAllCoursesAndResources(context);
            //ListAllCoursesWithMore5Resources(context);
            //ListAllCoursesByGivenDate(context, DateTime.Now);
            //ListAllStudentsAndTheirCourses(context);
        }

        private static void ListAllStudentsAndTheirCourses(StudentSystemContext context)
        {
            var wantedStudents = context.Students
                .OrderByDescending(student => student.Courses.Sum(course => course.Price))
                .ThenByDescending(student => student.Courses.Count)
                .ThenBy(student => student.Name);

            foreach (var student in wantedStudents)
            {
                Console.WriteLine("Student name: {0}," +
                                  "Number of courses: {1}," +
                                  "Total price: {2}," +
                                  "Average price: {3}",
                                  student.Name,
                                  student.Courses.Count,
                                  student.Courses.Sum(course => course.Price),
                                  student.Courses.Average(course => course.Price));

                Console.WriteLine();
            }
        }

        private static void ListAllCoursesByGivenDate(StudentSystemContext context, DateTime activeDate)
        {
            var wantedCourses = context.Courses
                .Where(course => course.StartDate <= activeDate && course.EndDate >= activeDate)
                .Select(course => new
                {
                    course.Name,
                    course.StartDate,
                    course.EndDate,
                    Duration = SqlFunctions.DateDiff("day", course.StartDate, course.EndDate),
                    StudentsCount = course.Students.Count
                })
                .OrderByDescending(course => course.StudentsCount)
                .ThenByDescending(course => course.Duration);

            foreach (var course in wantedCourses)
            {
                Console.WriteLine("Course name: {0}, " +
                                  "Start date: {1}, " +
                                  "End date: {2}, " +
                                  "Course duration: {3}, " +
                                  "Number of students enrolled: {4}",
                                  course.Name,
                                  course.StartDate,
                                  course.EndDate,
                                  course.Duration,
                                  course.StudentsCount);
                Console.WriteLine();
            }

        }

        private static void ListAllCoursesWithMore5Resources(StudentSystemContext context)
        {
            var wantedCourses = context.Courses
                .Where(course => course.Resources.Count > 5)
                .OrderByDescending(course => course.Resources.Count)
                .ThenByDescending(course => course.StartDate);

            foreach (var course in wantedCourses)
            {
                Console.WriteLine("Course name: {0} , Resource count: {1}", course.Name, course.Resources.Count);
            }
        }

        private static void ListAllCoursesAndResources(StudentSystemContext context)
        {
            var courses = context.Courses
                .OrderBy(course => course.StartDate)
                .ThenByDescending(course => course.EndDate);

            foreach (var course in courses)
            {
                Console.WriteLine("Course name: {0} , Description: {1}", course.Name, course.Description);

                foreach (var resource in course.Resources)
                {
                    Console.WriteLine("Resource Id: {0}, " +
                                      "Resource Name: {1}, " +
                                      "Resource Type: {2}," +
                                      " Resource Url: {3}," +
                                      " Course Id: {4}",
                                      resource.Id,
                                      resource.Name,
                                      resource.ResorceType,
                                      resource.Url,
                                      resource.CourseId);
                }

                Console.WriteLine();
            }
        }

        private static void ListAllStudentsAndTheirHomeworks(StudentSystemContext context)
        {
            var students = context.Students;

            foreach (var student in students)
            {
                Console.WriteLine("Student name: {0}", student.Name);

                if (student.Homeworks.Count == 0)
                {
                    Console.WriteLine("No homeworks for this student");
                    Console.WriteLine();
                    continue;              
                }

                foreach (var homework in student.Homeworks)
                {
                    Console.WriteLine("Homework content: {0}", homework.Content);
                    Console.WriteLine("Homework contentType: {0}", homework.ContentType);
                    Console.WriteLine();
                }
            }
        }
    }
}
