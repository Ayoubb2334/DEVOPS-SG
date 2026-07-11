using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Smartphones.Queries.GetAllSmartphones;

public class GetAllSmartphonesQueryHandler : IRequestHandler<GetAllSmartphonesQuery, List<SmartphoneDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllSmartphonesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SmartphoneDto>> Handle(GetAllSmartphonesQuery request, CancellationToken cancellationToken)
    {
        var smartphones = await _unitOfWork.Smartphones.GetAllAsync(cancellationToken);
        return _mapper.Map<List<SmartphoneDto>>(smartphones);
    }
}