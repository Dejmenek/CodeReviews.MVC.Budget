using Budget.BLL.Extensions;
using Budget.BLL.Interfaces;
using Budget.Shared.DTOs;
using Budget.UI.Helpers;
using Budget.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Budget.UI.Controllers;
public class TransactionsController : Controller
{
    private readonly ITransactionService _transactionService;
    private readonly ICategoryService _categoryService;
    public TransactionsController(ITransactionService transactionService, ICategoryService categoryService)
    {
        _transactionService = transactionService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Create()
    {
        if (!Request.Headers.XRequestedWith.Equals("XMLHttpRequest"))
        {
            return RedirectToAction("Index", "Home");
        }

        List<SelectListItem> options = await SelectListHelper.GetCategorySelectListAsync(_categoryService);

        var model = new TransactionCreateViewModel
        {
            Transaction = new TransactionCreateDTO(),
            Categories = options
        };

        return PartialView("_TransactionCreateForm", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TransactionCreateViewModel model)
    {
        ModelState.Remove("Categories");
        if (ModelState.IsValid)
        {
            if (await _transactionService.AddTransactionAsync(model.Transaction))
            {
                return Json(new { success = true, message = "Transaction created successfully!" });
            }

            return Json(new { success = false, message = "Error occurred while adding the transaction." });
        }

        List<SelectListItem> options = await SelectListHelper.GetCategorySelectListAsync(_categoryService);
        model.Categories = options;
        return PartialView("_TransactionCreateForm", model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (!Request.Headers.XRequestedWith.Equals("XMLHttpRequest"))
        {
            return RedirectToAction("Index", "Home");
        }

        var transaction = await _transactionService.GetTransacionByIdAsync(id);

        if (transaction == null) return Json(new { success = false });

        List<SelectListItem> options = await SelectListHelper.GetCategorySelectListAsync(_categoryService);

        var model = new TransactionEditViewModel
        {
            Transaction = transaction.ToEditDTO(),
            Categories = options
        };

        return PartialView("_TransactionEditForm", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TransactionEditViewModel model)
    {
        ModelState.Remove("Categories");
        if (ModelState.IsValid)
        {
            if (await _transactionService.EditTransactionAsync(model.Transaction))
            {
                return Json(new { success = true, message = "Tranasction edited successfully!" });
            }

            return Json(new { success = false, message = "Error occured while editing the transaction." });
        }

        List<SelectListItem> options = await SelectListHelper.GetCategorySelectListAsync(_categoryService);
        model.Categories = options;
        return PartialView("_TransactionEditForm", model);
    }

    public IActionResult Delete()
    {
        if (!Request.Headers.XRequestedWith.Equals("XMLHttpRequest"))
        {
            return RedirectToAction("Index", "Home");
        }

        return PartialView("_TransactionDeleteForm");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        if (!Request.Headers.XRequestedWith.Equals("XMLHttpRequest"))
        {
            return RedirectToAction("Index", "Home");
        }

        if (await _transactionService.DeleteTransactionAsync(id))
        {
            return Json(new { success = true, message = "Transaction deleted successfully!" });
        }

        return Json(new { success = false, message = "Error occurred while deleting the transaction." });
    }
}
