namespace EmployeesManagement.Api.Endpoints.Employees.Requests;

public record CreateEmployeeRequest
{
    public required string Name { get; init; }
    public required double Salary { get; init; }
    public required int ManagerId { get; init; }
    public required int DepartmentId { get; init; }
}