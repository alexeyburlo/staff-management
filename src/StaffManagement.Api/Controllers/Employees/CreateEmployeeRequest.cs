namespace StaffManagement.Controllers.Employees;

public record CreateEmployeeRequest
    (string Surname, string Name, string Patronymic, DateTime BirthDate);