using Budget.DAL.Models;
using Budget.Shared.DTOs;

namespace Budget.BLL.Extensions;
public static class CategoryMapping
{
    public static CategoryViewDTO ToViewDTO(this Category category)
    {
        return new CategoryViewDTO
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            Description = category.Description,
        };
    }

    public static CategoryEditDTO ToEditDTO(this Category category)
    {
        return new CategoryEditDTO
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            Description = category.Description,
        };
    }

    public static Category ToModel(this CategoryCreateDTO category, int id = 0)
    {
        return new Category
        {
            CategoryId = id,
            Name = category.Name,
            Description = category.Description,
        };
    }

    public static List<CategoryViewDTO> ToViewDTOList(this List<Category> categories)
        => categories.Select(c => c.ToViewDTO()).ToList();
}
