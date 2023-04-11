using MediatR;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Application.EmployeePositions.Commands;
using StaffManagement.Application.Employees.Commands;
using StaffManagement.Application.Employees.Queries;

namespace StaffManagement.Controllers.Employees;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetEmployeesCommand());

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeCommand(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request)
    {
        await _mediator.Send(new CreateEmployeeCommand(request.Surname, request.Name, request.Patronymic,
            request.BirthDate));

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateEmployeeRequest request)
    {
        await _mediator.Send(new UpdateEmployeeCommand(id, request.Surname, request.Name, request.Patronymic,
            request.BirthDate));

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteEmployeeCommand(id));

        return Ok();
    }

    [HttpPost("{id:guid}/positions")]
    public async Task<IActionResult> CreateEmployeePosition([FromRoute] Guid id,
        [FromBody] CreateEmployeePositionRequest request)
    {
        await _mediator.Send(new CreateEmployeePositionCommand(id, request.PositionId));

        return Ok();
    }

    [HttpDelete("{id:guid}/positions/{positionId:guid}")]
    public async Task<IActionResult> DeleteEmployeePosition([FromRoute] Guid id, [FromRoute] Guid positionId)
    {
        await _mediator.Send(new DeleteEmployeePositionCommand(id, positionId));

        return Ok();
    }
}