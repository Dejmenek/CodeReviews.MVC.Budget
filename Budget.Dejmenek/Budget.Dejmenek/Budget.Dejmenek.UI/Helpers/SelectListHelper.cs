using Budget.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Budget.UI.Helpers;

public static class SelectListHelper
{
    public static async Task<List<SelectListItem>> GetCategorySelectListAsync(ICategoryService categoryService)
    {
        List<SelectListItem> options = new List<SelectListItem>();

        var categories = await categoryService.GetlAllCategoriesAsync();
        foreach (var category in categories)
        {
            options.Add(new SelectListItem
            {
                Value = category.CategoryId.ToString(),
                Text = category.Name
            });
        }

        return options;
    }
}
