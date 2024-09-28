using EmployeesManagement.Core.Adapters;
using EmployeesManagement.Core.Domain;
using EmployeesManagement.Core.Exceptions;
using MapsterMapper;
using MediatR;

namespace EmployeesManagement.Core.UseCases.Employees.Commands.Create;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IMapper _mapper;
    private readonly IDepartmentsRepository _departmentsRepository;

    public CreateEmployeeCommandHandler(IEmployeesRepository employeesRepository, IMapper mapper,
        IDepartmentsRepository departmentsRepository)
    {
        _employeesRepository = employeesRepository;
        _mapper = mapper;
        _departmentsRepository = departmentsRepository;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var manager = await _employeesRepository.GetEmployee(request.ManagerId);
        if (manager == null)
            throw new EntityNotFoundException(nameof(Employee), request.ManagerId);

        var department = await _departmentsRepository.GetDepartment(request.DepartmentId);
        if (department == null)
            throw new EntityNotFoundException(nameof(Department), request.DepartmentId);

        var employee = _mapper.Map<Employee>(request);
        return await _employeesRepository.CreateEmployee(employee);
    }
}