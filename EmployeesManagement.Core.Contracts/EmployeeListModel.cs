namespace EmployeesManagement.Core.Contracts;

public record EmployeeListModel
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}