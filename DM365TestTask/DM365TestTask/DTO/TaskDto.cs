using DM365TestTask.Models;
using System.ComponentModel.DataAnnotations;
using DM365TestTask.Models;

namespace DM365TestTask.DTO
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public MyTaskStatus Status { get; set; }
        public string CreatedBy { get; set; }
        public string AssignedTo { get; set;}
    }
}
