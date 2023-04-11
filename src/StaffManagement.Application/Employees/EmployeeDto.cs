namespace StaffManagement.Application.Employees;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Patronymic { get; set; }
    public DateTime BirthDate { get; set; }
    public List<EmployeePositionDto> Positions { get; set; } = new();
}

public class EmployeePositionDto
{
    public Guid PositionId { get; set; }
    public string Title { get; set; }
}