using Microsoft.EntityFrameworkCore;
using PAS_1.Models;
using PAS_1.Data;
using PAS_1.Entities;
using PAS_1.Libs;

namespace PAS_1.Services
{
    public class TodoService(TodoDbContext context, IHttpContextAccessor httpContextAccessor) : ITodoService
    {
        public async Task<ServiceResult<IEnumerable<TodoDto>>> GetTodosAsync()
        {
            var currentUserId = httpContextAccessor.HttpContext?.User?.GetCurrentUserId();
            
            if (currentUserId is null)
                return ServiceResult<IEnumerable<TodoDto>>.Fail(ServiceError.Unauthorized("Token missing or invalid"));
            
            var todos = await context.Todos
                .Where(t => t.UserId == currentUserId)
                .Select(t => new TodoDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Finished = t.Finished
                })
                .ToListAsync();
            
            return ServiceResult<IEnumerable<TodoDto>>.Ok(todos);
        }

        public async Task<ServiceResult<TodoDto?>> GetTodoAsync(int id)
        {
            var currentUserId = httpContextAccessor.HttpContext?.User?.GetCurrentUserId();
            
            if (currentUserId is null)
                return ServiceResult<TodoDto?>.Fail(ServiceError.Unauthorized("Token missing or invalid"));

            var todo = await context.Todos
                .Where(t => t.Id == id && t.UserId == currentUserId)
                .FirstOrDefaultAsync();
            
            if (todo is null)
                return ServiceResult<TodoDto?>.Fail(ServiceError.NotFound("Todo not found"));

            return ServiceResult<TodoDto?>.Ok(new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Finished = todo.Finished
            });
        }

        public async Task<ServiceResult<TodoDto?>> CreateAsync(CreateTodoDto request)
        {
            var currentUserId = httpContextAccessor.HttpContext?.User?.GetCurrentUserId();
            if (currentUserId is null)
                return ServiceResult<TodoDto?>.Fail(ServiceError.Unauthorized("Token missing or invalid"));

            var todo = new Todo()
            {
                Title = request.Title,
                Description = request.Description ?? "",
                Finished = request.Finished ?? false,
                UserId = currentUserId
            };

            context.Todos.Add(todo);
            await context.SaveChangesAsync();

            return ServiceResult<TodoDto?>.Ok(new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Finished = todo.Finished
            });
        }

        public async Task<ServiceResult<TodoDto?>> UpdateAsync(int id, UpdateTodoDto request)
        {
            var currentUserId = httpContextAccessor.HttpContext?.User?.GetCurrentUserId();
            if (currentUserId is null)
                return ServiceResult<TodoDto?>.Fail(ServiceError.Unauthorized("Token missing or invalid"));

            var todo = await context.Todos
                .Where(t => t.Id == id && t.UserId == currentUserId)
                .FirstOrDefaultAsync();

            if (todo is null)
                return ServiceResult<TodoDto?>.Fail(ServiceError.NotFound("Todo not found"));

            if (request.Title is not null)
                todo.Title = request.Title;

            if (request.Description is not null)
                todo.Description = request.Description;

            if (request.Finished.HasValue)
                todo.Finished = request.Finished.Value;

            await context.SaveChangesAsync();

            return ServiceResult<TodoDto?>.Ok(new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Finished = todo.Finished
            });
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var currentUserId = httpContextAccessor.HttpContext?.User?.GetCurrentUserId();
            if (currentUserId is null)
                return ServiceResult<bool>.Fail(ServiceError.Unauthorized("Token missing or invalid"));

            var todo = await context.Todos
                .Where(t => t.Id == id && t.UserId == currentUserId)
                .FirstOrDefaultAsync();

            if (todo is null)
                return ServiceResult<bool>.Fail(ServiceError.NotFound("Todo not found"));

            context.Todos.Remove(todo);
            await context.SaveChangesAsync();

            return ServiceResult<bool>.Ok(true);
        }
    }
}
