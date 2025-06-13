using PAS_1.Libs;
using PAS_1.Models;

namespace PAS_1.Services
{
    public interface ITodoService
    {
        Task<ServiceResult<IEnumerable<TodoDto>>> GetTodosAsync(string? sort, bool? finished);
        Task<ServiceResult<TodoDto?>> GetTodoAsync(int id);
        Task<ServiceResult<TodoDto?>> CreateAsync(CreateTodoDto request);
        Task<ServiceResult<TodoDto?>> UpdateAsync(int id, UpdateTodoDto request);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}
