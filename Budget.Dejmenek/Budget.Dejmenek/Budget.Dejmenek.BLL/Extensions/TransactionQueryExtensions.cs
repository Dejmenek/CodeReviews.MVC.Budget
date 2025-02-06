using Budget.DAL.Models;
using Budget.Shared.DTOs;
using Budget.Shared.Enums;

namespace Budget.BLL.Extensions;
public static class TransactionQueryExtensions
{
    public static IQueryable<Transaction> WhereTransactionType(this IQueryable<Transaction> query, TransactionType type)
        => query.Where(t => t.Type == type);

    public static IQueryable<TransactionViewDTO> SelectAsViewDTO(this IQueryable<Transaction> transactions)
        => transactions.Select(t => t.ToViewDTO());
}
