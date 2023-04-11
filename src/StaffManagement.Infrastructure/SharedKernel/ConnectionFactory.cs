using System.Data;
using Npgsql;
using StaffManagement.Application.SharedKernel;

namespace StaffManagement.Infrastructure.SharedKernel;

internal class ConnectionFactory : IConnectionFactory, IDisposable
{
    private readonly string _connectionString;
    private IDbConnection _connection;

    public ConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection GetOpenConnection()
    {
        if (_connection is not { State: ConnectionState.Open })
        {
            _connection = new NpgsqlConnection(_connectionString);
            _connection.Open();
        }

        return _connection;
    }

    public IDbConnection CreateNewConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        return connection;
    }

    public void Dispose()
    {
        if (_connection is { State: ConnectionState.Open })
        {
            _connection.Dispose();
        }
    }
}