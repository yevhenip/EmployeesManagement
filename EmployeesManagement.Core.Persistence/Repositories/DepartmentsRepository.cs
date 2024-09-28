using Dapper;
using EmployeesManagement.Core.Adapters;
using EmployeesManagement.Core.Domain;

namespace EmployeesManagement.Core.Persistence.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    private readonly DbConnectionFactory _factory;

    public DepartmentsRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<Department?> GetDepartment(int id)
    {
        using var connection = _factory.Connection;
        const string sql = "select * from Department where ID = @id";
        var department = await connection.QueryFirstOrDefaultAsync<Department>(sql, new { id });

        return department;
    }

    public async Task<IEnumerable<Department>> GetDepartments()
    {
        using var connection = _factory.Connection;
        const string sql = "select * from Department";
        var departments = await connection.QueryAsync<Department>(sql);

        return departments;
    }
}