using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Smartphones.Commands.DeleteSmartphone;

public class DeleteSmartphoneCommandHandler : IRequestHandler<DeleteSmartphoneCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSmartphoneCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteSmartphoneCommand request, CancellationToken cancellationToken)
    {
        var smartphone = await _unitOfWork.Smartphones.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Smartphone), request.Id);

        _unitOfWork.Smartphones.Delete(smartphone);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}