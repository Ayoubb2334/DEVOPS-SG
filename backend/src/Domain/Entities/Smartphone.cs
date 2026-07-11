using Domain.Common;

namespace Domain.Entities;

public class Smartphone : BaseEntity
{
    public string Marque { get; set; } = string.Empty;
    public string Modele { get; set; } = string.Empty;
    public decimal Prix { get; set; }
    public int Stock { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}