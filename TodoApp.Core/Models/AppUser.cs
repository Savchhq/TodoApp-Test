using Microsoft.AspNetCore.Identity;

namespace TodoApp.Core.Models;

public class AppUser : IdentityUser<Guid>
{
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<TodoTask> Tasks { get; set; } = new List<TodoTask>();
}