using EmployeesManagement.Core.Domain;

namespace EmployeesManagement.Core.Adapters;

public interface IEmployeesRepository
{
    Task<Employee?> GetEmployee(int id);
    Task<IEnumerable<Employee>> GetEmployees();
    Task<IEnumerable<Employee>> GetEmployeesList();
    Task<int> CountSubEmployees(int id);
    Task<int> CreateEmployee(Employee employee);
    Task UpdateEmployee(Employee employee);
    Task DeleteEmployee(int id);
}