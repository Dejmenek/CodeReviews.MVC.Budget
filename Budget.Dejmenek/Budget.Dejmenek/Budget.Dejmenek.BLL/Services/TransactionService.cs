using Budget.BLL.Extensions;
using Budget.BLL.Interfaces;
using Budget.DAL;
using Budget.DAL.Models;
using Budget.Shared.DTOs;
using Budget.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace Budget.BLL.Services;
public class TransactionService : ITransactionService
{
    private readonly BudgetContext _context;
    private readonly ICategoryService _categoryService;
    public TransactionService(BudgetContext context, ICategoryService categoryService)
    {
        _context = context;
        _categoryService = categoryService;
    }

    public async Task<bool> AddTransactionAsync(TransactionCreateDTO transaction)
    {
        await _context.Transactions.AddAsync(transaction.ToModel());

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteTransactionAsync(int transactionId)
    {
        var transaction = await GetTransacionByIdAsync(transactionId);

        if (transaction == null) return false;

        _context.Transactions.Remove(transaction);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EditTransactionAsync(TransactionEditDTO transactionEdit)
    {
        var transaction = await GetTransacionByIdAsync(transactionEdit.TransactionId);

        if (transaction == null) return false;

        transaction.Name = transactionEdit.Name;
        transaction.Description = transactionEdit.Description;
        transaction.Amount = transactionEdit.Amount;
        transaction.Date = transactionEdit.Date;
        transaction.Type = transactionEdit.Type;
        transaction.CategoryId = transactionEdit.CategoryId;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<CategoryExpenseDTO>> GetAllCategoriesExpenseAsync()
    {
        return await _context.Transactions
            .AsNoTracking()
            .WhereTransactionType(TransactionType.Expense)
            .Include(t => t.Category)
            .GroupBy(t => t.Category.Name)
            .Select(g => new CategoryExpenseDTO
            {
                CategoryName = g.Key,
                TotalExpense = g.Select(t => t.Amount).Sum()
            })
            .ToListAsync();
    }

    public async Task<List<MonthSummaryDTO>> GetMonthlySummaryAsync()
    {
        int currentYear = DateTime.Now.Year;
        var data = await _context.Transactions
            .AsNoTracking()
            .Where(t => t.Date.Year == currentYear)
            .GroupBy(t => t.Date.Month)
            .Select(g => new
            {
                MonthNumber = g.Key,
                TotalExpense = g.Sum(t => t.Type == TransactionType.Expense ? t.Amount : 0),
                TotalIncome = g.Sum(t => t.Type == TransactionType.Income ? t.Amount : 0)
            })
            .ToListAsync();

        var months = Enumerable.Range(1, 12)
            .Select(m => new
            {
                MonthNumber = m,
                MonthName = new DateTime(currentYear, m, 1).ToString("MMMM"),
            });

        return months.GroupJoin(data, m => m.MonthNumber, d => d.MonthNumber, (m, d) => new MonthSummaryDTO
        {
            Month = m.MonthName,
            TotalExpense = d.FirstOrDefault()?.TotalExpense ?? 0,
            TotalIncome = d.FirstOrDefault()?.TotalIncome ?? 0
        }).ToList();
    }

    public async Task<List<TransactionViewDTO>> GetAllTransactionsAsync(
        SortOptions sortOption,
        int? categoryId,
        string? searchString,
        DateTime? startDate,
        DateTime? endDate)
    {
        var transactionsQuery = _context.Transactions.Include(t => t.Category).AsQueryable();

        if (!string.IsNullOrEmpty(searchString?.Trim()))
        {
            transactionsQuery = transactionsQuery.Where(t => t.Name.ToLower().Contains(searchString.ToLower()));
        }

        if (categoryId != null)
        {
            transactionsQuery = transactionsQuery.Where(t => t.CategoryId == categoryId);
        }

        if (startDate != null)
        {
            transactionsQuery = transactionsQuery.Where(t => t.Date >= startDate);
        }
        if (endDate != null)
        {
            transactionsQuery = transactionsQuery.Where(t => t.Date <= endDate);
        }

        transactionsQuery = sortOption switch
        {
            SortOptions.NameAsc => transactionsQuery.OrderBy(t => t.Name),
            SortOptions.NameDesc => transactionsQuery.OrderByDescending(t => t.Name),
            SortOptions.DateAsc => transactionsQuery.OrderBy(t => t.Date),
            SortOptions.DateDesc => transactionsQuery.OrderByDescending(t => t.Date),
            SortOptions.AmountAsc => transactionsQuery.OrderBy(t => t.Amount),
            SortOptions.AmountDesc => transactionsQuery.OrderByDescending(t => t.Amount),
            _ => transactionsQuery.OrderBy(t => t.Date)
        };

        return await transactionsQuery.AsNoTracking().SelectAsViewDTO().ToListAsync();
    }

    public async Task<decimal> GetTotalExpenseAsync()
        => await _context.Transactions
        .WhereTransactionType(TransactionType.Expense)
        .Select(t => t.Amount)
        .SumAsync();

    public async Task<decimal> GetTotalIncomeAsync()
        => await _context.Transactions
        .WhereTransactionType(TransactionType.Income)
        .Select(t => t.Amount)
        .SumAsync();

    public async Task<Transaction?> GetTransacionByIdAsync(int transactionId)
        => await _context.Transactions.FindAsync(transactionId);
}
