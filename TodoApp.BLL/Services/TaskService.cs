using AutoMapper;
using TodoApp.BLL.DTOs;
using TodoApp.BLL.Interfaces;
using TodoApp.Core.Interfaces;
using TodoApp.Core.Models;

namespace TodoApp.BLL.Services;

public class TaskService : ITaskService
{
    private readonly IMapper mapper;
    private readonly ITaskRepository taskRepository;
    private readonly ICategoryRepository categoryRepository;

    public TaskService(IMapper mapper, ITaskRepository taskRepository, ICategoryRepository categoryRepository)
    {
        this.mapper = mapper;
        this.taskRepository = taskRepository;
        this.categoryRepository = categoryRepository;
    }
    public async Task<(IEnumerable<TaskDto> Items, int TotalCount)> GetAllAsync(Guid userId, string? searchQuery = null, Guid? categoryId = null, int pageNumber = 1, int pageSize = 10)
    {
        var tasks = await  taskRepository.GetAllAsync(userId, searchQuery, categoryId, pageNumber, pageSize);
        var dtos = mapper.Map<IEnumerable<TaskDto>>(tasks.Items);
        return (dtos, tasks.TotalCount);
    }
    public async Task<TaskDto?> GetByIdAsync(Guid id, Guid userId)
    {
        var task = await taskRepository.GetByIdAsync(id, userId);
        if(task == null)
        return null;
        return mapper.Map<TaskDto>(task);
    }
    public async Task<TaskDto> CreateAsync(CreateTaskDto taskDto, Guid userId)
    {
        await EnsureCategoryBelongsToUserAsync(taskDto.CategoryId, userId);

        var task = mapper.Map<TodoTask>(taskDto);
        task.UserId = userId;
        task = await taskRepository.CreateAsync(task);
        return mapper.Map<TaskDto>(task);
    }
    public async Task<TaskDto?> UpdateAsync(Guid id, UpdateTaskDto taskDto, Guid userId)
    {
        await EnsureCategoryBelongsToUserAsync(taskDto.CategoryId, userId);

        var task = mapper.Map<TodoTask>(taskDto);

        var updatedTask = await taskRepository.UpdateAsync(id, task, userId);

        if (updatedTask == null)
        return null;

        return mapper.Map<TaskDto>(updatedTask);
    }
    public async Task<TaskDto?> DeleteAsync(Guid id, Guid userId)
    {
        var deletedTask = await taskRepository.DeleteAsync(id, userId);
    
        if (deletedTask == null)
        return null;
    
        return mapper.Map<TaskDto>(deletedTask);
    }

    private async Task EnsureCategoryBelongsToUserAsync(Guid categoryId, Guid userId)
    {
        if (categoryId == Guid.Empty)
        {
            throw new ArgumentException("CategoryId is required.");
        }

        var category = await categoryRepository.GetByIdAsync(categoryId, userId);
        if (category == null)
        {
            throw new ArgumentException("Category not found or does not belong to the current user.");
        }
    }
}