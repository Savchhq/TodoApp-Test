namespace TodoApp.BLL.DTOs;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}