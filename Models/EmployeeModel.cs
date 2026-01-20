using Person.Models;
namespace Person.Models;

public class EmployeeModel
{
    public EmployeeModel()
    {
        PersonId = Guid.Empty;
        PositionId = 0;
    }

    public Guid PersonId { get; set; } = Guid.Empty;
    public PersonModel Person { get; set; } = null!;
    public int PositionId { get; set; } = 0;
    public PositionModel Position { get; set; } = null!;
}