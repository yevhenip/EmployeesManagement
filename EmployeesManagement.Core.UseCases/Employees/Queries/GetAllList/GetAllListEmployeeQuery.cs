using EmployeesManagement.Core.Contracts;
using MediatR;

namespace EmployeesManagement.Core.UseCases.Employees.Queries.GetAllList;

public record GetEmployeesListQuery : IRequest<IEnumerable<EmployeeListModel>>;