using TodoApp.BLL.DTOs;

namespace TodoApp.BLL.Interfaces;

public interface ITaskService
{
    Task<(IEnumerable<TaskDto> Items, int TotalCount)> GetAllAsync(Guid userId, string? searchQuery = null, Guid? categoryId = null, int pageNumber = 1, int pageSize = 10);
    
    Task<TaskDto?> GetByIdAsync(Guid id, Guid userId);
    
    Task<TaskDto> CreateAsync(CreateTaskDto taskDto, Guid userId);
    
    Task<TaskDto?> UpdateAsync(Guid id, UpdateTaskDto taskDto, Guid userId);
    
    Task<TaskDto?> DeleteAsync(Guid id, Guid userId);
}