using EmployeesManagement.Core.Adapters;
using EmployeesManagement.Core.Contracts;
using MapsterMapper;
using MediatR;

namespace EmployeesManagement.Core.UseCases.Departments.Queries.GetAll;

public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, IEnumerable<DepartmentModel>>
{
    private readonly IDepartmentsRepository _repository;
    private readonly IMapper _mapper;

    public GetDepartmentsQueryHandler(IDepartmentsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DepartmentModel>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = await _repository.GetDepartments();

        return _mapper.Map<IEnumerable<DepartmentModel>>(departments);
    }
}