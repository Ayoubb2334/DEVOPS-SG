using MediatR;

namespace Application.Features.Smartphones.Commands.DeleteSmartphone;

public class DeleteSmartphoneCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public DeleteSmartphoneCommand(Guid id)
    {
        Id = id;
    }
}