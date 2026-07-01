using TodoApp.Core.Models;

namespace TodoApp.Core.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllByUserIdAsync(Guid userId);
    Task<Category?> GetByIdAsync(Guid id, Guid userId);
    Task<Category> CreateAsync(Category category);
    Task<Category?> UpdateAsync(Guid id, Category category, Guid userId);
    Task<Category?> DeleteAsync(Guid id, Guid userId);
}