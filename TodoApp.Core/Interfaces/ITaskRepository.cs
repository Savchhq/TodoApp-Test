using TodoApp.Core.Models;

namespace TodoApp.Core.Interfaces;

public interface ITaskRepository
{
    Task<(IEnumerable<TodoTask> Items, int TotalCount)> GetAllAsync(
        Guid userId, 
        string? searchQuery = null, 
        Guid? categoryId = null, 
        int pageNumber = 1, 
        int pageSize = 10);
        
    Task<TodoTask?> GetByIdAsync(Guid id, Guid userId);
    Task<TodoTask> CreateAsync(TodoTask todoTask);
    Task<TodoTask?> UpdateAsync(Guid id, TodoTask todoTask, Guid userId);
    Task<TodoTask?> DeleteAsync(Guid id, Guid userId);
}