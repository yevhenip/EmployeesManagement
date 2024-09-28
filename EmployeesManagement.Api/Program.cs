using EmployeesManagement.Api.Endpoints.Departments;
using EmployeesManagement.Api.Endpoints.Employees;
using EmployeesManagement.Api.Middleware;
using EmployeesManagement.Core.Persistence;
using EmployeesManagement.Core.UseCases;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

services.AddCors();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddSwaggerGen(opt => opt.SupportNonNullableReferenceTypes());

services.AddScoped<ExceptionHandlerMiddleware>();
services.AddApplicationLayer(config);
services.AddPersistenceLayer(config);

var app = builder.Build();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(x => x
    .SetIsOriginAllowed(_ => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithExposedHeaders("*")
);


var apiGroup = app.MapGroup("/api");

apiGroup
    .MapEmployeeEndpoints()
    .MapDepartmentEndpoints();

app.Run();