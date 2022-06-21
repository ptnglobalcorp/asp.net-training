using AwesomeBlog.Core.Entities;
using AwesomeBlog.Core.Interfaces;
using AwesomeBlog.Core.Services.Categories.Dtos;

namespace AwesomeBlog.Core.Services.Categories;

public class CategoryAppService : ICategoryAppService
{
    private readonly IRepository<Category> _categoryRepository;

    public CategoryAppService(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> Create(CreateCategoryDto input)
    {
        var category = Map(input);
        var entity = await _categoryRepository.AddAsync(category, CancellationToken.None);
        return Map(entity);
    }

    private Category Map(CreateCategoryDto dto)
    {
        return new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };
    }

    private CategoryDto Map(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }
}