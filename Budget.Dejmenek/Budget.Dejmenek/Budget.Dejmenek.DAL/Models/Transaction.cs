using Budget.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget.DAL.Models;
public class Transaction
{
    [Key]
    public int TransactionId { get; set; }
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    [Required]
    [Column(TypeName = "smalldatetime")]
    public DateTime Date { get; set; }
    [Required]
    public TransactionType Type { get; set; }
    public Category Category { get; set; } = null!;
}
