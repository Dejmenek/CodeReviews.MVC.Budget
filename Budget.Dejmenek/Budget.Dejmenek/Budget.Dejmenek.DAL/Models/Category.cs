using System.ComponentModel.DataAnnotations;

namespace Budget.DAL.Models;
public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
