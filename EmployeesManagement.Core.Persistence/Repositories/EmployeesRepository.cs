using Dapper;
using EmployeesManagement.Core.Adapters;
using EmployeesManagement.Core.Domain;

namespace EmployeesManagement.Core.Persistence.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly DbConnectionFactory _factory;

    public EmployeesRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<Employee?> GetEmployee(int id)
    {
        using var connection = _factory.Connection;
        const string sql = "select * from Employee where ID = @id";
        var employee = await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { id });

        return employee;
    }

    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        using var connection = _factory.Connection;
        const string sql = """
                           select *
                           from Employee e
                                    join Department d on e.DepartmentID = d.ID
                                    join Employee m on e.ManagerID = m.ID
                           """;
        var employees = await connection.QueryAsync<Employee, Department, Employee, Employee>(sql, (e, d, m) =>
        {
            e.Manager = m;
            e.Department = d;
            return e;
        }, "DepartmentID, ManagerID");

        return employees;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesList()
    {
        using var connection = _factory.Connection;
        const string sql = "select ID, Name from Employee";
        var employees = await connection.QueryAsync<Employee>(sql);

        return employees;
    }

    public async Task<int> CountSubEmployees(int id)
    {
        using var connection = _factory.Connection;
        const string sql = "select count(ID) from Employee where ManagerID = @id";
        var count = await connection.QuerySingleAsync<int>(sql, new { id });

        return count;
    }

    public async Task<int> CreateEmployee(Employee employee)
    {
        using var connection = _factory.Connection;
        const string sql = """
                           insert into Employee (DepartmentID, ManagerID, Name, Salary)
                           values (@DepartmentId, @ManagerId, @Name, @Salary);
                           select cast(scope_identity() as int)
                           """;

        var id = await connection.QuerySingleAsync<int>(sql,
            new
            {
                employee.DepartmentId, employee.ManagerId, employee.Name, employee.Salary
            });
        return id;
    }

    public async Task UpdateEmployee(Employee employee)
    {
        using var connection = _factory.Connection;
        const string sql = """
                           update Employee
                           set DepartmentID = @DepartmentId,
                               ManagerID    = @ManagerId,
                               Name         = @Name,
                               Salary       = @Salary
                           where ID = @Id
                           """;
        await connection.ExecuteAsync(sql, new
        {
            employee.DepartmentId,
            employee.ManagerId,
            employee.Name,
            employee.Salary,
            employee.Id
        });
    }

    public async Task DeleteEmployee(int id)
    {
        using var connection = _factory.Connection;
        var sql = "delete from Employee where ID = @id";
        await connection.ExecuteAsync(sql, new { id });
    }
}