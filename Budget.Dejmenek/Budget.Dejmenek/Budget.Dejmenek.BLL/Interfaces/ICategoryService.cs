using Budget.DAL.Models;
using Budget.Shared.DTOs;

namespace Budget.BLL.Interfaces;
public interface ICategoryService
{
    Task<bool> AddCategoryAsync(CategoryCreateDTO category);
    Task<bool> DeleteCategoryAsync(int categoryId);
    Task<bool> EditCategoryAsync(CategoryEditDTO category);
    Task<List<CategoryViewDTO>> GetlAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int categoryId);
    Task<bool> CategoryExists(int categoryId);
    Task<bool> CategoryWithNameExists(string name);
}
