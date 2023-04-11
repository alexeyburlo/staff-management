using System.Data;

namespace StaffManagement.Application.SharedKernel;

public interface IConnectionFactory
{
    IDbConnection GetOpenConnection();
    IDbConnection CreateNewConnection();
}