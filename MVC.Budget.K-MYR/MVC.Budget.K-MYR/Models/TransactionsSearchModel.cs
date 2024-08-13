﻿namespace MVC.Budget.K_MYR.Models;

public class TransactionsSearchModel
{
    public string? SearchString { get; set; }
    public int? FiscalPlanId { get; set; }
    public int? CategoryId { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    public DateTime? MinDate { get; set; }
    public DateTime? MaxDate { get; set; }
}
