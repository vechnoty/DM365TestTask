using DM365TestTask.Models;
namespace DM365TestTask.DTO
{
    public class TaskFilterSortParameters
    {
        public string? Title { get; set; }               // фильтрация по подстроке
        public MyTaskStatus? Status { get; set; }          // фильтрация по статусу
        public string? SortBy { get; set; }              // поле сортировки: Title, Status, CreatedBy
        public string? Descending { get; set; } = "false";    // порядок сортировки
    }
}
