using Budget.DAL.Models;
using Budget.Shared.DTOs;

namespace Budget.BLL.Extensions;
public static class TransactionMapping
{
    public static TransactionViewDTO ToViewDTO(this Transaction transaction)
    {
        return new TransactionViewDTO
        {
            TransactionId = transaction.TransactionId,
            Name = transaction.Name,
            Description = transaction.Description,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Type = transaction.Type,
            CategoryName = transaction.Category.Name
        };
    }

    public static TransactionEditDTO ToEditDTO(this Transaction transaction)
    {
        return new TransactionEditDTO
        {
            TransactionId = transaction.TransactionId,
            Name = transaction.Name,
            Description = transaction.Description,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Type = transaction.Type,
            CategoryId = transaction.CategoryId
        };
    }

    public static Transaction ToModel(this TransactionCreateDTO transaction, int id = 0)
    {
        return new Transaction
        {
            TransactionId = id,
            CategoryId = transaction.CategoryId,
            Name = transaction.Name,
            Description = transaction.Description,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Type = transaction.Type
        };
    }
}
