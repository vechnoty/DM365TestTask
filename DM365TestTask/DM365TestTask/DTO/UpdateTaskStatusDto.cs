using System.ComponentModel.DataAnnotations;

namespace DM365TestTask.DTO
{
    public class UpdateTaskStatusDto
    {
        [Required]
        public int Status { get; set; }
    }
}
