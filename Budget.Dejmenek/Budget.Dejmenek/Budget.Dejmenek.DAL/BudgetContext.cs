using Budget.DAL.Models;
using Budget.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace Budget.DAL;
public class BudgetContext : DbContext
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public BudgetContext(DbContextOptions<BudgetContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Transaction>()
            .Property(t => t.Type)
            .HasConversion<string>();

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Groceries", Description = "Grocery shopping" },
            new Category { CategoryId = 2, Name = "Utilities", Description = "Utility bills" }
        );

        modelBuilder.Entity<Transaction>().HasData(
            new Transaction { TransactionId = 1, CategoryId = 1, Name = "Walmart", Amount = 50.75M, Date = DateTime.Now, Type = TransactionType.Expense },
            new Transaction { TransactionId = 2, CategoryId = 2, Name = "Electricity Bill", Amount = 75.00M, Date = DateTime.Now, Type = TransactionType.Expense }
        );
    }
}
