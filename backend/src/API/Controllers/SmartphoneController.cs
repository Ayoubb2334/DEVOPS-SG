using Application.DTOs;
using Application.Features.Smartphones.Commands.CreateSmartphone;
using Application.Features.Smartphones.Commands.DeleteSmartphone;
using Application.Features.Smartphones.Commands.UpdateSmartphone;
using Application.Features.Smartphones.Queries.GetAllSmartphones;
using Application.Features.Smartphones.Queries.GetSmartphoneById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/smartphone")]
public class SmartphoneController : ControllerBase
{
    private readonly IMediator _mediator;

    public SmartphoneController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<SmartphoneDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllSmartphonesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SmartphoneDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetSmartphoneByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateSmartphoneCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateSmartphoneCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id) return BadRequest("L'identifiant ne correspond pas.");
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteSmartphoneCommand(id), cancellationToken);
        return NoContent();
    }
}