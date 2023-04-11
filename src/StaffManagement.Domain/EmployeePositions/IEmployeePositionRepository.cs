namespace StaffManagement.Domain.EmployeePositions;

public interface IEmployeePositionRepository
{
    Task Create(Guid employeeId, Guid positionId);
    Task Delete(Guid employeeId, Guid positionId);
}