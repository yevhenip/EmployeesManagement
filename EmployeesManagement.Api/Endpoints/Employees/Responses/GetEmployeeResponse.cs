﻿namespace EmployeesManagement.Api.Endpoints.Employees.Responses;

public record GetEmployeeResponse
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required double Salary { get; init; }
    public required int ManagerId { get; init; }
    public required int DepartmentId { get; init; }
}