using Budget.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Budget.Shared.DTOs;

public class TransactionViewDTO
{
    public int TransactionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}

public class TransactionCreateDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    [Display(Name = "Transaction Type")]
    public TransactionType Type { get; set; } = TransactionType.Expense;
    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
}

public class TransactionEditDTO
{
    public int TransactionId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    [Display(Name = "Transaction Type")]
    public TransactionType Type { get; set; }
    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
}
