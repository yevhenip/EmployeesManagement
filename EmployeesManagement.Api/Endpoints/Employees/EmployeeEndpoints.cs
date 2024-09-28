using EmployeesManagement.Api.Endpoints.Employees.Requests;
using EmployeesManagement.Api.Endpoints.Employees.Responses;
using EmployeesManagement.Core.UseCases.Employees.Commands.Create;
using EmployeesManagement.Core.UseCases.Employees.Commands.Delete;
using EmployeesManagement.Core.UseCases.Employees.Commands.Update;
using EmployeesManagement.Core.UseCases.Employees.Queries.Get;
using EmployeesManagement.Core.UseCases.Employees.Queries.GetAll;
using EmployeesManagement.Core.UseCases.Employees.Queries.GetAllList;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagement.Api.Endpoints.Employees;

public static class EmployeeEndpoints
{
    public static RouteGroupBuilder MapEmployeeEndpoints(this RouteGroupBuilder route)
    {
        var employeeGroup = route.MapGroup("/employees");

        employeeGroup.MapGet("/{id:int:min(1)}", GetEmployee).WithSummary("Get Employee");
        employeeGroup.MapGet("/", GetEmployees).WithSummary("Get Employees");
        employeeGroup.MapGet("/list", GetEmployeesList).WithSummary("Get Employees");
        employeeGroup.MapPost("/", CreateEmployee).WithSummary("Create Employee");
        employeeGroup.MapPut("/{id:int:min(1)}", UpdateEmployee).WithSummary("Update Employee");
        employeeGroup.MapDelete("/{id:int:min(1)}", DeleteEmployee).WithSummary("Delete Employee");

        return route;
    }

    private static async Task<Ok<GetEmployeeResponse>> GetEmployee(
        [FromServices] ISender sender,
        [FromServices] IMapper mapper,
        [FromRoute] int id
    )
    {
        var request = new GetEmployeeQuery { Id = id };

        var employee = await sender.Send(request);

        var response = mapper.Map<GetEmployeeResponse>(employee);
        return TypedResults.Ok(response);
    }

    private static async Task<Ok<GetEmployeesResponse>> GetEmployees(
        [FromServices] ISender sender,
        [FromServices] IMapper mapper
    )
    {
        var request = new GetEmployeesQuery();

        var employees = await sender.Send(request);

        var response = new GetEmployeesResponse { Employees = employees };
        return TypedResults.Ok(response);
    }
    
    private static async Task<Ok<GetEmployeesListResponse>> GetEmployeesList(
        [FromServices] ISender sender,
        [FromServices] IMapper mapper
    )
    {
        var request = new GetEmployeesListQuery();

        var employees = await sender.Send(request);

        var response = new GetEmployeesListResponse { Employees = employees };
        return TypedResults.Ok(response);
    }

    private static async Task<Ok<CreateEmployeeResponse>> CreateEmployee(
        [FromServices] ISender sender,
        [FromServices] IMapper mapper,
        [FromBody] CreateEmployeeRequest request
    )
    {
        var command = mapper.Map<CreateEmployeeCommand>(request);

        var id = await sender.Send(command);

        var response = new CreateEmployeeResponse
        {
            Id = id
        };
        return TypedResults.Ok(response);
    }

    private static async Task<Ok> UpdateEmployee(
        [FromServices] ISender sender,
        [FromServices] IMapper mapper,
        [FromRoute] int id,
        [FromBody] UpdateEmployeeRequest request
    )
    {
        var command = mapper.Map<UpdateEmployeeCommand>(request) with { Id = id };

        await sender.Send(command);

        return TypedResults.Ok();
    }

    private static async Task<Ok> DeleteEmployee(
        [FromServices] ISender sender,
        [FromRoute] int id
    )
    {
        var request = new DeleteEmployeeCommand { Id = id };

        await sender.Send(request);

        return TypedResults.Ok();
    }
}