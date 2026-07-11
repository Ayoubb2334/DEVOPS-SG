using Application.DTOs;
using MediatR;

namespace Application.Features.Smartphones.Queries.GetAllSmartphones;

public class GetAllSmartphonesQuery : IRequest<List<SmartphoneDto>>
{
}