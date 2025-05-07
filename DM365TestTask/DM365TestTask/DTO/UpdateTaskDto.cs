using System.ComponentModel.DataAnnotations;
using DM365TestTask.Models;

namespace DM365TestTask.DTO
{
    public class UpdateTaskDto
    {
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
        [EnumDataType(typeof(TaskStatus))]
        public MyTaskStatus Status { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "AssignedToId должен быть положительным числом")]
        public int AssignedToId { get; set; }
    }
}
