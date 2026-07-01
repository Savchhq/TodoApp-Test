using System.ComponentModel.DataAnnotations;

namespace TodoApp.BLL.DTOs;

public class CreateUpdateCategoryDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
}