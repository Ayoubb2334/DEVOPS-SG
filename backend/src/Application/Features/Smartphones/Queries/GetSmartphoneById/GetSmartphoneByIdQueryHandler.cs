using Application.Common.Exceptions;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Smartphones.Queries.GetSmartphoneById;

public class GetSmartphoneByIdQueryHandler : IRequestHandler<GetSmartphoneByIdQuery, SmartphoneDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSmartphoneByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SmartphoneDto> Handle(GetSmartphoneByIdQuery request, CancellationToken cancellationToken)
    {
        var smartphone = await _unitOfWork.Smartphones.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Smartphone), request.Id);

        return _mapper.Map<SmartphoneDto>(smartphone);
    }
}