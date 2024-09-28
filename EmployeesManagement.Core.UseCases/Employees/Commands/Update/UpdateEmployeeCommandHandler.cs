using EmployeesManagement.Core.Adapters;
using EmployeesManagement.Core.Domain;
using EmployeesManagement.Core.Exceptions;
using MapsterMapper;
using MediatR;

namespace EmployeesManagement.Core.UseCases.Employees.Commands.Update;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IMapper _mapper;
    private readonly IDepartmentsRepository _departmentsRepository;

    public UpdateEmployeeCommandHandler(IEmployeesRepository employeesRepository, IMapper mapper,
        IDepartmentsRepository departmentsRepository)
    {
        _employeesRepository = employeesRepository;
        _mapper = mapper;
        _departmentsRepository = departmentsRepository;
    }

    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeesRepository.GetEmployee(request.Id);
        if (employee == null)
            throw new EntityNotFoundException(nameof(Employee), request.ManagerId);

        if (request.Id == request.ManagerId)
            throw new UpdateManagerToYourselfException(request.Id);
        
        var manager = await _employeesRepository.GetEmployee(request.ManagerId);
        if (manager == null)
            throw new EntityNotFoundException(nameof(Employee), request.ManagerId);

        var department = await _departmentsRepository.GetDepartment(request.DepartmentId);
        if (department == null)
            throw new EntityNotFoundException(nameof(Department), request.DepartmentId);

        var updatedEmployee = _mapper.Map<Employee>(request);
        await _employeesRepository.UpdateEmployee(updatedEmployee);
    }
}