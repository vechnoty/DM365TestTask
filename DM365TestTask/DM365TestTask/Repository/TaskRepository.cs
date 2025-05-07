using DM365TestTask.DTO;
using DM365TestTask.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DM365TestTask.Models;


namespace DM365TestTask.Repository
{
    public class TaskRepository: ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TaskItem task) => await _context.TaskItems.AddAsync(task);

        public void Delete(TaskItem task) => _context.TaskItems.Remove(task);

        public async Task<IEnumerable<TaskItem>> GetAllAsync() =>
            await _context.TaskItems.Include(t => t.CreatedBy).Include(t => t.AssignedTo).ToListAsync();

        public async Task<TaskItem> GetByIdAsync(int id) =>
            await _context.TaskItems.Include(t => t.CreatedBy).Include(t => t.AssignedTo).FirstOrDefaultAsync(t => t.Id == id);

        public void Update(TaskItem task) => _context.TaskItems.Update(task);    
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<TaskItem>> GetFilteredAsync(TaskFilterSortParameters parameters)
        {
            var query = _context.TaskItems
                .Include(t => t.CreatedBy)
                .Include(t => t.AssignedTo)
                .AsQueryable();

            // Фильтрация
            if (!string.IsNullOrEmpty(parameters.Title))
                query = query.Where(t => t.Title.Contains(parameters.Title));

            if (parameters.Status.HasValue)
                query = query.Where(t => t.Status.ToString() == parameters.Status.ToString());

            bool isDescending = bool.TryParse(parameters.Descending, out var desc) && desc;

            // Сортировка
            query = parameters.SortBy?.ToLower() switch
            {
                "title" => isDescending ? query.OrderByDescending(t => t.Title) : query.OrderBy(t => t.Title),
                "status" => isDescending ? query.OrderByDescending(t => t.Status) : query.OrderBy(t => t.Status),
                "createdby" => isDescending ? query.OrderByDescending(t => t.CreatedBy.Name) : query.OrderBy(t => t.CreatedBy.Name),
                _ => query
            };

            return await query.ToListAsync();
        }
        public async Task UpdateAsync(TaskItem task)
        {
            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
        }

    }
}
