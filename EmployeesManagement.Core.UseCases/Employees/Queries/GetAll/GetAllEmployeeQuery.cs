using EmployeesManagement.Core.Contracts;
using MediatR;

namespace EmployeesManagement.Core.UseCases.Employees.Queries.GetAll;

public record GetEmployeesQuery : IRequest<IEnumerable<EmployeeModel>>;