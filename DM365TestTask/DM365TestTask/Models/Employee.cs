using System.ComponentModel.DataAnnotations;

namespace DM365TestTask.Models
{
    public class Employee
    {
        public Employee(string name)
        {
            Name = name;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
