using EmployeesManagement.Core.Adapters;
using EmployeesManagement.Core.Contracts;
using EmployeesManagement.Core.Domain;
using EmployeesManagement.Core.Exceptions;
using MapsterMapper;
using MediatR;

namespace EmployeesManagement.Core.UseCases.Employees.Queries.Get;

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeModel>
{
    private readonly IEmployeesRepository _repository;
    private readonly IMapper _mapper;

    public GetEmployeeQueryHandler(IEmployeesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EmployeeModel> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await _repository.GetEmployee(request.Id);

        if (employee == null)
            throw new EntityNotFoundException(nameof(Employee), request.Id);

        return _mapper.Map<EmployeeModel>(employee);
    }
}