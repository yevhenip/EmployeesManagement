using EmployeesManagement.Core.Contracts;
using MediatR;

namespace EmployeesManagement.Core.UseCases.Employees.Queries.Get;

public record GetEmployeeQuery : IRequest<EmployeeModel>
{
    public int Id { get; init; }
}