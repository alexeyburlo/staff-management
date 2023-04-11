namespace StaffManagement.Application.Positions;

public class PositionDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public byte Grade { get; set; }
}