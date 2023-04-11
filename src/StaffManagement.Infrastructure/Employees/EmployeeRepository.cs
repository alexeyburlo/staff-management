using Dapper;
using StaffManagement.Application.SharedKernel;
using StaffManagement.Domain.Employees;
using StaffManagement.Infrastructure.SharedKernel;

namespace StaffManagement.Infrastructure.Employees;

internal class EmployeeRepository : IEmployeeRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public EmployeeRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyList<Employee>> Get()
    {
        const string query = "SELECT " +
                             $"\"{nameof(Employee.Id)}\", " +
                             $"\"{nameof(Employee.Surname)}\", " +
                             $"\"{nameof(Employee.Name)}\", " +
                             $"\"{nameof(Employee.Patronymic)}\", " +
                             $"\"{nameof(Employee.BirthDate)}\", " +
                             $"\"{nameof(Employee.CreatedAt)}\", " +
                             $"\"{nameof(Employee.UpdatedAt)}\", " +
                             $"\"{nameof(Employee.ConcurrencyStamp)}\" " +
                             "FROM public.\"employees\"";

        var connection = _connectionFactory.GetOpenConnection();

        var result = await connection.QueryAsync<Employee>(query);

        return result.AsList();
    }

    public Task<Employee> Get(Guid id)
    {
        const string query = "SELECT " +
                             $"\"{nameof(Employee.Id)}\", " +
                             $"\"{nameof(Employee.Surname)}\", " +
                             $"\"{nameof(Employee.Name)}\", " +
                             $"\"{nameof(Employee.Patronymic)}\", " +
                             $"\"{nameof(Employee.BirthDate)}\", " +
                             $"\"{nameof(Employee.CreatedAt)}\", " +
                             $"\"{nameof(Employee.UpdatedAt)}\", " +
                             $"\"{nameof(Employee.ConcurrencyStamp)}\" " +
                             "FROM public.\"employees\" " +
                             $"WHERE \"{nameof(Employee.Id)}\" = @Id";

        var connection = _connectionFactory.GetOpenConnection();

        return connection.QuerySingleOrDefaultAsync<Employee>(query, new
        {
            Id = id
        });
    }

    public Task Create(Employee entity)
    {
        const string query = "INSERT INTO public.\"employees\" (" +
                             $"\"{nameof(Employee.Id)}\", " +
                             $"\"{nameof(Employee.Surname)}\", " +
                             $"\"{nameof(Employee.Name)}\", " +
                             $"\"{nameof(Employee.Patronymic)}\", " +
                             $"\"{nameof(Employee.BirthDate)}\", " +
                             $"\"{nameof(Employee.CreatedAt)}\", " +
                             $"\"{nameof(Employee.UpdatedAt)}\", " +
                             $"\"{nameof(Employee.ConcurrencyStamp)}\") " +
                             "VALUES (@Id, @Surname, @Name, @Patronymic, @BirthDate, @CreatedAt, @UpdatedAt, @ConcurrencyStamp)";

        var connection = _connectionFactory.GetOpenConnection();

        return connection.ExecuteAsync(query, entity);
    }

    public async Task Update(Employee entity)
    {
        var oldConcurrencyStamp = entity.ConcurrencyStamp;

        entity.UpdatedAt = DateTime.UtcNow;
        entity.ConcurrencyStamp = Guid.NewGuid().ToString("N");

        const string query = "UPDATE public.\"employees\" SET (" +
                             $"\"{nameof(Employee.Surname)}\", " +
                             $"\"{nameof(Employee.Name)}\", " +
                             $"\"{nameof(Employee.Patronymic)}\", " +
                             $"\"{nameof(Employee.BirthDate)}\", " +
                             $"\"{nameof(Employee.UpdatedAt)}\", " +
                             $"\"{nameof(Employee.ConcurrencyStamp)}\") " +
                             "= (@Surname, @Name, @Patronymic, @BirthDate, @UpdatedAt, @ConcurrencyStamp) " +
                             $"WHERE \"{nameof(Employee.Id)}\" = @Id AND \"{nameof(Employee.ConcurrencyStamp)}\" = @OldConcurrencyStamp";

        var connection = _connectionFactory.GetOpenConnection();

        var result = await connection.ExecuteAsync(query, new
        {
            entity.Id,
            entity.Surname,
            entity.Name,
            entity.Patronymic,
            entity.BirthDate,
            entity.UpdatedAt,
            entity.ConcurrencyStamp,
            OldConcurrencyStamp = oldConcurrencyStamp
        });

        if (result == 0)
        {
            throw new DbConcurrencyException();
        }
    }

    public async Task Delete(Employee entity)
    {
        const string query = "DELETE FROM public.\"employees\" " +
                             $"WHERE \"{nameof(Employee.Id)}\" = @Id AND \"{nameof(Employee.ConcurrencyStamp)}\" = @ConcurrencyStamp";

        var connection = _connectionFactory.GetOpenConnection();

        var result = await connection.ExecuteAsync(query, new
        {
            entity.Id,
            entity.ConcurrencyStamp,
        });

        if (result == 0)
        {
            throw new DbConcurrencyException();
        }
    }
}