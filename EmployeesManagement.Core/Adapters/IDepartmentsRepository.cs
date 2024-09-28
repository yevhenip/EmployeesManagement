using EmployeesManagement.Core.Domain;

namespace EmployeesManagement.Core.Adapters;

public interface IDepartmentsRepository
{
    Task<Department?> GetDepartment(int id);
    Task<IEnumerable<Department>> GetDepartments();
}