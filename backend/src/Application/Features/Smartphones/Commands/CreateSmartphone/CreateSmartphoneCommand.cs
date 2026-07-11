using MediatR;

namespace Application.Features.Smartphones.Commands.CreateSmartphone;

public class CreateSmartphoneCommand : IRequest<Guid>
{
    public string Marque { get; set; } = string.Empty;
    public string Modele { get; set; } = string.Empty;
    public decimal Prix { get; set; }
    public int Stock { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}