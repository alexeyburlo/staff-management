using StaffManagement.Domain.SharedKernel;

namespace StaffManagement.Domain.Positions;

public class Position : BaseEntity
{
    public string Title { get; private set; }
    public byte Grade { get; private set; }

    public Position()
    {
        // dapper only
    }
    
    private Position(string title, byte grade)
    {
        Title = title;
        Grade = grade;
    }

    public static Position CreateNew(string title, byte grade)
    {
        ValidatePosition(title, grade);
        return new Position(title, grade);
    }

    public void Update(string title, byte grade)
    {
        ValidatePosition(title, grade);

        Title = title;
        Grade = grade;
    }

    private static void ValidatePosition(string title, byte grade)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Title cannot be null, empty or contain only whitespace");
        }

        if (title.Length > 255)
        {
            throw new DomainException("Title cannot be greater than 255 symbols");
        }

        if (grade is < 1 or > 15)
        {
            throw new DomainException("Grade cannot be less than 1 or greater than 15");
        }
    }
}