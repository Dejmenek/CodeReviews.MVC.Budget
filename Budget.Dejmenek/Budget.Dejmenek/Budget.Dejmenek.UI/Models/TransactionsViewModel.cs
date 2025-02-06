using Budget.Shared.DTOs;
using Budget.Shared.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Budget.UI.Models;

public class TransactionsViewModel
{
    public List<TransactionViewDTO> Transactions { get; set; } = new List<TransactionViewDTO>();
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public List<MonthSummaryDTO> MonthlySummary { get; set; } = new List<MonthSummaryDTO>();
    public List<CategoryExpenseDTO> CategoryExpenses { get; set; } = new List<CategoryExpenseDTO>();
    public DateTime? TransactionStartDate { get; set; }
    public DateTime? TransactionEndDate { get; set; }
    public string SearchString { get; set; } = string.Empty;
    public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    [Display(Name = "Category")]
    public int? CategoryId { get; set; }
    public SortOptions SortOption { get; set; }
}
