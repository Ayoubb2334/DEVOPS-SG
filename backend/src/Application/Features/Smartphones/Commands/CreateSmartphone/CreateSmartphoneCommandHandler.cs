using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Smartphones.Commands.CreateSmartphone;

public class CreateSmartphoneCommandHandler : IRequestHandler<CreateSmartphoneCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSmartphoneCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateSmartphoneCommand request, CancellationToken cancellationToken)
    {
        var smartphone = new Smartphone
        {
            Marque = request.Marque,
            Modele = request.Modele,
            Prix = request.Prix,
            Stock = request.Stock,
            Description = request.Description,
            ImageUrl = request.ImageUrl
        };

        await _unitOfWork.Smartphones.AddAsync(smartphone, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return smartphone.Id;
    }
}