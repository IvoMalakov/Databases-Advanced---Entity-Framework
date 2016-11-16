using System.Diagnostics;

namespace StudentSystem.Data.Migrations
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Globalization;
    using StudentSystem.Models;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "StudentSystem.Data.StudentSystemContext";
        }

        protected override void Seed(StudentSystemContext context)
        {
            //Random random = new Random();
                     
            //SeedResorces(context, random);
            //SeedHomeworks(context, random);
            //SeedStudents(context, random);
            //SeedCourses(context, random);
        }

        private void SeedHomeworks(StudentSystemContext context, Random random)
        {
            using (var reader = new StreamReader("D:/DB Advanced/EntityFrameWork_Relations/StudentSystem/resources/content.txt"))
            {
                while (true)
                {
                    string line = reader.ReadLine();

                    if (line == null)
                    {
                        break;
                    }

                    string[] data = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    context.Homeworks.AddOrUpdate(homework => homework.Content,
                        new Homework()
                        {
                            Content = data[random.Next(data.Length)],
                            ContentType = (ContentType)random.Next(0, 3),
                            SubmissionDate = DateTime.Now.AddDays(15),
                        });
                }
            }

            context.SaveChanges();
        }

        private void SeedCourses(StudentSystemContext context, Random random)
        {
            string[] names = { "Math", "History", "C# Development", "Bulgarian Language", "Piene" };
    
            for (int i = 0; i < 10; i++)
            {
                context.Courses.AddOrUpdate(course => course.Name,
                    new Course()
                    {
                        Name = names[random.Next(names.Length)],
                        Description = "Description" + (i + 1),
                        StartDate = DateTime.Now,
                        EndDate = new DateTime((DateTime.Now.Year + 5), DateTime.Now.Month, DateTime.Now.Day),
                        Price = (decimal)random.NextDouble(),                  
                    });
            }

            context.SaveChanges();
        }

        private void SeedStudents(StudentSystemContext context, Random random)
        {
            string[] names =
            {
                "Maria", "Ivan", "Petur", "Viktoria", "Aishe", "Nakov", "Georgi", "General Radev",
                "Donald Trump"
            };

            string[] phoneNumbers =
            {
                "+359886000000", "+359886000001", "+359886000002", "+359886000003", "+359886111111",
                "+35952123456"
            };

            for (int i = 0; i < 10; i++)
            {
                context.Students.AddOrUpdate(student => student.Name,
                    new Student()
                    {
                        Name = names[random.Next(names.Length)],
                        PhoneNumber = phoneNumbers[random.Next(phoneNumbers.Length)],
                        RegisteredOn = DateTime.Now,
                        BirthDay = new DateTime(1920, 5, 7).AddDays(5 * i)
                    });
            }

            context.SaveChanges();
        }

        private void SeedResorces(StudentSystemContext context, Random random)
        {
            string[] names = { "Uchebnik", "Tetradka", "Pomaglo", "Himikalka", "Moliv", "Guma" };
            string[] urls = { "http://abv.bg", "http://koisumaz.bg", "https://www.donaldtrump.usa" };

            for (int i = 0; i < 10; i++)
            {
                context.Resources.AddOrUpdate(resource => resource.Name,
                    new Resource()
                    {
                        Name = names[random.Next(names.Length)],
                        ResorceType = (ResorceType)random.Next(0, 4),
                        Url = urls[random.Next(urls.Length)],
                    });
            }

            context.SaveChanges();
        }
    }
}