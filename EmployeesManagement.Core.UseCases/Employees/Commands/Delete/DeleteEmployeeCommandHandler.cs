using EmployeesManagement.Core.Adapters;
using EmployeesManagement.Core.Domain;
using EmployeesManagement.Core.Exceptions;
using MediatR;

namespace EmployeesManagement.Core.UseCases.Employees.Commands.Delete;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IEmployeesRepository _repository;

    public DeleteEmployeeCommandHandler(IEmployeesRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _repository.GetEmployee(request.Id);
        if (employee == null)
            throw new EntityNotFoundException(nameof(Employee), request.Id);

        var count = await _repository.CountSubEmployees(request.Id);
        if (count > 0)
            throw new DeleteManagerException(request.Id);
        
        await _repository.DeleteEmployee(request.Id);
    }
}