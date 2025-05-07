using System.ComponentModel.DataAnnotations;

namespace DM365TestTask.Models
{
    public class TaskItem
    {
        public TaskItem(string title, string description,int createdById,int assignedToId)
        {
            Title = title;
            Description = description;
            CreatedById = createdById;
            AssignedToId = assignedToId;
            Status = MyTaskStatus.ToDo;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public MyTaskStatus Status { get; set; } // сделать энам
        public int CreatedById { get; set; }
        public Employee CreatedBy { get; set; } // сделать класс с сотрудниками на кого и кто поставил задачу
        public int AssignedToId { get; set; }
        public Employee AssignedTo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public enum MyTaskStatus { ToDo, InProgress, Done }
}

