using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Interfaces;
using TodoApp.Core.Models;
using TodoApp.DAL.Data;

namespace TodoApp.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoAppDbContext dbContext;
        public TaskRepository(TodoAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(IEnumerable<TodoTask> Items, int TotalCount)> GetAllAsync(Guid userId, string? searchQuery = null, Guid? categoryId = null, int pageNumber = 1, int pageSize = 10)
        {
            var tasks = dbContext.TodoTasks.Include(t => t.Category).Where(c => c.UserId == userId).AsQueryable();

            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                tasks = tasks.Where(t => t.Title.Contains(searchQuery));
            }

            if (categoryId.HasValue)
            {
                tasks = tasks.Where(x => x.CategoryId == categoryId.Value);
            }

            var totalCount = await tasks.CountAsync();
            var skipResults = (pageNumber - 1) * pageSize;
            var items = await tasks.Skip(skipResults).Take(pageSize).ToListAsync();

            return(items, totalCount);
        }

        public async Task<TodoTask?> GetByIdAsync(Guid id, Guid userId)
        {
            return await dbContext.TodoTasks.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }
        public async Task<TodoTask> CreateAsync(TodoTask todoTask)
        {
            todoTask.Id = Guid.NewGuid();
            await dbContext.TodoTasks.AddAsync(todoTask);
            await dbContext.SaveChangesAsync(); 
            return todoTask;
        }
        public async Task<TodoTask?> UpdateAsync(Guid id, TodoTask todoTask, Guid userId)
        {
            var existing = await dbContext.TodoTasks.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (existing == null) 
            return null;

            existing.Title = todoTask.Title;
            existing.Description = todoTask.Description;
            existing.IsCompleted = todoTask.IsCompleted;
            existing.CategoryId = todoTask.CategoryId;
            await dbContext.SaveChangesAsync();
            return existing;
        }
        public async Task<TodoTask?> DeleteAsync(Guid id, Guid userId)
        {
            var todoTask = await dbContext.TodoTasks.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (todoTask == null) return null;

            dbContext.TodoTasks.Remove(todoTask);
            await dbContext.SaveChangesAsync();
            return todoTask;
        }

    }
}