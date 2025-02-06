using Budget.DAL.Models;
using Budget.Shared.DTOs;

namespace Budget.BLL.Extensions;
public static class CategoryQueryExtensions
{
    public static IQueryable<CategoryViewDTO> SelectAsViewDTO(this IQueryable<Category> categories)
        => categories.Select(c => c.ToViewDTO());
}
