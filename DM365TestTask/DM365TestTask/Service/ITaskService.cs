using DM365TestTask.DTO;
using DM365TestTask.Models;

namespace DM365TestTask.Service
{
    public interface ITaskService
    {
        Task CreateTaskAsync(CreateTaskDto dto);
        Task<IEnumerable<TaskDto>> GetAllTasksAsync(TaskFilterSortParameters parameters);
        Task<TaskDto> GetTaskByIdAsync(int id);
        Task<bool> DeleteTaskAsync(int id);
        Task<TaskItem?> UpdateTaskAsync(int id, UpdateTaskDto dto);
        Task<bool> UpdateStatusAsync(int id, int newStatus);
    }
}
