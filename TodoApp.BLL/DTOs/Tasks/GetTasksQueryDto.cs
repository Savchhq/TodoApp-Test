using System.ComponentModel.DataAnnotations;

namespace TodoApp.BLL.DTOs;

public class GetTasksQueryDto
{
    [StringLength(100)]
    public string? SearchQuery { get; set; }

    public Guid? CategoryId { get; set; }

    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    [Range(1, 100)]
    public int PageSize { get; set; } = 10;
}
