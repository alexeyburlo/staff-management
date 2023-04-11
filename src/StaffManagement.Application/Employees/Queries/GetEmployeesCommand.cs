using Dapper;
using MediatR;
using StaffManagement.Application.SharedKernel;
using StaffManagement.Domain.Employees;

namespace StaffManagement.Application.Employees.Queries;

public record GetEmployeesCommand : IRequest<List<ShortEmployeeDto>>;

internal class GetEmployeesCommandHandler : IRequestHandler<GetEmployeesCommand, List<ShortEmployeeDto>>
{
    private readonly IConnectionFactory _connectionFactory;

    public GetEmployeesCommandHandler(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<ShortEmployeeDto>> Handle(GetEmployeesCommand request, CancellationToken cancellationToken)
    {
        const string query = "SELECT " +
                             $"\"{nameof(Employee.Id)}\", " +
                             $"\"{nameof(Employee.Surname)}\", " +
                             $"\"{nameof(Employee.Name)}\", " +
                             $"\"{nameof(Employee.Patronymic)}\", " +
                             $"\"{nameof(Employee.BirthDate)}\" " +
                             "FROM public.\"employees\"";

        var connection = _connectionFactory.GetOpenConnection();

        var result = await connection.QueryAsync<ShortEmployeeDto>(query);

        return result.AsList();
    }
}