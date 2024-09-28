using EmployeesManagement.Core.Contracts;
using MediatR;

namespace EmployeesManagement.Core.UseCases.Departments.Queries.GetAll;

public record GetDepartmentsQuery : IRequest<IEnumerable<DepartmentModel>>;