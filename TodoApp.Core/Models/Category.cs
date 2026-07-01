namespace TodoApp.Core.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; } 
    public AppUser? User { get; set; }

    public ICollection<TodoTask> Tasks { get; set; } = new List<TodoTask>();
}