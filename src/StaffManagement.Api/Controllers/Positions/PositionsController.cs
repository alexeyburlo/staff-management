using MediatR;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Application.Positions.Commands;
using StaffManagement.Application.Positions.Queries;

namespace StaffManagement.Controllers.Positions;

[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PositionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetPositionsCommand());

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetPositionCommand(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePositionRequest request)
    {
        await _mediator.Send(new CreatePositionCommand(request.Title, request.Grade));

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePositionRequest request)
    {
        await _mediator.Send(new UpdatePositionCommand(id, request.Title, request.Grade));

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeletePositionCommand(id));

        return Ok();
    }
}