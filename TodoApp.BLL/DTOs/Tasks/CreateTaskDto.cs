using System.ComponentModel.DataAnnotations;

namespace TodoApp.BLL.DTOs;

public class CreateTaskDto
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
}