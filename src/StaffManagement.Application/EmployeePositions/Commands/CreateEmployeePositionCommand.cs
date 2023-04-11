using MediatR;
using StaffManagement.Domain.EmployeePositions;

namespace StaffManagement.Application.EmployeePositions.Commands;

public record CreateEmployeePositionCommand(Guid EmployeeId, Guid PositionId) : IRequest;

internal class CreateEmployeePositionCommandHandler : IRequestHandler<CreateEmployeePositionCommand>
{
    private readonly IEmployeePositionRepository _employeePositionRepository;

    public CreateEmployeePositionCommandHandler(IEmployeePositionRepository employeePositionRepository)
    {
        _employeePositionRepository = employeePositionRepository;
    }

    public async Task Handle(CreateEmployeePositionCommand request, CancellationToken cancellationToken)
    {
        await _employeePositionRepository.Create(request.EmployeeId, request.PositionId);
    }
}