using AutoMapper;
using DM365TestTask.DTO;
using DM365TestTask.Models;
using DM365TestTask.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DM365TestTask.Service
{
    public class TaskService: ITaskService
    {

        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync(TaskFilterSortParameters parameters)
        {
            var tasks = await _repo.GetFilteredAsync(parameters);
            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CreatedBy = t.CreatedBy.Name,
                AssignedTo = t.AssignedTo.Name,
            });
        }

        public async Task<TaskDto> GetTaskByIdAsync(int id)
        {
            var task = await _repo.GetByIdAsync(id);
            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CreatedBy = task.CreatedBy.Name,
                AssignedTo = task.AssignedTo.Name
            };
        }

        public async Task CreateTaskAsync(CreateTaskDto dto)
        {
            var task = new TaskItem(dto.Title, dto.Description, dto.CreatedById, dto.AssignedToId);
            await _repo.AddAsync(task);
            await _repo.SaveChangesAsync();
        }
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _repo.GetByIdAsync(id);
            if (task == null)
                return false;

            _repo.Delete(task); // передаём саму задачу
            await _repo.SaveChangesAsync();
            return true;
        }
        public async Task<TaskItem?> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            var task = await _repo.GetByIdAsync(id);
            if (task == null)
                return null;
            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.AssignedToId = dto.AssignedToId;
            task.UpdatedAt = DateTime.UtcNow;
            
            await _repo.SaveChangesAsync();
            return task;
        }
        public async Task<bool> UpdateStatusAsync(int id, int newStatus)
        {
            var task = await _repo.GetByIdAsync(id);
            if (task == null) return false;

            task.Status = (MyTaskStatus)newStatus;
            await _repo.UpdateAsync(task);
            return true;
        }
    }
}
