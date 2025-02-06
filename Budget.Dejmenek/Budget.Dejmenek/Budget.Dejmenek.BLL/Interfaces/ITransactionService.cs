using Budget.DAL.Models;
using Budget.Shared.DTOs;
using Budget.Shared.Enums;

namespace Budget.BLL.Interfaces;
public interface ITransactionService
{
    Task<bool> AddTransactionAsync(TransactionCreateDTO transaction);
    Task<bool> DeleteTransactionAsync(int transactionId);
    Task<bool> EditTransactionAsync(TransactionEditDTO transaction);
    Task<List<TransactionViewDTO>> GetAllTransactionsAsync(SortOptions sortOption, int? categoryId, string? searchString, DateTime? startDate, DateTime? endDate);
    Task<Transaction?> GetTransacionByIdAsync(int transactionId);
    Task<decimal> GetTotalExpenseAsync();
    Task<decimal> GetTotalIncomeAsync();
    Task<List<CategoryExpenseDTO>> GetAllCategoriesExpenseAsync();
    Task<List<MonthSummaryDTO>> GetMonthlySummaryAsync();
}
