using Budget.Shared.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Budget.UI.Models;

public class TransactionCreateViewModel
{
    public TransactionCreateDTO Transaction { get; set; } = null!;
    public List<SelectListItem> Categories { get; set; } = null!;
}
