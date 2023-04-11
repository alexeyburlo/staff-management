using MediatR;
using StaffManagement.Domain.Employees;

namespace StaffManagement.Application.Employees.Commands;

public record CreateEmployeeCommand
    (string Surname, string Name, string Patronymic, DateTime BirthDate) : IRequest;

internal class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = Employee.CreateNew(request.Surname, request.Name, request.Patronymic, request.BirthDate);

        await _employeeRepository.Create(employee);
    }
}