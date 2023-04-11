using MediatR;
using StaffManagement.Application.SharedKernel;
using StaffManagement.Domain.Employees;

namespace StaffManagement.Application.Employees.Commands;

public record DeleteEmployeeCommand(Guid Id) : IRequest;

internal class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(request.Id);

        if (employee is null)
        {
            throw new NotFoundException("Employee not found");
        }

        await _employeeRepository.Delete(employee);
    }
}