using MediatR;

namespace EmployeesManagement.Core.UseCases.Employees.Commands.Delete;

public record DeleteEmployeeCommand : IRequest
{
    public int Id { get; init; }
}