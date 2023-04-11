using Dapper;
using Npgsql;
using StaffManagement.Application.SharedKernel;
using StaffManagement.Domain.Positions;
using StaffManagement.Infrastructure.SharedKernel;

namespace StaffManagement.Infrastructure.Positions;

internal class PositionRepository : IPositionRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public PositionRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyList<Position>> Get()
    {
        const string query = "SELECT " +
                             $"\"{nameof(Position.Id)}\", " +
                             $"\"{nameof(Position.Title)}\", " +
                             $"\"{nameof(Position.Grade)}\", " +
                             $"\"{nameof(Position.CreatedAt)}\", " +
                             $"\"{nameof(Position.UpdatedAt)}\", " +
                             $"\"{nameof(Position.ConcurrencyStamp)}\" " +
                             "FROM public.\"positions\"";

        var connection = _connectionFactory.GetOpenConnection();

        var result = await connection.QueryAsync<Position>(query);

        return result.AsList();
    }

    public Task<Position> Get(Guid id)
    {
        const string query = "SELECT " +
                             $"\"{nameof(Position.Id)}\", " +
                             $"\"{nameof(Position.Title)}\", " +
                             $"\"{nameof(Position.Grade)}\", " +
                             $"\"{nameof(Position.CreatedAt)}\", " +
                             $"\"{nameof(Position.UpdatedAt)}\", " +
                             $"\"{nameof(Position.ConcurrencyStamp)}\" " +
                             "FROM public.\"positions\" " +
                             $"WHERE \"{nameof(Position.Id)}\" = @Id";

        var connection = _connectionFactory.GetOpenConnection();

        return connection.QuerySingleOrDefaultAsync<Position>(query, new
        {
            Id = id
        });
    }

    public Task Create(Position entity)
    {
        const string query = "INSERT INTO public.\"positions\" (" +
                             $"\"{nameof(Position.Id)}\", " +
                             $"\"{nameof(Position.Title)}\", " +
                             $"\"{nameof(Position.Grade)}\", " +
                             $"\"{nameof(Position.CreatedAt)}\", " +
                             $"\"{nameof(Position.UpdatedAt)}\", " +
                             $"\"{nameof(Position.ConcurrencyStamp)}\") " +
                             "VALUES (@Id, @Title, @Grade, @CreatedAt, @UpdatedAt, @ConcurrencyStamp)";

        var connection = _connectionFactory.GetOpenConnection();

        return connection.ExecuteAsync(query, entity);
    }

    public async Task Update(Position entity)
    {
        var oldConcurrencyStamp = entity.ConcurrencyStamp;

        entity.UpdatedAt = DateTime.UtcNow;
        entity.ConcurrencyStamp = Guid.NewGuid().ToString("N");

        const string query = "UPDATE public.\"positions\" SET (" +
                             $"\"{nameof(Position.Title)}\", " +
                             $"\"{nameof(Position.Grade)}\", " +
                             $"\"{nameof(Position.UpdatedAt)}\", " +
                             $"\"{nameof(Position.ConcurrencyStamp)}\") " +
                             "= (@Title, @Grade, @UpdatedAt, @ConcurrencyStamp) " +
                             $"WHERE \"{nameof(Position.Id)}\" = @Id AND \"{nameof(Position.ConcurrencyStamp)}\" = @OldConcurrencyStamp";

        var connection = _connectionFactory.GetOpenConnection();

        var result = await connection.ExecuteAsync(query, new
        {
            entity.Id,
            entity.Title,
            entity.Grade,
            entity.UpdatedAt,
            entity.ConcurrencyStamp,
            OldConcurrencyStamp = oldConcurrencyStamp
        });

        if (result == 0)
        {
            throw new DbConcurrencyException();
        }
    }

    public async Task Delete(Position entity)
    {
        const string query = "DELETE FROM public.\"positions\" " +
                             $"WHERE \"{nameof(Position.Id)}\" = @Id AND \"{nameof(Position.ConcurrencyStamp)}\" = @ConcurrencyStamp";

        var connection = _connectionFactory.GetOpenConnection();

        try
        {
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
        catch (PostgresException ex) when (ex.SqlState == "23503")
        {
            throw new DatabaseException("Position associated with employee(s)");
        }
    }
}