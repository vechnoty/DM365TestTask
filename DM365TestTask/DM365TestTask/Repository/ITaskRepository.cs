using DM365TestTask.DTO;
using DM365TestTask.Models;

namespace DM365TestTask.Repository
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskItem task);
        //Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<IEnumerable<TaskItem>> GetFilteredAsync(TaskFilterSortParameters parameters);
        Task SaveChangesAsync();
        Task<TaskItem> GetByIdAsync(int id);  
        void Update(TaskItem task);
        void Delete(TaskItem task);
        Task UpdateAsync(TaskItem task);

    }
}
