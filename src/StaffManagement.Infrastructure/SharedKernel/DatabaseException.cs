namespace StaffManagement.Infrastructure.SharedKernel;

public class DatabaseException : Exception
{
    public DatabaseException(string message) : base(message)
    {
    }
}