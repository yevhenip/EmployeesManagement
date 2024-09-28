using MediatR;

namespace EmployeesManagement.Core.UseCases.Employees.Commands.Create;

public record CreateEmployeeCommand : IRequest<int>
{
    public required string Name { get; init; }
    public required double Salary { get; init; }
    public required int ManagerId { get; init; }
    public required int DepartmentId { get; init; }
}