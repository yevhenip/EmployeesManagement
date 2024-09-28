namespace EmployeesManagement.Core.Contracts;

public record DepartmentModel
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}