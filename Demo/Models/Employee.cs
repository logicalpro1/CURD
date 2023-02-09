using System.Text.Json.Serialization;

namespace Demo.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
        public DateTime DoB { get; set; }
        public string Department { get; set; }
    }
}
