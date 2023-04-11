using MediatR;
using StaffManagement.Application.SharedKernel;
using StaffManagement.Domain.Employees;

namespace StaffManagement.Application.Employees.Commands;

public record UpdateEmployeeCommand
    (Guid Id, string Surname, string Name, string Patronymic, DateTime BirthDate) : IRequest;

internal class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(request.Id);

        if (employee is null)
        {
            throw new NotFoundException("Employee not found");
        }

        employee.Update(request.Surname, request.Name, request.Patronymic, request.BirthDate);

        await _employeeRepository.Update(employee);
    }
}