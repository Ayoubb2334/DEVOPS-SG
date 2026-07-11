using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Smartphones.Commands.UpdateSmartphone;

public class UpdateSmartphoneCommandHandler : IRequestHandler<UpdateSmartphoneCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSmartphoneCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateSmartphoneCommand request, CancellationToken cancellationToken)
    {
        var smartphone = await _unitOfWork.Smartphones.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Smartphone), request.Id);

        smartphone.Marque = request.Marque;
        smartphone.Modele = request.Modele;
        smartphone.Prix = request.Prix;
        smartphone.Stock = request.Stock;
        smartphone.Description = request.Description;
        smartphone.ImageUrl = request.ImageUrl;
        smartphone.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Smartphones.Update(smartphone);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}