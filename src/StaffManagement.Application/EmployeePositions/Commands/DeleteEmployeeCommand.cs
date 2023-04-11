using MediatR;
using StaffManagement.Domain.EmployeePositions;

namespace StaffManagement.Application.EmployeePositions.Commands;

public record DeleteEmployeePositionCommand(Guid EmployeeId, Guid PositionId) : IRequest;

internal class DeleteEmployeePositionCommandHandler : IRequestHandler<DeleteEmployeePositionCommand>
{
    private readonly IEmployeePositionRepository _employeePositionRepository;

    public DeleteEmployeePositionCommandHandler(IEmployeePositionRepository employeePositionRepository)
    {
        _employeePositionRepository = employeePositionRepository;
    }

    public async Task Handle(DeleteEmployeePositionCommand request, CancellationToken cancellationToken)
    {
        await _employeePositionRepository.Delete(request.EmployeeId, request.PositionId);
    }
}