
using System.Data;
using System.Data.SqlClient;

namespace EmployeesManagement.Core.Persistence;

public class DbConnectionFactory
{
    private readonly string? _connectionString;

    public DbConnectionFactory(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection Connection => new SqlConnection(_connectionString);
}