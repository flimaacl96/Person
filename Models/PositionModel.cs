using Person.Models;
namespace Person.Models;

public class PositionModel
{
    public PositionModel()
    {
        Id = 0;
        Description = string.Empty;
    }

    public int Id { get; set; }
    public string? Description { get; set; }

    public ICollection<EmployeeModel>? Employees { get; set; } = new List<EmployeeModel>();
}