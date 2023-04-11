namespace StaffManagement.Controllers.Employees;

public record UpdateEmployeeRequest
    (string Surname, string Name, string Patronymic, DateTime BirthDate);