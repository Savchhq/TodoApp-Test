using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.BLL.DTOs;
using TodoApp.BLL.Interfaces;

namespace TodoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll([FromQuery] GetTasksQueryDto query)
        {
            var userId = GetUserId();
            var (items, totalCount) = await taskService.GetAllAsync(
                userId,
                query.SearchQuery,
                query.CategoryId,
                query.PageNumber,
                query.PageSize);

            return Ok(new TasksPagedResultDto
            {
                Items = items,
                TotalCount = totalCount
            });
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var userId = GetUserId();
            var task = await taskService.GetByIdAsync(id, userId);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Add([FromBody] CreateTaskDto taskDto)
        {
            var userId = GetUserId();

            try
            {
                var task = await taskService.CreateAsync(taskDto, userId);
                return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskDto taskDto)
        {
            var userId = GetUserId();

            try
            {
                var task = await taskService.UpdateAsync(id, taskDto, userId);

                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var userId = GetUserId();
            var task = await taskService.DeleteAsync(id, userId);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        private Guid GetUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userIdString!);
        }
    }
}