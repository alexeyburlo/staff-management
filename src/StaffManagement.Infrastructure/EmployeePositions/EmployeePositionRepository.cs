using Dapper;
using Npgsql;
using StaffManagement.Application.SharedKernel;
using StaffManagement.Domain.EmployeePositions;
using StaffManagement.Infrastructure.SharedKernel;

namespace StaffManagement.Infrastructure.EmployeePositions;

internal class EmployeePositionRepository : IEmployeePositionRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public EmployeePositionRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public Task Create(Guid employeeId, Guid positionId)
    {
        const string query = "INSERT INTO public.\"employee_positions\" (" +
                             "\"EmployeeId\", " +
                             "\"PositionId\") " +
                             "VALUES (@EmployeeId, @PositionId)";

        var connection = _connectionFactory.GetOpenConnection();

        try
        {
            return connection.ExecuteAsync(query, new
            {
                EmployeeId = employeeId,
                PositionId = positionId
            });
        }
        catch (PostgresException ex) when (ex.SqlState == "23503")
        {
            throw new DatabaseException("Position cannot be associated with employee");
        }
    }

    public Task Delete(Guid employeeId, Guid positionId)
    {
        const string query = "DELETE FROM public.\"employee_positions\" " +
                             "WHERE \"EmployeeId\" = @EmployeeId AND \"PositionId\" = @PositionId";

        var connection = _connectionFactory.GetOpenConnection();

        return connection.ExecuteAsync(query, new
        {
            EmployeeId = employeeId,
            PositionId = positionId
        });
    }
}