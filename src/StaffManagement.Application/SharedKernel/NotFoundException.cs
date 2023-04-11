namespace StaffManagement.Application.SharedKernel;

public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    {
    }
}