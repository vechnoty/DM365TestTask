using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DM365TestTask.DTO
{
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "Поле 'Title' обязательно для заполнения")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина заголовка должна быть от 3 до 100 символов")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Описание не должно превышать 1000 символов")]
        public string? Description { get; set; }

        [Required]
        public int CreatedById { get; set; }

        [Required]
        public int AssignedToId { get; set; }
    }              
}
