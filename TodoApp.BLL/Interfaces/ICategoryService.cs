using TodoApp.BLL.DTOs;


namespace TodoApp.BLL.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllByUserIdAsync(Guid userId);
    Task<CategoryDto?> GetByIdAsync(Guid id, Guid userId);
    Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto categoryDto, Guid userId);
    Task<CategoryDto?> UpdateAsync(Guid id, CreateUpdateCategoryDto categoryDto, Guid userId);
    Task<CategoryDto?> DeleteAsync(Guid id, Guid userId);
}