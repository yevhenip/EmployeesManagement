namespace EmployeesManagement.Core.Contracts;

public record EmployeeModel
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required double Salary { get; init; }
    public required int ManagerId { get; init; }
    public required string ManagerName { get; init; }
    public required int DepartmentId { get; init; }
    public required string DepartmentName { get; init; }
}