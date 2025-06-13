using Microsoft.AspNetCore.Authorization;
using PAS_1.Services;
using Microsoft.AspNetCore.Mvc;
using PAS_1.Models;

namespace PAS_1.Controllers
{
    [Authorize]
    [Route("api/todos")]
    [ApiController]
    public class TodoController(ITodoService todoService) : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodos([FromQuery] string? sort = "desc", [FromQuery] bool? finished = null)
        {
            var todos = await todoService.GetTodosAsync(sort, finished);
            return this.ToActionResult(todos);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto?>> GetTodo(string id)
        {
            var todo = await todoService.GetTodoAsync(int.Parse(id));

            return this.ToActionResult(todo);
        }
        
        [HttpPost()]
        public async Task<ActionResult<TodoDto?>> CreateTodo(CreateTodoDto request)
        {
            var todo = await todoService.CreateAsync(request);

            return this.ToActionResult(todo);
        }
        
        [HttpPatch("{id}")]
        public async Task<ActionResult<TodoDto?>> UpdateTodo(string id, UpdateTodoDto request)
        {
            var todo = await todoService.UpdateAsync(int.Parse(id), request);

            return this.ToActionResult(todo);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTodo(string id)
        {
            var todo = await todoService.DeleteAsync(int.Parse(id));

            return this.ToActionResult(todo);
        }
    }
}