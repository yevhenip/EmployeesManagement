using EmployeesManagement.Core.Adapters;
using EmployeesManagement.Core.Contracts;
using MapsterMapper;
using MediatR;

namespace EmployeesManagement.Core.UseCases.Employees.Queries.GetAll;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<EmployeeModel>>
{
    private readonly IEmployeesRepository _repository;
    private readonly IMapper _mapper;

    public GetEmployeesQueryHandler(IEmployeesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeModel>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await _repository.GetEmployees();

        return _mapper.Map<IEnumerable<EmployeeModel>>(employees);
    }
}