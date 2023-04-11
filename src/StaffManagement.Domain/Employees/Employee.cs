using StaffManagement.Domain.SharedKernel;

namespace StaffManagement.Domain.Employees;

public class Employee : BaseEntity
{
    public string Surname { get; private set; }
    public string Name { get; private set; }
    public string Patronymic { get; private set; }
    public DateTime BirthDate { get; private set; }

    public Employee()
    {
        // dapper only
    }
    
    private Employee(string surname, string name, string patronymic, DateTime birthDate)
    {
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        BirthDate = birthDate;
    }

    public static Employee CreateNew(string surname, string name, string patronymic, DateTime birthDate)
    {
        ValidateEmployee(surname, name, patronymic, birthDate);

        return new Employee(surname, name, patronymic, birthDate);
    }

    public void Update(string surname, string name, string patronymic, DateTime birthDate)
    {
        ValidateEmployee(surname, name, patronymic, birthDate);

        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        BirthDate = birthDate;
    }

    private static void ValidateEmployee(string surname, string name, string patronymic, DateTime birthDate)
    {
        if (string.IsNullOrWhiteSpace(surname))
        {
            throw new DomainException("Surname cannot be null, empty or contain only whitespace");
        }

        if (surname.Length > 255)
        {
            throw new DomainException("Surname cannot be greater than 255 symbols");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Name cannot be null, empty or contain only whitespace");
        }

        if (name.Length > 255)
        {
            throw new DomainException("Name cannot be greater than 255 symbols");
        }

        if (string.IsNullOrWhiteSpace(patronymic))
        {
            throw new DomainException("Patronymic cannot be null, empty or contain only whitespace");
        }

        if (patronymic.Length > 255)
        {
            throw new DomainException("Patronymic cannot be greater than 255 symbols");
        }


        if (birthDate > DateTime.Today)
        {
            throw new DomainException("Birth date cannot be greater than today");
        }
    }
}