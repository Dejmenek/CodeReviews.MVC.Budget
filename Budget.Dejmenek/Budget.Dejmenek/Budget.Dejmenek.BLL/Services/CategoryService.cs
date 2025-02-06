using Budget.BLL.Extensions;
using Budget.BLL.Interfaces;
using Budget.DAL;
using Budget.DAL.Models;
using Budget.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Budget.BLL.Services;
public class CategoryService : ICategoryService
{
    private readonly BudgetContext _context;
    public CategoryService(BudgetContext context)
    {
        _context = context;
    }

    public async Task<bool> AddCategoryAsync(CategoryCreateDTO category)
    {
        if (await CategoryWithNameExists(category.Name)) return false;

        await _context.Categories.AddAsync(category.ToModel());

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var category = await GetCategoryByIdAsync(categoryId);

        if (category == null) return false;

        _context.Categories.Remove(category);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EditCategoryAsync(CategoryEditDTO categoryEdit)
    {
        var category = await GetCategoryByIdAsync(categoryEdit.CategoryId);

        if (category == null) return false;

        category.Name = categoryEdit.Name;
        category.Description = categoryEdit.Description;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Category?> GetCategoryByIdAsync(int categoryId) => await _context.Categories.FindAsync(categoryId);

    public async Task<List<CategoryViewDTO>> GetlAllCategoriesAsync()
    {
        return await _context.Categories.SelectAsViewDTO().ToListAsync();
    }

    public async Task<bool> CategoryExists(int categoryId)
        => await _context.Categories.FindAsync(categoryId) != null;

    public async Task<bool> CategoryWithNameExists(string name)
        => await _context.Categories.FirstOrDefaultAsync(c => c.Name == name) != null;
}
