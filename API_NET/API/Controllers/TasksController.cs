using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases;
using Application.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTasksController : ControllerBase
    {
        private readonly UserTaskUseCase _userTaskUseCase;

        public UserTasksController(UserTaskUseCase userTaskUseCase)
        {
            _userTaskUseCase = userTaskUseCase;
        }

        // GET: api/usertasks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _userTaskUseCase.GetAllTasksAsync();
            return Ok(tasks);
        }

        // GET: api/usertasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _userTaskUseCase.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // POST: api/usertasks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserTaskDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userTaskUseCase.CreateTaskAsync(dto);
            return Ok(new { Message = "Task created successfully" });
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
                return Ok(new { Message = "Task updated successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // DELETE: api/usertasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userTaskUseCase.DeleteTaskAsync(id);
                return Ok(new { Message = "Task deleted successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}