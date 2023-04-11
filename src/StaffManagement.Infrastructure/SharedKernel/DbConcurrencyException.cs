namespace StaffManagement.Infrastructure.SharedKernel;

public class DbConcurrencyException : Exception
{
    public DbConcurrencyException() : this("Data may have been modified or deleted since entities were loaded")
    {

    }

    public DbConcurrencyException(string message)
        : base(message)
    {

    }
}