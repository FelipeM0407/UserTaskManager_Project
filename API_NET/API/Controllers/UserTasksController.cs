using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases;
using Application.DTOs;
using Microsoft.Extensions.Logging;
using Domain.Enum;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTasksController : ControllerBase
    {
        private readonly UserTaskUseCase _userTaskUseCase;
        private readonly ILogger<UserTasksController> _logger;

        public UserTasksController(UserTaskUseCase userTaskUseCase, ILogger<UserTasksController> logger)
        {
            _userTaskUseCase = userTaskUseCase;
            _logger = logger;
        }

        // GET: api/usertasks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await _userTaskUseCase.GetAllTasksAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tasks.");
                return StatusCode(500, new { Message = "An error occurred while retrieving tasks." });
            }
        }

        // GET: api/usertasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var task = await _userTaskUseCase.GetTaskByIdAsync(id);
                if (task == null)
                {
                    return NotFound(new { Message = "Task not found." });
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving task with ID {id}.");
                return StatusCode(500, new { Message = "An error occurred while retrieving the task." });
            }
        }

        // POST: api/usertasks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserTaskDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                if (dto.Status == 0)
                {
                    dto.Status = (int)UserTaskStatus.Pending;
                }

                await _userTaskUseCase.CreateTaskAsync(dto);
                return Ok(new { Message = "Task created successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task.");
                return StatusCode(500, new { Message = "An error occurred while creating the task." });
            }
        }

        // PUT: api/usertasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserTaskDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userTaskUseCase.UpdateTaskAsync(id, dto);
                return Ok(new { Message = "Task updated successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating task with ID {id}.");

                if (ex.Message == "Status not found")
                {
                    return BadRequest(new { Message = "Status not found" });
                }
                
                return StatusCode(500, new { Message = "An error occurred while updating the task." });
            }
        }

        // DELETE: api/usertasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userTaskUseCase.DeleteTaskAsync(id);
                return Ok(new { Message = "Task deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting task with ID {id}.");
                return StatusCode(500, new { Message = "An error occurred while deleting the task." });
            }
        }
    }
}
