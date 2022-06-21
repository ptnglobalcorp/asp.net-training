using AwesomeBlog.Core.Services.Categories.Dtos;

namespace AwesomeBlog.Core.Services.Categories;

public interface ICategoryAppService
{
    Task<CategoryDto> Create(CreateCategoryDto input);
}