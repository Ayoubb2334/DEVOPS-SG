namespace Application.DTOs;

public class SmartphoneDto
{
    public Guid Id { get; set; }
    public string Marque { get; set; } = string.Empty;
    public string Modele { get; set; } = string.Empty;
    public decimal Prix { get; set; }
    public int Stock { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}