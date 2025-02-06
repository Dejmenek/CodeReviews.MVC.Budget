namespace Budget.Shared.DTOs;
public class MonthSummaryDTO
{
    public string Month { get; set; } = string.Empty;
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
}
