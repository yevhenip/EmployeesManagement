using EmployeesManagement.Core.Contracts;

namespace EmployeesManagement.Api.Endpoints.Employees.Responses;

public record GetEmployeesResponse
{
    public required IEnumerable<EmployeeModel> Employees { get; init; }
}