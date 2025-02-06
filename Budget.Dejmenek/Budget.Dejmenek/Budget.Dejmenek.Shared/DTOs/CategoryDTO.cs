namespace Budget.Shared.DTOs;

public class CategoryCreateDTO
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CategoryEditDTO
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CategoryViewDTO
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}