using EmployeesManagement.Core.Contracts;

namespace EmployeesManagement.Api.Endpoints.Employees.Responses;

public class GetEmployeesListResponse
{
    public required IEnumerable<EmployeeListModel> Employees { get; init; }
}