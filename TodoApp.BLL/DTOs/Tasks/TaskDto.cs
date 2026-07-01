namespace TodoApp.BLL.DTOs;
public class TaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } 
    
    public Guid CategoryId { get; set; }
    public string? CategoryName { get; set; }
}
