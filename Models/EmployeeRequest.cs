namespace Person.Models;

public record EmployeeRequest(
    Guid PersonId,
    string? PersonName,
    int PositionId,
    string? PositionDescription
);
