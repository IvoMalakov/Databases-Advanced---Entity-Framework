namespace StudentSystem.Client
{
    using System;
    using System.Linq;
    using StudentSystem.Models;
    using StudentSystem.Data;
    public class StudentSystemMain
    {
        static void Main()
        {
            var context = new StudentSystemContext();

            //context.Database.Initialize(true);

            ListAllStudentsAndTheirHomeworks(context);
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
