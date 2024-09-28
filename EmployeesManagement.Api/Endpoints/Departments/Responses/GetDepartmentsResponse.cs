using EmployeesManagement.Core.Contracts;

namespace EmployeesManagement.Api.Endpoints.Departments.Responses;

public record GetDepartmentsResponse
{
    public required IEnumerable<DepartmentModel> Departments { get; init; }
}