namespace StaffManagement.Domain.SharedKernel;

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}