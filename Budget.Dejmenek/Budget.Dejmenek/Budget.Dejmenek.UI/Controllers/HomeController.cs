using Budget.BLL.Interfaces;
using Budget.Models;
using Budget.Shared.Enums;
using Budget.UI.Helpers;
using Budget.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Budget.Controllers;
public class HomeController : Controller
{
    private readonly ITransactionService _transactionService;
    private readonly ICategoryService _categoryService;

    public HomeController(ITransactionService transactionService, ICategoryService categoryService)
    {
        _transactionService = transactionService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index(
        int? categoryId,
        string? searchString,
        DateTime? transactionStartDate,
        DateTime? transactionEndDate,
        SortOptions sortOption = SortOptions.DateAsc)
    {
        var model = new TransactionsViewModel
        {
            Transactions = await _transactionService.GetAllTransactionsAsync(sortOption, categoryId, searchString, transactionStartDate, transactionEndDate),
            TotalIncome = await _transactionService.GetTotalIncomeAsync(),
            TotalExpense = await _transactionService.GetTotalExpenseAsync(),
            CategoryExpenses = await _transactionService.GetAllCategoriesExpenseAsync(),
            MonthlySummary = await _transactionService.GetMonthlySummaryAsync(),
            TransactionStartDate = transactionStartDate,
            TransactionEndDate = transactionEndDate,
            SearchString = searchString ?? "",
            CategoryId = categoryId,
            Categories = await SelectListHelper.GetCategorySelectListAsync(_categoryService),
            SortOption = sortOption
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
