using MediatR;

namespace EmployeesManagement.Core.UseCases.Employees.Commands.Update;

public record UpdateEmployeeCommand : IRequest
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required double Salary { get; init; }
    public required int ManagerId { get; init; }
    public required int DepartmentId { get; init; }
}