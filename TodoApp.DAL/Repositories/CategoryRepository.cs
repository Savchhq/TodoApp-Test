using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Interfaces;
using TodoApp.Core.Models;
using TodoApp.DAL.Data;

namespace TodoApp.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TodoAppDbContext dbContext;
        public CategoryRepository(TodoAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllByUserIdAsync(Guid userId)
        {
            return await dbContext.Categories.Include(c => c.Tasks).Where(c => c.UserId == userId) .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id, Guid userId)
        {
            return await dbContext.Categories.Include(c => c.Tasks).FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }
        public async Task<Category> CreateAsync(Category category)
        {
            category.Id = Guid.NewGuid();
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync(); 
            return category;
        }
        public async Task<Category?> UpdateAsync(Guid id, Category category, Guid userId)
        {
            var existing = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (existing == null) 
            return null;

            existing.Name = category.Name;
            await dbContext.SaveChangesAsync();
            return existing;
        }
        public async Task<Category?> DeleteAsync(Guid id, Guid userId)
        {
            var category = await GetByIdAsync(id, userId);
            if (category == null) return null;

            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
            return category;
        }
    
    }
}