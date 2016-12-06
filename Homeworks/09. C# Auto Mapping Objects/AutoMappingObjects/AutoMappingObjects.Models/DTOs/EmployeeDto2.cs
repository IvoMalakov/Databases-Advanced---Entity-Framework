namespace AutoMappingObjects.Models.DTOs
{
    using System.Text;

    public class EmployeeDto2
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public string ManagerLastName { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{this.FirstName} {this.LastName} {this.Salary:F2} - Manager: ");
            sb.Append(this.ManagerLastName == null ? "[no manager]" : $"{this.ManagerLastName}");

            return sb.ToString();
        }
    }
}
