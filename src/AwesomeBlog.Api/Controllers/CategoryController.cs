using AwesomeBlog.Core.Services.Categories;
using AwesomeBlog.Core.Services.Categories.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeBlog.Api.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryAppService _categoryAppService;

    public CategoryController(ICategoryAppService categoryAppService)
    {
        _categoryAppService = categoryAppService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryDto input)
    {
        if (input.Name == null) return BadRequest();
        return Ok(await _categoryAppService.Create(input));
    }
}