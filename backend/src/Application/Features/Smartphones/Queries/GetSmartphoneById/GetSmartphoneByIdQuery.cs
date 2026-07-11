using Application.DTOs;
using MediatR;

namespace Application.Features.Smartphones.Queries.GetSmartphoneById;

public class GetSmartphoneByIdQuery : IRequest<SmartphoneDto>
{
    public Guid Id { get; set; }

    public GetSmartphoneByIdQuery(Guid id)
    {
        Id = id;
    }
}