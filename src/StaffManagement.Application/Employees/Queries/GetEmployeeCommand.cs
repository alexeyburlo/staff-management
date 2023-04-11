using Dapper;
using MediatR;
using StaffManagement.Application.SharedKernel;
using StaffManagement.Domain.Employees;
using StaffManagement.Domain.Positions;

namespace StaffManagement.Application.Employees.Queries;

public record GetEmployeeCommand(Guid Id) : IRequest<EmployeeDto>;

internal class GetEmployeeCommandHandler : IRequestHandler<GetEmployeeCommand, EmployeeDto>
{
    private readonly IConnectionFactory _connectionFactory;

    public GetEmployeeCommandHandler(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<EmployeeDto> Handle(GetEmployeeCommand request, CancellationToken cancellationToken)
    {
        const string query = "SELECT " +
                             $"e.\"{nameof(Employee.Id)}\", " +
                             $"\"{nameof(Employee.Surname)}\", " +
                             $"\"{nameof(Employee.Name)}\", " +
                             $"\"{nameof(Employee.Patronymic)}\", " +
                             $"\"{nameof(Employee.BirthDate)}\", " +
                             "\"PositionId\", " +
                             $"\"{nameof(Position.Title)}\" " +
                             "FROM public.\"employees\" e " +
                             "LEFT JOIN public.\"employee_positions\" ep ON e.\"Id\" = ep.\"EmployeeId\" " +
                             "LEFT JOIN public.\"positions\" p ON p.\"Id\" = ep.\"PositionId\" " +
                             $"WHERE e.\"{nameof(Employee.Id)}\" = @Id";

        var connection = _connectionFactory.GetOpenConnection();

        var result = await connection.QueryAsync<EmployeeDto, EmployeePositionDto, EmployeeDto>(query,
            (employee, position) =>
            {
                if (position != null)
                {
                    employee.Positions.Add(position);
                }

                return employee;
            },
            new
            {
                request.Id
            }, splitOn: "PositionId");

        var employee = result.FirstOrDefault();

        if (employee is null)
        {
            throw new NotFoundException("Employee not found");
        }

        return employee;
    }
}