using Budget.BLL.Extensions;
using Budget.BLL.Interfaces;
using Budget.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Budget.UI.Controllers;
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetlAllCategoriesAsync();
        return View(categories.ToList());
    }

    public IActionResult Create()
    {
        if (!Request.Headers.XRequestedWith.Equals("XMLHttpRequest"))
        {
            return RedirectToAction("Index", "Home");
        }

        return PartialView("_CategoryCreateForm", new CategoryCreateDTO());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryCreateDTO categoryCreate)
    {
        if (ModelState.IsValid)
        {
            if (await _categoryService.AddCategoryAsync(categoryCreate))
            {
                return Json(new { success = true, message = "Category created successfully!" });
            }
            else
            {
                return Json(new { success = false, message = "Error occurred while adding the category." });
            }
        }

        return PartialView("_CategoryCreateForm", categoryCreate);
    }

    public async Task<IActionResult> Edit(int categoryId)
    {
        if (!Request.Headers.XRequestedWith.Equals("XMLHttpRequest"))
        {
            return RedirectToAction("Index", "Home");
        }

        var category = await _categoryService.GetCategoryByIdAsync(categoryId);

        if (category == null) return Json(new
        {
            success = false,
            message = "Invalid category id"
        });

        return PartialView("_CategoryEditForm", category.ToEditDTO());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CategoryEditDTO categoryEdit)
    {
        if (ModelState.IsValid)
        {
            if (await _categoryService.EditCategoryAsync(categoryEdit))
            {
                return Json(new { success = true, message = "Category updated successfully!" });
            }
            else
            {
                return Json(new { success = false, message = "Error occurred while updating the category." });
            }
        }

        return PartialView("_CategoryEditForm", categoryEdit);
    }

    public IActionResult Delete()
    {
        if (!Request.Headers.XRequestedWith.Equals("XMLHttpRequest"))
        {
            return RedirectToAction("Index", "Home");
        }

        return PartialView("_CategoryDeleteForm");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        if (!Request.Headers.XRequestedWith.Equals("XMLHttpRequest"))
        {
            return RedirectToAction("Index", "Home");
        }

        if (await _categoryService.DeleteCategoryAsync(id))
        {
            return Json(new { success = true, message = "Category deleted successfully!" });
        }

        return Json(new { success = false, message = "Error occurred while deleting the category." });
    }
}
