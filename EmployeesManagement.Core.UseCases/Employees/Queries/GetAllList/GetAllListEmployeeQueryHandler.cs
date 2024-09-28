using EmployeesManagement.Core.Adapters;
using EmployeesManagement.Core.Contracts;
using MapsterMapper;
using MediatR;

namespace EmployeesManagement.Core.UseCases.Employees.Queries.GetAllList;

public class GetEmployeesListQueryHandler : IRequestHandler<GetEmployeesListQuery, IEnumerable<EmployeeListModel>>
{
    private readonly IEmployeesRepository _repository;
    private readonly IMapper _mapper;

    public GetEmployeesListQueryHandler(IEmployeesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeListModel>> Handle(GetEmployeesListQuery request, CancellationToken cancellationToken)
    {
        var employees = await _repository.GetEmployeesList();

        return _mapper.Map<IEnumerable<EmployeeListModel>>(employees);
    }
}