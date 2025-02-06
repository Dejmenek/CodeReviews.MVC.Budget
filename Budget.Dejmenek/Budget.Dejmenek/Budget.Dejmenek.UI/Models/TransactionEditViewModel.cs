using Budget.Shared.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Budget.UI.Models;

public class TransactionEditViewModel
{
    public TransactionEditDTO Transaction { get; set; } = null!;
    public List<SelectListItem> Categories { get; set; } = null!;
}
