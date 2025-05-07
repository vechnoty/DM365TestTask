using DM365TestTask.DTO;
using DM365TestTask.Models;
using DM365TestTask.Service;
using Microsoft.AspNetCore.Mvc;

namespace DM365TestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TaskFilterSortParameters parameters)
        {
            var tasks = await _taskService.GetAllTasksAsync(parameters);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            await _taskService.CreateTaskAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.CreatedById }, dto);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete (int id)
        {
            if(await _taskService.DeleteTaskAsync(id)) return Ok();
            return BadRequest();

        }
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto dto)
        {
            var updatedTask = await _taskService.UpdateTaskAsync(id, dto);
            if (updatedTask == null)
                return NotFound();

            return Ok(updatedTask);
        }
        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateTaskStatusDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _taskService.UpdateStatusAsync(id, dto.Status);
            if (!updated) return NotFound();

            return Ok();
        }
    }
}
