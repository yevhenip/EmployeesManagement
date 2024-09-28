using EmployeesManagement.Api.Endpoints.Departments.Responses;
using EmployeesManagement.Core.Contracts;
using EmployeesManagement.Core.UseCases.Departments.Queries.GetAll;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagement.Api.Endpoints.Departments;

public static class DepartmentEndpoints
{
    public static RouteGroupBuilder MapDepartmentEndpoints(this RouteGroupBuilder route)
    {
        var departmentGroup = route.MapGroup("/departments");

        departmentGroup.MapGet("/", GetDepartments).WithSummary("Get Departments");

        return route;
    }

    private static async Task<Ok<GetDepartmentsResponse>> GetDepartments(
        [FromServices] ISender sender,
        [FromServices] IMapper mapper
    )
    {
        var request = new GetDepartmentsQuery();

        var departments = await sender.Send(request);

        var response = new GetDepartmentsResponse
        {
            Departments = mapper.Map<IEnumerable<DepartmentModel>>(departments)
        };
        return TypedResults.Ok(response);
    }
}