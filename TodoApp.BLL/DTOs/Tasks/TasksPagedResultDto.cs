namespace TodoApp.BLL.DTOs;

public class TasksPagedResultDto
{
    public IEnumerable<TaskDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
