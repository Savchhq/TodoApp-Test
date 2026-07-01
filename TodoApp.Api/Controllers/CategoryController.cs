using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.BLL.DTOs;
using TodoApp.BLL.Interfaces;

namespace TodoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var categories = await categoryService.GetAllByUserIdAsync(userId);
            return Ok(categories);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var userId = GetUserId();
            var category = await categoryService.GetByIdAsync(id, userId);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Add([FromBody] CreateUpdateCategoryDto categoryDto)
        {
            var userId = GetUserId();
            var category = await categoryService.CreateAsync(categoryDto, userId);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateUpdateCategoryDto categoryDto)
        {
            var userId = GetUserId();
            var category = await categoryService.UpdateAsync(id, categoryDto, userId);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var userId = GetUserId();
            var category = await categoryService.DeleteAsync(id, userId);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        private Guid GetUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userIdString!);
        }
    }
}