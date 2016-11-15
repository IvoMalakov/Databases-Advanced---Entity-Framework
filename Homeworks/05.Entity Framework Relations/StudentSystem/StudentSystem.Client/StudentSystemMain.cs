namespace StudentSystem.Client
{
    using StudentSystem.Models;
    using StudentSystem.Data;
    public class StudentSystemMain
    {
        static void Main()
        {
            var context = new StudentSystemContext();

            context.Database.Initialize(true);
        }
    }
}
